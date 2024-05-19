using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiioNet8.Extensions
{
    internal static class ShortExtensions
    {
        public static byte[] ToBigEndian(this short value) => BitConverter.GetBytes(value).Reverse().ToArray();
    }
}
