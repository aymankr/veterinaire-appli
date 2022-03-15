using Mauxnimale_CE2.api.entities;
using System.Linq;
using System;

namespace Mauxnimale_CE2.api.controllers
{
    public class DayController
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

        public static void addDay(DateTime date)
        {
            JOURNEE day = new JOURNEE();
            day.DATE = date;
            DbContext.get().JOURNEE.Add(day);
            DbContext.get().SaveChanges();
        }

        public static JOURNEE getDay(DateTime date)
        {
            JOURNEE day = (from d in DbContext.get().JOURNEE
                           where DateTime.Equals(d.DATE, date)
                           select d).FirstOrDefault();
            return day;
        }
    }
}
