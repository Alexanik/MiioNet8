namespace MiioNet8.Interfaces
{
    public interface IPackage
    {
        byte[] Magic { get; }
        byte[] Unknown { get; }
        short DeviceType { get; }
        short DeviceId { get; }
        int Timestamp { get; }
        bool HasPayload { get; }

        Task<byte[]> ToByteArrayAsync();
        Task<string?> GetPayloadAsync();
    }
}
