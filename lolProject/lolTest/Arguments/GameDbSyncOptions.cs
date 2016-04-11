namespace lolTest.Arguments
{
    using System;
    using CommandLine;

    [Verb("db-sync", HelpText = "(NOT WORKING YET!) game synchronization with database")]
    public class GameDbSyncOptions
    {
        [Option('i', "inputJSONFile", Required = true, HelpText = "Input JSON file")]
        public String InputJsonFile { get; set; }

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

        [Option('r', "forceReload", Default = false, HelpText = "Force reload (if entity exist, delete it and recreate it)")]
        public Boolean ForceReload { get; set; }
    }
}