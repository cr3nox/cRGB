using System.Globalization;
using System.Windows.Controls;
using Caliburn.Micro;
using cRGB.WPF.Helpers;

namespace cRGB.WPF.Validators
{
    public class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return string.IsNullOrWhiteSpace((value ?? "").ToString())
                ? new ValidationResult(false, IoC.Get<ILocalizationHelper>().GetByKey("FieldRequired"))
                : ValidationResult.ValidResult;
        }
    }
}
