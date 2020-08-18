using System.Windows.Controls;
using Zadatak_1.Models;
using Zadatak_1.ViewModels;
using System.Windows;

namespace Zadatak_1.Views
{
    /// <summary>
    /// Interaction logic for EmployeeAbsencesView.xaml
    /// </summary>
    public partial class EmployeeAbsencesView : UserControl
    {
        public EmployeeAbsencesView(vwEmployee employee)
        {
            InitializeComponent();
            this.Name = "Absences";            
            this.DataContext = new EmployeeAbsencesViewModel(this, employee);
            btnApprove.Visibility = Visibility.Collapsed;
            btnReject.Visibility = Visibility.Collapsed;
            btnDelete.Visibility = Visibility.Collapsed;
            columEmployee.Visibility = Visibility.Collapsed;
        }

        public EmployeeAbsencesView(vwManager manager)
        {
            InitializeComponent();
            this.Name = "Absences";
            this.DataContext = new EmployeeAbsencesViewModel(this, manager);
            btnAdd.Visibility = Visibility.Collapsed;
        }
    }
}
