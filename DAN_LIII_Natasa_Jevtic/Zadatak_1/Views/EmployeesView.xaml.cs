using System.Windows.Controls;
using Zadatak_1.Models;
using Zadatak_1.ViewModels;
using System.Windows;

namespace Zadatak_1.Views
{
    /// <summary>
    /// Interaction logic for EmployeesView.xaml
    /// </summary>
    public partial class EmployeesView : UserControl
    {
        public EmployeesView()
        {
            InitializeComponent();
            this.Name = "Employees";
            this.DataContext = new EmployeesViewModel(this);
            btnDefineSalary.Visibility = Visibility.Collapsed;
            btnDefineSalaryToAll.Visibility = Visibility.Collapsed;
            txtAddition.Visibility = Visibility.Collapsed;
            lblAddition.Visibility = Visibility.Collapsed;
        }
        public EmployeesView(vwManager manager)
        {
            InitializeComponent();
            this.Name = "Managers";
            this.DataContext = new EmployeesViewModel(this, manager);
            btnAdd.Visibility = Visibility.Collapsed;
        }
    }
}
