using System;
using System.Globalization;
using BerryHomeController.Common.Models;
using Xamarin.Forms;

namespace BerryHomeController.Common.Converters
{
    public class DeviceTypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var choice = (DeviceType) value;
            switch (choice)
            {
                case DeviceType.Input:
                    return "Input";
                case DeviceType.Output:
                    return "Output";
                case DeviceType.PWM:
                    return "PWM";
                default:
                    return "Undefined";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var choice = ((string) value).ToLower();
            switch (choice)
            {
                case "input":
                    return DeviceType.Input;
                case "output":
                    return DeviceType.Output;
                case "pwm":
                    return DeviceType.PWM;
                default:
                    return null;
            }
        }
    }
}
