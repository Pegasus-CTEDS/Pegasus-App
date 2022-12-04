using Pegasus_App.Pages;
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

namespace Pegasus_App
{
    /// <summary>
    /// Lógica interna para UserSpace.xaml
    /// </summary>
    public partial class UserSpace : Window
    {
        public UserSpace()
        {
            InitializeComponent();
            UserSpaceFrame.Content = new Resume();
            //MainWindowFrame.Content = new InitialPage();
        }
    }
}
