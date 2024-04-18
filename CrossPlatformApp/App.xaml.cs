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
        }

        protected override void OnStart()
        {
            _adbService.StartAdbServerBackgroundProcess();
        }
    }
}
