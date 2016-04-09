namespace lolTest
{
    using System;
    using Arguments;
    using CommandLine;
    using Runner;

    internal class Program
    {
        private static Int32 Main(String[] Args)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                const String key = "Vapor eyJkYXRlX3RpbWUiOjE0NjAyMzMwNTIzNTYsImdhc19hY2NvdW50X2lkIjozNDI5MjAwOCwicHZwbmV0X2FjY291bnRfaWQiOjM0MjkyMDA4LCJzdW1tb25lcl9uYW1lIjoiQlNOIHIxIiwidm91Y2hpbmdfa2V5X2lkIjoiOTAzNDc1MmIyYjQ1NjA0NGFlODdmMjU5ODJkYWQwN2QiLCJzaWduYXR1cmUiOiJJRGVKMlBRQWo1enhkWmdmTnZWOE00T1N0NTVVMDRPRmxTcDVEaW5TdHU2cmNENjhOdUwwMkI3SjEwTjdTSXBjcktKZFpQMkF5eUVSdlFVRThwN1NqdGNLRDFVUGF6ajU4U3UrcFIwRGgvUm5rek5Vc2d5VXM4Z0RIVm9ET2NQZjhPTTFjSWNFZHVDaHZEdUxmZDJkWUJJdnhOcFZnWUxVSmsvWVI3M3ZFVG89In0=";

                /*GameUpdateRunner.Run(PlatformId:"EUW1", AccountId: "34292008", AuthorizationKey: key,
                    TemporaryDirectory: @"d:\test_lol", InputJsonFile: @"d:\lol_1.json", OutputJsonFile: @"d:\lol_2.json",
                    GameDetails: true, DeleteTemporaryFile: true, IndentedJson: true);*/

                GameUpdateRunner.Run(PlatformId: "EUW1", AccountId: "34292008", AuthorizationKey: key,
                                    TemporaryDirectory: @"d:\test_lol", InputJsonFile: "", OutputJsonFile: @"d:\lol_new.json",
                                    CloudUpdate: true, DetailsUpdate: false,
                                    DeleteTemporaryFile: true, IndentedJson: true);

                /*GameAnalyzeRunner.Run(InputJsonFile: @"d:\lol_complete.json", OutputDirectory: @"d:\test_lol",
                    SeparateForEachPlayer: false, SeparateForEachGame: false, IndentedJson: true);*/

                //GameInfosRunner.Run(InputJsonFile: @"d:\lol_complete.json");

                return 0;
            }
            else
            {
                var exitCode = Parser.Default.ParseArguments<GameUpdateOptions, GameAnalyseOptions, GameDbSyncOptions, GameInfosOptions>(Args)
                    .MapResult(
                        (GameUpdateOptions o) => GameUpdateRunner.Run(o.PlatformId, o.AccoundId, o.AuthorizationKey,
                            o.TemporaryDirectory, o.InputJsonFile, o.OutputJsonFile,
                            o.CloudUpdate, o.DetailsUpdate,
                            o.DeleteTemporaryFile, o.IndentedJson),
                        (GameAnalyseOptions o) => GameAnalyzeRunner.Run(o.InputJsonFile, o.OutputDirectory,
                            o.SeparateForEachPlayer, o.SeparateForEachGame,
                            o.IndentedJson),
                        (GameDbSyncOptions o) => GameDbSyncRunner.Run(o.Host, o.Port,
                            o.Username, o.Password, o.Table),
                        (GameInfosOptions o) => GameInfosRunner.Run(o.InputJsonFile),
                        errs => 1);

                return exitCode;
            }
        }
    }
}