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
        /// <param name="reason"></param>
        public static void AddCare(string reason)
        {
            SOIN newCare = new SOIN
            {
                DESCRIPTION = reason
            };
            DbContext.get().SOIN.Add(newCare);
            DbContext.get().SaveChanges();
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

        public static void AddDisease(string name)
        {
            MALADIE m = new MALADIE
            {
                NOMMALADIE = name
            };
            DbContext.get().MALADIE.Add(m);
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
    }
}
