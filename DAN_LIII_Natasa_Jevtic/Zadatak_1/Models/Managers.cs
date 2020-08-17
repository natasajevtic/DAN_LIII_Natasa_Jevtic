using System;
using System.Diagnostics;
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
    }
}
