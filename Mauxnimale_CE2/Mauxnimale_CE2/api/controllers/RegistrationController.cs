using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2.api.controllers
{
    /// <summary>
    /// Static class implementing the users registration related methods.
    /// </summary>
    internal static class RegistrationController
    {
        /// <summary>
        /// Verify if the given user already exists in the current context.
        /// </summary>
        /// <param name="userToVerify">The user to verify</param>
        /// <returns>true if the user has the same login, email or phone number as one user in the current context, false otherwise.</returns>
        public static bool userAlreadyExists(SALARIE userToVerify)
        {
            PT4_S4P2C_E2Entities dbAccessor = new PT4_S4P2C_E2Entities();

            foreach (SALARIE registeredUser in dbAccessor.SALARIE)
            {
                if (registeredUser.LOGIN.Equals(userToVerify.LOGIN) ||
                    registeredUser.EMAIL.Equals(userToVerify.EMAIL) ||
                    registeredUser.TEL.Equals(userToVerify.TEL))
                    return true;
            }

            return false;
        }
    }
}
