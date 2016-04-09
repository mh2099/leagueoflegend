namespace lolTest.Runner
{
    using System;
    using System.Threading.Tasks;
    using lolLib;

    public static class GameInfosRunner
    {
        private static String _inputJsonFile;

        public static Int32 Run(String InputJsonFile)
        {
            _inputJsonFile = InputJsonFile;

            RunAsync().Wait();

            return 0;
        }

        private static async Task RunAsync()
        {
            // create class
            var ga = new GameAnalyse();
            // set parameters
            ga.LoadFile(_inputJsonFile);
            // work
            ga.Analyze(ShowInfos: true);
            // end
            //Console.WriteLine("done!");
        }
    }
}