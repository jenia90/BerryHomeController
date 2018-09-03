using BerryHomeController.Common.Models;
using BerryHomeController.Common.Services;
using BerryHomeController.Common.ViewModels;
using BerryHomeController.Common.Views;
using Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Device = BerryHomeController.Common.Models.Device;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BerryHomeController.Common
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var vm = InitializeContainers().Resolve<MainPageViewModel>();

            MainPage = new NavigationPage(new MainPage (){BindingContext = vm});
            //MainPage = new NavigationPage(new MainPage(new BerryApiDeviceServiceMock()));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private UnityContainer InitializeContainers()
        {
            var container = new UnityContainer();

            container.RegisterType<IBerryApiService<Device>, BerryApiDeviceServiceMock>();
            container.RegisterType<IBerryApiService<Job>, BerryApiJobServiceMock>();
            container.RegisterType<IBerryApiService<State>, BerryApiStateService>();

            return container;
        }
    }
}
