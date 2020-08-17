﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Zadatak_1.ViewModels;

namespace Zadatak_1.Views
{
    /// <summary>
    /// Interaction logic for UserControlMenuItem.xaml
    /// </summary>
    public partial class UserControlMenuItem : UserControl
    {
        OwnerView _context;        

        private bool mouseClicked;

        public UserControlMenuItem(ItemMenu itemMenu, OwnerView context)
        {
            InitializeComponent();
            _context = context;

            ExpanderMenu.Visibility = itemMenu.SubItems == null ? Visibility.Collapsed : Visibility.Visible;
            ListViewItemMenu.Visibility = itemMenu.SubItems == null ? Visibility.Visible : Visibility.Collapsed;

            this.DataContext = itemMenu;
        }           

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mouseClicked)
            {
                if (e.AddedItems.Count > 0)
                {
                    if (_context != null)
                    {
                        _context.SwitchScreen(((SubItem)((ListView)sender).SelectedItem).Screen);
                    }                    
                }
            }
        }
        private void ListViewMenu_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseClicked = true;

            if (ListViewMenu.SelectedItem != null)
            {
                if (_context != null)
                {
                    _context.SwitchScreen(((SubItem)((ListView)sender).SelectedItem).Screen);
                }                
            }
        }
    }
}