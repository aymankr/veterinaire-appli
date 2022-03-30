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
                if(newCare == care)
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

        public static ICollection<SOIN> AllCares()
        {
            return DbContext.get().SOIN.ToList();
        }

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
                if (currentDisease == testedDisease)
                {
                    return true;
                }
            }
            return false;
        }

        public static void RemoveDisease(MALADIE disease)
        {
            disease.LIEN_MALADIE.Clear();
            DbContext.get().MALADIE.Remove(disease);
            DbContext.get().SaveChanges();
        }

        public static ICollection<MALADIE> AllDiseases()
        {
            return DbContext.get().MALADIE.ToList();
        }

        internal static ICollection<MALADIE> SearchDiseasesByName(string name)
        {
            var diseases = from d in DbContext.get().MALADIE
                           where d.NOMMALADIE.Contains(name)
                           select d;
            return diseases.ToList();
        }

        internal static ICollection<SOIN> SearchCaresByNames(string name)
        {
            var cares = from c in DbContext.get().SOIN
                           where c.DESCRIPTION.Contains(name)
                           select c;
            return cares.ToList();
        }

        internal static ICollection<SOIN> ListCaresByDisease(MALADIE disease)
        {
            return disease.SOIN.ToList();
        }

        internal static IEnumerable<SOIN> ResearchCareByName(string name)
        {
            var result = from c in DbContext.get().SOIN
                         where c.DESCRIPTION.Contains(name)
                         select c;
            return result.ToList();
        }
    }
}
