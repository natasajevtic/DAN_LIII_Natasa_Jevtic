using System;
using System.Globalization;
using System.Windows.Controls;

namespace Zadatak_1.Validations
{
    class AdditionValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string number = value as string;
            if (!Int32.TryParse(number, out int addition))
            {
                return new ValidationResult(false, "Please enter a number.");
            }
            else if (addition <= 1 || addition >= 1000)
            {
                return new ValidationResult(false, "Value must be between 1 and 1000.");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
