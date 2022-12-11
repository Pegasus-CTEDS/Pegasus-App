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
        public Brush background_left_grid;
        public ClientConnection conn;
        public Portfolio(ClientConnection conn)
        {
            this.conn = conn;
            InitializeComponent();
            background_left_grid = MeuPerfilGrid.Background;
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

        private void MeuPerfilGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            MeuPerfilGrid.Background = (Brush)newcolor.ConvertFrom("#1163ada8");
        }

        private void MeuPerfilGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            MeuPerfilGrid.Background = background_left_grid;
        }

        private void MeuPerfilGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Portfolio(conn));
        }

        private void MinhaCarteiraGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            MinhaCarteiraGrid.Background = (Brush)newcolor.ConvertFrom("#1163ada8");
        }

        private void MinhaCarteiraGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            MinhaCarteiraGrid.Background = background_left_grid;
        }

        private void MinhaCarteiraGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Wallet(conn));
        }

        private void MeusInvestimentosGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            MeusInvestimentosGrid.Background = (Brush)newcolor.ConvertFrom("#1163ada8");
        }

        private void MeusInvestimentosGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            MeusInvestimentosGrid.Background = background_left_grid;
        }

        private void MeusInvestimentosGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void AcoesGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            AcoesGrid.Background = (Brush)newcolor.ConvertFrom("#1163ada8");
        }

        private void AcoesGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            AcoesGrid.Background = background_left_grid;
        }

        private void AcoesGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ConfiguracoesGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            ConfiguracoesGrid.Background = (Brush)newcolor.ConvertFrom("#1163ada8");
        }

        private void ConfiguracoesGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            ConfiguracoesGrid.Background = background_left_grid;
        }

        private void ConfiguracoesGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
