using System;
using System.Linq;
using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.api.controllers.utils;

namespace Mauxnimale_CE2.api.controllers
{
    internal class RegistrationController
    {
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

        /// <summary>
        /// Register a new user with assistant permissions with a temporary password.
        /// </summary>
        /// <param name="newUserLogin">The login of the new user</param>
        /// <returns>Null if the registration failed, and the temporary password otherwise.</returns>
        public static string registerNewUser(string newUserLogin)
        {
            // Verify that the login does not already exists
            if (userWithLoginAlreadyExists(newUserLogin))
            {
                Console.WriteLine("User with login: " + newUserLogin + " already exists.");
                return null;
            }

            // Generate a temporary password
            string tempPassword = PasswordUtils.generateRandomString();

            // Create the new user without the informations (will be modified by him and Annie Maux later)
            SALARIE newUser = new SALARIE();
            newUser.LOGIN = newUserLogin;
            newUser.MDP = PasswordUtils.getHash(tempPassword);
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

            Console.WriteLine("New user with login: " + newUserLogin + " and password: " + tempPassword + " registered");
            return tempPassword;
        }
    }
}
