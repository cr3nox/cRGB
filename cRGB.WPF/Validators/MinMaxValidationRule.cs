using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace cRGB.WPF.Validators
{
    public class IntegerMinMaxValidationRule : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public IntegerMinMaxValidationRule(int min, int max)
        {
            Min = min;
            Max = max;
        }
        public IntegerMinMaxValidationRule()
        {
            Min = 0;
            Max = 0;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(value == null)
                return new ValidationResult(false, new ArgumentException("Value is null"));

            if (!int.TryParse(value.ToString(), out var val))
                return new ValidationResult(false, new ArgumentException("Value is not an Integer"));

            if (val < Min || val > Max)
                return new ValidationResult(false, $"Value {val} is not in Range of {Min} to {Max}!");

            return new ValidationResult(true, null);
        }
    }
}
