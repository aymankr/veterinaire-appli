using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.ui.components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mauxnimale_CE2.api.entities;
using System.Windows.Forms;
using System.Collections;
using Mauxnimale_CE2.api;
using Mauxnimale_CE2.api.controllers;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceAppointmentCreation : AInterface
    {
        MainWindow window;


        Header header;
        Footer footer;

        UIButton modifConsult, newClient, deleteConsult, createConsult;
        UIRoundButton back;
        MonthCalendar calendar;



        Label calendarLabel, clientLabel, animalLabel, appointmentTypeLabel, timeLabel, descriptionLabel;

        ComboBox clientComboBox, animalComboBox, appointmentTypeComboBox;
        TextBox descriptionTexBox;


        public InterfaceAppointmentCreation(MainWindow window, SALARIE s)
        {
            this.window = window;
            user = s;
            header = new Header(window);
            footer = new Footer(window, user);

        }
        public override void load()
        {
            header.load("Mauxnimale - Gestion des Consultations");
            footer.load();
            generateButton();
            generateLabels();
            generateListBox();
        }

        public void generateLabels()
        {
            calendarLabel = new Label();
            calendarLabel.Text = "Sélectionnez une date";
            calendarLabel.TextAlign = ContentAlignment.MiddleLeft;
            calendarLabel.Font = new Font("Poppins", window.Height * 2 / 100);
            calendarLabel.ForeColor = UIColor.DARKBLUE;
            calendarLabel.Size = new Size(window.Width * 3 / 10, window.Height * 1 / 10);
            calendarLabel.Location = new Point(window.Width * 25 / 1000, window.Height / 10);

            clientLabel = new Label();
            clientLabel.Text = "Choisissez un Client";
            clientLabel.TextAlign = ContentAlignment.MiddleLeft;
            clientLabel.Font = new Font("Poppins", window.Height * 2 / 100);
            clientLabel.ForeColor = UIColor.DARKBLUE;
            clientLabel.Size = new Size(window.Width * 3 / 10, window.Height * 1 / 20);
            clientLabel.Location = new Point(window.Width * 350 / 1000, window.Height * 5 / 40);

            animalLabel = new Label();
            animalLabel.Text = "Choisissez un Animal";
            animalLabel.TextAlign = ContentAlignment.MiddleLeft;
            animalLabel.Font = new Font("Poppins", window.Height * 2 / 100);
            animalLabel.ForeColor = UIColor.DARKBLUE;
            animalLabel.Size = new Size(window.Width * 3 / 10, window.Height * 1 / 20);
            animalLabel.Location = new Point(window.Width * 650 / 1000, window.Height * 5 / 40);

            appointmentTypeLabel = new Label();
            appointmentTypeLabel.Text = "Choisissez le type du rendez-vous";
            appointmentTypeLabel.TextAlign = ContentAlignment.MiddleLeft;
            appointmentTypeLabel.Font = new Font("Poppins", window.Height * 2 / 100);
            appointmentTypeLabel.ForeColor = UIColor.DARKBLUE;
            appointmentTypeLabel.Size = new Size(window.Width * 3 / 10, window.Height * 1 / 20);
            appointmentTypeLabel.Location = new Point(window.Width * 350 / 1000, window.Height * 10 / 40);

            timeLabel = new Label();
            timeLabel.Text = "Choisissez l'heure du rendez-vous";
            timeLabel.TextAlign = ContentAlignment.MiddleLeft;
            timeLabel.Font = new Font("Poppins", window.Height * 2 / 100);
            timeLabel.ForeColor = UIColor.DARKBLUE;
            timeLabel.Size = new Size(window.Width * 3 / 10, window.Height * 1 / 20);
            timeLabel.Location = new Point(window.Width * 650 / 1000, window.Height * 10/ 40);

            descriptionLabel = new Label();
            descriptionLabel.Text = "Description :";
            descriptionLabel.TextAlign = ContentAlignment.MiddleLeft;
            descriptionLabel.Font = new Font("Poppins", window.Height * 2 / 100);
            descriptionLabel.ForeColor = UIColor.DARKBLUE;
            descriptionLabel.Size = new Size(window.Width * 3 / 10, window.Height * 1 / 10);
            descriptionLabel.Location = new Point(window.Width * 350 / 1000, window.Height *7 / 20);


            window.Controls.Add(calendarLabel);
            window.Controls.Add(clientLabel);
            window.Controls.Add(animalLabel);
            window.Controls.Add(appointmentTypeLabel);
            window.Controls.Add(timeLabel);
            window.Controls.Add(descriptionLabel);
        }

        public void generateListBox()
        {
            clientComboBox = new ComboBox();
            clientComboBox.Size = new Size(window.Width * 20/100, window.Height * 3 / 20);
            clientComboBox.Location = new Point(window.Width * 350 / 1000, window.Height * 8 / 40);

            animalComboBox = new ComboBox();
            animalComboBox.Size = new Size(window.Width * 20 / 100, window.Height * 3 / 20);
            animalComboBox.Location = new Point(window.Width * 650 / 1000, window.Height * 8 / 40);


            appointmentTypeComboBox = new ComboBox();
            appointmentTypeComboBox.Size = new Size(window.Width * 20 / 100, window.Height * 6 / 20);
            appointmentTypeComboBox.Location = new Point(window.Width * 350 / 1000, window.Height * 13 / 40);


            calendar = new MonthCalendar();
            calendar.Location = new Point(window.Width * 25 / 1000, window.Height * 2 / 10);
            calendar.Size = new Size(window.Width * 20 / 100, window.Height * 45 / 100);
            calendar.DateSelected += new DateRangeEventHandler(dateSelection);
            
            
            window.Controls.Add(calendar);
            window.Controls.Add(clientComboBox);
            window.Controls.Add(appointmentTypeComboBox);
            window.Controls.Add(animalComboBox);
        }


        public void generateButton()
        {
            modifConsult = new UIButton(UIColor.DARKBLUE, "Modifier Consultation", window.Width * 3 / 20);
            modifConsult.Location = new Point(window.Width * 5 / 15, window.Height * 14 / 20);
            window.Controls.Add(modifConsult);

            newClient = new UIButton(UIColor.DARKBLUE, "Créer ordonance", window.Width * 3 / 20);
            newClient.Location = new Point(window.Width * 8 / 15, window.Height * 14 / 20);
            window.Controls.Add(newClient);

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
            newClient.Click += new EventHandler(createOrdonanceClick);
            deleteConsult.Click += new EventHandler(deleteConsultClick);
            createConsult.Click += new EventHandler(createConsultClick);
        }


        #region eventHandler
       

        public void backClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceAppointmentManagment(window, user));
        }
        private void dateSelection(object sender, DateRangeEventArgs e)
        {
            throw new NotImplementedException();
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



