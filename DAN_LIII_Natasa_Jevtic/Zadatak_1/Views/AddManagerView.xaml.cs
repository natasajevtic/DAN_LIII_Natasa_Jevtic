using System.Windows;
using Zadatak_1.ViewModels;

namespace Zadatak_1.Views
{
    /// <summary>
    /// Interaction logic for AddManagerView.xaml
    /// </summary>
    public partial class AddManagerView : Window
    {
        public AddManagerView()
        {
            InitializeComponent();
            this.DataContext = new AddManagerViewModel(this);
        }
    }
}
