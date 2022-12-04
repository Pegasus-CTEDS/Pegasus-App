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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pegasus_App.Pages
{
    /// <summary>
    /// Interação lógica para Register.xam
    /// </summary>
    public partial class Register : Page
    {
        public Register()
        {
            InitializeComponent();
        }

        private void RegisterButtonClick(object sender, RoutedEventArgs e)
        {
            if (PasswordInput.Password != PasswordConfirmationInput.Password)
            {
                MessageBox.Show("Passwords didn't match");
            } else if (NameInput.Text == "")
            {
                MessageBox.Show("Name cannot be null");
            } else if (EmailInput.Text == "")
            {
                MessageBox.Show("Email cannot be null");
            } else
            {
                RegisterUser(NameInput.Text, EmailInput.Text, PasswordInput.Password);
                
            }
        }
        private void RegisterUser(string name, string email, string password)
        {
            bool sucess = true;
            if (sucess)
            {
                MessageBox.Show("User registered with sucess!");
                this.NavigationService.Navigate(new Login());
            } else {
                MessageBox.Show("User registration fail!");
            }
            
        }
    }
}
