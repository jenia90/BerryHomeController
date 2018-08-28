using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using BerryHomeController.Common.Models;
using Xamarin.Forms;
using Device = BerryHomeController.Common.Models.Device;

namespace BerryHomeController.Common.ViewModels
{
    internal class NewEditDeviceViewModel : ViewModelBase
    {
        private Device _device;
        public NewEditDeviceViewModel()
        {
            _device = new Device() { Type = DeviceType.Output };
        }

        private string _title;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public Device Device
        {
            get => _device;
            set
            {
                _device = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveDeviceCommand => new Command(async () =>
        {
            MessagingCenter.Send(this, "add_device", Device);
            await NavigateBack();
        });
    }
}
