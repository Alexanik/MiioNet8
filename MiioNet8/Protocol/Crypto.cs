using System.Security.Cryptography;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MiioNet8.Protocol
{
    internal static class Crypto
    {
        public static byte[] Encrypt(byte[] salt, string payload)
        {
            using var aes = Aes.Create();

            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.KeySize = 128;
            aes.BlockSize = 128;
            aes.Key = MD5.HashData(salt);
            aes.IV = MD5.HashData(aes.Key.Concat(salt).ToArray());

            using var encryptor = aes.CreateEncryptor();
            var data = Encoding.ASCII.GetBytes(payload);
            var result = encryptor.TransformFinalBlock(data, 0, data.Length);

            return result;
        }

        public static async Task<string> Decrypt(byte[] salt, byte[] data)
        {
            using var aes = Aes.Create();

            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.KeySize = 128;
            aes.BlockSize = 128;
            aes.Key = MD5.HashData(salt);
            aes.IV = MD5.HashData(aes.Key.Concat(salt).ToArray());

            using var decryptor = aes.CreateDecryptor();
            using var encryptedMemoryStream = new MemoryStream(data);
            using var cryptoStream = new CryptoStream(encryptedMemoryStream, decryptor, CryptoStreamMode.Read);
            using var streamReader = new StreamReader(cryptoStream);

            return await streamReader.ReadToEndAsync();
        }

        private static Aes CreateAes(byte[] salt)
        {
            var aes = Aes.Create();
            aes.Key = MD5.HashData(salt);
            aes.IV = MD5.HashData(aes.Key.Concat(salt).ToArray());
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            return aes;
        }
    }
}
