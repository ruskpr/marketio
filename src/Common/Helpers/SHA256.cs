using System.Text;

namespace Common.Helpers
{
    public class SHA256
    {
        public static string Hash(string input)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
    {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
