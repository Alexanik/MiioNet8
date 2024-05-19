namespace MiioNet8.Extensions
{
    internal static class IntExtensions
    {
        public static byte[] ToBigEndian(this int value) => BitConverter.GetBytes(value).Reverse().ToArray();
    }
}
