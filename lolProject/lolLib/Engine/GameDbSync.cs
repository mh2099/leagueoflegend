namespace lolLib
{
    using System;

    public class GameDbSync
    {
        #region Constructor
        private String _host;
        private UInt16 _port;
        private String _username;
        private String _password;
        private String _table;

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
        public void Sync()
        {
            
        }
        #endregion
    }
}