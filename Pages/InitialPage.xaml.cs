﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pegasus_App.Pages
{
    /// <summary>
    /// Interação lógica para InitialPage.xam
    /// </summary>
    public partial class InitialPage : Page
    {
        public InitialPage()
        {
            InitializeComponent();
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Login());
        }

        private void RegisterButtonClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Register());
        }
    }
}