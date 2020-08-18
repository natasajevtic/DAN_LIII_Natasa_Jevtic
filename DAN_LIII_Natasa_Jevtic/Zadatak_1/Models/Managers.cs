using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Zadatak_1.Helper;

namespace Zadatak_1.Models
{
    class Managers
    {
        /// <summary>
        /// This method adds managers to DbSet and then save changes to database.
        /// </summary>
        /// <param name="managerToAdd">Manager.</param>
        /// <returns>True if added, false if not.</returns>
        public bool AddManager(vwManager managerToAdd)
        {
            try
            {
                using (HotelEntities context = new HotelEntities())
                {
                    tblUser user = new tblUser
                    {
                        DateOfBirth = managerToAdd.DateOfBirth,
                        Email = managerToAdd.Email,
                        NameAndSurname = managerToAdd.NameAndSurname,
                        Password = Encryption.EncryptPassword(managerToAdd.Password),
                        Username = managerToAdd.Username
                    };
                    context.tblUsers.Add(user);
                    context.SaveChanges();
                    managerToAdd.UserId = user.UserId;
                    tblManager manager = new tblManager
                    {
                        UserId = managerToAdd.UserId,
                        ExperienceWorkingInHotels = managerToAdd.ExperienceWorkingInHotels,
                        HotelFloor = managerToAdd.HotelFloor,
                        ProfessionalQualifications = managerToAdd.ProfessionalQualifications
                    };
                    context.tblManagers.Add(manager);
                    context.SaveChanges();
                    managerToAdd.ManagerId = manager.ManagerId;
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
        /// This method creates a list of data from view of all managers.
        /// </summary>
        /// <returns>List of managers.</returns>
        public List<vwManager> GetAllManagers()
        {
            try
            {
                using (HotelEntities context = new HotelEntities())
                {
                    return context.vwManagers.ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// This method checks if manager exists in database.
        /// </summary>
        /// <returns>True if exists, false if not.</returns>
        public bool CheckIfManagerExists()
        {
            try
            {
                using (HotelEntities context = new HotelEntities())
                {
                    return context.vwManagers.Any();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }
        /// <summary>
        /// This method creates a list of employees of forwarded manager.
        /// </summary>
        /// <param name="manager">manager.</param>
        /// <returns>List of employees.</returns>
        public List<vwEmployee> GetEmployees(vwManager manager)
        {
            try
            {
                using (HotelEntities context = new HotelEntities())
                {
                    return context.vwEmployees.Where(x => x.HotelFloor == manager.HotelFloor).ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// This method creates a list of requests for absence to forwarded manager.
        /// </summary>
        /// <param name="manager">Manager.</param>
        /// <returns>List of requests.</returns>
        public List<vwAbsence> GetRequests(vwManager manager)
        {
            try
            {
                using (HotelEntities context = new HotelEntities())
                {
                    return context.vwAbsences.Where(x => x.HotelFloor == manager.HotelFloor).ToList();
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
