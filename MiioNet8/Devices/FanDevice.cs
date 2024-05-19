using MiioNet8.Interfaces;
using MiioNet8.Protocol;
using System.Net;

namespace MiioNet8.Devices
{
    public class FanDevice : GenericDevice
    {
        public override string Model { get; protected set; } = "dmaker.fan.p45";

        public FanDevice(ICommunication communication, Token token) : base(communication, token)
        {
        }

        public async Task On() => await SwitchPower(true);

        public async Task Off() => await SwitchPower(false);

        public async Task SwitchPower(bool value) => await SetPropertyAsync("fan", "switch status", value);

        public async Task<bool> PowerState() => await GetPropertyAsync<bool>("fan", "switch status");
    }
}
