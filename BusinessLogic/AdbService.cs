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


            string adbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "platform-tools");

            try
            { // TODO: figure out how to handle be allowed to execute adb.exe file
                process.StartInfo.FileName = "adb.exe"; // TODO: Change adb.exe to use cmd (or akin) to make this a waiting process instead of fire and forget
                process.StartInfo.WorkingDirectory = adbPath;
                process.StartInfo.CreateNoWindow = false;
                process.StartInfo.UseShellExecute = true; // TODO: Figure out why "true" this prervents start from throwing exception as if it was not allowed to execute the file
                process.StartInfo.Arguments = "adb devices";

                process.OutputDataReceived += OnOutputReceivedHandler;
                process.Start();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{nameof(Exception)}: {e.Message}");
                throw;
            }

            processHasStarted = true;
        }

        public void OnOutputReceivedHandler(object source, DataReceivedEventArgs e)
        {
            Debug.WriteLine(e.Data);
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
