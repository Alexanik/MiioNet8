using MiioNet8.Interfaces;

namespace MiioNet8.Protocol
{
    internal class CommandPackage : BasePackage
    {
        public ICommand Command { get; }

        public CommandPackage(IDevice device, ICommand command) : base(device, null)
        {
            Command = command;
        }

        public override async Task<byte[]> ToByteArrayAsync()
        {
            Payload = await Command.ToJson();

            return await base.ToByteArrayAsync();
        }
    }
}
