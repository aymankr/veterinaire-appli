using System;
using System.Collections.Generic;
using Mauxnimale_CE2.api.entities;
using System.Linq;

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
            PT4_S4P2C_E2Entities dbContext = DbContext.get();

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
            int birthYearNumber = 0;
            if (birthYear.Length != 4 || !int.TryParse(birthYear, out birthYearNumber))
            {
                Console.WriteLine("Birth year: " + birthYear + " is not correct. Length has to be 4 and it has to be a number (a year).");
                return false;
            }

            ANIMAL animalToRegister = new ANIMAL();
            animalToRegister.RACE = race;
            animalToRegister.IDRACE = race.IDRACE;
            animalToRegister.CLIENT = owner;
            animalToRegister.IDCLIENT = owner.IDCLIENT;
            animalToRegister.NOM = name;
            animalToRegister.ANNEENAISSANCE = birthYear;
            animalToRegister.TAILLE = height;
            animalToRegister.POIDS = weight;
            animalToRegister.ESTMALE = isMale;
            
            bool isAlreadyRegistered = AnimalController.isAlreadyRegistered(animalToRegister);

            if (!isAlreadyRegistered)
            {
                Console.WriteLine("Animal doest not exists in database, we add it.");

                PT4_S4P2C_E2Entities dbContext = DbContext.get();
                dbContext.ANIMAL.Add(animalToRegister);
                dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("Animal already exists in database, can't add it.");
            }

            return !isAlreadyRegistered;
        }

        /// <summary>
        /// Permet de supprimer un animal de la base de données
        /// </summary>
        /// <param name="animalToRemove"></param>
        public static void RemoveAnimal(ANIMAL animalToRemove)
        {
            // Supprime les lien entre l'animal et les maladies
            if (animalToRemove.LIEN_MALADIE.Any())
            {
                var links = from l in DbContext.get().LIEN_MALADIE
                           where l.ANIMAL == animalToRemove
                           select l;
                foreach(LIEN_MALADIE lm in links.ToList())
                {
                    DbContext.get().LIEN_MALADIE.Remove(lm);
                }
            }
            // Supprime les rendez-vous de cet animal
            if (animalToRemove.RENDEZ_VOUS.Any())
            {
                var rdvs = from r in DbContext.get().RENDEZ_VOUS
                           where r.ANIMAL == animalToRemove
                           select r;
                foreach (RENDEZ_VOUS r in rdvs.ToList())
                {
                    DbContext.get().RENDEZ_VOUS.Remove(r);
                }
            }
            // Supprime les ordonnance lié à cet animal
            if (animalToRemove.ORDONNANCE.Any())
            {
                var prescriptions = from p in DbContext.get().ORDONNANCE
                                    where p.ANIMAL == animalToRemove
                                    select p;
                foreach (ORDONNANCE r in prescriptions.ToList())
                {
                    DbContext.get().ORDONNANCE.Remove(r);
                }
            }
            // On supprime l'animal
            animalToRemove.RENDEZ_VOUS.Clear();
            PT4_S4P2C_E2Entities dbContext = DbContext.get();
            dbContext.ANIMAL.Remove(animalToRemove);
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Méthode permettant de retourner toutes les espèces de la base de données.
        /// </summary>
        /// <returns>Une collection contenant toute les espèces</returns>
        public static ICollection<ESPECE> AllSpecies()
        {
            var species = from s in DbContext.get().ESPECE
                         select s;
            return species.ToList();
        }

        /// <summary>
        /// Méthode permettant de rechercher toutes les races liées à une espèce.
        /// </summary>
        /// <param name="specie">Espèce choisi</param>
        /// <returns>Une collection de race</returns>
        public static ICollection<RACE> BreedsWithSpecie(ESPECE specie)
        {
            var breeds = from b in DbContext.get().RACE
                         where b.ESPECE.NOMESPECE == specie.NOMESPECE
                         select b;
            return breeds.ToList();
        }

        /// <summary>
        /// Méthode permettant d'ajouter une espèce à la base de données.
        /// </summary>
        /// <param name="specieName">Nom de l'espèce à ajouter</param>
        /// <returns>Vrai si l'ajout s'est bien passé, faux sinon</returns>
        internal static bool AddSpecie(string specieName)
        {
            if (!SpecieIsAlreadyRegistered(specieName))
            {
                ESPECE specie = new ESPECE
                {
                    NOMESPECE = specieName
                };
                DbContext.get().ESPECE.Add(specie);
                DbContext.get().SaveChanges();
                return true;
            } else
            {
                return false;
            }
        } 

        /// <summary>
        /// Méthode permettant de savoir si une espèce existe déjà dans la base de donnée.
        /// </summary>
        /// <param name="specieName">Nom de l'espèce</param>
        /// <returns>Vrai si l'espèce existe déjà, faux sion</returns>
        private static bool SpecieIsAlreadyRegistered(string specieName)
        {
            var specie = from s in DbContext.get().ESPECE
                         where s.NOMESPECE == specieName
                         select s;
            bool isAlready = specie.Count() != 0;
            return isAlready;
        }

        /// <summary>
        /// Méthode permettant d'ajouter une race à la base de donnée
        /// </summary>
        /// <param name="specie">Nom de l'espèce de la race</param>
        /// <param name="breedName">Nom de la race</param>
        /// <returns>Vrai si la race à été ajoutée, faux sinon</returns>
        internal static bool AddBreed(ESPECE specie, string breedName)
        {
            if (!BreedIsAlreadyRegistered(specie.NOMESPECE, breedName))
            {
                RACE breed = new RACE
                {
                    NOMRACE = breedName,
                    ESPECE = specie
                };
                DbContext.get().RACE.Add(breed);
                DbContext.get().SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Méthode permettant de savoir si une race existe déjà
        /// </summary>
        /// <param name="breedName">Nom de la race à ajouter</param>
        /// <returns>Vrai si la race existe déjà, faux</returns>
        /// <exception cref="NotImplementedException"></exception>
        private static bool BreedIsAlreadyRegistered(string specie, string breedName)
        {
            var breeds = from s in DbContext.get().RACE
                         where s.NOMRACE == breedName
                         where s.ESPECE.NOMESPECE == specie
                         select s;
            bool isAlready = breeds.Count() != 0;
            return isAlready;
        }
    }
}
