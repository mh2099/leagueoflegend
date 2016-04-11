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
            var ga = new GameFileAnalyse();
            // set parameters
            ga.LoadFile(Filename: _inputJsonFile);
            // work
            ga.Analyze(ShowInfos: true);
            //
            await Task.Delay(1);
            // end
            //Console.WriteLine("done!");
        }
    }
}