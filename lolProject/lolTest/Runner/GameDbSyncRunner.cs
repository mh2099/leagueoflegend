namespace lolTest.Runner
{
    using System;
    using System.Threading.Tasks;
    using lolLib;

    public static class GameDbSyncRunner
    {
        private static String _inputJsonFile;
        private static String _host;
        private static UInt16 _port;
        private static String _username;
        private static String _password;
        private static String _table;
        private static Boolean _forceReload;

        public static Int32 Run(String InputJsonFile, String Host, UInt16 Port,
            String Username, String Password, String Table, Boolean ForceReload)
        {
            _inputJsonFile = InputJsonFile;
            _host = Host;
            _port = Port;
            _username = Username;
            _password = Password;
            _table = Table;
            _forceReload = ForceReload;

            RunAsync();

            return 0;
        }

        private static void RunAsync()
        {
            // create class
            var gs = new GameDbSync(_inputJsonFile, _host, _port, _username, _password, _table, _forceReload);
            // work
            gs.Sync();
            // end
            //Console.WriteLine("done!");
        }
    }
}