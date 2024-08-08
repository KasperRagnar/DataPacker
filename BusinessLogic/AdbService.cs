using System.Management.Automation;

namespace BusinessLogic
{
    // All the code in this file is included in all platforms.
    public sealed class AdbService : IAdbService, IDisposable
    {
        string adbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "platform-tools");

        public string WriteInput(string input)
        {
            try
            {
                List<PSObject> psObjects = PowerShell.Create().AddCommand($"echo").AddArgument(input).Invoke().ToList();
                PSObject echoOutputObject = psObjects.First();
                return echoOutputObject.BaseObject.ToString()!;
            }
            catch (Exception e)
            {
                return $"Unexpected error: {e.Message}";
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
                PowerShell.Create().AddCommand($"adb.exe").AddArgument("kill-server").Invoke();
            }
        }
    }
}
