using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries.Extension
{
    public static class StringExtension
    {
        public static byte[] HexStringToByteArray(this string hex)
        {
            return Enumerable.Range(0, hex.Length / 2)
                .Select(x => byte.Parse(hex.Substring(2 * x, 2), NumberStyles.HexNumber))
                .ToArray();
        }

        public static byte[] Base64StringToByteArray(this string base64)
        {
            return Convert.FromBase64String(base64);
        }
    }
}
