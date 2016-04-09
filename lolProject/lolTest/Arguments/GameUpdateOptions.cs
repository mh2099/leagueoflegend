namespace lolTest.Arguments
{
    using System;
    using CommandLine;

    [Verb("update", HelpText = "load, update and export game list")]
    public class GameUpdateOptions
    {
        [Option('p', "platformId", Default = "EUW1", HelpText = "Platform for cloud updates")]
        public String PlatformId { get; set; }

        [Option('a', "accountId", Required = true, HelpText = "Accound id for cloud updates")]
        public String AccoundId { get; set; }

        [Option('k', "authorizationKey", Required = true, HelpText = "Authorization key (get it from firebug)")]
        public String AuthorizationKey { get; set; }

        [Option('t', "temporaryDirectory", HelpText = "Set a delete temporary JSON directory ( needed for update from cloud )")]
        public String TemporaryDirectory { get; set; }

        [Option('i', "inputJSONFile", HelpText = "Input JSON file (can be the same from output)")]
        public String InputJsonFile { get; set; }

        [Option('o', "outputJSONFile", Required = true, HelpText = "Output JSON file (can be the same from input)")]
        public String OutputJsonFile { get; set; }

        [Option('d', "gameDetails", HelpText = "Scan details for each game")]
        public Boolean GameDetails { get; set; }

        [Option('d', "deleteTemporaryFile", Default = true, HelpText = "Set if temporary JSON file are automatically deleted ( needed for update from cloud )")]
        public Boolean DeleteTemporaryFile { get; set; }

        [Option('j', "indentedJson", Default = true, HelpText = "Indent or not output JSON file")]
        public Boolean IndentedJson { get; set; }
    }
}