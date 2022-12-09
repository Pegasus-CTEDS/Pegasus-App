using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Pegasus_App.Pages
{
    /// <summary>
    /// Interação lógica para Wallet.xaml
    /// </summary>
    public partial class Wallet : Page
    {
        public Wallet(ClientConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
            LoadWalletData();
        }
        
        ClientConnection conn;
        double invested;
        double balance;

        public void LoadWalletData()
        {
            Packet packetIn;
            
            packetIn = conn.RequestWalletDataGet();
            balance = packetIn.WalletData.Balance;
            invested = packetIn.WalletData.Invested;
            BalanceLabel.Content = String.Format("Disponível: R$ {0}", balance);
            InvestedLabel.Content = String.Format("Investido: R$ {0}", invested);

            packetIn = conn.RequestPortfolioAssetsData();
            List<Packet.PortfolioDataField> portfolioData = packetIn.PortfolioData;
            YieldLabel.Content = String.Format("Ganho Total: {0} %", CalculateTotalYield(invested, portfolioData, packetIn.AssetData));
        }

        public double CalculateTotalYield(double invested, List<Packet.PortfolioDataField> portfolio, List<Packet.AssetDataField> assetData)
        {
            if (invested == 0.0)
            {
                return 0.0;
            }

            double? totalGains = 0.0;
            foreach (Packet.PortfolioDataField field in portfolio) 
            {
                double closingPrice = AssetMostRecentClosingPrice(assetData, field.Symbol);
                double? averagePrice = field.AveragePrice;
                if (averagePrice == null) averagePrice = 0.0;
                int? quantity = field.Quantity;
                if (quantity == null) quantity = 0;
                totalGains+= quantity * (closingPrice - averagePrice);
            }
            double? totalYield = (invested + totalGains) / invested;
            return (double)totalYield;
        }

        public double AssetMostRecentClosingPrice(List<Packet.AssetDataField> assetDataFields, string assetSymbol)
        {
            foreach (Packet.AssetDataField field in assetDataFields)
            {
                if (field.Symbol == assetSymbol)
                {
                    return field.PriceHistory.Last();
                }   
            }
            return 0.0;
        }

        private void AddFunds_Click(object sender, RoutedEventArgs e)
        {
            if (AddFundsAmount.Text == "") return;
            double amount = Convert.ToDouble(AddFundsAmount.Text) + balance;
            Packet packetIn = conn.RequestWalletDataPut(amount, invested);
            if (packetIn != null && packetIn.RequestStatus == Packet.Status.Success)
            {
                BalanceLabel.Content = String.Format("Disponível: R$ {0}", amount);
                MessageBox.Show("Sucesso!");
            }
            else
            {
                MessageBox.Show(packetIn.ServerResponseMessage);
            }
        }

        private void OpenOperationsPage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Operations(conn));
            LoadWalletData();
        }

        private void OpenPortfolioPage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Portfolio(conn));
            LoadWalletData();
        }
    }
}
