using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using BerryHomeController.Common.Models;
using BerryHomeController.Common.Services;

namespace BerryHomeController.Common.ViewModels
{
    public class ExpandedDeviceViewModel : INotifyPropertyChanged
    {
        private readonly Device _device;
        private readonly IBerryApiService<Device> _deviceApiService;

        public ExpandedDeviceViewModel(Device device, IBerryApiService<Device> deviceApiService)
        {
            _device = device;
            _deviceApiService = deviceApiService;
        }

        public string Title => _device.DeviceName;


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
