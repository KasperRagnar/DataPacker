using System.Diagnostics;
using System.Text;

namespace BusinessLogic
{
    // All the code in this file is included in all platforms.
    public sealed class AdbService : IAdbService, IDisposable
    {
        private readonly Process process;
        private bool processHasStarted;
        private StringBuilder outputBuilder = new StringBuilder();

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
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                //process.StartInfo.FileName = "cmd.exe"; // TODO: Change adb.exe to use cmd (or akin) to make this a waiting process instead of fire and forget
                process.StartInfo.FileName = "powershell.exe"; // TODO: Change adb.exe to use cmd (or akin) to make this a waiting process instead of fire and forget
                process.StartInfo.WorkingDirectory = adbPath;
                process.StartInfo.CreateNoWindow = true;
                //process.StartInfo.UseShellExecute = true; // TODO: Figure out why "true" this prervents start from throwing exception as if it was not allowed to execute the file
                process.StartInfo.UseShellExecute = false;

                //process.OutputDataReceived += OnOutputReceivedHandler;
                //process.ErrorDataReceived += OnErrorReceivedHandler;
                process.Start();
                process.BeginOutputReadLine();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{nameof(Exception)}: {e.Message}");
                throw;
            }

            processHasStarted = true;
        }

        public void WriteInput(string input)
        {
            process.StandardInput.WriteLine($"{input}");
        }

        public void AttachHandlerToProcessOutputEvents(DataReceivedEventHandler handler)
        {
            process.OutputDataReceived += handler;
        }

        public void AttachHandlerToProcessErrorEvents(DataReceivedEventHandler handler)
        {
            process.ErrorDataReceived += handler;
        }

        public void OnOutputReceivedHandler(object source, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                outputBuilder.AppendLine(e.Data);
                Debug.WriteLine(e.Data);
                // Do we need to react to any kind of output?
            }
        }

        public static void OnErrorReceivedHandler(object source, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                Debug.WriteLine("ERROR: " + e.Data);
            }
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
