using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;

namespace cRGB.WPF.Helpers
{
    public class LocalizationHelper : ILocalizationHelper
    {
        public ResourceDictionary ResourceDictionary { get; set; }
        public LocalizationHelper()
        {
            ResourceDictionary = new ResourceDictionary
            {
                Source =
                new Uri("Localization/Translations/Strings.en-GB.xaml",
                    UriKind.RelativeOrAbsolute)
            };
        }

        public string GetByKey(string key)
        {
            return ResourceDictionary[key].ToString();
        }

        public CultureInfo Culture { get; set; }
    }

}
