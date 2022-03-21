using System;
using System.Windows.Forms;
using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.api.controllers;
using Mauxnimale_CE2.api;

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
            Application.Run(new MainWindow());

        }
    }
}
