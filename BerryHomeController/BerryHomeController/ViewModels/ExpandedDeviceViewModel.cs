using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using BerryHomeController.Common.Models;
using BerryHomeController.Common.Services;

namespace BerryHomeController.Common.ViewModels
{
    internal class ExpandedDeviceViewModel : ViewModelBase
    {
        private readonly Device _device;

        public ExpandedDeviceViewModel(Device device)
        {
            _device = device;
        }

        public string Title => _device.DeviceName;
        public Device Device => _device;
    }
}
