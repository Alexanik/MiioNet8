namespace MiioNet8.Extensions
{
    internal static class StringExtensions
    {
        public static byte[] FromHexToBytesArray(this string value) =>
            Enumerable.Range(0, value.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(value.Substring(x, 2), 16))
                .ToArray();
    }
}
