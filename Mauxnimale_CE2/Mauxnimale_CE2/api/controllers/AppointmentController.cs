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

        /**
         * Method to add an appointment to the database and to set attributes
         */
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

        /**
         * Delete an appointment from the database
         */
        public static void deleteAppointment(RENDEZ_VOUS currentAppointment)
        {
            // current appointment is the selected item
            currentAppointment.ANIMAL.Clear();
            DbContext.get().RENDEZ_VOUS.Remove(currentAppointment);
            DbContext.get().SaveChanges();
        }

        /**
         * Get appointments of this date
         */
        public static ICollection<RENDEZ_VOUS> getAppointmentsFromDate(DateTime date)
        {
            JOURNEE day = (JOURNEE)(from d in DbContext.get().JOURNEE 
                                    where d.DATE.Equals(date) 
                                    select d);  
            return day.RENDEZ_VOUS;
        }
    }
}
