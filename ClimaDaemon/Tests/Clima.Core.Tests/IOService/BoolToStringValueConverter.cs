using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Clima.Core.Tests.IOService
{
    public class BoolToStringValueConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tmp = (bool) value;
            if (tmp)
                return "True";
            
            return "False";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}