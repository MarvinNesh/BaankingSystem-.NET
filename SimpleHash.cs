using System.Security.Cryptography;
using System.Text;

namespace BankingSystem
{
    public static class SimpleHash
    {
        public static string Compute(string s)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(s);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}