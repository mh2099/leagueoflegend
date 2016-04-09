namespace lolTest.Arguments
{
    using System;
    using CommandLine;

    [Verb("infos", HelpText = "game list informations")]
    public class GameInfosOptions
    {
        [Option('i', "inputJSONFile", Required = true, HelpText = "Input JSON file")]
        public String InputJsonFile { get; set; }
    }
}