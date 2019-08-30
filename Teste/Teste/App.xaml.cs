using Prism;
using Prism.Ioc;
using Teste.Repositories.Cotacoes;
using Teste.Services.Api;
using Teste.Services.Request;
using Teste.ViewModels;
using Teste.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Teste
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
            InitializeComponent();

#if DEBUG
            HotReloader.Current.Run(this);
#endif

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IAPIService, APIService>();
            containerRegistry.Register<ICotacaoRepository, CotacaoRepository>();
            containerRegistry.Register<IRequestService, RequestService>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<AdicionarCotacaoView, AdicionarCotacaoViewModel>();
        }
    }
}
