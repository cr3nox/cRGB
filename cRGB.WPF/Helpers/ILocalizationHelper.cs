using System.Globalization;
using System.Windows;

namespace cRGB.WPF.Helpers
{
    public interface ILocalizationHelper
    {
        public ResourceDictionary ResourceDictionary { get; set; }
        public string GetByKey(string key);
        public CultureInfo Culture { get; set; }
    }
}
