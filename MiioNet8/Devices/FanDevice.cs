using MiioNet8.Protocol;
using System.Net;

namespace MiioNet8.Devices
{
    public class FanDevice : GenericDevice
    {
        public override string Model { get; protected set; } = "dmaker.fan.p45";

        public FanDevice(IPAddress iPAddress, int port, Token token) : base(iPAddress, port, token)
        {
        }

        public async Task On() => await SwitchPower(true);

        public async Task Off() => await SwitchPower(false);

        public async Task SwitchPower(bool value) => await SetPropertyAsync("fan", "switch status", value);

        public async Task<bool> PowerState() => await GetPropertyAsync<bool>("fan", "switch status");
    }
}
