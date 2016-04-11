namespace lolTest.Runner
{
    using System;
    using System.Threading.Tasks;
    using lolLib;

    public static class GameAnalyzeRunner
    {
        private static String _inputJsonFile;
        private static String _ouputDirectory;
        private static Boolean _playerExportSeparatePlayer;
        private static Boolean _gameExportSeparatePlayer;
        private static Boolean _gameExportSeparateGame;
        private static Int32 _selectPlayer;
        private static Int64 _selectGame;
        private static Boolean _indentedJson;

        public static Int32 Run(String InputJsonFile, String OutputDirectory,
            Boolean PlayerExportSeparatePlayer, Boolean GameExportSeparatePlayer, Boolean GameExportSeparateGame,
            Int32 SelectPlayer, Int64 SelectGame, Boolean IndentedJson)
        {
            _inputJsonFile = InputJsonFile;
            _ouputDirectory = OutputDirectory;
            _playerExportSeparatePlayer = PlayerExportSeparatePlayer;
            _gameExportSeparatePlayer = GameExportSeparatePlayer;
            _gameExportSeparateGame = GameExportSeparateGame;
            _selectPlayer = SelectPlayer;
            _selectGame = SelectGame;
            _indentedJson = IndentedJson;

            RunAsync();

            return 0;
        }

        private static void RunAsync()
        {
            // create class
            var ga = new GameFileAnalyse();
            // set parameters
            ga.LoadFile(Filename: _inputJsonFile);
            // work
            ga.Analyze();
            ga.ExportAnalyze(_ouputDirectory, PlayerExportSeparatePlayer: _playerExportSeparatePlayer,
                GameExportSeparatePlayer: _gameExportSeparatePlayer, GameExportSeparateGame: _gameExportSeparateGame,
                SelectPlayer: _selectPlayer, SelectGame: _selectGame, IndentedJson: _indentedJson);
            // end
            //Console.WriteLine("done!");
        }
    }
}