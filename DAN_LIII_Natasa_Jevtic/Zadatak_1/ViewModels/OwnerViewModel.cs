using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class OwnerViewModel
    {
        OwnerView ownerView;        

        private ICommand logOut;
        public ICommand LogOut
        {
            get
            {
                if (logOut == null)
                {
                    logOut = new RelayCommand(param => LogOutExecute(), param => CanLogOutExecute());
                }
                return logOut;
            }
        }

        public OwnerViewModel(OwnerView ownerView)
        {
            this.ownerView = ownerView;            
        }

        public bool CanLogOutExecute()
        {
            return true;
        }

        public void LogOutExecute()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to log out?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                ownerView.Close();
            }
        }
    }
}