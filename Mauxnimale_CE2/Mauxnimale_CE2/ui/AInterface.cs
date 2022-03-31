using System.Windows.Forms;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2
{
    public abstract class AInterface
    {
        protected MainWindow window;
        protected SALARIE user;

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="window"></param>
        /// <param name="user"></param>
        public AInterface(MainWindow window, SALARIE user)
        {
            this.user = user;
            this.window = window;
        }

        /// <summary>
        /// Fonction permettant de générer l'interface
        /// </summary>
        public abstract void load();

        /// <summary>
        /// Permet de modifier la taille de la base
        /// </summary>
        public void updateSize()
        {
            if (window.WindowState != FormWindowState.Minimized)
            {
                window.Controls.Clear();
                load();
            }
        }
    }
}
