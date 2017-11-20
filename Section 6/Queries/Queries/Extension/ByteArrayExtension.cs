using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries.Extension
{
    public static class ByteArrayExtension
    {
        public static string ByteArrayToHexString(this byte[] bytes, bool uppercase = false)
        {
            return string.Concat(from x in bytes
                                 select x.ToString(uppercase ? "X2" : "x2"));
        }

        public static string ByteArrayToBase64String(this byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }
    }
}
