namespace lolTest.Arguments
{
    using System;
    using CommandLine;

    [Verb("analyze", HelpText = "analyze game list for each player")]
    public class GameAnalyseOptions
    {
        [Option('i', "inputJSONFile", HelpText = "Input JSON file")]
        public String InputJsonFile { get; set; }

        [Option('o', "outputDirectory", Required = true, HelpText = "Output directory (JSON files are exported in")]
        public String OutputDirectory { get; set; }

        [Option('p', "separateForEachPlayer", HelpText="Separate JSON files for each player")]
        public Boolean SeparateForEachPlayer { get; set; }

        [Option('g', "separateForEachGame", HelpText = "Separate JSON files for each game")]
        public Boolean SeparateForEachGame { get; set; }

        [Option('j', "indentedJson", Default = true, HelpText = "Indent or not output JSON file")]
        public Boolean IndentedJson { get; set; }
    }
}