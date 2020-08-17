using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class ManagersViewModel : BaseViewModel
    {
        ManagersView managersView;
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

        private List<vwManager> managerList;

        public List<vwManager> ManagerList
        {
            get
            {
                return managerList;
            }
            set
            {
                managerList = value;
                OnPropertyChanged("ManagerList");
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

        public ManagersViewModel(ManagersView managersView)
        {
            this.managersView = managersView;
            ManagerList = managers.GetAllManagers();
        }
        /// <summary>
        /// This method invokes method for opening a window for adding manager.
        /// </summary>
        public void AddExecute()
        {
            try
            {
                AddManagerView form = new AddManagerView();
                form.ShowDialog();
                ManagerList = managers.GetAllManagers();
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