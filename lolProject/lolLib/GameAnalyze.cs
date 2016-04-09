namespace lolLib
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Class;
    using Newtonsoft.Json;

    public class GameAnalyse
    {
        #region Constructor
        // Working variables
        private BlockingCollection<Game> _games = new BlockingCollection<Game>();
        private ConcurrentDictionary<Int32, Game> _analyze = new ConcurrentDictionary<Int32, Game>();


        public GameAnalyse()
        {
            
        }
        #endregion
        #region Set Parameters

        
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
        /// Analyse game list and separate data for each player
        /// </summary>
        public void Analyze()
        {
            if (_games.Count == 0) return;


        }

        public void ExportAnalyze(String OutputDirectory,
            Boolean SeparateForEachPlayer = true, Boolean SeparateForEachGame = true,
            Boolean IndentedJson = true)
        {
            
        }
        #endregion
    }
}