using System.Windows;
using Zadatak_1.Models;
using Zadatak_1.ViewModels;

namespace Zadatak_1.Views
{
    /// <summary>
    /// Interaction logic for ReasonForDeletingView.xaml
    /// </summary>
    public partial class ReasonForDeletingView : Window
    {
        public ReasonForDeletingView(vwAbsence absence)
        {
            InitializeComponent();
            this.DataContext = new ReasonForDeletingViewModel(this, absence);
        }
    }
}
