namespace lolLib
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Analyse;
    using DTO;
    using Newtonsoft.Json;

    public class GameAnalyse
    {
        #region Constructor
        // Input variables
        private readonly BlockingCollection<Game> _games = new BlockingCollection<Game>();
        // Analyze variables
        private BlockingCollection<Game> _aGames = new BlockingCollection<Game>();
        private readonly BlockingCollection<Player> _aPlayers = new BlockingCollection<Player>();
        private readonly BlockingCollection<GameForThisPlayer> _aGameForThisPlayers = new BlockingCollection<GameForThisPlayer>();        
        #endregion
        #region Public Methods
        /// <summary>
        /// Reset game list
        /// </summary>
        public void Reset()
        {
            // reset _games variables
            _games.Clear();
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
        /// Analyse game list and separate data for each player
        /// </summary>
        public void Analyze(Boolean ShowInfos = false)
        {
            if (_games.Count == 0) return;

            // participantCounter
            var pCounter2 = 0;
            var pCounter4 = 0;
            var pCounter6 = 0;
            var pCounter8 = 0;
            var pCounter10 = 0;
            var pCounter12 = 0;
            // participantNullCount
            var pCounterNoNull = 0;
            var pCounterNull = 0;

            foreach (var game in _games)
            {
                var pCount = game.participantIdentities.Count;
                if (pCount == 2) pCounter2++;
                else if (pCount == 4) pCounter4++;
                else if (pCount == 6) pCounter6++;
                else if (pCount == 8) pCounter8++;
                else if (pCount == 10) pCounter10++;
                else if (pCount == 12) pCounter12++;

                if (game.participantIdentities[0].player != null)
                    pCounterNoNull++;
                else
                    pCounterNull++;

                // analyze players
                    foreach (var participantIdentity in game.participantIdentities)
                    {
                        var player = participantIdentity.player;
                        if (player == null) continue;
                        if (!_aPlayers.Any(a => a.accountId == player.accountId))
                            _aPlayers.Add(player);

                        // analyse games for each players
                        var gftp = new GameForThisPlayer
                        {
                            // global
                            gameId = game.gameId,
                            platformId = game.platformId,
                            gameCreation = game.gameCreation,
                            gameDuration = game.gameDuration,
                            queueId = game.queueId,
                            mapId = game.mapId,
                            seasonId = game.seasonId,
                            gameVersion = game.gameVersion,
                            gameMode = game.gameMode,
                            gameType = game.gameType,
                            // player
                            accountId = player.accountId,
                            summonerName = player.summonerName,
                            summonerId = player.summonerId,
                            currentPlatformId = player.currentPlatformId,
                            currentAccountId = player.currentAccountId,
                            matchHistoryUri = player.matchHistoryUri,
                            profileIcon = player.profileIcon
                        };

                        var p = game.participants.FirstOrDefault(a => a.participantId == participantIdentity.participantId);
                        // participant
                        if (p != null)
                        {
                            gftp.participantId = p.participantId;
                            gftp.teamId = p.teamId;
                            gftp.championId = p.championId;
                            gftp.spell1Id = p.spell1Id;
                            gftp.spell2Id = p.spell2Id;
                            gftp.masteries = p.masteries;
                            gftp.runes = p.runes;
                            gftp.highestAchievedSeasonTier = p.highestAchievedSeasonTier;
                            gftp.stats = p.stats;
                            gftp.timeline = p.timeline;
                        }

                        // team
                        var t = game.teams.FirstOrDefault(a => a.teamId == p.teamId);
                        if (t != null)
                        {
                            gftp.win = t.win;
                            gftp.firstBlood = t.firstBlood;
                            gftp.firstTower = t.firstTower;
                            gftp.firstInhibitor = t.firstInhibitor;
                            gftp.firstBaron = t.firstBaron;
                            gftp.firstDragon = t.firstDragon;
                            gftp.firstRiftHerald = t.firstRiftHerald;
                            gftp.towerKills = t.towerKills;
                            gftp.inhibitorKills = t.inhibitorKills;
                            gftp.baronKills = t.baronKills;
                            gftp.dragonKills = t.dragonKills;
                            gftp.vilemawKills = t.vilemawKills;
                            gftp.riftHeraldKills = t.riftHeraldKills;
                            gftp.dominionVictoryScore = t.dominionVictoryScore;
                            gftp.bans = t.bans;
                        }

                        _aGameForThisPlayers.Add(gftp);
                    }
            }

            if (!ShowInfos) return;

            Console.WriteLine("-- general infos ---------------");
            Console.WriteLine($"game count: {_games.Count}");
            Console.WriteLine($"player count: {_aPlayers.Count}");
            Console.WriteLine("-- player count null -----------");
            Console.WriteLine($"game with known players: {pCounterNoNull}");
            Console.WriteLine($"game with null players: {pCounterNull}");
            Console.WriteLine("-- player count infos ----------");
            Console.WriteLine($"game with 2 players: {pCounter2}");
            Console.WriteLine($"game with 4 players: {pCounter4}");
            Console.WriteLine($"game with 6 players: {pCounter6}");
            Console.WriteLine($"game with 8 players: {pCounter8}");
            Console.WriteLine($"game with 10 players: {pCounter10}");
            Console.WriteLine($"game with 12 players: {pCounter12}");
        }
        /// <summary>
        /// Export analyze to a JSON file
        /// </summary>
        /// <param name="OutputDirectory"></param>
        /// <param name="SeparateForEachPlayer"></param>
        /// <param name="SeparateForEachGame"></param>
        /// <param name="IndentedJson"></param>
        public void ExportAnalyze(String OutputDirectory,
            Boolean PlayerExportSeparatePlayer, Boolean GameExportSeparatePlayer, Boolean GameExportSeparateGame,
            Int32 SelectPlayer, Int64 SelectGame, Boolean IndentedJson = true)
        {
            if (!Directory.Exists(OutputDirectory))
                Directory.CreateDirectory(OutputDirectory);

            if (!Directory.Exists(OutputDirectory)) throw new DirectoryNotFoundException($"{OutputDirectory} not found!");

            // export players
            if (_aPlayers?.Count > 0)
                if (SelectPlayer > 0)
                {
                    var player = _aPlayers.FirstOrDefault(a => a.accountId == SelectPlayer);
                    if (player != null)
                    {
                        var aPlayerJson = JsonConvert.SerializeObject(player, IndentedJson ? Formatting.Indented : Formatting.None);
                        var aPlayerFile = Path.Combine(OutputDirectory, $"player_{SelectPlayer}.json");
                        File.WriteAllText(aPlayerFile, aPlayerJson);
                    }
                    else
                        Console.WriteLine($"player {SelectPlayer} not found!");
                }
                else
                {                
                    if (PlayerExportSeparatePlayer)
                        foreach (var player in _aPlayers)
                        {
                            var aPlayerJson = JsonConvert.SerializeObject(player, IndentedJson ? Formatting.Indented : Formatting.None);
                            var aPlayerFile = Path.Combine(OutputDirectory, $"player_{player.accountId}.json");
                            File.WriteAllText(aPlayerFile, aPlayerJson);
                        }
                    else
                    {
                        var aPlayersJson = JsonConvert.SerializeObject(_aPlayers, IndentedJson ? Formatting.Indented : Formatting.None);
                        var aPlayersFile = Path.Combine(OutputDirectory, "players.json");
                        File.WriteAllText(aPlayersFile, aPlayersJson);
                    }
                }

            // export games for each players
            if (_aGameForThisPlayers?.Count > 0)
            {
                // select one player
                if (SelectPlayer > 0)
                {
                    var aGame = _aGameForThisPlayers.Where(a => a.accountId == SelectPlayer);
                    if (aGame.Any())
                    {
                        var aGameJson = JsonConvert.SerializeObject(aGame, IndentedJson ? Formatting.Indented : Formatting.None);
                        var aGameFile = Path.Combine(OutputDirectory, $"games_player_{SelectPlayer}.json");
                        File.WriteAllText(aGameFile, aGameJson);
                    }
                    else
                        Console.WriteLine($"player {SelectPlayer} not found!");
                }
                // select one game
                else if (SelectGame > 0)
                {
                    var aGame = _aGameForThisPlayers.Where(a => a.gameId == SelectGame);
                    if (aGame.Any())
                    {
                        var aGameJson = JsonConvert.SerializeObject(aGame, IndentedJson ? Formatting.Indented : Formatting.None);
                        var aGameFile = Path.Combine(OutputDirectory, $"game_{SelectGame}.json");
                        File.WriteAllText(aGameFile, aGameJson);
                    }
                    else
                        Console.WriteLine($"game {SelectGame} not found!");
                }
                // select one player and one game
                else if (SelectPlayer > 0 && SelectGame > 0)
                {
                    var aGame = _aGameForThisPlayers.Where(a => a.gameId == SelectGame).Where(a => a.accountId == SelectPlayer);
                    if (aGame.Any())
                    {
                        var aGameJson = JsonConvert.SerializeObject(aGame, IndentedJson ? Formatting.Indented : Formatting.None);
                        var aGameFile = Path.Combine(OutputDirectory, $"game_{SelectGame}_player_{SelectPlayer}.json");
                        File.WriteAllText(aGameFile, aGameJson);
                    }
                    else
                        Console.WriteLine($"game {SelectGame} and player {SelectPlayer} not found!");
                }
                // no selection
                else
                {
                    // One file by game player
                    if (GameExportSeparateGame && GameExportSeparatePlayer)
                    {
                        foreach (var game in _aGameForThisPlayers)
                        {
                            var aGameJson = JsonConvert.SerializeObject(game, IndentedJson ? Formatting.Indented : Formatting.None);
                            var aGameFile = Path.Combine(OutputDirectory, $"game_{game.gameId}_player_{game.accountId}.json");
                            File.WriteAllText(aGameFile, aGameJson);
                        }
                    }
                    // One file
                    else if (!GameExportSeparateGame && !GameExportSeparatePlayer)
                    {
                        var aGamesJson = JsonConvert.SerializeObject(_aGameForThisPlayers, IndentedJson ? Formatting.Indented : Formatting.None);
                        var aGamesFile = Path.Combine(OutputDirectory, "games.json");
                        File.WriteAllText(aGamesFile, aGamesJson);
                    }
                    // One file by game for each by player
                    else
                    {
                        foreach (var player in _aPlayers)
                        {
                            var aGamePlayer = _aGameForThisPlayers.Where(a => a.accountId == player.accountId).ToList();
                            var aGamePlayerJson = JsonConvert.SerializeObject(aGamePlayer, IndentedJson ? Formatting.Indented : Formatting.None);
                            var aGamePlayerFile = Path.Combine(OutputDirectory, $"games_player_{player.accountId}.json");
                            File.WriteAllText(aGamePlayerFile, aGamePlayerJson);
                        }
                    }
                }
            }

        }
        #endregion
    }
}