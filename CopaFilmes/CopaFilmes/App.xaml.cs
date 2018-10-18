using Prism;
using Prism.Ioc;
using CopaFilmes.ViewModels;
using CopaFilmes.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CopaFilmes.Services.Abstract;
using CopaFilmes.Services.Implements;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CopaFilmes
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            LiveReload.Init();

            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IService,Service>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<ResultadoPage, ResultadoPageViewModel>();
        }
    }
}
