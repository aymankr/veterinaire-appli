using Mauxnimale_CE2.api.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mauxnimale_CE2.api.controllers
{
    internal class JourneeController
    {

        public static void addJournee(DateTime date)
        {
            JOURNEE journee = new JOURNEE();
            journee.DATE = date;
            DbContext.get().JOURNEE.Add(journee);
            DbContext.get().SaveChanges(); 
        }

        public static JOURNEE getJOURNEE(DateTime date)
        {
            JOURNEE day = (from d in DbContext.get().JOURNEE
                           where DateTime.Equals(d.DATE, date)
                           select d).FirstOrDefault();
            return day;
        }
    }
}
