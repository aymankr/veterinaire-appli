using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Mauxnimale_CE2.api.entities;

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

            PT4_S4P2C_E2Entities dbContext = DbContext.get();
            dbContext.JOURNEE_SALARIE.Add(j);
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Supprimer la journée d'un salarié
        /// </summary>
        /// <param name="dayEmployee"></param>
        public static void removeDayEmployee(JOURNEE_SALARIE dayEmployee)
        {
            PT4_S4P2C_E2Entities dbContext = DbContext.get();
            dbContext.JOURNEE_SALARIE.Remove(dayEmployee);
            dbContext.SaveChanges();
        }

        /// <param name="day">Le jour concerné</param>
        /// <param name="employee">L'employé concerné</param>
        /// <returns>La journée du salarié correspondante si elle existe, null sinon.</returns>
        public static JOURNEE_SALARIE getDayEmployee(JOURNEE day, SALARIE employee)
        {
            List<JOURNEE_SALARIE> daysFound = DbContext.get().JOURNEE_SALARIE.Where(d => 
                                              d.JOURNEE.IDJOURNEE == day.IDJOURNEE && d.SALARIE.IDCOMPTE == employee.IDCOMPTE).ToList();
            if (daysFound.Count > 0)
                return daysFound[0];
            return null;

        }

        /// <summary>
        /// Poser ou supprimer un congé pour un salarié
        /// </summary>
        /// <param name="vacationDay"></param>
        /// <param name="isAVacation"></param>
        public static void setVacation(JOURNEE_SALARIE vacationDay, bool isAVacation)
        {
            PT4_S4P2C_E2Entities dbContext = DbContext.get();
            dbContext.JOURNEE_SALARIE.Find(vacationDay.IDJOURNEESALARIE).CONGE = isAVacation;
            dbContext.SaveChanges();
        }

        /// <summary>
        /// </summary>
        /// <param name="employee">Le salarié dont on veut obtenir les congés.</param>
        /// <returns>Une liste contenant les jours de congés posés du salarié.</returns>
        public static List<JOURNEE_SALARIE> getEmployeeVacations(SALARIE employee) => 
            DbContext.get().JOURNEE_SALARIE.Where(day => day.SALARIE.IDCOMPTE == employee.IDCOMPTE && day.CONGE).ToList();

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
