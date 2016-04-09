namespace lolTest.Runner
{
    using System;
    using System.Threading.Tasks;
    using lolLib;

    public static class GameUpdateRunner
    {
        private static String _platformId;
        private static String _accountId;
        private static String _authorizationKey;
        private static String _temporaryDirectory;
        private static String _inputJsonFile;
        private static String _ouputJsonFile;
        private static Boolean _gameDetails;
        private static Boolean _deleteTemporaryFile;
        private static Boolean _indentedJson;

        public static Int32 Run(String PlatformId, String AccountId, String AuthorizationKey,
            String TemporaryDirectory, String InputJsonFile, String OutputJsonFile,
            Boolean GameDetails, Boolean DeleteTemporaryFile, Boolean IndentedJson)
        {
            _platformId = PlatformId;
            _accountId = AccountId;
            _authorizationKey = AuthorizationKey;
            _temporaryDirectory = TemporaryDirectory;
            _inputJsonFile = InputJsonFile;
            _ouputJsonFile = OutputJsonFile;
            _gameDetails = GameDetails;
            _deleteTemporaryFile = DeleteTemporaryFile;
            _indentedJson = IndentedJson;

            RunAsync().Wait();

            return 0;
        }

        private static async Task RunAsync()
        {
            // create class
            var gm = new GameCloudUpdater(_platformId, _accountId, _authorizationKey);
            // set parameters
            gm.LoadFile(_inputJsonFile);
            gm.SetUpdateStep(20);
            gm.SetTemporaryDirectory(_temporaryDirectory);
            gm.SetDeleteTemporaryJson(DeleteTemporaryJson: _deleteTemporaryFile);
            // work
            await gm.UpdateGamesFromCloud();
            if (_gameDetails) await gm.UpdateDetailsFromCloud();
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