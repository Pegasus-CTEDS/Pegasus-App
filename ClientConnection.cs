using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PegasusPacket;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Printing;

namespace Pegasus_App
{
    public class ClientConnection 
    {
        public ClientConnection(string address = "localhost", int port = 13013) 
        {
            Port = port;
            Client = new TcpClient(address, Port);
            Stream = Client.GetStream();
        }

        int Port { get; } 
        TcpClient Client { get; } 
        NetworkStream Stream { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Packet RequestSignIn(string username, string email, string password)
        {
            Packet packetOut = new Packet(Packet.PacketType.SignIn);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            packetOut.PersonalData.Username = username;
            packetOut.PersonalData.Email = email;
            packetOut.PersonalData.Password = password;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            SendPacket(packetOut);
            Packet packetIn = ReceivePacket();
            return packetIn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Packet RequestLogin(string username, string password)
        {
            Packet packetOut = new Packet(Packet.PacketType.Login);
#pragma warning disable CS8602 // Dereference of a possibly null reference. -> PersonalData is initialized inside Packet Constructor
            packetOut.PersonalData.Username = username;
            packetOut.PersonalData.Password = password;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            SendPacket(packetOut);
            Packet packetIn = ReceivePacket();
            return packetIn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Packet RequestLogout()
        {
            Packet packetOut = new Packet(Packet.PacketType.Logout);
            SendPacket(packetOut);
            Packet packetIn = ReceivePacket();
            return packetIn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestedData"></param>
        /// <returns></returns>
        public Packet RequestDataGet(List<Packet.RequestedDataType> requestedData)
        {
            Packet packetOut = new Packet(Packet.PacketType.DataGet);
            packetOut.RequestedData = requestedData;
            SendPacket(packetOut);
            Packet packetIn = ReceivePacket();
            return packetIn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestedData"></param>
        /// <returns></returns>
        public Packet RequestWalletDataGet()
        {
            List<Packet.RequestedDataType> requestedData = new List<Packet.RequestedDataType>() { Packet.RequestedDataType.Wallet };
            return RequestDataGet(requestedData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="balance"></param>
        /// <param name="invested"></param>
        /// <returns></returns>
        public Packet RequestWalletDataPut(double balance, double invested)
        {
            Packet packetOut = new Packet(Packet.PacketType.DataPut);
            packetOut.RequestedData.Add(Packet.RequestedDataType.Wallet);
            packetOut.WalletData.Balance = balance;
            packetOut.WalletData.Invested = invested;
            SendPacket(packetOut);
            Packet packetIn = ReceivePacket();
            return packetIn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assetSymbol"></param>
        /// <param name="date"></param>
        /// <param name="price"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public Packet RequestSellOperation(string assetSymbol, DateTime date, double price, int quantity)
        {
            List<Packet.RequestedDataType> requestedData = new List<Packet.RequestedDataType>() { Packet.RequestedDataType.Operation };
            Packet.OperationDataField operationData = new();
            operationData.Symbol= assetSymbol;
            operationData.Date = date;
            operationData.Price = price;
            operationData.Quantity = quantity;
            operationData.Type = Packet.OperationType.Sell;
            return RequestOperation(requestedData, operationData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assetSymbol"></param>
        /// <param name="date"></param>
        /// <param name="price"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public Packet RequestBuyOperation(string assetSymbol, DateTime date, double price, int quantity)
        {
            List<Packet.RequestedDataType> requestedData = new List<Packet.RequestedDataType>() { Packet.RequestedDataType.Operation };
            Packet.OperationDataField operationData = new();
            operationData.Symbol= assetSymbol;
            operationData.Date = date;
            operationData.Price = price;
            operationData.Quantity = quantity;
            operationData.Type = Packet.OperationType.Buy;
            return RequestOperation(requestedData, operationData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestedData"></param>
        /// <param name="operationData"></param>
        /// <returns></returns>
        public Packet RequestOperation(List<Packet.RequestedDataType> requestedData, Packet.OperationDataField operationData)
        {
            Packet packetOut = new Packet(Packet.PacketType.DataPut);
            packetOut.RequestedData = requestedData;
            packetOut.OperationData = operationData;
            SendPacket(packetOut);
            Packet packetIn = ReceivePacket();
            return packetIn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Packet RequestPortfolioDataGet()
        {
            List<Packet.RequestedDataType> requestedData = new List<Packet.RequestedDataType>() { Packet.RequestedDataType.Portfolio };
            return RequestDataGet(requestedData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="portfolio"></param>
        /// <returns></returns>
        public Packet RequestPortfolioAssetsData()
        {
            Packet packetIn = RequestPortfolioDataGet();
            List<Packet.PortfolioDataField> portfolio = packetIn.PortfolioData;

            List<string> assetsSymbols = new();
            foreach (Packet.PortfolioDataField field in portfolio)
            {
                assetsSymbols.Add(field.Symbol);
            }
            return RequestAssetsData(assetsSymbols);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assetsSymbols"></param>
        /// <returns></returns>
        public Packet RequestAssetsData(List<string> assetsSymbols)
        {
            Packet packetOut = new Packet(Packet.PacketType.DataGet);
            packetOut.RequestedData = new List<Packet.RequestedDataType>() { Packet.RequestedDataType.Assets };
            foreach (string assetSymbol in assetsSymbols)
            {
                Packet.AssetDataField field = new();
                field.Symbol = assetSymbol;
                packetOut.AssetData.Add(field);
            }
            SendPacket(packetOut);
            Packet packetIn = ReceivePacket();
            return packetIn;
        }

        // public Packet RequestAssetsList()
        // {
        //     Packet packetOut = new Packet(Packet.PacketType.DataGet);
        //     packetOut.RequestedData.Add(Packet.RequestedDataType.Assets);
        // }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packetOut"></param>
        public void SendPacket(Packet packetOut)
        {
            byte[] data = Packet.PacketSerialize(packetOut);
            Stream.Write(data, 0, data.Length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Packet ReceivePacket()
        {
            Packet? packetIn;
            int bytesReceivedNumber = 0;
            byte[] bytesReceived = new byte[4096];

            // TODO: Do NOT erase these comments
            //do
            //{   
                bytesReceivedNumber = Stream.Read(bytesReceived, 0, bytesReceived.Length);
            //} while (bytesReceivedNumber > 0);

            packetIn = Packet.PacketDeserialize(bytesReceived);

            return packetIn;
        }
    }
}