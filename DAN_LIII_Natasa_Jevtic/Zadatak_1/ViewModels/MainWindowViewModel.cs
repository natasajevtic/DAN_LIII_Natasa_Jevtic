using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class MainWindowViewModel : BaseViewModel
    {
        MainWindow main;
        Users users = new Users();
        readonly string source = @"../../OwnerAccess.txt";
        string OwnerUsername { get; set; }
        string OwnerPassword { get; set; }
        public vwManager Manager { get; set; }

        private string username;

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }
        private string password;

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        private ICommand logIn;

        public ICommand LogIn
        {
            get
            {
                if (logIn == null)
                {
                    logIn = new RelayCommand(LogInExecute, CanLogInExecute);
                }
                return logIn;
            }
        }
        public MainWindowViewModel(MainWindow main)
        {
            this.main = main;
            GetOwnerUsernameAndPassword();
        }
        /// <summary>
        /// This method checks if username and password valid.
        /// </summary>
        /// <param name="password">User input for password.</param>
        public void LogInExecute(object password)
        {
            Password = (password as PasswordBox).Password;
            if (Username == OwnerUsername && Password == OwnerPassword)
            {
                OwnerView ownerView = new OwnerView();
                ownerView.ShowDialog();
            }
            else if (users.FindManager(Username, Password) != null)
            {
                Manager = users.FindManager(Username, Password);
                ManagerView managerView = new ManagerView();
                managerView.ShowDialog();

            }
            else if (users.FindEmployee(Username, Password) != null)
            {
                EmployeeView employeeView = new EmployeeView();
                employeeView.ShowDialog();
            }
            else
            {
                MessageBox.Show("Wrong username or password. Please, try again.", "Notification");
            }
        }
        /// <summary>
        /// This method ensures that the login can only be executed when the login fields are not empty.
        /// </summary>
        /// <param name="password">User input for password.</param>
        /// <returns>True if login can execute, false if not.</returns>
        public bool CanLogInExecute(object password)
        {
            Password = (password as PasswordBox).Password;
            if (!String.IsNullOrEmpty(Username) && !String.IsNullOrEmpty(Password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// This method reads username and password of owner from file.
        /// </summary>
        public void GetOwnerUsernameAndPassword()
        {
            if (File.Exists(source))
            {
                string[] lines = File.ReadAllLines(source);
                string[] firstLine = lines[0].Split(':');
                OwnerUsername = firstLine[1];
                string[] secondLine = lines[1].Split(':');
                OwnerPassword = secondLine[1];
            }
        }
    }
}
