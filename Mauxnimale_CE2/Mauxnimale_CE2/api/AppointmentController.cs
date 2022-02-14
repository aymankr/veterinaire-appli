﻿using System;
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
        public static void addAppointment(TYPE_RDV appointmentType, ORDONNANCE prescription, CLIENT costumer, ANIMAL animal, JOURNEE day, string reason, string startHour, string endHour)
        {
            RENDEZ_VOUS newAppointment = new RENDEZ_VOUS();
            newAppointment.CLIENT = costumer;
            newAppointment.ANIMAL.Add(animal); // a costumer can have many animals
            newAppointment.JOURNEE = day;
            newAppointment.HEUREDEBUT = TimeSpan.Parse(startHour);
            newAppointment.HEUREFIN = TimeSpan.Parse(endHour);
            newAppointment.RAISON = reason;
            newAppointment.ORDONNANCE.Add(prescription); // many animals, so there are many prescriptions
            Tools.getDatabase().RENDEZ_VOUS.Add(newAppointment);
            Tools.getDatabase().SaveChanges();
        }

        /**
         * Delete an appointment from the database
         */
        public static void deleteAppointment(RENDEZ_VOUS currentAppointment)
        {
            // current appointment is the selected item
            Tools.getDatabase().RENDEZ_VOUS.Remove(currentAppointment);
            Tools.getDatabase().SaveChanges();
        }
    }
}
