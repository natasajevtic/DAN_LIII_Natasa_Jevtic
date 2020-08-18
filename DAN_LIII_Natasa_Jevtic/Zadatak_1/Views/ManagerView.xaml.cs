using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Zadatak_1.Models;
using Zadatak_1.ViewModels;

namespace Zadatak_1.Views
{
    /// <summary>
    /// Interaction logic for ManagerView.xaml
    /// </summary>
    public partial class ManagerView : Window
    {
        public vwManager Manager { get; set; }
        public ManagerView(vwManager manager)
        {
            InitializeComponent();
            Manager = manager;
            this.DataContext = new ManagerViewModel(this, manager);

            var menuAbsences = new List<SubItem>();
            menuAbsences.Add(new SubItem("View all requests", new EmployeeAbsencesView(manager)));
            var item1 = new ItemMenu("Absences", menuAbsences, PackIconKind.PeopleGroupOutline);

            var menuEmployees = new List<SubItem>();
            menuEmployees.Add(new SubItem("View all employees", new EmployeesView(manager)));
            var item2 = new ItemMenu("Employees", menuEmployees, PackIconKind.PeopleGroupOutline);

            var item0 = new ItemMenu("", new UserControl(), PackIconKind.ViewDashboard);

            Menu.Children.Add(new UserControlMenuItem(item0, this));
            Menu.Children.Add(new UserControlMenuItem(item1, this));
            Menu.Children.Add(new UserControlMenuItem(item2, this));
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
                    EmployeeAbsencesView absencesView = new EmployeeAbsencesView(Manager);
                }
                else if (screen.Name == "Employees")
                {
                    EmployeesView employeesView = new EmployeesView(Manager);
                }
            }
        }
    }
}
