using System;
using System.Globalization;
using System.Windows.Controls;

namespace Zadatak_1.Validations
{
    class LastDayValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string date = value as string;
            try
            {
                DateTime conversion = DateTime.ParseExact(date, "M/d/yyyy", CultureInfo.InvariantCulture);
                if (this.Wrapper != null)
                {
                    //DateTime conversionFirstDay = DateTime.ParseExact(Wrapper.FirstDay, "M/d/yyyy", CultureInfo.InvariantCulture);
                    //compare with first day of absence
                    if (conversion >= Wrapper.FirstDay)
                    {
                        return new ValidationResult(true, null);
                    }
                    //if user creating
                    else
                    {
                        return new ValidationResult(false, "Last day must be after first day.");
                    }
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
        public Wrapper Wrapper { get; set; }
    }
}
