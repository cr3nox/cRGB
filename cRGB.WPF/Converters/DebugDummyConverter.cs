#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;
using System.Diagnostics;
using System.Windows.Data;

namespace cRGB.WPF.Converters
{
    /*
     * 
	<Window.Resources>
		<self:DebugDummyConverter x:Key="DebugDummyConverter" />
	</Window.Resources>
    
		<TextBlock Text="{Binding Title, diag:PresentationTraceSources.TraceLevel=High}" />
		<TextBlock Text="{Binding Test, ElementName=Hi, Converter={StaticResource DebugDummyConverter}}" />
     */
    public class DebugDummyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Debugger.Break();
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Debugger.Break();
            return value;
        }
    }
}