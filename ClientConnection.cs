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

        public Packet RequestLogout()
        {
            Packet packetOut = new Packet(Packet.PacketType.Logout);
            SendPacket(packetOut);
            Packet packetIn = ReceivePacket();
            return packetIn;
        }

        public Packet RequestData(List<Packet.RequestedDataType> requestedData)
        {
            Packet packetOut = new Packet(Packet.PacketType.DataGet);
            packetOut.RequestedData = requestedData;
            SendPacket(packetOut);
            Packet packetIn = ReceivePacket();
            return packetIn;
        }
        public Packet RequestSellOperation(string assetName, DateTime date, double price, int quantity)
        {
            List<Packet.RequestedDataType> requestedData = new List<Packet.RequestedDataType>() { Packet.RequestedDataType.Operation };
            Packet.OperationDataField operationData = new();
            operationData.AssetName= assetName;
            operationData.Date = date;
            operationData.Price = price;
            operationData.Quantity = quantity;
            operationData.Type = Packet.OperationType.Sell;
            return RequestOperation(requestedData, operationData);
        }

        public Packet RequestBuyOperation(string assetName, DateTime date, double price, int quantity)
        {
            List<Packet.RequestedDataType> requestedData = new List<Packet.RequestedDataType>() { Packet.RequestedDataType.Operation };
            Packet.OperationDataField operationData = new();
            operationData.AssetName= assetName;
            operationData.Date = date;
            operationData.Price = price;
            operationData.Quantity = quantity;
            operationData.Type = Packet.OperationType.Buy;
            return RequestOperation(requestedData, operationData);
        }

        public Packet RequestOperation(List<Packet.RequestedDataType> requestedData, Packet.OperationDataField operationData)
        {
            Packet packetOut = new Packet(Packet.PacketType.DataGet);
            packetOut.RequestedData = requestedData;
            packetOut.OperationData = operationData;
            SendPacket(packetOut);
            Packet packetIn = ReceivePacket();
            return packetIn;
        }

        public Packet RequestPortfolioAssetsData(List<Packet.PortfolioDataField> portfolio)
        {
            List<string> assetsNames = new();
            foreach (Packet.PortfolioDataField field in portfolio)
            {
                assetsNames.Add(field.Name);
            }
            return RequestAssetsData(assetsNames);
        }

        public Packet RequestAssetsData(List<string> assetsNames)
        {
            Packet packetOut = new Packet(Packet.PacketType.DataGet);
            packetOut.RequestedData = new List<Packet.RequestedDataType>() { Packet.RequestedDataType.Assets };
            foreach (string assetName in assetsNames)
            {
                Packet.AssetDataField field = new();
                field.Name = assetName;
                packetOut.AssetData.Add(field);
            }
            SendPacket(packetOut);
            Packet packetIn = ReceivePacket();
            return packetIn;
        }

        public void SendPacket(Packet packetOut)
        {
            byte[] data = Packet.PacketSerialize(packetOut);
            Stream.Write(data, 0, data.Length);
        }

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
