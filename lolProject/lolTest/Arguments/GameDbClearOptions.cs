namespace lolTest.Arguments
{
    using System;
    using CommandLine;

    [Verb("db-clear", HelpText = "delete all data in database")]
    public class GameDbClearOptions
    {
        [Option('h', "host", Required = true, HelpText = "Host address")]
        public String Host { get; set; }

        [Option('P', "port", Required = true, HelpText = "Host port")]
        public UInt16 Port { get; set; }

        [Option('u', "username", Required = true, HelpText = "Database username")]
        public String Username { get; set; }

        [Option('u', "password", Required = true, HelpText = "Database password")]
        public String Password { get; set; }

        [Option('t', "table", Required = true, HelpText = "Database table")]
        public String Table { get; set; }
    }
}