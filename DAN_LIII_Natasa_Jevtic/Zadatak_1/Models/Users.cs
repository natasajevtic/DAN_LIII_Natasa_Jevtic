using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Zadatak_1.Helper;

namespace Zadatak_1.Models
{
    class Users
    {
        /// <summary>
        /// This methods finds employee based on forwarded username and password.
        /// </summary>
        /// <param name="username">Employee username.</param>
        /// <param name="password">Employee password.</param>
        /// <returns>Employee.</returns>
        public vwEmployee FindEmployee(string username, string password)
        {
            string encryptedPassword = Encryption.EncryptPassword(password);
            try
            {
                using (HotelEntities context = new HotelEntities())
                {
                    return context.vwEmployees.Where(x => x.Username == username && x.Password == encryptedPassword).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// This methods finds manager based on forwarded username and password.
        /// </summary>
        /// <param name="username">Manager username.</param>
        /// <param name="password">Manager password.</param>
        /// <returns>Manager.</returns>
        public vwManager FindManager(string username, string password)
        {
            string encryptedPassword = Encryption.EncryptPassword(password);
            try
            {
                using (HotelEntities context = new HotelEntities())
                {
                    return context.vwManagers.Where(x => x.Username == username && x.Password == encryptedPassword).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }        
        /// <summary>
        /// This method creates a list of data from view of all users.
        /// </summary>
        /// <returns>List of users.</returns>
        public List<vwUser> GetAllUsers()
        {
            try
            {
                using (HotelEntities context = new HotelEntities())
                {
                    return context.vwUsers.ToList();
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
