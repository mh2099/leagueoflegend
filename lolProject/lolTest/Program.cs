namespace lolTest
{
    using System;
    using System.Threading.Tasks;
    using CommandLine;
    using lolLib;

    internal class Program
    {
        private static String _platformId;
        private static String _accountId;
        private static String _authorizationKey;
        private static String _temporaryPath;
        private static String _inputJsonFile;
        private static String _ouputJsonFile;
        private static Boolean _gameDetails;
        private static Boolean _deleteTemporaryFile;
        private static Boolean _indentedJson;

        private static Int32 Main(String[] Args)
        {
#if DEBUG
            const String key = "Vapor eyJkYXRlX3RpbWUiOjE0NjAxMjcyMDE4ODYsImdhc19hY2NvdW50X2lkIjozNDI5MjAwOCwicHZwbmV0X2FjY291bnRfaWQiOjM0MjkyMDA4LCJzdW1tb25lcl9uYW1lIjoiQlNOIHIxIiwidm91Y2hpbmdfa2V5X2lkIjoiOTAzNDc1MmIyYjQ1NjA0NGFlODdmMjU5ODJkYWQwN2QiLCJzaWduYXR1cmUiOiJudmIzYjRFeXRqRVVjWEx0dFZGZ0R1bTU0blJIemlQU2lnYkVBR2NtVGNYalRKSGwwdDdYVXV5cnExdEtheDlFZDdNRnN2ODUvNXAvbFpSYjdMWkJUL0VVQ3FzcGxDejZ1ZFpUUG0zUEpQTDBDTVJHNXV1MFExNVdwck5ZUXZmZTFIL255KzEwU3VBSWJ1UVdGS0tMUXp5T0dScGZDc2FPZXRUS2tFOVY5Rms9In0=";

            Run(PlatformId:"EUW1", AccountId: "34292008", AuthorizationKey: key,
                TemporaryPath: @"d:\test_lol", InputJsonFile: @"d:\lol_1.json", OutputJsonFile: @"d:\lol_2.json",
                GameDetails: true, DeleteTemporaryFile: true, IndentedJson: true);

            return 0;
#else
            var result = Parser.Default.ParseArguments<Options>(Args);
            var exitCode = result
                .MapResult(
                    o => 
                    {
                        Run(o.PlatformId, o.AccoundId, o.AuthorizationKey,
                            o.TemporaryDirectory, o.InputJsonFile, o.OutputJsonFile,
                            o.GameDetails, o.DeleteTemporaryFile, o.IndentedJson);

                        return 0;
                    },
                    errors => 1
                );
            return exitCode;
#endif
        }

        private static void Run(String PlatformId, String AccountId, String AuthorizationKey,
            String TemporaryPath, String InputJsonFile, String OutputJsonFile,
            Boolean GameDetails, Boolean DeleteTemporaryFile, Boolean IndentedJson)
        {
            _platformId = PlatformId;
            _accountId = AccountId;
            _authorizationKey = AuthorizationKey;
            _temporaryPath = TemporaryPath;
            _inputJsonFile = InputJsonFile;
            _ouputJsonFile = OutputJsonFile;
            _gameDetails = GameDetails;
            _deleteTemporaryFile = DeleteTemporaryFile;
            _indentedJson = IndentedJson;

            RunAsync().Wait();
        }

        private static async Task RunAsync()
        {
            // create class
            var gm = new GameUpdater(_platformId, _accountId, _authorizationKey);
            // set parameters
            gm.LoadFile(_inputJsonFile);
            gm.SetUpdateStep(20);
            gm.SetTemporaryDirectory(_temporaryPath);
            gm.SetDeleteTemporaryJson(DeleteTemporaryJson: _deleteTemporaryFile);
            // work
            //await gm.UpdateGamesFromCloud();
            if(_gameDetails) await gm.UpdateDetailsFromCloud();
            // export
            if (_gameDetails)
                gm.GenerateGameDetailsJson(_ouputJsonFile, Indented: _indentedJson);
            else
                gm.GenerateGameJson(_ouputJsonFile, Indented: _indentedJson);
            // end
            Console.WriteLine("done!");
        }
    }
}