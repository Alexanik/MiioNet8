using MiioNet8.Interfaces;
using MiioNet8.Protocol;
using System.Net;

namespace MiioNet8.Devices
{
    public class CameraDevice : GenericDevice
    {
        public override string Model { get; protected set; } = "isa.camera.hlc7";

        public CameraDevice(ICommunication communication, Token token) : base(communication, token)
        {
        }

        public async Task TestOn()
        {
            var service = Spec.Services.FirstOrDefault(s => s.Description.ToLower() == "camera control");
            var property = service.Properties.FirstOrDefault(p => p.Description.ToLower() == "switch status");

            //await SetPropertiesAsync(service, property, false);
            //await SetPropertiesAsync(service, property, true);
        }
    }
}
