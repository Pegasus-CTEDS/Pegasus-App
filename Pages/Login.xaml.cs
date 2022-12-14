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
using Pegasus_App.Models;
using PegasusPacket;

namespace Pegasus_App.Pages
{
    /// <summary>
    /// Interação lógica para Login.xam
    /// </summary>
    public partial class Login : Page
    {

        ClientConnection conn;

        public Login(ClientConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Packet packetIn = conn.RequestLogin(UsernameInput.Text, PasswordInput.Password);
            if(packetIn.RequestStatus == Packet.Status.Success)
            {
                UserSpace us;
                User.Username = UsernameInput.Text;
                us = new UserSpace(conn);
                
                us.Show();
            } 
            else
            {
                MessageBox.Show(packetIn.ServerResponseMessage);
            }
        }
    }
}
