using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Zadatak_1.Models
{
    class Absences
    {
        /// <summary>
        /// This method adds absence to DbSet and then save changes to database.
        /// </summary>
        /// <param name="absence">Absence.</param>
        /// <returns>True if added, false if not.</returns>
        public bool AddAbsence(vwAbsence absence)
        {
            try
            {
                using (HotelEntities context = new HotelEntities())
                {
                    tblAbsence newAbsence = new tblAbsence
                    {
                        FirstDay = absence.FirstDay,
                        LastDay = absence.LastDay,
                        Status = "on hold",
                        Reason = absence.Reason,
                        UserId = absence.UserId
                    };
                    context.tblAbsences.Add(newAbsence);
                    context.SaveChanges();                    
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }
        /// <summary>
        /// This method creates a list of data from view of all absences of employee.
        /// </summary>
        /// <returns>List of absences.</returns>
        public List<vwAbsence> GetEmployeeRequests(vwEmployee employee)
        {
            try
            {
                using (HotelEntities context = new HotelEntities())
                {
                    return context.vwAbsences.Where(x => x.UserId == employee.UserId).ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
    }
}
