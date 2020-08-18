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
        /// <summary>
        /// This method remove request from DbSet and save changes to database.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>True if deleted, false if not.</returns>
        public bool DeleteRequest(vwAbsence request)
        {
            try
            {
                using (HotelEntities context = new HotelEntities())
                {
                    tblAbsence requestToDelete = context.tblAbsences.Where(x => x.AbsenceId == request.AbsenceId).FirstOrDefault();
                    requestToDelete.Status = "deleted";
                    requestToDelete.ReasonForRejection = request.ReasonForRejection;
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
        /// This method changes status of absence to rejected.
        /// </summary>
        /// <param name="request">Request to be rejected.</param>
        /// <returns>True if rejected, false if not.</returns>
        public bool RejectRequest(vwAbsence request)
        {
            try
            {
                using (HotelEntities context = new HotelEntities())
                {
                    tblAbsence requestToReject = context.tblAbsences.Where(x => x.AbsenceId == request.AbsenceId).FirstOrDefault();
                    requestToReject.Status = "rejected";
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
        /// This method changes status of absence to approved.
        /// </summary>
        /// <param name="request">Request to be approved.</param>
        /// <returns>True if approved, false if not.</returns>
        public bool ApproveRequest(vwAbsence request)
        {
            try
            {
                using (HotelEntities context = new HotelEntities())
                {
                    tblAbsence requestToApprove = context.tblAbsences.Where(x => x.AbsenceId == request.AbsenceId).FirstOrDefault();
                    requestToApprove.Status = "approved";
                    context.SaveChanges();                    
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
    }
}