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

        private void ViewMoreInv0_MouseLeave(object sender, MouseEventArgs e)
        {
            //ViewMoreInv0.Source = new BitmapImage(new Uri("/Pages/ViewMoreDeactivatedIcon.png", UriKind.Relative));
        }

        private void ViewMoreInv0_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void NotificationBell0_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void CloseMenuBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(MenuGrid.Visibility == Visibility.Visible)
            {
                // hide the lateral menu
                MenuGrid.Visibility = Visibility.Collapsed;
                ShowArrow.Visibility = Visibility.Visible;
                HideArrow.Visibility = Visibility.Collapsed;
                MainPageContent.Width = 1200;
            } else
            {
                // shows the lateral menu
                MenuGrid.Visibility = Visibility.Visible;
                ShowArrow.Visibility = Visibility.Collapsed;
                HideArrow.Visibility = Visibility.Visible;
                MainPageContent.Width = 900;
            }
        }

        private void CloseMenuBar_MouseEnter(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            CloseMenuBar.Background = (Brush)newcolor.ConvertFrom("#142627cc");
        }

        private void CloseMenuBar_MouseLeave(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            CloseMenuBar.Background = (Brush)newcolor.ConvertFrom("#14262799");
        }

        private void SaldoGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            SaldoGrid.Background = (Brush)newcolor.ConvertFrom("#11FFFFFF");
        }

        private void SaldoGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            SaldoGrid.Background = (Brush)newcolor.ConvertFrom("#1163ada8");
        }

        private void GanhosGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            GanhosGrid.Background = (Brush)newcolor.ConvertFrom("#11FFFFFF");
        }

        private void GanhosGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            GanhosGrid.Background = (Brush)newcolor.ConvertFrom("#1163ada8");
        }

        private void RendimentoGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            RendimentoGrid.Background = (Brush)newcolor.ConvertFrom("#11FFFFFF");
        }

        private void RendimentoGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            RendimentoGrid.Background = (Brush)newcolor.ConvertFrom("#1163ada8");
        }




        private void GridInvestimento0_MouseEnter(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            GridInvestimento0.Background = (Brush)newcolor.ConvertFrom("#11FFFFFF");
        }

        private void GridInvestimento0_MouseLeave(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            GridInvestimento0.Background = (Brush)newcolor.ConvertFrom("#1163ada8");
        }

        private void GridInvestimentos1_MouseEnter(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            GridInvestimentos1.Background = (Brush)newcolor.ConvertFrom("#11FFFFFF");
        }

        private void GridInvestimentos1_MouseLeave(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            GridInvestimentos1.Background = (Brush)newcolor.ConvertFrom("#1163ada8");
        }

        private void GridInvestimentos2_MouseEnter(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            GridInvestimentos2.Background = (Brush)newcolor.ConvertFrom("#11FFFFFF");
        }

        private void GridInvestimentos2_MouseLeave(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            GridInvestimentos2.Background = (Brush)newcolor.ConvertFrom("#1163ada8");
        }

        private void VerCarteira_MouseEnter(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            VerCarteira.Background = (Brush)newcolor.ConvertFrom("#11FFFFFF");
        }

        private void VerCarteira_MouseLeave(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            VerCarteira.Background = (Brush)newcolor.ConvertFrom("#1163ada8");
        }

        private void VerCarteira_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void VerInvestimentos_MouseEnter(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            VerInvestimentos.Background = (Brush)newcolor.ConvertFrom("#11FFFFFF");
        }

        private void VerInvestimentos_MouseLeave(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            VerInvestimentos.Background = (Brush)newcolor.ConvertFrom("#1163ada8");
        }

        private void VerInvestimentos_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void GridAcao0_MouseEnter(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            GridAcao0.Background = (Brush)newcolor.ConvertFrom("#11FFFFFF");
        }

        private void GridAcao0_MouseLeave(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            GridAcao0.Background = (Brush)newcolor.ConvertFrom("#1163ada8");
        }

        private void GridAcao1_MouseEnter(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            GridAcao1.Background = (Brush)newcolor.ConvertFrom("#11FFFFFF");
        }

        private void GridAcao1_MouseLeave(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            GridAcao1.Background = (Brush)newcolor.ConvertFrom("#1163ada8");
        }

        private void GridAcao2_MouseEnter(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            GridAcao2.Background = (Brush)newcolor.ConvertFrom("#11FFFFFF");
        }

        private void GridAcao2_MouseLeave(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            GridAcao2.Background = (Brush)newcolor.ConvertFrom("#1163ada8");
        }

        private void VerAcoes_MouseEnter(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            VerAcoes.Background = (Brush)newcolor.ConvertFrom("#11FFFFFF");
        }

        private void VerAcoes_MouseLeave(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            VerAcoes.Background = (Brush)newcolor.ConvertFrom("#1163ada8");
        }

        private void VerAcoes_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
