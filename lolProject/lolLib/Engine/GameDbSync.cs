namespace lolLib
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using DTO;
    using EF;
    using Newtonsoft.Json;

    public class GameDbSync
    {
        #region Constructor
        // Definition variables
        private String _host;
        private UInt16 _port;
        private String _username;
        private String _password;
        private String _table;
        // Working variables
        private BlockingCollection<Game> _games = new BlockingCollection<Game>();

        public GameDbSync(String Host, UInt16 Port, String Username, String Password, String Table)
        {
            _host = Host;
            _port = Port;
            _username = Username;
            _password = Password;
            _table = Table;
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
        ///  Clear database
        /// </summary>
        public Boolean Clear()
        {
            // db connect
            DBConnection.SetConnection(_host, _port, _username, _password, _table);
            // db ping
            var pingResult = EntityManager.Ping();
            if (!pingResult) return false;
            // db clear
            EntityManager.ClearAll();
            //
            return true;
        }
        /// <summary>
        /// Sync database with game
        /// </summary>
        public Boolean Sync(Boolean ForceReload = false)
        {
            // db connect
            DBConnection.SetConnection(_host, _port, _username, _password, _table);
            // db ping
            var pingResult = EntityManager.Ping();
            if (!pingResult) return false;
            // for each game
            foreach (var game in _games)
            {
                // check if entity exist
                var exist = EntityManager.GameExist(game.gameId);
                // if need delete, delete it
                if (ForceReload && exist)
                    EntityManager.GameDelete(game.gameId);
                // add entity
                EntityManager.GameAdd(game);
            }
            //
            return true;
        }
        #endregion
    }
}