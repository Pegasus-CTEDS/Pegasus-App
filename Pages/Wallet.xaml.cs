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

namespace Pegasus_App.Pages
{
    /// <summary>
    /// Interação lógica para Wallet.xaml
    /// </summary>
    public partial class Wallet : Page
    {

        ClientConnection conn;

        public Wallet(ClientConnection conn)
        {

            this.conn = conn;
            List<Packet.RequestedDataType> requestedData = new List<Packet.RequestedDataType>() { Packet.RequestedDataType.Wallet, Packet.RequestedDataType.Portfolio };
            Packet packetIn = conn.RequestData(requestedData);
            double balance = packetIn.WalletData.Balance;
            double invested = packetIn.WalletData.Invested;
            BalanceLabel.Content = String.Format("Disponível: R$ {0}", balance);
            InvestedLabel.Content = String.Format("Investido: R$ {0}", invested);
            packetIn = conn.RequestPortfolioAssetsData(packetIn.PortfolioData);
            YieldLabel.Content = String.Format("Ganho Total: R$ {0}", CalculateTotalYield(invested, packetIn.PortfolioData, packetIn.AssetData));
            
            InitializeComponent();
        }

        private void OpenOperationsPage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Operations(conn));
        }
        private void OpenPortfolioPage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Portfolio(conn));
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
                double closingPrice = AssetMostRecentClosingPrice(assetData, field.Name);
                double? averagePrice = field.AveragePrice;
                if (averagePrice == null) averagePrice = 0.0;
                int? quantity = field.Quantity;
                if (quantity == null) quantity = 0;
                totalGains+= quantity * (closingPrice - averagePrice);
            }

            double? totalYield = (invested + totalGains) / invested;

            return (double)totalYield;
        }

        double AssetMostRecentClosingPrice(List<Packet.AssetDataField> assetDataFields, string assetName)
        {
            foreach (Packet.AssetDataField field in assetDataFields)
            {
                if (field.Name == assetName)
                {
                    return field.PriceHistory.Last();
                }   
            }
            return 0.0;
        }


    }
}
