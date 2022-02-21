using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.UI.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceHome : AInterface
    {
        MainWindow window;

        Header header;
        Footer footer;

        UIButton manageCongé, manageMaladie, manageVentes, stats, manageConsultation, manageStock;
        Label incEvent;
        TextBox events;

        public InterfaceHome(MainWindow window)
        {
            this.window = window;
            header = new Header(window);
            footer = new Footer(window);

        }
        public override void load()
        {
            header.load("Mauxnimale - Page d'accueil");
            footer.load();
            generateButton();
            generateLabel();
            generateTextBox();
        }

        public void generateLabel()
        {
            incEvent = new Label();
            incEvent.Text = "Evenements à venir";
            incEvent.TextAlign = ContentAlignment.MiddleLeft;
            incEvent.Font = new System.Drawing.Font("Poppins", window.Height * 2 / 100);
            incEvent.ForeColor = Color.Black;
            incEvent.Size = new System.Drawing.Size(window.Width * 3 / 10, window.Height * 1 / 10);
            incEvent.Location = new Point(window.Width * 25 / 1000, window.Height * 2 / 10);
            window.Controls.Add(incEvent);
        }

        public void generateTextBox()
        {
            events = new TextBox();
            events.Text = "";
            events.Font = new System.Drawing.Font("Poppins", window.Height * 2 / 100);
            events.ForeColor = Color.Black;
            events.BackColor = Color.White;
            events.ReadOnly = true;
            events.Multiline = true;
            events.Size = new System.Drawing.Size(window.Width * 25 / 100, window.Height * 45 / 100);
            events.Location = new Point(window.Width * 25 / 1000, window.Height * 3 / 10);
            window.Controls.Add(events);
        }

        public void generateButton()
        {
            manageStock = new UIButton(UIColor.DARKBLUE, "Gestion du stock", window.Width / 4);
            manageStock.Location = new System.Drawing.Point(window.Width * 325 / 1000, window.Height * 225 / 1000);
            window.Controls.Add(manageStock);

            manageMaladie = new UIButton(UIColor.DARKBLUE, "Gestion des maladies", window.Width / 4);
            manageMaladie.Location = new System.Drawing.Point(window.Width * 6 / 10, window.Height * 225 / 1000);
            window.Controls.Add(manageMaladie);

            manageVentes = new UIButton(UIColor.DARKBLUE, "Gestion des ventes", window.Width / 4);
            manageVentes.Location = new System.Drawing.Point(window.Width * 325 / 1000, window.Height * 425 / 1000);
            window.Controls.Add(manageVentes);

            manageConsultation = new UIButton(UIColor.DARKBLUE, "Gestion des consultations", window.Width / 4);
            manageConsultation.Location = new System.Drawing.Point(window.Width * 6 / 10, window.Height * 425 / 1000);
            window.Controls.Add(manageConsultation);

            manageCongé = new UIButton(UIColor.DARKBLUE, "Gestion des congés", window.Width / 4);
            manageCongé.Location = new System.Drawing.Point(window.Width * 325 / 1000, window.Height * 625 / 1000);
            window.Controls.Add(manageCongé);

            stats = new UIButton(UIColor.DARKBLUE, "Statistiques", window.Width / 4);
            stats.Location = new System.Drawing.Point(window.Width * 6 / 10, window.Height * 625 / 1000);
            window.Controls.Add(stats);

            manageCongé.Click += new EventHandler(manageCongéClick);
            manageStock.Click += new EventHandler(manageStockClick);
            manageMaladie.Click += new EventHandler(manageMaladieClick);
            manageVentes.Click += new EventHandler(manageVentesClick);
            manageConsultation.Click += new EventHandler(manageConsultationClick);
            stats.Click += new EventHandler(statsClick);
        }

        #region eventHandler
        public void manageCongéClick(object sender, EventArgs e)
        {
            //Partie BD : Inscrire l'utilisateur en vérifiant qu'il n'existe pas déja
            window.Controls.Clear();
            //form.changerClasse(new Interface...());
        }

        public void manageStockClick(object sender, EventArgs e)
        {
            //Partie BD : Inscrire l'utilisateur en vérifiant qu'il n'existe pas déja
            window.Controls.Clear();
            //form.changerClasse(new Interface...());
        }

        public void manageMaladieClick(object sender, EventArgs e)
        {
            //Partie BD : Inscrire l'utilisateur en vérifiant qu'il n'existe pas déja
            window.Controls.Clear();
            //form.changerClasse(new Interface...());
        }

        public void manageVentesClick(object sender, EventArgs e)
        {
            //Partie BD : Inscrire l'utilisateur en vérifiant qu'il n'existe pas déja
            window.Controls.Clear();
            //form.changerClasse(new Interface...());
        }

        public void manageConsultationClick(object sender, EventArgs e)
        {
            //Partie BD : Inscrire l'utilisateur en vérifiant qu'il n'existe pas déja
            window.Controls.Clear();
            //form.changerClasse(new Interface...());
        }

        public void statsClick(object sender, EventArgs e)
        {
            //Partie BD : Inscrire l'utilisateur en vérifiant qu'il n'existe pas déja
            window.Controls.Clear();
            //form.changerClasse(new Interface...());
        }
        #endregion
    }
}
