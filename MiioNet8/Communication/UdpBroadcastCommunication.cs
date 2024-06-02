using MiioNet8.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiioNet8.Communication
{
    public class UdpBroadcastCommunication : ICommunication
    {
        public Task<(CommunicationResult, IPackage?)> SendAndReceiveAsync(IDevice device, IPackage package)
        {
            throw new NotImplementedException();
        }
    }
}
