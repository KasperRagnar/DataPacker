using System.Diagnostics;

namespace BusinessLogic
{
    // All the code in this file is included in all platforms.
    public sealed class AdbService : IAdbService, IDisposable
    {
        private readonly Process process;
        private bool processHasStarted;

        public AdbService()
        {
            process = new Process();
        }

        public void StartAdbApplicationAsBackgroundProcess()
        {
            if (processHasStarted)
            {
                throw new InvalidOperationException("Server is already started");
            }


            //Debug.WriteLine("Hello world");
            string adbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "platform-tools");

            process.StartInfo.FileName = "adb.exe";
            process.StartInfo.WorkingDirectory = adbPath;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.Arguments = "adb start-server";

            process.OutputDataReceived += OnOutputReceivedHandler;
            process.Start();

            processHasStarted = true;
        }

        public void OnOutputReceivedHandler(object source, DataReceivedEventArgs e)
        {
            // Do we need to react to any kind of output?
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!process.HasExited)
                {
                    process.StandardInput.WriteLine("adb kill-server");
                    process.Dispose();
                }
            }
        }
    }
}
