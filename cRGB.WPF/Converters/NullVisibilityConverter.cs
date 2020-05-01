using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace cRGB.WPF.Converters
{
    public class NullVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// If the bound Object is null give Back Visibility based on Parameter Mode
        /// When Parameter VisibleHidden is passed then this will return Visibility.Visible if the Object is null. (Hidden when not null)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter">VisibleHidden, HiddenVisible, CollapsedVisible, VisibleCollapsed [Default: VisibleHidden]</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility ret = parameter?.ToString() switch
            {
                "VisibleHidden" => value == null ? Visibility.Visible : Visibility.Hidden,
                "HiddenVisible" => value == null ? Visibility.Hidden : Visibility.Visible,
                "CollapsedVisible" => value == null ? Visibility.Collapsed : Visibility.Visible,
                "VisibleCollapsed" => value == null ? Visibility.Visible : Visibility.Collapsed,
                _ => value == null ? Visibility.Visible : Visibility.Hidden
            };
            // for debugging
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
