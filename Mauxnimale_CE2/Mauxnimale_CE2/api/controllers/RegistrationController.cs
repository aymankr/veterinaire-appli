using System;

using System.Security.Cryptography;
using System.Text;

using System.Linq;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2.api.controllers
{
    internal class RegistrationController
    {
        /// <summary>
        /// Creates and returns the hash of the given text.
        /// </summary>
        /// <param name="text">The text to get the hash</param>
        /// <returns>the hash of the given text</returns>
        private static string getHash(string text)
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

        /// <summary>
        /// Generates a random string with a given size.
        /// </summary>
        /// <param name="size">The size of the string to generate.</param>
        /// <param name="lowerCase">True to generate a lower case string.</param>
        /// <returns>The generated string.</returns>
        private static string generateRandomString(int size = 10, bool lowerCase = false)
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
        /// Verify if the a user with the given login already exists in the database or not.
        /// </summary>
        /// <param name="login">The login to verify</param>
        /// <returns>true if a user with the given login already exists, false otherwise.</returns>
        private static bool userWithLoginAlreadyExists(string login)
        {
            PT4_S4P2C_E2Entities dbContext = DbContext.get();

            var userWithLogin = from user in dbContext.SALARIE
                                where user.LOGIN == login
                                select user;

            return userWithLogin.Any();
        }

        public static bool registerNewUser(string newUserLogin, out string tempHashedPassword)
        {
            // Verify that the login does not already exists
            if (userWithLoginAlreadyExists(newUserLogin))
            {
                Console.WriteLine("User with login: " + newUserLogin + " already exists.");
                tempHashedPassword = null;
                return false;
            }

            // Generate a temporary password
            tempHashedPassword = getHash(generateRandomString());

            // Create the new user without the informations (will be modified by him and Annie Maux later)
            SALARIE newUser = new SALARIE();
            newUser.LOGIN = newUserLogin;
            newUser.MDP = tempHashedPassword;
            newUser.NOM = "None";
            newUser.PRENOM = "None";
            newUser.SALAIRE = -1;
            newUser.ADRESSE = "None";
            newUser.EMAIL = "None";
            newUser.TEL = "None";
            newUser.ASSISTANT = true;
            newUser.PREMIERECONNEXION = false;

            // Add the create user in db
            PT4_S4P2C_E2Entities dbContext = DbContext.get();
            dbContext.SALARIE.Add(newUser);
            dbContext.SaveChanges();

            return true;
        }
    }
}
