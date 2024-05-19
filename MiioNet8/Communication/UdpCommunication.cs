using MiioNet8.Interfaces;
using MiioNet8.Protocol;
using System.Net;
using System.Net.Sockets;

namespace MiioNet8.Communication
{
    public class UdpCommunication : ICommunication
    {
        public IPEndPoint IPEndPoint { get; private set; }
        public Socket Socket { get; private set; }

        public UdpCommunication(IPAddress ipAddress, int port) 
        { 
            IPEndPoint = new IPEndPoint(ipAddress, port);
            Socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
        }

        public async Task<(CommunicationResult, int)> SendAsync(IDevice device, IPackage package)
        {
            var buffer = await package.ToByteArrayAsync();
            var bufferMemory = buffer.AsMemory();
            var bytesSended = await Socket.SendToAsync(bufferMemory, SocketFlags.None, IPEndPoint);

            if (bytesSended != buffer.Length)
                return (CommunicationResult.Error, bytesSended);

            return (CommunicationResult.Success, bytesSended);
        }

        public async Task<(CommunicationResult, IPackage?)> ReceiveAsync(IDevice device)
        {
            var buffer = GC.AllocateArray<byte>(0xFFFF, true);
            var bufferMemory = buffer.AsMemory();
            var receivedBytes = await Socket.ReceiveFromAsync(bufferMemory, SocketFlags.None, IPEndPoint);

            if (receivedBytes.ReceivedBytes < 32)
                return (CommunicationResult.Error, null);

            var data = buffer[0..receivedBytes.ReceivedBytes];

            try
            {
                var package = new AnswerPackage(device, data);

                return (CommunicationResult.Success, package);
            } catch (Exception ex)
            {
                return (CommunicationResult.Error, null);
            }
        }

        public async Task<(CommunicationResult, IPackage?)> SendAndReceiveAsync(IDevice device, IPackage package)
        {
            var (sendResult, _) = await SendAsync(device, package);

            if (sendResult != CommunicationResult.Success)
                return (sendResult, null);

            return await ReceiveAsync(device);
        }
    }
}
