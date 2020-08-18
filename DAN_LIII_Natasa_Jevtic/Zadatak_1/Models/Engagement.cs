using System.Collections.Generic;

namespace Zadatak_1.Models
{
    class Engagement
    {
        /// <summary>
        /// This method created list of employee's engagement.
        /// </summary>
        /// <returns>List of engagement.</returns>
        public List<string> GetEngagements()
        {
            return new List<string> { "cleaning", "cooking", "monitoring", "reporting" };
        }
    }
}