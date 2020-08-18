using System.Windows.Controls;
using Zadatak_1.ViewModels;

namespace Zadatak_1.Views
{
    /// <summary>
    /// Interaction logic for ManagersView.xaml
    /// </summary>
    public partial class ManagersView : UserControl
    {
        public ManagersView()
        {
            InitializeComponent();
            this.Name = "Managers";
            this.DataContext = new ManagersViewModel(this);
        }
    }
}
