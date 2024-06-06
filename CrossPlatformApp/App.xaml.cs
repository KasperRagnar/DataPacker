using BusinessLogic;

namespace CrossPlatformApp
{
    public partial class App : Application
    {
        private readonly IAdbService _adbService;
        public App(IAdbService adbService)
        {
            InitializeComponent();

            _adbService = adbService ?? throw new ArgumentNullException(nameof(adbService));
            MainPage = new AppShell();
            // TODO: Figure out how to inject dependencies to main page (or other pages). If this is not the way to do it in MAUI, then figure that out
        }

        protected override void OnStart()
        {
            _adbService.StartAdbApplicationAsBackgroundProcess();
        }
    }
}
