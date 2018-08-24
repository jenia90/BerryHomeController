using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BerryHomeController.Common.Services;
using BerryHomeController.Common.Views;
using Xamarin.Forms;
using Device = BerryHomeController.Common.Models.Device;

namespace BerryHomeController.Common.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IBerryApiService<Device> _berryApiService;

        public MainPageViewModel(IBerryApiService<Device> berryApiService)
        {
            _berryApiService = berryApiService;

            RefreshDevices();
        }

        #region Properties

        private ObservableCollection<Device> _devices;
        private Device _selectedDevice;
        private bool _isRefreshing;

        public ObservableCollection<Device> Devices
        {
            get => _devices;
            set
            {
                _devices = value;
                OnPropertyChanged();
            }
        }

        public Device SelectedDevice
        {
            get => _selectedDevice;
            set
            {
                _selectedDevice = value;
                OnPropertyChanged();
            }
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public ICommand ExpandDeviceCommand => new Command(ExpandDevice);
        public ICommand RefreshDevicesCommand => new Command(RefreshDevices);
        public ICommand SwitchDeviceCommand => new Command<Guid>(SwitchDevice);

        #endregion

        #region Methods

        private async void ExpandDevice()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new NavigationPage(new ExpandedDevicePage(SelectedDevice)));
            SelectedDevice = null;
        }

        private async void RefreshDevices()
        {
#if DEBUG
            await Task.Delay(500);
#endif
            Devices = new ObservableCollection<Device>(await _berryApiService.GetAsync());
            IsRefreshing = false;
        }

        public async void SwitchDevice(Guid id)
        {
#if !DEBUG
            var device = Devices.First(d => d.Id == id);
            await _berryApiService.PutAsync(id, device);
#endif
            RefreshDevices();
        }

#endregion

    }
}
