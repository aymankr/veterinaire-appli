using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2.api.controllers
{
    /// <summary>
    /// Static class implementing methods related to animals management.
    /// </summary>
    internal static class AnimalController
    {
        /// <summary>
        /// Verify wether the given animal is already registered in the database or not.
        /// </summary>
        /// <param name="animalToVerify">The animal to verify</param>
        /// <returns>true if an animal with the same owner and name as the one given exists, false otherwise.</returns>
        public static bool isAlreadyRegistered(ANIMAL animalToVerify)
        {
            PT4_S4P2C_E2Entities dbContext = new PT4_S4P2C_E2Entities();

            foreach (ANIMAL registeredAnimal in dbContext.ANIMAL)
            {
                if (registeredAnimal.CLIENT == animalToVerify.CLIENT &&
                    registeredAnimal.NOM == animalToVerify.NOM)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Try to register (add to the database) a new animal with the given informations.
        /// </summary>
        /// <param name="race">the animal's race</param>
        /// <param name="owner">the animal's owner who is a registered client</param>
        /// <param name="name">the animal's name</param>
        /// <param name="birthYear">the animal's year of birth</param>
        /// <param name="height">the animal's height</param>
        /// <param name="weight">the animal's weight</param>
        /// <param name="isMale">true to set the new animal as a male, false as a female</param>
        /// <returns>true if the animal has been registered (added to the database) successfully, false otherwise.</returns>
        public static bool registerAnimal(RACE race, CLIENT owner, string name, string birthYear, int height, int weight, bool isMale)
        {
            ANIMAL animalToRegister = new ANIMAL(race, owner, name, birthYear, height, weight, isMale);
            bool isAlreadyRegistered = AnimalController.isAlreadyRegistered(animalToRegister);

            if (!isAlreadyRegistered)
            {
                PT4_S4P2C_E2Entities dbContext = new PT4_S4P2C_E2Entities();
                dbContext.ANIMAL.Add(animalToRegister);
                dbContext.SaveChanges();
            }

            return !isAlreadyRegistered;
        }
    }
}
