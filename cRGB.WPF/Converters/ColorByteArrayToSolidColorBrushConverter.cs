using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace cRGB.WPF.Converters
{
    public class ColorByteArrayToSolidColorBrushConverter :  IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(value is byte[] arr) ? null : new SolidColorBrush(Color.FromArgb(255, arr[0], arr[1], arr[2]));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is SolidColorBrush brush))
                return null;
            var bytes = new byte[3];
            bytes[0] = brush.Color.R;
            bytes[1] = brush.Color.G;
            bytes[2] = brush.Color.B;
            return bytes;
        }
    }
}
