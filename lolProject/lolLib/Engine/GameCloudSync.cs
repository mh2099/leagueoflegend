namespace lolLib
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Class;
    using Newtonsoft.Json;

    public class GameCloudSync
    {
        #region Constructor
        // Definition variables
        private readonly String _accountId;
        private readonly String _authorizationKey;
        private readonly String _plateformId;
        private Int32 _updateStep;
        // Working variables
        private BlockingCollection<Game> _games = new BlockingCollection<Game>();
        private BlockingCollection<Game> _dGames = new BlockingCollection<Game>();

        public GameCloudSync(String PlatformId, String AccountId, String AuthorizationKey)
        {
            _plateformId = PlatformId;
            _accountId = AccountId;
            _authorizationKey = AuthorizationKey;

            SetUpdateStep(20);
        }
        #endregion
        #region Set Parameters
        /// <summary>
        /// Set update step (by default: 20)
        /// </summary>
        /// <param name="UpdateStep">update step between 1 and 20</param>
        /// <returns></returns>
        public Boolean SetUpdateStep(Int32 UpdateStep)
        {
            if (UpdateStep <= 1 || UpdateStep > 20) return false;
            _updateStep = UpdateStep;
            return true;
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Reset game list
        /// </summary>
        public void Reset()
        {
            // reset _games variables
            _games.Clear();
            // reset _dGames variables
            _dGames.Clear();
        }
        /// <summary>
        /// Load a game list from a JSON file
        /// </summary>
        /// <param name="Filename">JSON file</param>
        /// <param name="ClearBefore">Clear game list before file loading</param>
        public void LoadFile(String Filename, Boolean ClearBefore = true)
        {
            // check if file exist
            if (!File.Exists(Filename)) return;
            // clear _games
            if (ClearBefore) Reset();
            // read file and deserialize json
            var json = File.ReadAllText(Filename);
            var games = JsonConvert.DeserializeObject<List<Game>>(json);
            // add only new game to _games variable
            foreach (var game in games)
                if (!_games.Any(a => a.gameId == game.gameId))
                    _games.Add(game);
        }
        /// <summary>
        /// Update games from cloud (https://acs.leagueoflegends.com/)
        /// </summary>
        public async Task UpdateGamesFromCloud(IProgress<SyncProgress> Progress)
        {
            // sync progress init
            SyncProgress.New("update game from cloud");
            // init firstIndex and lastIndex variables
            var firstIndex = 0;
            var lastIndex = _updateStep;
            // prepare download
            var matchHistoryUri = new Uri($"https://acs.leagueoflegends.com/v1/stats/player_history/{_plateformId}/{_accountId}?begIndex={firstIndex}&endIndex={lastIndex}");
            var matchHistoryResult = await DownloadAsync(matchHistoryUri);
            // deserialize result
            var matchHistory = JsonConvert.DeserializeObject<MatchHistory>(matchHistoryResult);
            // add only new game to _games variable
            var count = matchHistory.games.gameCount;
            foreach (var game in matchHistory.games.games)
                if (!_games.Any(a => a.gameId == game.gameId))
                    _games.Add(game);
            // calculate task count
            var taskCount = Convert.ToInt32(Math.Ceiling((Convert.ToDouble(count) - Convert.ToDouble(_updateStep))/Convert.ToDouble(_updateStep)));
            var tasks = new Task[taskCount];
            // sync progress max
            SyncProgress.SetProgressMax(taskCount);
            // progress update
            Progress.Report(new SyncProgress());
            // prepare and execute parallel tasks
            for (var i = 0; i < taskCount; i++)
            {
                var tId = i;
                tasks[i] = Task.Factory.StartNew(() =>
                {
                    // aggregate firstIndex and lastIndex variables
                    var tFirstIndex = _updateStep*tId + _updateStep;
                    var tLastIndex = tFirstIndex + _updateStep;
                    // prepare download
                    var tMatchHistoryUri = new Uri($"https://acs.leagueoflegends.com/v1/stats/player_history/{_plateformId}/{_accountId}?begIndex={tFirstIndex}&endIndex={tLastIndex}");
                    var tMatchHistoryResult = Download(tMatchHistoryUri);
                    // deserialize result
                    var tMatchHistory = JsonConvert.DeserializeObject<MatchHistory>(tMatchHistoryResult);
                    // add only new game to _games variable
                    foreach (var game in tMatchHistory.games.games)
                        if (!_games.Any(a => a.gameId == game.gameId))
                            _games.Add(game);
                    // progressupdate
                    Progress.Report(new SyncProgress());
                });
            }

            await Task.WhenAll(tasks);
        }        
        /// <summary>
        /// Update details to existing game from cloud (https://acs.leagueoflegends.com/)
        /// </summary>
        /// <returns></returns>
        public async Task UpdateDetailsFromCloud(IProgress<SyncProgress> Progress)
        {
            // sync progress init
            SyncProgress.New("update details from cloud");
            // calculate task count
            var taskCount = _games.Count;
            var tasks = new Task[taskCount];
            // sync progress max
            SyncProgress.SetProgressMax(taskCount);
            // prepare and execute parallel tasks
            var i = 0;
            foreach (var game in _games)
            {
                var tId = i;
                var gameId = game.gameId;

                tasks[i] = Task.Factory.StartNew(() =>
                {
                    // prepare uri
                    var tGameDetailUri = new Uri($"https://acs.leagueoflegends.com/v1/stats/game/{_plateformId}/{gameId}");
                    // download json
                    var json = Download(tGameDetailUri);
                    // deserialize result
                    var tGameDetail = JsonConvert.DeserializeObject<Game>(json);
                    // replace game by detail game
                    _dGames.Add(tGameDetail);
                    // progressupdate
                    Progress.Report(new SyncProgress());
                });

                i++;
            }

            await Task.WhenAll(tasks);
        }
        /// <summary>
        /// Export game list to JSON file
        /// </summary>
        /// <param name="Filename">JSON file</param>
        /// <param name="IndentedJson">Indented or not JSON file</param>
        public void GenerateGameJson(String Filename, Boolean IndentedJson = true)
        {
            if (File.Exists(Filename))
                File.Delete(Filename);

            var json = JsonConvert.SerializeObject(_games, IndentedJson ? Formatting.Indented : Formatting.None);

            File.WriteAllText(Filename, json);
        }

        /// <summary>
        /// Export details game to JSON file
        /// </summary>
        /// <param name="Filename"></param>
        /// <param name="IndentedJson"></param>
        public void GenerateGameDetailsJson(String Filename, Boolean IndentedJson = true)
        {
            if (File.Exists(Filename))
                File.Delete(Filename);

            var json = JsonConvert.SerializeObject(_dGames, IndentedJson ? Formatting.Indented : Formatting.None);

            File.WriteAllText(Filename, json);
        }
        #endregion
        #region Private Methods
        /// <summary>
        /// Download method with specific headers
        /// </summary>
        /// <param name="Uri">Uri</param>
        /// <returns>Result string</returns>
        public String Download(Uri Uri)
        {
            using (var wc = new WebClient())
            {
                wc.Headers.Add("Host", "acs.leagueoflegends.com");
                wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:44.0) Gecko/20100101 Firefox/44.0");
                wc.Headers.Add("Accept", "application/json, text/javascript, */*; q=0.01");
                wc.Headers.Add("Accept-Language", "fr,fr-FR;q=0.8,en-US;q=0.5,en;q=0.3");
                wc.Headers.Add("Region", _plateformId);
                wc.Headers.Add("Authorization", _authorizationKey);
                wc.Headers.Add("Referer", "http://matchhistory.euw.leagueoflegends.com/en/");
                wc.Headers.Add("Origin", "http://matchhistory.euw.leagueoflegends.com");

                var result = wc.DownloadString(Uri);
                return result;
            }
        }
        /// <summary>
        /// Async download method with specific headers
        /// </summary>
        /// <param name="Uri">Uri</param>
        /// <returns>Result string</returns>
        public async Task<String> DownloadAsync(Uri Uri)
        {
            using (var wc = new WebClient())
            {
                wc.Headers.Add("Host", "acs.leagueoflegends.com");
                wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:44.0) Gecko/20100101 Firefox/44.0");
                wc.Headers.Add("Accept", "application/json, text/javascript, */*; q=0.01");
                wc.Headers.Add("Accept-Language", "fr,fr-FR;q=0.8,en-US;q=0.5,en;q=0.3");
                wc.Headers.Add("Region", _plateformId);
                wc.Headers.Add("Authorization", _authorizationKey);
                wc.Headers.Add("Referer", "http://matchhistory.euw.leagueoflegends.com/en/");
                wc.Headers.Add("Origin", "http://matchhistory.euw.leagueoflegends.com");

                var result = await wc.DownloadStringTaskAsync(Uri);
                return result;
            }
        }
        #endregion
    }
}