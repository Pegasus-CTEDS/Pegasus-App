using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PegasusPacket;
using System.Net.Sockets;

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
    }

    internal class Networking
    {
        static public Packet ClientRequestSignIn(TcpClient client, NetworkStream stream, string username, string email, string password)
        {
            Packet packetOut = new Packet(Packet.PacketType.SignIn);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            packetOut.PersonalData.Username = username;
            packetOut.PersonalData.Email = email;
            packetOut.PersonalData.Password = password;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            ClientSendPacket(client, stream, packetOut);
            Packet packetIn = ClientReceivePacket(client, stream);
            return packetIn;
        }

                Packet ClientRequestLogin(TcpClient client, NetworkStream stream, string username, string password)
        {
            Packet packetOut = new Packet(Packet.PacketType.Login);
#pragma warning disable CS8602 // Dereference of a possibly null reference. -> PersonalData is initialized inside Packet Constructor
            packetOut.PersonalData.Username = username;
            packetOut.PersonalData.Password = password;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            ClientSendPacket(client, stream, packetOut);
            Packet packetIn = ClientReceivePacket(client, stream);
            return packetIn;
        }

        Packet ClientRequestLogout(TcpClient client, NetworkStream stream)
        {
            Packet packetOut = new Packet(Packet.PacketType.Logout);
            ClientSendPacket(client, stream, packetOut);
            Packet packetIn = ClientReceivePacket(client, stream);
            return packetIn;
        }

        static Packet ClientRequestData(List<Packet.RequestedDataType> requestedData, TcpClient client, NetworkStream stream)
        {
            Packet packetOut = new Packet(Packet.PacketType.DataGet);
            packetOut.RequestedData = requestedData;
            ClientSendPacket(client, stream, packetOut);
            Packet packetIn = ClientReceivePacket(client, stream);
            return packetIn;
        }

        static void ClientSendPacket(TcpClient client, NetworkStream stream, Packet packetOut)
        {
            byte[] data = Packet.PacketSerialize(packetOut);
            stream.Write(data, 0, data.Length);
        }

        static Packet ClientReceivePacket(TcpClient client, NetworkStream stream)
        {
            Packet? packetIn;
            int bytesReceivedNumber = 0;
            byte[] bytesReceived = new byte[4096];

            // TODO: Do NOT erase these comments
            //do
            //{   
                bytesReceivedNumber = stream.Read(bytesReceived, 0, bytesReceived.Length);
            //} while (bytesReceivedNumber > 0);

            packetIn = Packet.PacketDeserialize(bytesReceived);

            return packetIn;
        }
    }
}
