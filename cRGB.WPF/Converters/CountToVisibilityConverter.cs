#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Serilog;

namespace cRGB.WPF.Converters
{
    public class CountToVisibilityConverter : IValueConverter
    {
        public Visibility TrueValue { get; set; }
        public Visibility FalseValue { get; set; }

        public CountToVisibilityConverter()
        {
            // set defaults
            TrueValue = Visibility.Visible;
            FalseValue = Visibility.Collapsed;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && (value is int i && i > 0) ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}