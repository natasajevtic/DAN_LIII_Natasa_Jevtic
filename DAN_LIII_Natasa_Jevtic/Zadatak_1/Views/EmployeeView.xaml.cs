using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Zadatak_1.Models;
using Zadatak_1.ViewModels;

namespace Zadatak_1.Views
{
    /// <summary>
    /// Interaction logic for EmployeeView.xaml
    /// </summary>
    public partial class EmployeeView : Window
    {
        public vwEmployee Employee { get; set; }
        public EmployeeView(vwEmployee employee)
        {
            InitializeComponent();
            Employee = employee;
            this.DataContext = new EmployeeViewModel(this, employee);

            var menuAbsences = new List<SubItem>();
            menuAbsences.Add(new SubItem("View all requests", new EmployeeAbsencesView(employee)));
            var item1 = new ItemMenu("Absences", menuAbsences, PackIconKind.PeopleGroupOutline);           

            var item0 = new ItemMenu("", new UserControl(), PackIconKind.ViewDashboard);

            Menu.Children.Add(new UserControlMenuItem(item0, this));
            Menu.Children.Add(new UserControlMenuItem(item1, this));
        }

        public void SwitchScreen(object sender)
        {
            var screen = ((UserControl)sender);
            if (screen != null)
            {
                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(screen);

                if (screen.Name == "Absences")
                {
                    EmployeeAbsencesView absencesView = new EmployeeAbsencesView(Employee);
                }                
            }
        }
    }
}
