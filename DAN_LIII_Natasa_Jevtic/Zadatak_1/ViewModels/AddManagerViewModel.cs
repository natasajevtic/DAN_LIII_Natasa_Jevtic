using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class AddManagerViewModel : BaseViewModel
    {
        AddManagerView managerView;
        EducationDegrees levels = new EducationDegrees();
        Managers managers = new Managers();

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

        private List<string> educationDegreeList;

        public List<string> EducationDegreeList
        {
            get
            {
                return educationDegreeList;
            }
            set
            {
                educationDegreeList = value;
                OnPropertyChanged("EducationDegreeList");
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

        public AddManagerViewModel(AddManagerView managerView)
        {
            this.managerView = managerView;
            Manager = new vwManager();
            EducationDegreeList = levels.GetEducationDegree();
        }

        public void SaveExecute()
        {
            if (String.IsNullOrEmpty(Manager.NameAndSurname) || String.IsNullOrEmpty(Manager.DateOfBirth.ToString()) || String.IsNullOrEmpty(Manager.Email) || String.IsNullOrEmpty(Manager.Username)
               || String.IsNullOrEmpty(Manager.Password) || String.IsNullOrEmpty(Manager.HotelFloor.ToString()) || String.IsNullOrEmpty(Manager.ExperienceWorkingInHotels.ToString()) || String.IsNullOrEmpty(Manager.ProfessionalQualifications)
               || Manager.DateOfBirth == DateTime.MinValue)
            {
                MessageBox.Show("Please fill all fields.", "Notification");
            }
            else
            {
                try
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to save the manager?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        bool isCreated = managers.AddManager(Manager);
                        if (isCreated)
                        {
                            MessageBox.Show("Manager is created.", "Notification", MessageBoxButton.OK);
                            managerView.Close();
                        }
                        else
                        {
                            MessageBox.Show("Manager cannot be created.", "Notification", MessageBoxButton.OK);
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
                MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel creating the manager?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    managerView.Close();
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
