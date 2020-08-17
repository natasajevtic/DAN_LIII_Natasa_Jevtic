using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Zadatak_1.Validations
{
    class ExperienceValidation : ValidationRule
    {
        /// <summary>
        /// This method checks if user enter a positive number or zero for years of experience.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns>ValidationResult.</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string number = value as string;
            if (!Int32.TryParse(number, out int numberOfExperience))
            {
                return new ValidationResult(false, "Please enter a number.");
            }
            else if (numberOfExperience < 0)
            {
                return new ValidationResult(false, "Please enter a positive number.");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
