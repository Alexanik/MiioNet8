using MiioNet8.Interfaces;

namespace MiioNet8.Protocol
{
    internal class HelloPackage : BasePackage
    {
        public override byte[] Unknown { get; protected set; } = [0xFF, 0xFF, 0xFF, 0xFF];

        public HelloPackage(IDevice device) : base(device, null)
        {
        }

        public override Task<byte[]> ToByteArrayAsync()
        {
            var data = GC.AllocateArray<byte>(headerSize, true);

            for (var index = 0; index < data.Length; index++)
                data[index] = 0xFF;

            Array.Copy(Magic, 0, data, 0, magicSize);
            data[2] = 0x00;
            data[3] = 0x20;

            return Task.FromResult(data);
        }
    }
}
