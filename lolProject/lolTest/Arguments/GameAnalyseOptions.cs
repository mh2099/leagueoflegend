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

        [Option('p', "playerExportSeparatePlayer", Default = false, HelpText="In players export, separate JSON files for each player")]
        public Boolean PlayerExportSeparatePlayer { get; set; }

        [Option('g', "gameExportSeparatePlayer", Default = false, HelpText = "In games export, separate JSON files for each player")]
        public Boolean GameExportSeparatePlayer { get; set; }

        [Option('G', "gameExportSeparateGame", Default = false, HelpText = "In games export, separate JSON files for each game")]
        public Boolean GameExportSeparateGame { get; set; }

        [Option('x', "selectPlayer", HelpText = "Select only one player (AccountId)")]
        public Int32 SelectPlayer { get; set; }

        [Option('y', "SelectGame", HelpText = "Select only one game (GameId)")]
        public Int64 SelectGame { get; set; }

        [Option('j', "indentedJson", Default = true, HelpText = "Indent or not output JSON file")]
        public Boolean IndentedJson { get; set; }
    }
}