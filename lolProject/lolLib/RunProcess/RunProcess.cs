namespace lolLib.RunProcess
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public static class RunProcess
    {
        public static void Run(String Filename, String Arguments)
        {
            var process = new Process
            {
                StartInfo =
                {
                    FileName = Filename,
                    Arguments = Arguments,
                    WindowStyle = ProcessWindowStyle.Hidden
                },
                EnableRaisingEvents = true
            };

            process.Start();
            process.WaitForExit();
        }

        public static Task RunAsync(String Filename, String Arguments)
        {
            var tcs = new TaskCompletionSource<Boolean>();

            var process = new Process
            {
                StartInfo =
                {
                    FileName = Filename,
                    Arguments = Arguments,
                    WindowStyle = ProcessWindowStyle.Hidden
                },
                EnableRaisingEvents = true
            };

            process.Exited += (Sender, Args) =>
            {
                tcs.SetResult(true);
                process.Dispose();
            };

            process.Start();

            return tcs.Task;
        }
    }
}