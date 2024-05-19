using MiioNet8.Communication;

namespace MiioNet8.Interfaces
{
    public interface ICommunication
    {
        Task<(CommunicationResult, IPackage?)> SendAndReceiveAsync(IDevice device, IPackage package);
    }
}
