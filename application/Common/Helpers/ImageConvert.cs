using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public class ImageConvert
    {
        // create a method that converts a byte array to base64
        public static string ConvertByteArrayToBase64(byte[] imageBytes)
        {
            return $"data:image/png;base64,{Convert.ToBase64String(imageBytes)}";
        }

    }
}
