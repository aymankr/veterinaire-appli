using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mauxnimale_CE2.api;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            PT4_S4P2C_E2Entities1 p = Tools.getDatabase();
            AppointmentController.addAppointment(p.TYPE_RDV.First(), p.ORDONNANCE.First(), p.CLIENT.First(), p.ANIMAL.First(), p.JOURNEE.First(), "aa", "10:00:00", "12:00:00");
            Application.Run(new MainWindow());
        }
    }
}
