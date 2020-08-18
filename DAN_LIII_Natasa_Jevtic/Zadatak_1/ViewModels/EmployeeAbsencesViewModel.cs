using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class EmployeeAbsencesViewModel : BaseViewModel
    {
        EmployeeAbsencesView absencesView;
        Absences absences = new Absences();
        Managers managers = new Managers();

        private vwEmployee employee;

        public vwEmployee Employee
        {
            get
            {
                return employee;
            }
            set
            {
                employee = value;
                OnPropertyChanged("Employee");
            }
        }

        private vwManager manager;

        public vwManager Manager
        {
            get
            {
                return manager;
            }
            set
            {
                manager = value;
                OnPropertyChanged("Manager");
            }
        }

        private vwAbsence absence;

        public vwAbsence Absence
        {
            get
            {
                return absence;
            }
            set
            {
                absence = value;
                OnPropertyChanged("Absence");
            }
        }

        private List<vwAbsence> absencesList;

        public List<vwAbsence> AbsencesList
        {
            get
            {
                return absencesList;
            }
            set
            {
                absencesList = value;
                OnPropertyChanged("AbsencesList");
            }
        }

        private ICommand add;
        public ICommand Add
        {
            get
            {
                if (add == null)
                {
                    add = new RelayCommand(param => AddExecute(), param => CanAddExecute());
                }
                return add;
            }
        }

        private ICommand delete;
        public ICommand Delete
        {
            get
            {
                if (delete == null)
                {
                    delete = new RelayCommand(param => DeleteRequestExecute(), param => CanDeleteRequestExecute());
                }
                return delete;
            }
        }

        private ICommand approve;
        public ICommand Approve
        {
            get
            {
                if (approve == null)
                {
                    approve = new RelayCommand(param => ApproveRequestExecute(), param => CanApproveRequestExecute());
                }
                return approve;
            }
        }

        private ICommand reject;
        public ICommand Reject
        {
            get
            {
                if (reject == null)
                {
                    reject = new RelayCommand(param => RejectRequestExecute(), param => CanRejectRequestExecute());
                }
                return reject;
            }
        }

        public EmployeeAbsencesViewModel(EmployeeAbsencesView absencesView, vwEmployee employee)
        {
            this.absencesView = absencesView;
            Employee = employee;
            AbsencesList = absences.GetEmployeeRequests(Employee);
        }

        public EmployeeAbsencesViewModel(EmployeeAbsencesView absencesView, vwManager manager)
        {
            this.absencesView = absencesView;
            Manager = manager;
            AbsencesList = managers.GetRequests(Manager);
        }
        /// <summary>
        /// This method invokes method for opening a window for creating request.
        /// </summary>
        public void AddExecute()
        {
            try
            {
                AddAbsenceView form = new AddAbsenceView(Employee);
                form.ShowDialog();
                AbsencesList = absences.GetEmployeeRequests(Employee);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanAddExecute()
        {
            return true;
        }

        public void DeleteRequestExecute()
        {
            try
            {
                if (Absence != null)
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this request?", "Confirmation", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        ReasonForDeletingView form = new ReasonForDeletingView(Absence);
                        form.ShowDialog();
                        AbsencesList = managers.GetRequests(Manager);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanDeleteRequestExecute()
        {
            if (Absence != null)
            {
                if (Absence.Status == "deleted")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public void RejectRequestExecute()
        {
            try
            {
                if (Absence != null)
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to reject this request?", "Confirmation", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        bool isRejected = absences.RejectRequest(Absence);
                        if (isRejected == true)
                        {
                            MessageBox.Show("Request is rejected.", "Notification", MessageBoxButton.OK);
                            AbsencesList = managers.GetRequests(Manager);
                        }
                        else
                        {
                            MessageBox.Show("Request cannot be rejected.", "Notification", MessageBoxButton.OK);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanRejectRequestExecute()
        {
            if (Absence != null)
            {
                if (Absence.Status == "deleted")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public void ApproveRequestExecute()
        {
            try
            {
                if (Absence != null)
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to approve this request?", "Confirmation", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        bool isApproved = absences.ApproveRequest(Absence);
                        if (isApproved == true)
                        {
                            MessageBox.Show("Request is approved.", "Notification", MessageBoxButton.OK);
                            AbsencesList = managers.GetRequests(Manager);
                        }
                        else
                        {
                            MessageBox.Show("Request cannot be approved.", "Notification", MessageBoxButton.OK);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanApproveRequestExecute()
        {
            if (Absence != null)
            {
                if (Absence.Status == "deleted")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
