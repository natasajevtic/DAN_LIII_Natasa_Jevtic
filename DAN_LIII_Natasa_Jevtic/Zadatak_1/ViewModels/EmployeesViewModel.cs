using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class EmployeesViewModel : BaseViewModel
    {
        EmployeesView employeesView;
        Employees employees = new Employees();
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

        private List<vwEmployee> employeeList;

        public List<vwEmployee> EmployeeList
        {
            get
            {
                return employeeList;
            }
            set
            {
                employeeList = value;
                OnPropertyChanged("EmployeeList");
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

        public EmployeesViewModel(EmployeesView employeesView)
        {
            this.employeesView = employeesView;
            EmployeeList = employees.GetAllEmployees();
        }
        /// <summary>
        /// This method invokes method for opening a window for adding employees.
        /// </summary>
        public void AddExecute()
        {
            try
            {
                if (managers.CheckIfManagerExists())
                {
                    AddEmployee form = new AddEmployee();
                    form.ShowDialog();
                    EmployeeList = employees.GetAllEmployees();
                }
                else
                {
                    MessageBox.Show("Please create a manager first.", "Notification.", MessageBoxButton.OK);
                }
                
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
