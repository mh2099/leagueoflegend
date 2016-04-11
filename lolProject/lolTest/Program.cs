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
                const String key = "Vapor eyJkYXRlX3RpbWUiOjE0NjAzMTIxMjczMDQsImdhc19hY2NvdW50X2lkIjozNDI5MjAwOCwicHZwbmV0X2FjY291bnRfaWQiOjM0MjkyMDA4LCJzdW1tb25lcl9uYW1lIjoiQlNOIHIxIiwidm91Y2hpbmdfa2V5X2lkIjoiOTAzNDc1MmIyYjQ1NjA0NGFlODdmMjU5ODJkYWQwN2QiLCJzaWduYXR1cmUiOiJZVjNNd25lcTZ1U3FTMG55NCtDQ1IxdTBRQkQ2RjJGYlR6S1NvSmJmNFJvaEVpbWRKSHVlYjhtNjljdGpFUXdOTWg0QkdUL21CRW42UzFkdEdDbDdRTHdleHdwdWRBMndYbDVSZHF6Tit6UmVBamxwcTY2SVNnZnZZcENOK0FMUlVtd3pTanp4ZE9SSldLUEtrd0dGNkZ5NzVhTGxGSzNJVWxrVUNheHRwTUU9In0=";

                GameCloudSyncRunner.Run(PlatformId:"EUW1", AccountId: "34292008", AuthorizationKey: key,
                    InputJsonFile: @"d:\lol_1.json", OutputJsonFile: @"d:\lol_2.json",
                    CloudUpdate: true, DetailsUpdate: true, AddFrame: true, IndentedJson: true);

                /*GameUpdateRunner.Run(PlatformId: "EUW1", AccountId: "34292008", AuthorizationKey: key,
                                    InputJsonFile: "", OutputJsonFile: @"d:\lol_new.json",
                                    CloudUpdate: true, DetailsUpdate: true, IndentedJson: true);*/

                /*GameAnalyzeRunner.Run(InputJsonFile: @"d:\lol_new.json", OutputDirectory: @"d:\test_lol\",
                    PlayerExportSeparatePlayer: false, GameExportSeparatePlayer : false, GameExportSeparateGame: false,
                    SelectPlayer: 34292008, SelectGame: 0, IndentedJson: true);*/
 
                //GameInfosRunner.Run(InputJsonFile: @"d:\lol_complete.json");

                return 0;
            }
            else
            {
                var exitCode = Parser.Default.ParseArguments<GameCloudSyncOptions, GameDbSyncOptions, GameFileAnalyseOptions, GameFileInfosOptions>(Args)
                    .MapResult(
                        (GameCloudSyncOptions o) => GameCloudSyncRunner.Run(o.PlatformId, o.AccoundId, o.AuthorizationKey,
                            o.InputJsonFile, o.OutputJsonFile, o.CloudUpdate, o.DetailsUpdate, o.AddFrame, o.IndentedJson),
                        (GameFileAnalyseOptions o) => GameAnalyzeRunner.Run(o.InputJsonFile, o.OutputDirectory,
                            o.PlayerExportSeparatePlayer, o.GameExportSeparatePlayer, o.GameExportSeparateGame,
                            o.SelectPlayer, o.SelectGame, o.IndentedJson),
                        (GameDbSyncOptions o) => GameDbSyncRunner.Run(o.InputJsonFile, o.Host, o.Port,
                            o.Username, o.Password, o.Table, o.ForceReload),
                        (GameFileInfosOptions o) => GameInfosRunner.Run(o.InputJsonFile),
                        errs => 1);

                return exitCode;
            }
        }
    }
}