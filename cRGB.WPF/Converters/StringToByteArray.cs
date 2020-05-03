using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace cRGB.WPF.Converters
{
    public class StringToByteArray :  IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string;
            if (string.IsNullOrEmpty(str))
                return null;
            var enc = new ASCIIEncoding();
            return enc.GetBytes(str);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is byte[] arr))
                return null;
            var enc = new ASCIIEncoding();
            return enc.GetString(arr);
        }
    }
}
