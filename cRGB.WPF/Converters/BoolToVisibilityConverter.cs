#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace cRGB.WPF.Converters
{
    /* Usage:
     
        <Cntrl.Resources>
            <local:BoolToVisibilityConverter
                x:Key="BoolToHiddenConverter"
                TrueValue="Visible" FalseValue="Hidden" />
        </Cntrl.Resources>

        <Foo Visibility="{Binding IsItFridayAlready, 
                          Converter={StaticResource BoolToHiddenConverter}}" />
     */
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public sealed class BoolToVisibilityConverter : IValueConverter
    {
        public Visibility TrueValue { get; set; }
        public Visibility FalseValue { get; set; }

        public BoolToVisibilityConverter()
        {
            // set defaults
            TrueValue = Visibility.Visible;
            FalseValue = Visibility.Collapsed;
        }

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                return null;
            return (bool)value ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (Equals(value, TrueValue))
                return true;
            if (Equals(value, FalseValue))
                return false;
            return null;
        }
    }
}