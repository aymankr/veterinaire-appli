using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2.api
{
    public static class AppointmentController
    {

        /// <summary>
        /// Ajouter un rendez-vous avec ses informations
        /// </summary>
        /// <param name="appointmentType"></param>
        /// <param name="costumer"></param>
        /// <param name="animal"></param>
        /// <param name="day"></param>
        /// <param name="reason"></param>
        /// <param name="startHour"></param>
        /// <param name="endHour"></param>
        public static void addAppointment(TYPE_RDV appointmentType, CLIENT costumer, ANIMAL animal, JOURNEE day, string reason, string startHour, string endHour)
        {
            RENDEZ_VOUS newAppointment = new RENDEZ_VOUS();
            newAppointment.TYPE_RDV = appointmentType;
            newAppointment.CLIENT = costumer;
            newAppointment.ANIMAL.Add(animal); // a costumer can have many animals
            newAppointment.JOURNEE = day;
            newAppointment.HEUREDEBUT = TimeSpan.Parse(startHour);
            newAppointment.HEUREFIN = TimeSpan.Parse(endHour);
            newAppointment.RAISON = reason;
            DbContext.get().RENDEZ_VOUS.Add(newAppointment);
            DbContext.get().SaveChanges();
        }

        /// <summary>
        /// Supprimer une date de la bd
        /// </summary>
        /// <param name="currentAppointment"></param>
        public static void deleteAppointment(RENDEZ_VOUS currentAppointment)
        {
            // current appointment is the selected item
            currentAppointment.ANIMAL.Clear();
            DbContext.get().RENDEZ_VOUS.Remove(currentAppointment);
            DbContext.get().SaveChanges();
        }

       /// <summary>
       /// Récupérer un rendez vous selon la date
       /// </summary>
       /// <param name="date"></param>
       /// <returns></returns>
        public static ICollection<RENDEZ_VOUS> getAppointmentsFromDate(DateTime date)
        {
            JOURNEE day = (from d in DbContext.get().JOURNEE
                       where DateTime.Equals(d.DATE, date)
                       select d).FirstOrDefault();
            if (day != null)
            {
                return day.RENDEZ_VOUS;
            }
            return null;
        }

        /// <summary>
        /// Récupérere les animaux inclus dans le rendez-vous
        /// </summary>
        /// <param name="rdv"></param>
        /// <returns></returns>
        public static List<ANIMAL> getAnimalFromRDV(RENDEZ_VOUS rdv)
        {

            return rdv.ANIMAL.ToList();
        }
    }
}
