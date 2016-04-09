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
        private static Boolean _deleteTemporaryFile;
        private static Boolean _indentedJson;

        private static Int32 Main(String[] Args)
        {
            var result = Parser.Default.ParseArguments<Options>(Args);
            var exitCode = result
                .MapResult(
                    o => 
                    {
                        Run(o.PlatformId, o.AccoundId, o.AuthorizationKey,
                            o.TemporaryDirectory, o.InputJsonFile, o.OutputJsonFile,
                            o.DeleteTemporaryFile, o.IndentedJson);

                        return 0;
                    },
                    errors => 1
                );
            return exitCode;
        }

        private static void Run(String PlatformId, String AccountId, String AuthorizationKey,
            String TemporaryPath, String InputJsonFile, String OutputJsonFile,
            Boolean DeleteTemporaryFile, Boolean IndentedJson)
        {
            _platformId = PlatformId;
            _accountId = AccountId;
            _authorizationKey = AuthorizationKey;
            _temporaryPath = TemporaryPath;
            _inputJsonFile = InputJsonFile;
            _ouputJsonFile = OutputJsonFile;
            _deleteTemporaryFile = DeleteTemporaryFile;
            _indentedJson = IndentedJson;

            RunAsync().Wait();
        }

        private static async Task RunAsync()
        {
            var gm = new GameUpdater(_platformId, _accountId, _authorizationKey);
            gm.LoadFile(_inputJsonFile);
            gm.SetUpdateStep(20);
            gm.SetTemporaryDirectory(_temporaryPath);
            gm.SetDeleteTemporaryJson(DeleteTemporaryJson: _deleteTemporaryFile);
            await gm.UpdateGamesFromCloud();
            gm.GenerateJson(_ouputJsonFile, Indented: _indentedJson);
        }
    }
}