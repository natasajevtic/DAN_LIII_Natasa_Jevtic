using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
                    EmployeeAbsencesView absencesView = new EmployeeAbsencesView(Manager);
                }
            }
        }
    }
}
