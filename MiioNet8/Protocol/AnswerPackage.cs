using MiioNet8.Interfaces;

namespace MiioNet8.Protocol
{
    internal class AnswerPackage : BasePackage
    {
        public byte[]? PayloadData { get; }

        public AnswerPackage(IDevice device, byte[] data) : base(device, null)
        {
            var magicData = data[0..magicSize];
            if (!magicData.SequenceEqual(Magic))
                throw new Exception();

            var lengthData = data[magicSize..(magicSize + lengthSize)];
            var length = BitConverter.ToInt16(lengthData.Reverse().ToArray());

            var unknownData = data[(magicSize + lengthSize)..(magicSize + lengthSize + unknownSize)];
            if (unknownData.Any(v => v != 0x00))
                throw new Exception();

            var deviceTypeData = data[(magicSize + lengthSize + unknownSize)..(magicSize + lengthSize + unknownSize + deviceTypeSize)];
            DeviceType = BitConverter.ToInt16(deviceTypeData.Reverse().ToArray());

            var deviceIdData = data[(magicSize + lengthSize + unknownSize + deviceTypeSize)..(magicSize + lengthSize + unknownSize + deviceTypeSize + deviceIdSize)];
            DeviceId = BitConverter.ToInt16(deviceIdData.Reverse().ToArray());

            var timestampData = data[(magicSize + lengthSize + unknownSize + deviceTypeSize + deviceIdSize)..(magicSize + lengthSize + unknownSize + deviceTypeSize + deviceIdSize + timestampSize)];
            Timestamp = BitConverter.ToInt32(timestampData.Reverse().ToArray());
            
            var checksumData = data[(headerSize - checksumSize)..headerSize];
            
            //Hello Answer
            if (length == headerSize && checksumData.All(v => v == 0xFF))
                return;

            if (length == headerSize)
                throw new Exception();

            PayloadData = data[headerSize..];
        }

        public override async Task<string?> GetPayloadAsync()
        {
            if (PayloadData == null)
                return null;

            if (Payload == null)
                Payload = await Decrypt();

            return Payload;
        }

        private async Task<string?> Decrypt()
        {
            if (PayloadData == null)
                return null;

            return await Crypto.Decrypt(Device.Token.ToByteArray(), PayloadData);
        }
    }
}
