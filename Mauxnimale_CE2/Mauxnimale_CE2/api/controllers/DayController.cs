using Mauxnimale_CE2.api.entities;
using System.Linq;
using System;

namespace Mauxnimale_CE2.api.controllers
{
    public class DayController
    {
        /// <summary>
        /// Ajout d'une journée d'un salarié
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="day"></param>
        /// <param name="startHour"></param>
        /// <param name="endHour"></param>
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

        /// <summary>
        /// Supprimer la journée d'un salarié
        /// </summary>
        /// <param name="dayEmployee"></param>
        public static void removeDayEmployee(JOURNEE_SALARIE dayEmployee)
        {
            DbContext.get().JOURNEE_SALARIE.Remove(dayEmployee);
            DbContext.get().SaveChanges();
        }

        /// <summary>
        /// Poser un congé pour un salarié
        /// </summary>
        /// <param name="vacationDay"></param>
        /// <param name="isAVacation"></param>
        public static void setVacation(JOURNEE_SALARIE vacationDay, bool isAVacation)
        {
            DbContext.get().JOURNEE_SALARIE.Find(vacationDay.IDJOURNEESALARIE).CONGE = isAVacation;
            DbContext.get().SaveChanges();
        }

        /// <summary>
        /// Ajout d'une journée
        /// </summary>
        /// <param name="date"></param>
        public static void addDay(DateTime date)
        {
            JOURNEE day = new JOURNEE();
            day.DATE = date;
            DbContext.get().JOURNEE.Add(day);
            DbContext.get().SaveChanges();
        }

        /// <summary>
        /// Récupérer un objet de journée selon la date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static JOURNEE getDay(DateTime date)
        {
            JOURNEE day = (from d in DbContext.get().JOURNEE
                           where DateTime.Equals(d.DATE, date)
                           select d).FirstOrDefault();
            return day;
        }
    }
}
