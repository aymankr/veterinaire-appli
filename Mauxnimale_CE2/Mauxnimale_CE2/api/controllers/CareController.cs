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
        /**
         * Method to add a care
         */
        public static void addCare(string reason)
        {
            SOIN newCare = new SOIN();
            newCare.DESCRIPTION = reason;
            DbContext.get().SOIN.Add(newCare);
            DbContext.get().SaveChanges();
        }

        /**
         * Delete a care from the database
         */
        public static void removeCare(SOIN currentCare)
        {
            currentCare.MALADIE.Clear();
            DbContext.get().SOIN.Remove(currentCare);
            DbContext.get().SaveChanges();
        }
    }
}
