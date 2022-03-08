using System;
using System.Security.Cryptography;
using System.Text;

namespace Mauxnimale_CE2.api.controllers.utils
{
    internal class PasswordUtils
    {
        /// <summary>
        /// Generates a random string with a given size.
        /// </summary>
        /// <param name="size">The size of the string to generate.</param>
        /// <param name="lowerCase">True to generate a lower case string.</param>
        /// <returns>The generated string.</returns>
        public static string generateRandomString(int size = 10, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);
            Random randomizer = new Random();

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)randomizer.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        /// <summary>
        /// Creates and returns the hash of the given text.
        /// </summary>
        /// <param name="text">The text to get the hash</param>
        /// <returns>the hash of the given text</returns>
        public static string getHash(string text)
        {
            // SHA512 is disposable by inheritance.  
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                // Get the hashed string.  
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
