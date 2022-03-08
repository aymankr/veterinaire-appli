using Mauxnimale_CE2.api.entities;
using System;

namespace Mauxnimale_CE2.api.controllers
{
    public class VacationController
    {
        /**
         * Add a day for an employee, a day working
         */
        public static void addDayEmployee(SALARIE employee, JOURNEE day, string startHour, string endHour)
        {
            JOURNEE_SALARIE j = new JOURNEE_SALARIE();
            j.SALARIE = employee;
            j.JOURNEE = day;
            j.HEUREDEBUT = TimeSpan.Parse(startHour);
            j.HEUREFIN = TimeSpan.Parse(endHour);
            j.CONGE = false;
            DbContext.get().JOURNEE_SALARIE.Add(j);
            DbContext.get().SaveChanges();
        }

        /**
         * Remove a day working for an employee
         */
        public static void removeDayEmployee(JOURNEE_SALARIE dayEmployee)
        {
            DbContext.get().JOURNEE_SALARIE.Remove(dayEmployee);
            DbContext.get().SaveChanges();
        }

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
