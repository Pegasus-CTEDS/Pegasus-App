using PegasusPacket;
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
using System.Data;
using System.Runtime.CompilerServices;
using System.Net.Sockets;

namespace Pegasus_App.Pages
{
    /// <summary>
    /// </summary>
    public partial class Operations : Page
    {
        public ClientConnection conn;
        public Operations(ClientConnection conn)
        {
            this.conn = conn;
            InitializeComponent();
        }

        public void CreateOperation_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = Convert.ToDateTime(DateInput.Text);
            double price = Convert.ToDouble(PriceInput.Text);
            int quantity = Convert.ToInt32(QuantityInput.Text);
            Packet? packetIn;
            if (TypeInput.Text.ToLower() == "compra")
            {
                packetIn = conn.RequestBuyOperation(NameInput.Text, date, price, quantity);
            }
            else
            {
                packetIn = conn.RequestSellOperation(NameInput.Text, date, price, quantity);
            }
            if (packetIn != null && packetIn.Type != Packet.PacketType.Undefined) 
                MessageBox.Show(packetIn.ServerResponseMessage);
        }
    }
}
