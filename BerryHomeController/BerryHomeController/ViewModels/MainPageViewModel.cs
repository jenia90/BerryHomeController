using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using BerryHomeController.Common.Services;
using BerryHomeController.Common.Models;
using Xamarin.Forms;
using Device = BerryHomeController.Common.Models.Device;

namespace BerryHomeController.Common.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly IBerryApiService<Device> _berryApiService;
        private ObservableCollection<Device> _devices;

        public MainPageViewModel()
        {
            _berryApiService = new BerryApiDeviceServiceMock();

            RefreshDevices();

            ExpandDeviceCommand = new Command(ExpandDevice);
            RefreshDevicesCommand = new Command(RefreshDevices);
            SwitchDeviceCommand = new Command<Guid>(SwitchDevice);
        }

        public ObservableCollection<Device> Devices
        {
            get => _devices;
            set
            {
                _devices = value;
                OnPropertyChanged();
            }
        }

        #region Commands

        public ICommand ExpandDeviceCommand { get; private set; }

        public ICommand RefreshDevicesCommand { get; private set; }

        public ICommand SwitchDeviceCommand { get; private set; }

        #endregion

        #region Methods

        private void ExpandDevice()
        {

        }

        private async void RefreshDevices()
        {
            Devices = new ObservableCollection<Device>(await _berryApiService.GetAsync());
        }

        private async void SwitchDevice(Guid id)
        {
            Devices.First(d => d.Id == id).State = !Devices.First(d => d.Id == id).State;
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
