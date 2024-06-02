using MiioNet8.Devices;
using MiioNet8.Interfaces;
using MiioNet8.Protocol;
using System.Net;

namespace MiioNet8
{
    public static class DeviceFactory
    {
        public static async Task<T> Create<T>(IPAddress iPAddress, string tokenValue, int port = 54321) where T : BaseDevice
        {
            if (!Token.TryParse(tokenValue, out var token))
                throw new Exception();

            if (Activator.CreateInstance(typeof(T), iPAddress, port, token) is not T device)
                throw new Exception();

            if (await device.ConnectAsync() != Communication.CommunicationResult.Success)
                throw new Exception();

            return device;
        }

        public static async Task<T> Create<T>(string iPAddress, string tokenValue, int port = 54321) where T : BaseDevice
        {
            if (!IPAddress.TryParse(iPAddress, out var address))
                throw new Exception();

            return await Create<T>(address, tokenValue, port);
        }

        public static async Task<GenericDevice> Create(IPAddress iPAddress, string tokenValue, Type deviceType, int port = 54321)
        {
            if (!Token.TryParse(tokenValue, out var token))
                throw new Exception();

            if (Activator.CreateInstance(deviceType, iPAddress, port, token) is not GenericDevice device)
                throw new Exception();

            if (await device.ConnectAsync() != Communication.CommunicationResult.Success)
                throw new Exception();

            return device;
        }

        public static async Task<GenericDevice> Create(string iPAddress, string tokenValue, Type deviceType, int port = 54321)
        {
            if (!IPAddress.TryParse(iPAddress, out var address))
                throw new Exception();

            return await Create(address, tokenValue, deviceType, port);
        }
    }
}
