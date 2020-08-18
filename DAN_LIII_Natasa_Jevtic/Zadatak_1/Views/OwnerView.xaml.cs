using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Zadatak_1.ViewModels;

namespace Zadatak_1.Views
{
    /// <summary>
    /// Interaction logic for MasterView.xaml
    /// </summary>
    public partial class OwnerView : Window
    {
        public OwnerView()
        {
            InitializeComponent();            
            this.DataContext = new OwnerViewModel(this);

            var menuManagers = new List<SubItem>();
            menuManagers.Add(new SubItem("View all managers", new ManagersView()));
            var item1 = new ItemMenu("Managers", menuManagers, PackIconKind.PeopleGroupOutline);

            var menuEmployee = new List<SubItem>();
            menuEmployee.Add(new SubItem("View all emloyees", new EmployeesView()));
            var item2 = new ItemMenu("Employees", menuEmployee, PackIconKind.PeopleGroupOutline);

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

                if (screen.Name == "Managers")
                {
                    ManagersView managersView = new ManagersView();
                }
                else if (screen.Name == "Employees")
                {
                    EmployeesView employeesView = new EmployeesView();
                }
            }
        }
    }
}