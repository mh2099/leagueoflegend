namespace lolTest
{
    using System;
    using System.Threading.Tasks;
    using Arguments;
    using CommandLine;
    using lolLib;
    using Runner;

    internal class Program
    {
        private static Int32 Main(String[] Args)
        {
#if DEBUG
            /*const String key = "Vapor eyJkYXRlX3RpbWUiOjE0NjAxMjcyMDE4ODYsImdhc19hY2NvdW50X2lkIjozNDI5MjAwOCwicHZwbmV0X2FjY291bnRfaWQiOjM0MjkyMDA4LCJzdW1tb25lcl9uYW1lIjoiQlNOIHIxIiwidm91Y2hpbmdfa2V5X2lkIjoiOTAzNDc1MmIyYjQ1NjA0NGFlODdmMjU5ODJkYWQwN2QiLCJzaWduYXR1cmUiOiJudmIzYjRFeXRqRVVjWEx0dFZGZ0R1bTU0blJIemlQU2lnYkVBR2NtVGNYalRKSGwwdDdYVXV5cnExdEtheDlFZDdNRnN2ODUvNXAvbFpSYjdMWkJUL0VVQ3FzcGxDejZ1ZFpUUG0zUEpQTDBDTVJHNXV1MFExNVdwck5ZUXZmZTFIL255KzEwU3VBSWJ1UVdGS0tMUXp5T0dScGZDc2FPZXRUS2tFOVY5Rms9In0=";

            GameUpdateRunner.Run(PlatformId:"EUW1", AccountId: "34292008", AuthorizationKey: key,
                TemporaryDirectory: @"d:\test_lol", InputJsonFile: @"d:\lol_1.json", OutputJsonFile: @"d:\lol_2.json",
                GameDetails: true, DeleteTemporaryFile: true, IndentedJson: true);*/

            GameAnalyzeRunner.Run(InputJsonFile: @"d:\lol_complete.json", OutputDirectory: @"d:\test_lol",
                SeparateForEachPlayer: true, SeparateForEachGame: true, IndentedJson: true);

            return 0;
#else

            var exitCode = Parser.Default.ParseArguments<GameUpdateOptions, GameAnalyseOptions>(Args)
              .MapResult(
                  (GameUpdateOptions o) => GameUpdateRunner.Run(o.PlatformId, o.AccoundId, o.AuthorizationKey,
                            o.TemporaryDirectory, o.InputJsonFile, o.OutputJsonFile,
                            o.GameDetails, o.DeleteTemporaryFile, o.IndentedJson),
                  (GameAnalyseOptions o) => GameAnalyzeRunner.Run(o.InputJsonFile, o.OutputDirectory,
                            o.SeparateForEachPlayer, o.SeparateForEachGame,
                            o.IndentedJson),
                errs => 1);

            return exitCode;
#endif
        }
    }
}