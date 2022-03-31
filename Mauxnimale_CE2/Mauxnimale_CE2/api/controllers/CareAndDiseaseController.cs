using System;
using System.Collections.Generic;
using System.Linq;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2.api.controllers
{
    public static class CareAndDiseaseController
    {
        /// <summary>
        /// Méthode pour ajouter un soin dans la base de données, avec une raison
        /// </summary>
        /// <param name="description"></param>
        public static bool AddCare(string description)
        {
            SOIN newCare = new SOIN
            {
                DESCRIPTION = description
            };
            if (!CareAlreadyExist(newCare))
            {
                DbContext.get().SOIN.Add(newCare);
                DbContext.get().SaveChanges();
                return true;
            } else
            {
                return false;
            }
        }

        /// <summary>
        /// Permet de savoir si le nouveau soin est déjà dans la base.
        /// </summary>
        /// <param name="newCare">Le nouveau soin à tester</param>
        /// <returns>Vrai si il est déjà présent, faux sinon</returns>
        private static bool CareAlreadyExist(SOIN newCare)
        {
            foreach(SOIN care in DbContext.get().SOIN)
            {
                if(newCare.DESCRIPTION == care.DESCRIPTION)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Supprimer un soin de la bd
        /// </summary>
        /// <param name="currentCare"></param>
        public static void RemoveCare(SOIN currentCare)
        {
            currentCare.MALADIE.Clear();
            DbContext.get().SOIN.Remove(currentCare);
            DbContext.get().SaveChanges();
        }

        /// <summary>
        /// Liste de tous les soins.
        /// </summary>
        /// <returns>La liste de tous les soins</returns>
        public static ICollection<SOIN> AllCares()
        {
            return DbContext.get().SOIN.ToList();
        }

        /// <summary>
        /// Permet d'ajouter une maladie à la base.
        /// </summary>
        /// <param name="name">Nom de la nouvelle maladie</param>
        /// <param name="associatedCares">Les soins qui lui sont associés</param>
        /// <returns></returns>
        public static bool AddDisease(string name, ICollection<SOIN> associatedCares)
        {
            MALADIE m = new MALADIE
            {
                NOMMALADIE = name
            };
            if(associatedCares.Any())
            {
                m.SOIN = associatedCares;
            }
            if (!DiseasesAlreadyExist(m))
            {
                DbContext.get().MALADIE.Add(m);
                DbContext.get().SaveChanges();
                return true;
            } else
            {
                return false;
            }
        }

        /// <summary>
        /// Permet de savoir si une maladie est déjà présete dans la base de données
        /// </summary>
        /// <param name="m">La maladie a tester</param>
        /// <returns>Vrai si la maladie existe déjà, faux sinon</returns>
        private static bool DiseasesAlreadyExist(MALADIE testedDisease)
        {
            foreach(MALADIE currentDisease in DbContext.get().MALADIE){
                if (currentDisease.NOMMALADIE == testedDisease.NOMMALADIE && currentDisease.SOIN == testedDisease.SOIN)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Permet de supprimer une maladie de la base.
        /// </summary>
        /// <param name="disease">La maladie a enlever</param>
        public static void RemoveDisease(MALADIE disease)
        {
            disease.LIEN_MALADIE.Clear();
            DbContext.get().MALADIE.Remove(disease);
            DbContext.get().SaveChanges();
        }

        /// <summary>
        /// Récupère toutes les maladies da la base.
        /// </summary>
        /// <returns>La liste de toutes les maladies</returns>
        public static ICollection<MALADIE> AllDiseases()
        {
            return DbContext.get().MALADIE.ToList();
        }

        /// <summary>
        /// Permet de rechercher une maladie en fonction de son nom.
        /// </summary>
        /// <param name="name">Partie du nom de la maladie</param>
        /// <returns>La liste des maladies qui ont le nom recherché dans leur nom</returns>
        internal static ICollection<MALADIE> SearchDiseasesByName(string name)
        {
            var diseases = from d in DbContext.get().MALADIE
                           where d.NOMMALADIE.Contains(name)
                           select d;
            return diseases.ToList();
        }

        /// <summary>
        /// Permet de rechercher un soin en fonction de son nom.
        /// </summary>
        /// <param name="name">Partie du nom du soin</param>
        /// <returns>La liste des soins qui ont le nom recherché dans leur nom</returns>
        internal static ICollection<SOIN> SearchCaresByNames(string name)
        {
            var cares = from c in DbContext.get().SOIN
                           where c.DESCRIPTION.Contains(name)
                           select c;
            return cares.ToList();
        }

        /// <summary>
        /// Permet de récupérer les soins liés à une maladie donnée.
        /// </summary>
        /// <param name="disease">Maladie</param>
        /// <returns>La liste des soins associés à cette maladie</returns>
        internal static ICollection<SOIN> ListCaresByDisease(MALADIE disease)
        {
            return disease.SOIN.ToList();
        }

        internal static bool UpdateDisease(MALADIE disease, string name, List<SOIN> cares)
        {

            MALADIE tempDisease = new MALADIE()
            {
                NOMMALADIE = name,
                SOIN = cares
            };
            if (!DiseasesAlreadyExist(tempDisease))
            {
                disease.NOMMALADIE = name;
                disease.SOIN = cares;
                DbContext.get().SaveChanges();
                return true;
            } else
            {
                return false;
            }
        }

        internal static bool UpdateCare(SOIN care, string name)
        {
            SOIN tempCare = new SOIN()
            {
                DESCRIPTION = name,
            };
            if (!CareAlreadyExist(tempCare))
            {
                care.DESCRIPTION = name;
                DbContext.get().SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
