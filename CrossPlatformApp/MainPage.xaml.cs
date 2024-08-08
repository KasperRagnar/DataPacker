using BusinessLogic;
using System.Diagnostics;

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
            string finalOutput = _adbService.WriteInput("hello world");

            MainThread.BeginInvokeOnMainThread(() =>
            {
                OutputLabel.Text += finalOutput;
                SemanticScreenReader.Announce(OutputLabel.Text);
            });
        }
    }

}
