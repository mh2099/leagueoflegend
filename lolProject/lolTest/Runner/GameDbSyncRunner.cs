namespace lolTest.Runner
{
    using System;
    using System.Threading.Tasks;
    using lolLib;

    public static class GameDbSyncRunner
    {
        private static String _host;
        private static UInt16 _port;
        private static String _username;
        private static String _password;
        private static String _table;

        public static Int32 Run(String Host, UInt16 Port,
            String Username, String Password, String Table)
        {
            _host = Host;
            _port = Port;
            _username = Username;
            _password = Password;
            _table = Table;

            RunAsync();

            return 0;
        }

        private static void RunAsync()
        {
            // create class
            var gs = new GameDbSync(_host, _port, _username, _password, _table);
            // work
            gs.Sync();
            // end
            Console.WriteLine("done!");
        }
    }
}