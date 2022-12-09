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

namespace Pegasus_App.Pages
{
    public partial class Portfolio : Page
    {
        public ClientConnection conn;
        public Portfolio(ClientConnection conn)
        {
            this.conn = conn;
            InitializeComponent();
            LoadPortfolioDataGridView();
        }

        void LoadPortfolioDataGridView()
        {
            DataTable portfolioDataTable = new();
            portfolioDataTable.Columns.Add("Nome");
            portfolioDataTable.Columns.Add("Quantidade");
            portfolioDataTable.Columns.Add("Rendimento");

            List<Packet.RequestedDataType> portfolioData = new List<Packet.RequestedDataType>() { Packet.RequestedDataType.Portfolio };
            Packet packetIn = conn.RequestDataGet(portfolioData);
            foreach (Packet.PortfolioDataField field in packetIn.PortfolioData)
            {
                DataRow row = portfolioDataTable.NewRow();
                row["Código"] = field.Symbol.ToString();
                row["Quantidade"] = field.Quantity.ToString();
                row["Rendimento"] = field.AveragePrice.ToString(); // TODO [0]
                portfolioDataTable.Rows.Add(row);
            }
            // PortfolioGrid.DataContext = portfolioDataTable.DefaultView;
        }

        static void NewBuyOperation_Click(object sender, RoutedEventArgs e)
        {
            
        }

        static void NewSellOperation_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
