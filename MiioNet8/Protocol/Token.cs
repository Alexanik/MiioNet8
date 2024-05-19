using MiioNet8.Extensions;
using MiioNet8.Interfaces;

namespace MiioNet8.Protocol
{
    public class Token : IToken
    {
        private string _tokenString;

        private byte[] _tokenByteArray;

        private Token(string value)
        {
            _tokenString = value;
            _tokenByteArray = value.FromHexToBytesArray();
        }

        private Token(byte[] value)
        {

        }

        public override string ToString() => _tokenString;

        public byte[] ToByteArray() => _tokenByteArray;

        public static bool TryParse(string value, out Token token)
        {
            token = null;

            if (value.Length != 32)
                return false;

            token = new Token(value);

            return true;
        }
    }
}
