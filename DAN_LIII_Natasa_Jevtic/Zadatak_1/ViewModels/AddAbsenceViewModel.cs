using System;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class AddAbsenceViewModel : BaseViewModel
    {
        AddAbsenceView absenceView;
        Employees employees = new Employees();
        Absences absences = new Absences();

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

        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }

        private ICommand cancel;
        public ICommand Cancel
        {
            get
            {
                if (cancel == null)
                {
                    cancel = new RelayCommand(param => CancelExecute(), param => CanCancelExecute());
                }
                return cancel;
            }
        }

        public AddAbsenceViewModel(AddAbsenceView absenceView, vwEmployee employee)
        {
            this.absenceView = absenceView;
            Employee = employee;
            Absence = new vwAbsence();
            Absence.UserId = employee.UserId;
        }

        public void SaveExecute()
        {
            if (String.IsNullOrEmpty(Absence.FirstDay.ToString()) || String.IsNullOrEmpty(Absence.LastDay.ToString()) || String.IsNullOrEmpty(Absence.Reason)
                || Absence.FirstDay == DateTime.MinValue || Absence.LastDay == DateTime.MinValue)
            {
                MessageBox.Show("Please fill all fields.", "Notification");
            }
            else
            {
                try
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to send the request?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        bool isCreated = absences.AddAbsence(Absence);
                        if (isCreated)
                        {
                            MessageBox.Show("Request for absence is sent.", "Notification", MessageBoxButton.OK);
                            absenceView.Close();
                        }
                        else
                        {
                            MessageBox.Show("Request for absence cannot be sent.", "Notification", MessageBoxButton.OK);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        public bool CanSaveExecute()
        {
            return true;
        }

        public void CancelExecute()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    absenceView.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanCancelExecute()
        {
            return true;
        }
    }
}


