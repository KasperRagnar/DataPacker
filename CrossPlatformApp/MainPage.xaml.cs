using BusinessLogic;
using System.Diagnostics;

namespace CrossPlatformApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private readonly IAdbService _adbService;

        public MainPage(IAdbService adbService) // throws exception right now until figured out how to inject properly
        {
            InitializeComponent();
            _adbService = adbService;
            //_adbService.AttachHandlerToProcessOutputEvents(OnOutputReceived);
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private void OnWriteInputClicked(object sender, EventArgs e)
        {
            _adbService.WriteInput("echo hello world");
        }

        private void OnOutputReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                //OutputLabel.Text += e.Data;
                //SemanticScreenReader.Announce(OutputLabel.Text);
            }
        }
    }

}
