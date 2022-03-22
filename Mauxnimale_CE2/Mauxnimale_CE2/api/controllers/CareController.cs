using Mauxnimale_CE2.api.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mauxnimale_CE2.api.controllers
{
    public static class CareController
    {
        /// <summary>
        /// Méthode pour ajouter un soin dans la base de données, avec une raison
        /// </summary>
        /// <param name="reason"></param>
        public static void addCare(string reason)
        {
            SOIN newCare = new SOIN();
            newCare.DESCRIPTION = reason;
            DbContext.get().SOIN.Add(newCare);
            DbContext.get().SaveChanges();
        }

        /// <summary>
        /// Supprimer un soin de la bd
        /// </summary>
        /// <param name="currentCare"></param>
        public static void removeCare(SOIN currentCare)
        {
            currentCare.MALADIE.Clear();
            DbContext.get().SOIN.Remove(currentCare);
            DbContext.get().SaveChanges();
        }
    }
}
