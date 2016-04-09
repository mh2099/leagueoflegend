﻿namespace lolLib
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Analyse;
    using Class;
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
        ///     Reset game list
        /// </summary>
        public void Reset()
        {
            // reset _games variables
            _games.Clear();
        }

        /// <summary>
        ///     Load a game list from a JSON file
        /// </summary>
        /// <param name="Filename"></param>
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
        ///     Analyse game list and separate data for each player
        /// </summary>
        public void Analyze()
        {
            if (_games.Count == 0) return;

            foreach (var game in _games)
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
                        gameId = game.gameId,
                        platformId = game.platformId,
                        gameCreation = game.gameCreation,
                        gameDuration = game.gameDuration,
                        queueId = game.queueId,
                        mapId = game.mapId,
                        seasonId = game.seasonId,
                        gameVersion = game.gameVersion,
                        gameMode = game.gameMode,
                        gameType = game.gameType
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

        public void ExportAnalyze(String OutputDirectory,
            Boolean SeparateForEachPlayer = true, Boolean SeparateForEachGame = true,
            Boolean IndentedJson = true)
        {
            if (!Directory.Exists(OutputDirectory))
                Directory.CreateDirectory(OutputDirectory);

            if (!Directory.Exists(OutputDirectory)) throw new DirectoryNotFoundException($"{OutputDirectory} not found!");

            // export players
            if (_aPlayers?.Count > 0)
                if (SeparateForEachPlayer)
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
            // export games for each players
            if (_aGameForThisPlayers?.Count > 0)
                if (SeparateForEachGame)
                    foreach (var game in _aGameForThisPlayers)
                    {
                        var aGameJson = JsonConvert.SerializeObject(game, IndentedJson ? Formatting.Indented : Formatting.None);
                        var aGameFile = Path.Combine(OutputDirectory, $"game_{game.gameId}.json");
                        File.WriteAllText(aGameFile, aGameJson);
                    }
                else
                {
                    var aGamesJson = JsonConvert.SerializeObject(_aGameForThisPlayers, IndentedJson ? Formatting.Indented : Formatting.None);
                    var aGamesFile = Path.Combine(OutputDirectory, "games.json");
                    File.WriteAllText(aGamesFile, aGamesJson);
                }
        }
        #endregion
    }
}