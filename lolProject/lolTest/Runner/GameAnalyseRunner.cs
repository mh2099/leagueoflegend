﻿namespace lolTest.Runner
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
        private static Boolean _indentedJson;

        public static Int32 Run(String InputJsonFile, String OutputDirectory,
            Boolean PlayerExportSeparatePlayer, Boolean GameExportSeparatePlayer, Boolean GameExportSeparateGame, Boolean IndentedJson)
        {
            _inputJsonFile = InputJsonFile;
            _ouputDirectory = OutputDirectory;
            _playerExportSeparatePlayer = PlayerExportSeparatePlayer;
            _gameExportSeparatePlayer = GameExportSeparatePlayer;
            _gameExportSeparateGame = GameExportSeparateGame;
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
            ga.ExportAnalyze(_ouputDirectory, PlayerExportSeparatePlayer: _playerExportSeparatePlayer,
                GameExportSeparatePlayer: _gameExportSeparatePlayer, GameExportSeparateGame: _gameExportSeparateGame,
                IndentedJson: _indentedJson);
            // end
            //Console.WriteLine("done!");
        }
    }
}