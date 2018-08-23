using BerryHomeController.Common.Services;
using BerryHomeController.Common.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Device = BerryHomeController.Common.Models.Device;

namespace BerryHomeController.Common.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpandedDevicePage : TabbedPage
    {
        public ExpandedDevicePage (Device device, IBerryApiService<Device> berryApiService)
        {
            InitializeComponent();
            BindingContext = new ExpandedDeviceViewModel(device, berryApiService);
        }
    }
}