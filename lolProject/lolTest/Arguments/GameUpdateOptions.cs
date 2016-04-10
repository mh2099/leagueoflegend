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

        [Option('k', "authorizationKey", HelpText = "Authorization key (get it from firebug) (without get only ranked game)")]
        public String AuthorizationKey { get; set; }

        [Option('i', "inputJSONFile", HelpText = "Input JSON file (can be the same as output)")]
        public String InputJsonFile { get; set; }

        [Option('o', "outputJSONFile", Required = true, HelpText = "Output JSON file (can be the same as input)")]
        public String OutputJsonFile { get; set; }

        [Option('c', "cloudUpdate", Default = false, HelpText = "Update game from cloud")]
        public Boolean CloudUpdate { get; set; }

        [Option('d', "detailsUpdate", Default = false, HelpText = "Update game details for each game")]
        public Boolean DetailsUpdate { get; set; }

        [Option('f', "addFrame", Default = false, HelpText = "Update game frame for each game (only if detailsUpdate is enabled)")]
        public Boolean AddFrame { get; set; }

        [Option('j', "indentedJson", Default = true, HelpText = "Indent or not output JSON file")]
        public Boolean IndentedJson { get; set; }
    }
}