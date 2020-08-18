using System;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class ReasonForDeletingViewModel : BaseViewModel
    {
        ReasonForDeletingView absenceView;
        Absences absences = new Absences();

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

        public ReasonForDeletingViewModel(ReasonForDeletingView absenceView, vwAbsence absence)
        {
            this.absenceView = absenceView;            
            Absence = absence;
        }

        public void SaveExecute()
        {
            if (String.IsNullOrEmpty(Absence.ReasonForRejection))
            {
                MessageBox.Show("Please fill field.", "Notification");
            }
            else
            {
                try
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this request?", "Confirmation", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        bool isDeleted = absences.DeleteRequest(Absence);

                        if (isDeleted == true)
                        {
                            MessageBox.Show("Request is deleted.", "Notification", MessageBoxButton.OK);
                            absenceView.Close();
                        }
                        else
                        {
                            MessageBox.Show("Request cannot be deleted.", "Notification", MessageBoxButton.OK);
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