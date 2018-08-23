using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using BerryHomeController.Common.Services;
using BerryHomeController.Common.Views;
using Xamarin.Forms;
using Device = BerryHomeController.Common.Models.Device;

namespace BerryHomeController.Common.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
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

        #endregion

        #region Commands

        public ICommand ExpandDeviceCommand => new Command(ExpandDevice);
        public ICommand RefreshDevicesCommand => new Command(RefreshDevices);
        public ICommand SwitchDeviceCommand => new Command<Guid>(SwitchDevice);

        #endregion

        #region Methods

        private async void ExpandDevice()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new NavigationPage(new ExpandedDevicePage(SelectedDevice, _berryApiService)));
            SelectedDevice = null;
        }

        private async void RefreshDevices()
        {
            Devices?.Clear();
            Devices = new ObservableCollection<Device>(await _berryApiService.GetAsync());
        }

        public async void SwitchDevice(Guid id)
        {
#if !DEBUG
            var device = Devices.First(d => d.Id == id);
            _berryApiService.PutAsync(id, device).Wait();
#endif
            RefreshDevices();
        }

#endregion

#region INPC

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
#endregion
    }
}
