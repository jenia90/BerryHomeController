﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BerryHomeController.Common.Models;
using BerryHomeController.Common.Services;
using BerryHomeController.Common.Views;
using Xamarin.Forms;
using Device = BerryHomeController.Common.Models.Device;

namespace BerryHomeController.Common.ViewModels
{
    internal class MainPageViewModel : ViewModelBase
    {
        #region Constructor

        /// <summary>
        /// Initialize a new instance of MainPageViewModel with a given ApiService
        /// </summary>
        /// <param name="berryApiService">DeviceApiService that would be used for different CRUD operations.</param>
        public MainPageViewModel()
        {
            RefreshDevices();
        }

        #endregion

        #region Properties
        
        private Device _selectedDevice;
        private bool _isRefreshing;
        private ObservableCollection<Device> _devices;

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
        public ICommand AddDeviceCommand => new Command(AddDevice);
        public ICommand RemoveDeviceCommand => new Command<Guid>(async id =>
        {
            await DeviceService.DeleteAsync(id);
            RefreshDevices();
        });

        #endregion

        #region Methods

        /// <summary>
        /// Shows details page for a selected Device.
        /// </summary>
        private async void ExpandDevice()
        {
            var expandViewModel = new ExpandedDeviceViewModel(SelectedDevice);
            await NavigateTo(new ExpandedDevicePage { BindingContext = expandViewModel});
            SelectedDevice = null;
        }

        /// <summary>
        /// Refreshes the list of devices from the ApiService.
        /// </summary>
        private async void RefreshDevices()
        {
#if DEBUG
            await Task.Delay(500);
#endif
            Devices = new ObservableCollection<Device>(await DeviceService.GetAsync());
            IsRefreshing = false;
        }

        /// <summary>
        /// Switches the state of a device given by its ID.
        /// </summary>
        /// <param name="id">Guid of the device.</param>
        public async void SwitchDevice(Guid id)
        {
#if !DEBUG
            var device = Devices.First(d => d.Id == id);
            await DeviceService.PostAsync(new State() { DeviceId = id, DeviceState = device.State });
#endif
            RefreshDevices();
        }

        /// <summary>
        /// Shows the NewDevicePage to the user and then waits for user to config the new device.
        /// </summary>
        public async void AddDevice()
        {
            // Initialize NewDevicePage.
            var newDevicePage = new NewEditDevicePage
            {
                BindingContext = new NewEditDeviceViewModel("New Device")
            };

            //Subscribe to device saved message.
            MessagingCenter.Subscribe<NewEditDeviceViewModel, Device>(this, "add_device", async (s, d) =>
            {
                d.Id = Guid.NewGuid();
                await DeviceService.PostAsync(d);
                RefreshDevices();
            });
            
            await NavigateTo(newDevicePage);
        }

        #endregion

    }
}
