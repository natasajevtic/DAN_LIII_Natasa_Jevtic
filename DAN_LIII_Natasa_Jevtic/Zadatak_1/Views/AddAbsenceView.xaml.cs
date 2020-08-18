using System.Windows;
using Zadatak_1.Models;
using Zadatak_1.ViewModels;

namespace Zadatak_1.Views
{
    /// <summary>
    /// Interaction logic for AddAbsenceView.xaml
    /// </summary>
    public partial class AddAbsenceView : Window
    {
        public AddAbsenceView(vwEmployee employee)
        {
            InitializeComponent();
            this.DataContext = new AddAbsenceViewModel(this, employee);
        }
    }
}
