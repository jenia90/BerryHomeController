using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace BerryHomeController.Common.Converters
{
    public class BoolToObjectConverter<T> : IValueConverter
    {
        public T TrueObject { get; set; }
        public T FalseObject { get; set; }


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool) value ? TrueObject : FalseObject;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
