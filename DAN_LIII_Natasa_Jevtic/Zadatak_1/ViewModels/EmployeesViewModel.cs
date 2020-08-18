using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Views;
using Zadatak_1.Helper;

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

        private int addition;

        public int Addition
        {
            get
            {
                return addition;
            }
            set
            {
                addition = value;
                OnPropertyChanged("Addition");
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

        private ICommand defineSalary;
        public ICommand DefineSalary
        {
            get
            {
                if (defineSalary == null)
                {
                    defineSalary = new RelayCommand(param => DefineSalaryExecute(), param => CanDefineSalaryExecute());
                }
                return defineSalary;
            }
        }

        public EmployeesViewModel(EmployeesView employeesView)
        {
            this.employeesView = employeesView;
            EmployeeList = employees.GetAllEmployees();
        }

        public EmployeesViewModel(EmployeesView employeesView, vwManager manager)
        {
            this.employeesView = employeesView;
            Manager = manager;
            EmployeeList = managers.GetEmployees(Manager);
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

        public void DefineSalaryExecute()
        {
            if (String.IsNullOrEmpty(Addition.ToString()) || Addition == 0)
            {
                MessageBox.Show("Please fill field for addition.", "Notification");
            }
            else
            {

                try
                {
                    if (Employee != null)
                    {
                        MessageBoxResult result = MessageBox.Show("Are you sure you want to define salary?", "Confirmation", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            Employee.Salary = CalculateSalary.CalculateForOne(Manager, Employee, Addition);
                            bool isDefine = employees.SetSalary(Employee);
                            if (isDefine == true)
                            {
                                MessageBox.Show("Salary is defined.", "Notification", MessageBoxButton.OK);
                                EmployeeList = managers.GetEmployees(Manager);
                            }
                            else
                            {
                                MessageBox.Show("Salary cannot be defined.", "Notification", MessageBoxButton.OK);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public bool CanDefineSalaryExecute()
        {
            if (Employee != null)
            {
                if (Employee.Engagement == "monitoring" || Employee.Engagement == "reporting")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
