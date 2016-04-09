namespace lolLib
{
    using System;
    using System.IO;

    public class GameDbManager
    {
        #region Constructor
        private String _dbFile;

        public GameDbManager()
        {
            
        }
        #endregion
        #region Set Parameters
        /// <summary>
        /// Set database file (sqlite3 db file)
        /// </summary>
        /// <param name="Filename"></param>
        public void SetDb(String Filename)
        {
            // Save actual db to old file
            SaveDb();
            // Change db file
            _dbFile = Filename;
            if (!File.Exists(Filename))
                // If db file exist, reload db
                LoadDb();
            else
            {
                // If db not exist, create it
                CreateDb();
                // Save db in file
                SaveDb();
            }
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Load database file to the class
        /// </summary>
        /// <returns>loading state</returns>
        public Boolean LoadDb()
        {
            return false;
        }
        /// <summary>
        /// Save class to the database file
        /// </summary>
        /// <returns>save state</returns>
        public Boolean SaveDb()
        {
            return false;
        }
        #endregion
        #region Private Methods
        private void CreateDb()
        {
            
        }
        #endregion
    }
}