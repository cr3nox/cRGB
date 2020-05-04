using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace cRGB.WPF.Converters
{
    public class CommaSeparatedToIntegerListConverter : IValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter">The Split Char, default is ','</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value?.ToString();
            if (string.IsNullOrEmpty(str))
                return null;
            var strings = str.Split(parameter?.ToString() == null ? ',' : parameter.ToString().ToCharArray()[0]);
            var retList = new List<int>();
            foreach (var s in strings)
            {
                if(!int.TryParse(s, out var i))
                    throw new ArgumentException($"Element {s} could not be parsed to int");
                retList.Add(i);
            }

            return retList;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is List<int> list))
                return null;
            var character = ',';
            if (parameter is char c)
                character = c;

            var ret = list.Aggregate("", (current, i) => current + (i.ToString() + character));

            if (ret.EndsWith(character))
                ret = ret.Substring(ret.Length - 1, 1);
            return ret;
        }
    }
}
