using Zadatak_1.Models;

namespace Zadatak_1.Helper
{
    class CalculateSalary
    {
        public static decimal CalculateForOne(vwManager manager, vwEmployee employee, int addition)
        {
            EducationDegrees degree = new EducationDegrees();
            decimal i = 0.75M * manager.ExperienceWorkingInHotels;
            decimal s = 0.15M * degree.levels[manager.ProfessionalQualifications];
            decimal p = 0;
            if (employee.Gender == "M")
            {
                p = 1.12M;
            }
            else
            {
                p = 1.15M;
            }
            return 1000 * i * s * p + addition;
        }
    }
}
