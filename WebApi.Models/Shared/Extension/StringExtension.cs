using System.Security.Cryptography;
using System.Text;

namespace WebApi.Models.Shared.Extension
{
    /// <summary>
    /// Provides extension methods for string manipulation.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Converts a string into a SHA256 hash string.
        /// </summary>
        /// <param name="str">The input string to be hashed.</param>
        /// <returns>The SHA256 hash of the input string as a hexadecimal string.</returns>
        public static string ToHashString(this string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentNullException(nameof(str), "Input string cannot be null or empty.");

            // Convert string to byte array
            var buffer = Encoding.UTF8.GetBytes(str);

            // Compute SHA256 hash
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(buffer);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }
}
