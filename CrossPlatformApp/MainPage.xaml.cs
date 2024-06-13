using BusinessLogic;
using System.Diagnostics;

namespace CrossPlatformApp
{
    public partial class MainPage : ContentPage
    {
        string adbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "platform-tools");
        private string unneededSubString = "C:\\WINDOWS\\system32>";
        private string currentCommand = string.Empty;

        int count = 0;
        private bool isAttached;
        private readonly IAdbService _adbService;

        public MainPage(IAdbService adbService) // throws exception right now until figured out how to inject properly
        {
            InitializeComponent();
            _adbService = adbService;
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

        private void OnConnectBtnClicked(object sender, EventArgs e)
        {
            if (isAttached)
                return;
            _adbService.AttachHandlerToProcessOutputEvents(OnOutputReceived);
            _adbService.AttachHandlerToProcessErrorEvents(OnErrorReceived);
            isAttached = true;
            ConnectBtnTest.IsEnabled = false;
        }

        private void OnWriteInputClicked(object sender, EventArgs e)
        {
            //string nextCmdCommand = "echo hello world";
            string nextCmdCommand = "adb.exe devices";
            currentCommand = nextCmdCommand;
            _adbService.WriteInput(currentCommand);
        }

        private void OnStartServerClicked(object sender, EventArgs e)
        {
            //string nextCmdCommand = "echo hello world";
            string nextCmdCommand = "adb.exe start-server";
            currentCommand = nextCmdCommand;
            _adbService.WriteInput(currentCommand);
        }

        private void OnKillServerClicked(object sender, EventArgs e)
        {
            //string nextCmdCommand = "echo hello world";
            string nextCmdCommand = "adb.exe kill-server";
            currentCommand = nextCmdCommand;
            _adbService.WriteInput(currentCommand);
        }

        private void OnOutputReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                string input = adbPath + ">" +  currentCommand;
                if (e.Data == input)
                {
                    return;
                }
                if (e.Data != currentCommand)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        OutputLabel.Text += e.Data;
                        SemanticScreenReader.Announce(OutputLabel.Text);
                    });
                }
            }
        }
        private void OnErrorReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                string input = adbPath + ">" + currentCommand;
                if (e.Data == input)
                {
                    return;
                }
                if (e.Data != currentCommand)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        ErrorLabel.Text += e.Data;
                        SemanticScreenReader.Announce(ErrorLabel.Text);
                    });
                }
            }
        }

    }

}
