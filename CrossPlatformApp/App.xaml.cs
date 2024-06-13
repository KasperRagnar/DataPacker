using BusinessLogic;

namespace CrossPlatformApp
{
    public partial class App : Application
    {
        private readonly IAdbService _adbService;
        public App(IAdbService adbService, IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _adbService = adbService;
            //MainPage = new AppShell();

            // This is a "workaround" way of injecting services into pages without losing access to Static resources
            // Inspiration from https://github.com/dotnet/maui/issues/11485
            // Will roll with this for now until a better solution is presented
            MainPage mainPage = serviceProvider.GetService<MainPage>()!;
            MainPage = new NavigationPage(mainPage);
        }

        protected override void OnStart()
        {
            _adbService.StartAdbApplicationAsBackgroundProcess();
        }
    }
}
