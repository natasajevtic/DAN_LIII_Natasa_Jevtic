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

        public EmployeeAbsencesViewModel(EmployeeAbsencesView absencesView, vwEmployee employee)
        {
            this.absencesView = absencesView;
            Employee = employee;
            AbsencesList = absences.GetEmployeeRequests(Employee);
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
    }
}
