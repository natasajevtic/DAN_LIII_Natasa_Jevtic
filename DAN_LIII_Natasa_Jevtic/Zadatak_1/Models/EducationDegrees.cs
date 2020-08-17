using System.Collections.Generic;
using System.Linq;

namespace Zadatak_1.Models
{
    class EducationDegrees
    {
        Dictionary<string, int> levels = new Dictionary<string, int>()
        {
            {"I",1 },
            {"II", 2 },
            { "III", 3 },
            { "IV", 4 },
            { "V", 5 },
            { "VI", 6 },
            { "VII", 7 }
        };
        /// <summary>
        /// This method created collection of education degrees.
        /// </summary>
        /// <returns>List of education degrees.</returns>
        public List<string> GetEducationDegree()
        {            
            return levels.Keys.ToList();
        }       
    }
}
