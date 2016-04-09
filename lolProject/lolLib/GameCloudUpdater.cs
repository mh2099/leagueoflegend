namespace lolLib
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Class;
    using Newtonsoft.Json;

    public class GameCloudUpdater
    {
        #region Constructor
        private readonly String _accountId;
        private readonly String _authorizationKey;
        private readonly String _plateformId;
        
        private BlockingCollection<Game> _games = new BlockingCollection<Game>();
        private BlockingCollection<Game> _dGames = new BlockingCollection<Game>(); 
        private Int32 _updateStep;
        private String _temporaryDirectory = String.Empty;
        private Boolean _deleteTemporaryJson = true;

        public GameCloudUpdater(String PlatformId, String AccountId, String AuthorizationKey)
        {
            _plateformId = PlatformId;
            _accountId = AccountId;
            _authorizationKey = AuthorizationKey;
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
        /// <summary>
        /// Set a delete temporary JSON directory ( needed for update from cloud )
        /// </summary>
        /// <param name="TemporaryDirectory"></param>
        /// <returns></returns>
        public Boolean SetTemporaryDirectory(String TemporaryDirectory)
        {
            Directory.CreateDirectory(TemporaryDirectory);
            if (!Directory.Exists(TemporaryDirectory)) return false;
            _temporaryDirectory = TemporaryDirectory;
            return true;
        }
        /// <summary>
        /// Set if temporary JSON file are automatically deleted ( needed for update from cloud )
        /// </summary>
        /// <param name="DeleteTemporaryJson"></param>
        public void SetDeleteTemporaryJson(Boolean DeleteTemporaryJson)
        {
            _deleteTemporaryJson = DeleteTemporaryJson;
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
        public void LoadFile(String Filename)
        {
            // check if file exist
            if (!File.Exists(Filename)) return;
            // read file and deserialize json
            var json = File.ReadAllText(Filename);
            var games = JsonConvert.DeserializeObject<List<Game>>(json);
            // add only new game to _games variable
            foreach (var game in games)
                if(!_games.Any(a => a.gameId == game.gameId))
                    _games.Add(game);
        }
        /// <summary>
        /// Update games from cloud (https://acs.leagueoflegends.com/)
        /// </summary>
        public async Task UpdateGamesFromCloud()
        {
            // check if temporary directory exist
            if (_temporaryDirectory == String.Empty) throw new DirectoryNotFoundException("temporary directory not set!");
            if (!Directory.Exists(_temporaryDirectory)) throw new DirectoryNotFoundException($"{_temporaryDirectory} not found!");
            // init firstIndex and lastIndex variables
            var firstIndex = 0;
            var lastIndex = _updateStep;
            // prepare curl executable
            var matchHistoryOutput = Path.Combine(_temporaryDirectory, $"matchHistory_{DateTime.Now.ToString("yyyyMMddHHmmss")}_{_plateformId}_{_accountId}_{firstIndex}_{lastIndex}.json");
            var matchHistoryParams = $"\"https://acs.leagueoflegends.com/v1/stats/player_history/{_plateformId}/{_accountId}?begIndex={firstIndex}&endIndex={lastIndex}\" -H \"Host: acs.leagueoflegends.com\" -H \"User-Agent: Mozilla/5.0 (Windows NT 6.3; WOW64; rv:44.0) Gecko/20100101 Firefox/44.0\" -H \"Accept: application/json, text/javascript, */*; q=0.01\" -H \"Accept-Language: fr,fr-FR;q=0.8,en-US;q=0.5,en;q=0.3\" -H \"Region: {_plateformId}\" -H \"Authorization: {_authorizationKey}\" -H \"Referer: http://matchhistory.euw.leagueoflegends.com/en/\" -H \"Origin: http://matchhistory.euw.leagueoflegends.com\" -o \"{matchHistoryOutput}\"";
            await RunProcess.RunProcess.RunAsync("curl.exe ", matchHistoryParams);
            // deserialize curl result
            var matchHistoryFile = File.ReadAllText(matchHistoryOutput);
            if(_deleteTemporaryJson) File.Delete(matchHistoryOutput);
            var matchHistory = JsonConvert.DeserializeObject<MatchHistory>(matchHistoryFile);
            // add only new game to _games variable
            var count = matchHistory.games.gameCount;
            foreach (var game in matchHistory.games.games)
                if(!_games.Any(a => a.gameId == game.gameId))
                    _games.Add(game);
            // calculate task count
            var taskCount = Convert.ToInt32(Math.Ceiling((Convert.ToDouble(count) - Convert.ToDouble(_updateStep))/ Convert.ToDouble(_updateStep)));
            var tasks = new Task[taskCount];
            // prepare and execute parallel tasks
            for (var i = 0; i < taskCount; i++)
            {
                var tId = i;
                tasks[i] = Task.Factory.StartNew(() =>
                {
                    // aggregate firstIndex and lastIndex variables
                    var tFirstIndex = _updateStep * tId + _updateStep;
                    var tLastIndex = tFirstIndex + _updateStep;
                    // prepare curl executable
                    var tMatchHistoryOutput = Path.Combine(_temporaryDirectory, $"matchHistory_{DateTime.Now.ToString("yyyyMMddHHmmss")}_{_plateformId}_{_accountId}_{tFirstIndex}_{tLastIndex}.json");
                    var tMatchHistoryParams = $"\"https://acs.leagueoflegends.com/v1/stats/player_history/{_plateformId}/{_accountId}?begIndex={tFirstIndex}&endIndex={tLastIndex}\" -H \"Host: acs.leagueoflegends.com\" -H \"User-Agent: Mozilla/5.0 (Windows NT 6.3; WOW64; rv:44.0) Gecko/20100101 Firefox/44.0\" -H \"Accept: application/json, text/javascript, */*; q=0.01\" -H \"Accept-Language: fr,fr-FR;q=0.8,en-US;q=0.5,en;q=0.3\" -H \"Region: {_plateformId}\" -H \"Authorization: {_authorizationKey}\" -H \"Referer: http://matchhistory.euw.leagueoflegends.com/en/\" -H \"Origin: http://matchhistory.euw.leagueoflegends.com\" -o \"{tMatchHistoryOutput}\"";
                    RunProcess.RunProcess.Run("curl.exe ", tMatchHistoryParams);
                    // deserialize curl result
                    var tMatchHistoryFile = File.ReadAllText(tMatchHistoryOutput);
                    if (_deleteTemporaryJson) File.Delete(tMatchHistoryOutput);
                    var tMatchHistory = JsonConvert.DeserializeObject<MatchHistory>(tMatchHistoryFile);
                    // add only new game to _games variable
                    foreach (var game in tMatchHistory.games.games)
                        if (!_games.Any(a => a.gameId == game.gameId))
                            _games.Add(game);
                });
            }

            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Update details to existing game from cloud (https://acs.leagueoflegends.com/)
        /// </summary>
        /// <returns></returns>
        public async Task UpdateDetailsFromCloud()
        {
            // calculate task count
            var taskCount = _games.Count;
            var tasks = new Task[taskCount];
            // prepare and execute parallel tasks
            var i = 0;
            foreach (var game in _games)
            {
                var tId = i;
                var gameId = game.gameId;

                tasks[i] = Task.Factory.StartNew(() =>
                {
                    using (var wc = new WebClient())
                    {
                        // prepare uri
                        var tGameDetailUri = $"https://acs.leagueoflegends.com/v1/stats/game/{_plateformId}/{gameId}";
                        Console.WriteLine(tGameDetailUri);
                        // download json
                        var json = wc.DownloadString(tGameDetailUri);
                        // deserialize result
                        var tGameDetail = JsonConvert.DeserializeObject<Game>(json);
                        // replace game by detail game
                        _dGames.Add(tGameDetail);
                    }
                });

                i++;
            }

            await Task.WhenAll(tasks);
        }
        /// <summary>
        /// Export game list to JSON file
        /// </summary>
        /// <param name="Filename">JSON file</param>
        /// <param name="Indented">Indented or not JSON file</param>
        public void GenerateGameJson(String Filename, Boolean Indented = true)
        {
            if(File.Exists(Filename))
                File.Delete(Filename);
            
            var json = JsonConvert.SerializeObject(_games, Indented ? Formatting.Indented : Formatting.None);

            File.WriteAllText(Filename, json);
        }
        /// <summary>
        /// Export details game to JSON file
        /// </summary>
        /// <param name="Filename"></param>
        /// <param name="Indented"></param>
        public void GenerateGameDetailsJson(String Filename, Boolean Indented = true)
        {
            if(File.Exists(Filename))
                File.Delete(Filename);

            var json = JsonConvert.SerializeObject(_dGames, Indented ? Formatting.Indented : Formatting.None);

            File.WriteAllText(Filename, json);
        }
        #endregion
    }
}