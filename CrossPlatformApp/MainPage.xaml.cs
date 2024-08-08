using BusinessLogic;
using BusinessLogic.Models;

namespace CrossPlatformApp
{
    public partial class MainPage : ContentPage
    {
        string adbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "platform-tools");

        private readonly IAdbService _adbService;

        public MainPage(IAdbService adbService)
        {
            InitializeComponent();
            _adbService = adbService;
        }

        private void OnWriteInputClicked(object sender, EventArgs e)
        {
            PowershellCommand newCommand = new PowershellCommand("echo", new List<string>() { "hello world" });
            string finalOutput = _adbService.WriteInput(newCommand);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                OutputLabel.Text += finalOutput;
                SemanticScreenReader.Announce(OutputLabel.Text);
            });
        }
    }

}
