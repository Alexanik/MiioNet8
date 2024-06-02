using MiioNet8.Interfaces;
using MiioNet8.Protocol;
using System.Net;

namespace MiioNet8.Devices
{
    public class FanDevice : GenericDevice, IPowerStateDevice, IHorizontalSwingDevice, IOffDelayTimeDevice, IFanPowerControlDevice
    {
        public override string Model { get; protected set; } = "dmaker.fan.p45";

        public FanDevice(IPAddress iPAddress, int port, Token token) : base(iPAddress, port, token)
        {
        }

        public async Task On() => await SwitchPower(true);

        public async Task Off() => await SwitchPower(false);

        public async Task SwitchPower(bool value) => await SetPropertyAsync("fan", "switch status", value);

        public async Task<bool> PowerState() => await GetPropertyAsync<bool>("fan", "switch status");

        public async Task SetHorizontalSwing(bool value) => await SetPropertyAsync("fan", "horizontal swing", value);

        public async Task<int> GetHorizontalSwingAngle() => await GetPropertyAsync<int>("fan", "horizontal swing included angle");

        public async Task SetHorizontalSwingAngle(int angle) => await SetPropertyAsync("fan", "horizontal swing included angle", angle);

        public async Task<int> GetOffDelayTime() => await GetPropertyAsync<int>("off-delay-time", "");

        public async Task SetOffDelayTime(int min) => await SetPropertyAsync("off-delay-time", "", min);

        public async Task<int> GetCurrentPower() => await GetPropertyAsync<int>("fan", "fan level");

        public async Task SetCurrentPower(int power) => await SetPropertyAsync("fan", "fan level", power);
    }
}
