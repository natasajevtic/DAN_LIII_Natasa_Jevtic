using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Views;
using Zadatak_1.Helper;
using System.ComponentModel;
using System.Threading;

namespace Zadatak_1.ViewModels
{
    class EmployeesViewModel : BaseViewModel
    {
        EmployeesView employeesView;
        Employees employees = new Employees();
        Managers managers = new Managers();

        BackgroundWorker backgroundWorker = new BackgroundWorker()
        {
            WorkerReportsProgress = true
        };

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

        private int percent;
        public int Percent
        {
            get { return this.percent; }
            set
            {
                this.percent = value;
                OnPropertyChanged("Percent");
            }
        }
        
        private string message;
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
                OnPropertyChanged("Message");
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

        private ICommand defineSalaryAll;
        public ICommand DefineSalaryAll
        {
            get
            {
                if (defineSalaryAll == null)
                {
                    defineSalaryAll = new RelayCommand(param => DefineSalaryAllExecute(), param => CanDefineSalaryAllExecute());
                }
                return defineSalaryAll;
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
            backgroundWorker.DoWork += BW_DoWork;
            //adding method to ProgressChanged event
            backgroundWorker.ProgressChanged += BW_ProgressChanged;
            //adding method to RunWorkerCompleted event
            backgroundWorker.RunWorkerCompleted += BW_RunWorkerCompleted;
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

        public void DefineSalaryAllExecute()
        {
            if (String.IsNullOrEmpty(Addition.ToString()) || Addition == 0)
            {
                MessageBox.Show("Please fill field for addition.", "Notification");
            }
            else
            {

                try
                {
                    if (EmployeeList != null)
                    {
                        MessageBoxResult result = MessageBox.Show("Are you sure you want to define salary?", "Confirmation", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            if (backgroundWorker.IsBusy)
                            {
                                //id condition is true, display notification to user
                                MessageBox.Show("Defining is in progress, please wait.");
                            }
                            //if condition is not true, runs printing
                            else
                            {
                                backgroundWorker.RunWorkerAsync();
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

        public bool CanDefineSalaryAllExecute()
        {
            return true;
        }
        /// <summary>
        /// This method performs calculating salary.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">DoWorkEventArgs object.</param>
        public void BW_DoWork(object sender, DoWorkEventArgs e)
        {
            double count = EmployeeList.Count;
            double result = 100 / count;
            int i = 1;
            Random r = new Random();
            foreach (var employee in EmployeeList)
            {
                employee.Salary = CalculateSalary.CalculateForOne(Manager, employee, Addition);
                bool isDefine = employees.SetSalary(employee);
                if (isDefine == true)
                {
                    Thread.Sleep(r.Next(250,500));
                    backgroundWorker.ReportProgress(Convert.ToInt32(result*i++));                    
                }                
            }            
        }
        /// <summary>
        /// This method updates user interface element with the progress made so far.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">ProgressChangedEventArgs object.</param>
        public void BW_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //setting value of user interface elements
            Percent = e.ProgressPercentage;
            Message = e.ProgressPercentage.ToString() + "%";
        }
        /// <summary>
        /// This method update user interface element to employee list.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">RunWorkerCompletedEventArgs object.</param>
        public void BW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Message = e.Error.Message.ToString();
            }
            //if defining successfully finished
            else
            {
                Message = "Defining salary completed.";
            }
            EmployeeList = managers.GetEmployees(Manager);
        }
    }
}
