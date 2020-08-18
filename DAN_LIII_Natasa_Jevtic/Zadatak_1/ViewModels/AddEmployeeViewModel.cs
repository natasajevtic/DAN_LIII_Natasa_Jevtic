using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class AddEmployeeViewModel : BaseViewModel
    {
        AddEmployee employeeView;
        Genders genders = new Genders();
        Engagement engagement = new Engagement();
        Employees employees = new Employees();

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

        private List<string> engagementList;

        public List<string> EngagementList
        {
            get
            {
                return engagementList;
            }
            set
            {
                engagementList = value;
                OnPropertyChanged("EngagementList");
            }
        }

        private List<string> genderList;

        public List<string> GenderList
        {
            get
            {
                return genderList;
            }
            set
            {
                genderList = value;
                OnPropertyChanged("GenderList");
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

        public AddEmployeeViewModel(AddEmployee employeeView)
        {
            this.employeeView = employeeView;
            Employee = new vwEmployee();
            EngagementList = engagement.GetEngagements();
            GenderList = genders.GetGenders();
        }

        public void SaveExecute()
        {
            if (String.IsNullOrEmpty(Employee.NameAndSurname) || String.IsNullOrEmpty(Employee.DateOfBirth.ToString()) || String.IsNullOrEmpty(Employee.Email) || String.IsNullOrEmpty(Employee.Username)
               || String.IsNullOrEmpty(Employee.Password) || String.IsNullOrEmpty(Employee.HotelFloor.ToString()) || String.IsNullOrEmpty(Employee.Engagement) || String.IsNullOrEmpty(Employee.Citizenship)
               || String.IsNullOrEmpty(Employee.Gender) || Employee.DateOfBirth == DateTime.MinValue)
            {
                MessageBox.Show("Please fill all fields.", "Notification");
            }
            else
            {
                try
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to save the employee?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        bool canCreate = employees.CanCreate(employee);
                        if (canCreate == true)
                        {
                            bool isCreated = employees.AddEmployee(Employee);
                            if (isCreated)
                            {
                                MessageBox.Show("Employee is created.", "Notification", MessageBoxButton.OK);
                                employeeView.Close();
                            }
                            else
                            {
                                MessageBox.Show("Employee cannot be created.", "Notification", MessageBoxButton.OK);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Employee cannot be created, because there is no competent manager for this floor.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel creating the employee?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    employeeView.Close();
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

