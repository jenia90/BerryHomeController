using System;
using System.Collections.Generic;
using System.Text;

namespace BerryHomeController.Common.ViewModels
{
    public class NewEditDeviceViewModel : ViewModelBase
    {
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
    }
}
