namespace lolTest.Arguments
{
    using System;
    using CommandLine;

    [Verb("db", HelpText = "game synchronization with database")]
    public class GameDbSyncOptions
    {
        [Option('h', "host", HelpText = "Host address")]
        public String Host { get; set; }

        [Option('P', "port", HelpText = "Host port")]
        public UInt16 Port { get; set; }

        [Option('u', "username", HelpText = "Database username")]
        public String Username { get; set; }

        [Option('u', "password", HelpText = "Database password")]
        public String Password { get; set; }

        [Option('t', "table", HelpText = "Database table")]
        public String Table { get; set; }
    }
}