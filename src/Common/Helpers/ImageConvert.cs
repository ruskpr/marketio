using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public static class ImageConvert
    {
        public static string ToDataUrl(this MemoryStream dataStream, string format)
        {
            var span = new Span<byte>(dataStream.GetBuffer()).Slice(0, (int)dataStream.Length);
            return $"data:{format};base64,{Convert.ToBase64String(span)}";
        }

        public static byte[]? ToBytes(string url)
        {
            var commPos = url.IndexOf(',');
            if (commPos >= 0)
            {
                var base64 = url.Substring(commPos + 1);
                return Convert.FromBase64String(base64);
            }

            return null;
        }

    }
}
