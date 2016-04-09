namespace lolLib.Class
{
    using System;

    public class SyncProgress
    {
        private static Int32 _progress;
        private static Int32 _progressMax;
        private static String _infos;

        public static void SetProgressMax(Int32 Max)
        {
            _progressMax = Max;
        }

        public static void New(String Infos)
        {
            _progress = 0;
            _progressMax = 0;
            _infos = Infos;
        }

        public SyncProgress()
        {
            _progress++;
        }

        public String GetPercentage()
        {
            var prc = Math.Round(Convert.ToDouble(_progress)/Convert.ToDouble(_progressMax)*100, 2);
            if (prc > 100) prc = 100;
            return prc.ToString("00.00");
        }

        public String GetInfos()
        {
            return _infos;
        }
    }
}