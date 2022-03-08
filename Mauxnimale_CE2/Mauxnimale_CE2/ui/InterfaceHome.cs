using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.ui.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceHome : AInterface
    {
        MainWindow window;

        Header header;
        Footer footer;

        UIButton manageCongé, manageMaladie, manageVentes, stats, manageConsultation, manageStock;
        Button compte;
        Label incEvent;
        TextBox events;

        public InterfaceHome(MainWindow window, SALARIE s)
        {
            this.window = window;
            header = new Header(window);
            footer = new Footer(window);
            salarie = s;

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

            /* parti a finir lors de la création des fonctions API
            if(admin){*/
                manageCongé = new UIButton(UIColor.DARKBLUE, "Gestion des congés", window.Width / 4);
                manageCongé.Location = new System.Drawing.Point(window.Width * 325 / 1000, window.Height * 625 / 1000);
                window.Controls.Add(manageCongé);

                stats = new UIButton(UIColor.DARKBLUE, "Statistiques", window.Width / 4);
                stats.Location = new System.Drawing.Point(window.Width * 6 / 10, window.Height * 625 / 1000);
                window.Controls.Add(stats);
            //}

            compte = new Button();
            compte.FlatAppearance.BorderSize = 0;
            compte.Size = new Size(window.Width/10, window.Height/10);
            compte.Location = new System.Drawing.Point(window.Width * 87 / 100, window.Height * 15 / 100);
            window.Controls.Add(compte);

            compte.Paint += new PaintEventHandler(comptePaint);
            compte.Click += new EventHandler(manageCompteClick);
            manageCongé.Click += new EventHandler(manageCongéClick);
            manageStock.Click += new EventHandler(manageStockClick);
            manageMaladie.Click += new EventHandler(manageMaladieClick);
            manageVentes.Click += new EventHandler(manageVentesClick);
            manageConsultation.Click += new EventHandler(manageConsultationClick);
            stats.Click += new EventHandler(statsClick);
        }

        #region eventHandler

        protected void comptePaint(object sender, PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            g.DrawEllipse(new Pen(UIColor.ORANGE), 0, 0, compte.Size.Width, compte.Size.Height);
            g.FillEllipse(new SolidBrush(UIColor.ORANGE), 0, 0, compte.Size.Width, compte.Size.Height);
            g.DrawString("☺", new System.Drawing.Font("Roboto", (compte.Size.Width + compte.Size.Height) / 4), new SolidBrush(Color.White), compte.Width/4, compte.Height/200);

            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, compte.Size.Width, compte.Size.Height);
            compte.Region = new Region(path);


        }

        public void manageCompteClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            //form.changerClasse(new Interface...());
        }

        public void manageCongéClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceGestionCongé(window));
        }

        public void manageStockClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            //form.changerClasse(new Interface...());
        }

        public void manageMaladieClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            //form.changerClasse(new Interface...());
        }

        public void manageVentesClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            //form.changerClasse(new Interface...());
        }

        public void manageConsultationClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            //form.changerClasse(new Interface...());
        }

        public void statsClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceStatsPage(window));
        }
        #endregion

        public override void updateSize()
        {
            window.Controls.Clear();
            this.load();
        }
    }
}
