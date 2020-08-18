using System.Windows.Controls;
using Zadatak_1.Models;
using Zadatak_1.ViewModels;

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
        }
    }
}
