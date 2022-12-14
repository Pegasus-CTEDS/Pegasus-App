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
using Pegasus_App.Models;

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
            background_left_grid = InvestirGrid.Background;
            LoadPortfolioDataGridView();
            LoadAssets();
            LoadInvestments();
            WelcomeLabel.Content = $"Bem-vindo,\n" + User.Username + "!";
            WelcomeLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
        }

        void LoadAssets()
        {
            UserWallet.GetWallet();
            foreach (var asset in UserWallet.UserAssets)
            {
                MinhasAcoesPanel.Children.Add(AssetGrid(asset.Name, asset.ComercialName, asset.Price, asset.Invested, asset.Variation));
            }
        }
        void LoadInvestments()
        {

        }
        public Grid AssetGrid(string asset_name, string comercial_name, double price, double amount, double variation)
        {
            Grid asset = new Grid();
            var newcolor = new BrushConverter();

            asset.Width = 800;
            asset.Height = 80;
            asset.Name = asset_name;
            asset.Margin = margin(0,0,0,10);
            
            asset.Background = (Brush)newcolor.ConvertFrom("#1163ada8");

            StackPanel AssetContent = new StackPanel();
            AssetContent.Orientation = Orientation.Horizontal;

            StackPanel AssetNamePanel = new StackPanel();
            StackPanel PricePanel = new StackPanel();
            StackPanel InvestedPanel = new StackPanel();
            StackPanel VariationPanel = new StackPanel();

            asset.Children.Add(AssetContent);
            AssetContent.Children.Add(AssetNamePanel);
            AssetContent.Children.Add(PricePanel);
            AssetContent.Children.Add(InvestedPanel);
            AssetContent.Children.Add(VariationPanel);

            Thickness margin(double left, double top, double right, double bottom)
            {
                Thickness mg = new Thickness();
                mg.Left = left;
                mg.Top = top;
                mg.Right = right;
                mg.Bottom = bottom;
                return mg;
            }

            Label lb(string content, double size, FontStyle fs, FontWeight fw)
            {
                Label lb = new Label();
                lb.Foreground = (Brush)newcolor.ConvertFrom("#FFFFFFFF");
                lb.FontWeight = fw;
                lb.FontStyle = fs;
                lb.FontSize = size;
                lb.Content = content;
                return lb;
            }

            AssetContent.Margin = margin(0, 0, 0, 0);
            AssetNamePanel.Margin = margin(20, 0, 80, 0);
            PricePanel.Margin = margin(60, 0, 0, 0);
            InvestedPanel.Margin = margin(60, 0, 0, 0);
            VariationPanel.Margin = margin(60, 0, 0, 0);

            AssetNamePanel.Children.Add(lb(asset_name, 26, FontStyles.Normal, FontWeights.Medium));
            AssetNamePanel.Children.Add(lb(comercial_name, 20, FontStyles.Italic, FontWeights.ExtraLight));
            AssetNamePanel.Width = 200;

            PricePanel.Children.Add(lb("valor", 26, FontStyles.Italic, FontWeights.ExtraLight));
            PricePanel.Children.Add(lb($"R$ {price}", 20, FontStyles.Italic, FontWeights.ExtraLight));

            InvestedPanel.Children.Add(lb("investido", 26, FontStyles.Italic, FontWeights.ExtraLight));
            InvestedPanel.Children.Add(lb($"R$ {amount}", 20, FontStyles.Italic, FontWeights.ExtraLight));

            Label variation_label = lb($"{variation}%", 20, FontStyles.Italic, FontWeights.ExtraLight);
            if (variation >= 0)
            {
                variation_label.Foreground = (Brush)newcolor.ConvertFrom("#d10000");
            } else
            {
                variation_label.Foreground = (Brush)newcolor.ConvertFrom("#00ff00");
            }
            VariationPanel.Children.Add(lb("variação", 26, FontStyles.Italic, FontWeights.ExtraLight));
            VariationPanel.Children.Add(variation_label);
            
            return asset;
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

        private void InvestirGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            var newcolor = new BrushConverter();
            InvestirGrid.Background = (Brush)newcolor.ConvertFrom("#1163ada8");
        }

        private void InvestirGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            InvestirGrid.Background = background_left_grid;
        }

        private void InvestirGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
            ResumoCarteira.Visibility = Visibility.Visible;
            PainelInvestmentos.Visibility = Visibility.Collapsed;
            MinhasAcoes.Visibility = Visibility.Collapsed;
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
            ResumoCarteira.Visibility = Visibility.Collapsed;
            PainelInvestmentos.Visibility = Visibility.Visible;
            MinhasAcoes.Visibility = Visibility.Collapsed;
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
            ResumoCarteira.Visibility = Visibility.Collapsed;
            MinhasAcoes.Visibility = Visibility.Visible;
            PainelInvestmentos.Visibility = Visibility.Collapsed;
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
            ResumoCarteira.Visibility = Visibility.Collapsed;
            PainelInvestmentos.Visibility = Visibility.Visible;
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
            ResumoCarteira.Visibility = Visibility.Collapsed;
            MinhasAcoes.Visibility = Visibility.Visible;
        }

        private void BotaoVoltar_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void BotaoVoltar_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void BotaoVoltar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PainelInvestmentos.Visibility = Visibility.Collapsed;
            ResumoCarteira.Visibility = Visibility.Visible;
        }

        private void BotaoVoltarMinhasAcoes_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void BotaoVoltarMinhasAcoes_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void BotaoVoltarMinhasAcoes_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MinhasAcoes.Visibility = Visibility.Collapsed;
            ResumoCarteira.Visibility = Visibility.Visible;
        }
    }
}
