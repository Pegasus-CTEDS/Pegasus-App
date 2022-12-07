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

using PegasusPacket;
using System.Net.Sockets;

namespace Pegasus_App.Pages
{
    /// <summary>
    /// Interação lógica para Register.xam
    /// </summary>
    public partial class Register : Page
    {

        ClientConnection conn;

        public Register(ClientConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
        }

        private void RegisterButtonClick(object sender, RoutedEventArgs e)
        {
            if (PasswordInput.Password != PasswordConfirmationInput.Password)
            {
                MessageBox.Show("Senhas não são equivalentes.");
            }
            else if (NameInput.Text == "")
            {
                MessageBox.Show("Usuário não foi preenchido.");
            }
            else if (EmailInput.Text == "")
            {
                MessageBox.Show("Email não foi preenchido.");
            }
            else
            {
                RegisterUser(NameInput.Text, EmailInput.Text, PasswordInput.Password);
                
            }
        }
        private void RegisterUser(string username, string email, string password)
        {
            Packet packetIn = conn.RequestSignIn(username, email, password);
            MessageBox.Show(packetIn.ServerResponseMessage);
            if (packetIn.RequestStatus == Packet.Status.Success)
            {
                this.NavigationService.Navigate(new Login(conn));
            }
        }
    }
}
