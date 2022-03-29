using System;
using System.Collections.Generic;
using Mauxnimale_CE2.api.entities;
using System.Linq;

namespace Mauxnimale_CE2.api.controllers
{
    /// <summary>
    /// Static class implementing methods related to animals management.
    /// </summary>
    public static class AnimalController
    {
        /// <summary>
        /// Vérifie si l'animal donné est déjà enregistré dans la base.
        /// </summary>
        /// <param name="animalToVerify">L'animal testé</param>
        /// <returns>Vrai si l'animal est déjà enregistré, faux sinon</returns>
        public static bool IsAlreadyRegistered(ANIMAL animalToVerify)
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
        /// Méthode permettant d'ajouter à la base de données un animal.
        /// </summary>
        /// <param name="breed">La race de l'animal</param>
        /// <param name="owner">Le propriétaire de l'animal</param>
        /// <param name="name">Le nom de l'animalS</param>
        /// <param name="birthYear">L'année de naissance de l'animal</param>
        /// <param name="height">La taille de l'animal</param>
        /// <param name="weight">Le poids de l'animal</param>
        /// <param name="isMale">Le genre de l'animal, vrai un male et faux une femelle</param>
        /// <returns>Vrai si l'animal à bien était ajouté à la base de données, faux sinon</returns>
        public static bool RegisterAnimal(RACE breed, CLIENT owner, string name, string birthYear, int height, int weight, bool isMale)
        {
            ANIMAL animalToRegister = new ANIMAL
            {
                RACE = breed,
                IDRACE = breed.IDRACE,
                CLIENT = owner,
                IDCLIENT = owner.IDCLIENT,
                NOM = name,
                ANNEENAISSANCE = birthYear,
                TAILLE = height,
                POIDS = weight,
                ESTMALE = isMale
            };

            if (!IsAlreadyRegistered(animalToRegister))
            {
                DbContext.get().ANIMAL.Add(animalToRegister);
                DbContext.get().SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
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
                List<RENDEZ_VOUS> appointments = animalToRemove.RENDEZ_VOUS.ToList();
                foreach(RENDEZ_VOUS appointment in appointments)
                {
                    AppointmentController.deleteAppointment(appointment);
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
            return DbContext.get().ESPECE.ToList();
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

        internal static bool UpdateAnimal(ANIMAL animal, RACE newBreed, CLIENT newOwner, string name, string birthYear, int size, int weight, bool isMale)
        {
            try
            {
                animal.RACE = newBreed;
                animal.CLIENT = newOwner;
                animal.NOM = name;
                animal.ANNEENAISSANCE = birthYear;
                animal.TAILLE = size;
                animal.POIDS = weight;
                animal.ESTMALE = isMale;
                DbContext.get().SaveChanges();
                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Méthode permettant de rechercher des espèse par leur nom
        /// </summary>
        /// <param name="specieName">Le nom de l'espèce recherchée</param>
        /// <returns>La liste des espèce comportant ce nom dans leur nom</returns>
        internal static ICollection<ESPECE> ResearchSpeciesByName(string specieName)
        {
            var species = from s in DbContext.get().ESPECE
                         where s.NOMESPECE.Contains(specieName)
                          select s;
            return species.ToList();
        }

        internal static bool UpdateSpecie(ESPECE specie, string name)
        {
            if (!SpecieIsAlreadyRegistered(specie.NOMESPECE))
            {
                specie.NOMESPECE = name;
                DbContext.get().SaveChanges();
                return true;
            } else
            {
                return false;
            }
            
        }

        internal static ICollection<RACE> ResearchBreedByName(ESPECE specie , string breedName)
        {
            if (specie != null)
            {
                var breeds = from b in DbContext.get().RACE
                             where b.ESPECE.NOMESPECE == specie.NOMESPECE
                             where b.NOMRACE.Contains(breedName)
                             select b;
                return breeds.ToList();
            }
            else {
                var breeds = from b in DbContext.get().RACE
                             where b.NOMRACE.Contains(breedName)
                             select b;
                return breeds.ToList();
            }
            
        }


        /// <summary>
        /// Méthode permettant de récupérer toutes les races de la base de données
        /// </summary>
        /// <returns>Une collection contenant toutes les races</returns>
        internal static ICollection<RACE> AllBreeds()
        {
            return DbContext.get().RACE.ToList();
        }

        /// <summary>
        /// Méthode permettant de modifier une race.
        /// </summary>
        /// <param name="breed">Race a modifier</param>
        /// <param name="specie">Nouvelle espèce de la race</param>
        /// <param name="nameBreed">Nouveau nom de la race</param>
        /// <returns>Vrai si la race a bien été modifiée, faux sinon</returns>
        internal static bool UpdateBreed(RACE breed, ESPECE specie, string nameBreed)
        {
            if(!BreedIsAlreadyRegistered(specie.NOMESPECE, nameBreed))
            {
                breed.ESPECE = specie;
                breed.NOMRACE = nameBreed;
                DbContext.get().SaveChanges();
                return true;
            } else
            {
                return false;
            }
        }

        /// <summary>
        /// Méthode permettant de supprimer une espèce et toutes les races qui lui sont liées.
        /// </summary>
        /// <param name="specieToBeDeleted">L'espèce a supprimer</param>
        /// <returns>Vrai si l'espèce a été supprimée avec succès, faux sinon</returns>
        internal static bool DeleteSpecie(ESPECE specieToBeDeleted)
        {
            try
            {
                if (specieToBeDeleted.RACE.Any())
                {
                    List<RACE> breeds = specieToBeDeleted.RACE.ToList();
                    foreach(RACE breed in breeds)
                    {
                        DeleteBreed(breed);
                    }
                }
                DbContext.get().ESPECE.Remove(specieToBeDeleted);
                DbContext.get().SaveChanges();
                return true;
            }catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Méthode permettant de supprimer une race et tout les animaux qui lui sont liés.
        /// </summary>
        /// <param name="breedToBeDeleted">Race a supprimer</param>
        /// <returns>Vrai si la  race a été supprimée avec succès, faux sinon</returns>
        public static bool DeleteBreed(RACE breedToBeDeleted)
        {
            try
            {
                if (breedToBeDeleted.ANIMAL.Any())
                {
                    List<ANIMAL> animals = breedToBeDeleted.ANIMAL.ToList();
                    foreach(ANIMAL animal in animals)
                    {
                        RemoveAnimal(animal);
                    }
                }
                DbContext.get().RACE.Remove(breedToBeDeleted);
                DbContext.get().SaveChanges();
                return true;
            }catch (Exception)
            {
                return false;
            }
        }
    }
}
