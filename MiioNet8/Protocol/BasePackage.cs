using MiioNet8.Extensions;
using MiioNet8.Interfaces;
using System.Security.Cryptography;

namespace MiioNet8.Protocol
{
    internal class BasePackage : IPackage
    {
        protected static readonly int headerSize = 32;
        protected static readonly int magicSize = 2;
        protected static readonly int lengthSize = 2;
        protected static readonly int unknownSize = 4;
        protected static readonly int deviceTypeSize = 2;
        protected static readonly int deviceIdSize = 2;
        protected static readonly int timestampSize = 4;
        protected static readonly int checksumSize = 16;

        public IDevice Device { get; private set; }
        public string? Payload { get; protected set; }

        public virtual byte[] Magic { get; protected set; } = [0x21, 0x31];
        public virtual byte[] Unknown { get; protected set; } = [0x00, 0x00, 0x00, 0x00];
        public short DeviceType { get; protected set; }
        public short DeviceId { get; protected set; }
        public int Timestamp { get; protected set; }
        public bool HasPayload => Payload != null;

        public DateTime CreationDate { get; private set; }

        public BasePackage(IDevice device, string? payload = null)
        {
            Device = device;
            CreationDate = DateTime.UtcNow;
            Payload = payload;
        }

        public virtual async Task<byte[]> ToByteArrayAsync()
        {
            var encryptedData = Payload != null ? Crypto.Encrypt(Device.Token.ToByteArray(), Payload) : null;
            var totalSize = (short)(headerSize + (encryptedData != null ? encryptedData.Length : 0));
            var data = GC.AllocateArray<byte>(totalSize, true);

            Array.Copy(Magic, 0, data, 0, magicSize);
            Array.Copy(totalSize.ToBigEndian(), 0, data, magicSize, lengthSize);
            Array.Copy(Unknown, 0, data, magicSize + lengthSize, unknownSize);
            Array.Copy(Device.Type.ToBigEndian(), 0, data, magicSize + lengthSize + unknownSize, deviceTypeSize);
            Array.Copy(Device.Id.ToBigEndian(), 0, data, magicSize + lengthSize + unknownSize + deviceTypeSize, deviceIdSize);

            var timeDifference = DateTime.UtcNow - Device.DeviceLastCommunicationDateTime;
            var timestamp = Device.DeviceLastTimestamp + (int)timeDifference.TotalSeconds;

            Array.Copy(timestamp.ToBigEndian(), 0, data, magicSize + lengthSize + unknownSize + deviceTypeSize + deviceIdSize, timestampSize);
            Array.Copy(Device.Token.ToByteArray(), 0, data, headerSize - checksumSize, checksumSize);

            if (encryptedData != null)
            {
                Array.Copy(encryptedData, 0, data, headerSize, encryptedData.Length);

                var checksum = MD5.HashData(data);
                Array.Copy(checksum, 0, data, headerSize - checksumSize, checksumSize);
            }

            return data;
        }

        public virtual async Task<string?> GetPayloadAsync() => Payload;
    }
}
