namespace lolTest.Runner
{
    using System;
    using System.Threading.Tasks;
    using lolLib;
    using lolLib.Class;

    public static class GameUpdateRunner
    {
        private static String _platformId;
        private static String _accountId;
        private static String _authorizationKey;
        private static String _inputJsonFile;
        private static String _ouputJsonFile;
        private static Boolean _cloudUpdate;
        private static Boolean _detailsUpdate;
        private static Boolean _indentedJson;

        public static Int32 Run(String PlatformId, String AccountId, String AuthorizationKey,
            String InputJsonFile, String OutputJsonFile, Boolean CloudUpdate, Boolean DetailsUpdate, Boolean IndentedJson)
        {
            _platformId = PlatformId;
            _accountId = AccountId;
            _authorizationKey = AuthorizationKey;
            _inputJsonFile = InputJsonFile;
            _ouputJsonFile = OutputJsonFile;
            _cloudUpdate = CloudUpdate;
            _detailsUpdate = DetailsUpdate;
            _indentedJson = IndentedJson;

            RunAsync().Wait();

            return 0;
        }

        private static async Task RunAsync()
        {
            // create class
            var gm = new GameCloudSync(_platformId, _accountId, _authorizationKey);
            // set parameters
            gm.LoadFile(_inputJsonFile);
            gm.SetUpdateStep(20);
            // progress bar
            var progress = new Progress<SyncProgress>();
            progress.ProgressChanged += (Sender, Progress) => { Console.Write($"\r {Progress.GetInfos()} {Progress.GetPercentage()} % "); };
            // work
            if(_cloudUpdate) await gm.UpdateGamesFromCloud(progress);
            if (_detailsUpdate) await gm.UpdateDetailsFromCloud(progress);
            // export
            if (_detailsUpdate)
                gm.GenerateGameDetailsJson(_ouputJsonFile, IndentedJson: _indentedJson);
            else
                gm.GenerateGameJson(_ouputJsonFile, IndentedJson: _indentedJson);
            // end
            //Console.WriteLine("done!");
        }
    }
}