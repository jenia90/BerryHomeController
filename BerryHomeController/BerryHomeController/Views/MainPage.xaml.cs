using BerryHomeController.Common.Services;
using BerryHomeController.Common.ViewModels;
using Xamarin.Forms;
using Device = BerryHomeController.Common.Models.Device;

namespace BerryHomeController.Common.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel(new BerryApiDeviceServiceMock());
        }

        public MainPage(IBerryApiService<Device> deviceApiService)
        {
            InitializeComponent();

            BindingContext = new MainPageViewModel(deviceApiService);
        }
    }
}
