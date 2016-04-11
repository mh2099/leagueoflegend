namespace lolTest.Arguments
{
    using System;
    using CommandLine;

    [Verb("file-infos", HelpText = "game list informations")]
    public class GameFileInfosOptions
    {
        [Option('i', "inputJSONFile", Required = true, HelpText = "Input JSON file")]
        public String InputJsonFile { get; set; }
    }
}