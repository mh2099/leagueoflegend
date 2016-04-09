namespace lolTest.Runner
{
    using System;
    using System.Threading.Tasks;
    using lolLib;

    public static class GameAnalyzeRunner
    {
        private static String _inputJsonFile;
        private static String _ouputDirectory;
        private static Boolean _separateForEachPlayer;
        private static Boolean _separateForEachGame;
        private static Boolean _indentedJson;

        public static Int32 Run(String InputJsonFile, String OutputDirectory,
            Boolean SeparateForEachPlayer, Boolean SeparateForEachGame, Boolean IndentedJson)
        {
            _inputJsonFile = InputJsonFile;
            _ouputDirectory = OutputDirectory;
            _separateForEachPlayer = SeparateForEachPlayer;
            _separateForEachGame = SeparateForEachGame;
            _indentedJson = IndentedJson;

            RunAsync();

            return 0;
        }

        private static void RunAsync()
        {
            // create class
            var ga = new GameAnalyse();
            // set parameters
            ga.LoadFile(_inputJsonFile);
            // work
            ga.Analyze();
            ga.ExportAnalyze(_ouputDirectory, SeparateForEachPlayer: _separateForEachPlayer, SeparateForEachGame: _separateForEachGame, IndentedJson: _indentedJson);
            // end
            //Console.WriteLine("done!");
        }
    }
}