using Mauxnimale_CE2.api.entities;
using System;

namespace Mauxnimale_CE2.api.controllers
{
    public class VacationController
    {
        /**
         * Set a vacation to true or false
         */
        public static void setVacation(JOURNEE_SALARIE vacationDay, bool isAVacation)
        {
            DbContext.get().JOURNEE_SALARIE.Find(vacationDay.IDJOURNEESALARIE).CONGE = isAVacation;
            DbContext.get().SaveChanges();
        }
    }
}
