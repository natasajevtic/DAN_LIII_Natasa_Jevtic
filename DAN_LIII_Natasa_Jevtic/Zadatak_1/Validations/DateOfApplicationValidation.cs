using System;
using System.Globalization;
using System.Windows.Controls;

namespace Zadatak_1.Validations
{
    class DateOfApplicationValidation : ValidationRule
    {
        /// <summary>
        /// This method checks if user input for date of application for absence valid.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns>ValidationResult.</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string date = value as string;
            try
            {
                DateTime conversion = DateTime.ParseExact(date, "M/d/yyyy", CultureInfo.InvariantCulture);
                DateTime d = DateTime.Today.AddDays(20);
                if (conversion < DateTime.Today)
                {
                    return new ValidationResult(false, "Cannot be in the past.");
                }
                if (conversion < d)
                {
                    return new ValidationResult(false, "For this day you had to apply 20 days earlier.");
                }
                else
                {
                    return new ValidationResult(true, null);
                }
            }
            //if cannot convert to DateTime, because  doesn't contain a valid date
            catch (Exception)
            {
                return new ValidationResult(false, "Invalid date. Format: M/d/yyyy");
            }
        }
    }
}
