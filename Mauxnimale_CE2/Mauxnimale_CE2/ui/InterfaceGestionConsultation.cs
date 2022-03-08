using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
        using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.ui.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mauxnimale_CE2.api.entities;
using System.Windows.Forms;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceGestionConsultation : AInterface
    {
        Salarie

        MainWindow window;

        Header header;
        Footer footer;

        UIButton modifConsult, createOrdonance, deleteConsult, createConsult;
        UIRoundButton back;
        MonthCalendar calendar;

        Label incEvent;
        ListBox consultationsDuJour, infosConsultation;

        public InterfaceGestionConsultation(MainWindow window)
        {
            this.window = window;
            header = new Header(window);
            footer = new Footer(window);

        }
        public override void load()
        {
            header.load("Mauxnimale - Gestion des Consultations");
            footer.load();
            generateButton();
            generateLabel();
            generateListBox();
        }

        public void generateLabel()
        {
            incEvent = new Label();
            incEvent.Text = "Consultations à venir";
            incEvent.TextAlign = ContentAlignment.MiddleLeft;
            incEvent.Font = new Font("Poppins", window.Height * 2 / 100);
            incEvent.ForeColor = UIColor.DARKBLUE;
            incEvent.Size = new Size(window.Width * 3 / 10, window.Height * 1 / 10);
            incEvent.Location = new Point(window.Width * 25 / 1000, window.Height / 10);
            window.Controls.Add(incEvent);
        }

        public void generateListBox()
        {
            consultationsDuJour = new ListBox();
            consultationsDuJour.Text = "";
            consultationsDuJour.Font = new Font("Poppins", window.Height * 1 / 100);
            consultationsDuJour.ForeColor = Color.Black;
            consultationsDuJour.BackColor = Color.White;
            consultationsDuJour.Location = new Point(window.Width * 275 / 1000, window.Height * 2 / 10);
            consultationsDuJour.Size = new Size(window.Width * 20 / 100, window.Height * 45 / 100);
            window.Controls.Add(consultationsDuJour);

            infosConsultation = new ListBox();
            infosConsultation.Text = "";
            infosConsultation.Font = new Font("Poppins", window.Height * 1 / 100);
            infosConsultation.ForeColor = Color.Black;
            infosConsultation.BackColor = Color.White;
            infosConsultation.Location = new Point(window.Width * 500 / 1000, window.Height * 2 / 10);
            infosConsultation.Size = new Size(window.Width * 40 / 100, window.Height * 45 / 100);
            window.Controls.Add(infosConsultation);



            calendar = new MonthCalendar();
            calendar.Location = new Point(window.Width * 25 / 1000, window.Height * 2 / 10);
            calendar.Size = new Size(window.Width * 20 / 100, window.Height * 45 / 100);
            calendar.DateSelected += new DateRangeEventHandler(dateSelection);
            window.Controls.Add(calendar);
        }


        public void generateButton()
        {
            modifConsult = new UIButton(UIColor.DARKBLUE, "Modifier Consultation", window.Width * 3  / 20);
            modifConsult.Location = new Point(window.Width * 5 / 15, window.Height * 14 / 20);
            window.Controls.Add(modifConsult);

            createOrdonance = new UIButton(UIColor.DARKBLUE, "Créer ordonance", window.Width * 3 / 20);
            createOrdonance.Location = new Point(window.Width * 8 / 15, window.Height * 14 / 20);
            window.Controls.Add(createOrdonance);

            deleteConsult = new UIButton(UIColor.DARKBLUE, "Supprimer Consultation", window.Width * 3 / 20);
            deleteConsult.Location = new Point(window.Width * 11 / 15, window.Height * 14 / 20);
            window.Controls.Add(deleteConsult);

            createConsult = new UIButton(UIColor.DARKBLUE, "Créer Consultation", window.Width * 3 / 20);
            createConsult.Location = new Point(window.Width * 2 / 15, window.Height * 14 / 20);
            window.Controls.Add(createConsult);

            back = new UIRoundButton(window.Width / 20, "<");
            back.Location = new Point(window.Width * 9 / 10, window.Height / 10);
            window.Controls.Add(back);
            back.Click += new EventHandler(backClick);


            modifConsult.Click += new EventHandler(modifConsultClick);
            createOrdonance.Click += new EventHandler(createOrdonanceClick);
            deleteConsult.Click += new EventHandler(deleteConsultClick);
            createConsult.Click += new EventHandler(createConsultClick);
        }


        #region eventHandler

        private void dateSelection(object sender, EventArgs e)
        {
            consultationsDuJour.Items.Add("aujourd'hui on est le" + sender.ToString());
        }

        public void backClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceHome(window));
        }

        public void manageCompteClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            //form.changerClasse(new Interface...());
        }

        public void modifConsultClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            //form.changerClasse(new Interface...());
        }

        public void createOrdonanceClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            //form.changerClasse(new Interface...());
        }

        public void deleteConsultClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            //form.changerClasse(new Interface...());
        }

        public void createConsultClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            //form.changerClasse(new Interface...());
        }

        #endregion

        public override void updateSize()
        {
            window.Controls.Clear();
            this.load();
        }
    }
}



