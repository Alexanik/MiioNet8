using MiioNet8.Communication;
using System.Net;

namespace MiioNet8.Interfaces
{
    public interface IDevice
    {
        IPAddress IPAddress { get; }
        int Port { get; }
        short Type { get; }
        short Id { get; }
        IToken Token { get; }
        int DeviceLastTimestamp { get; }
        DateTime DeviceLastCommunicationDateTime { get; }
        string Model { get; }

        Task<CommunicationResult> ConnectAsync();
    }
}
