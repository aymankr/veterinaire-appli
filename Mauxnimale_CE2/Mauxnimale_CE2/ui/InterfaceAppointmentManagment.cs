﻿using System;
using System.Collections.Generic;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.ui.components;
using System.Drawing;
using Mauxnimale_CE2.api.entities;
using System.Windows.Forms;
using Mauxnimale_CE2.api;
using Mauxnimale_CE2.api.controllers;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceAppointmentManagment : AInterface
    {
        MainWindow window;


        Header header;
        Footer footer;

        UIButton modifConsult, createOrdonance, deleteConsult, createConsult;
        UIRoundButton back;
        MonthCalendar calendar;


        List<RENDEZ_VOUS> rdvOfDay;
        RENDEZ_VOUS selected;

        Label calendarLabel;
        ListBox consultOfDay, infosConsult;

        public InterfaceAppointmentManagment(MainWindow window, SALARIE s)
        {
            this.window = window;
            user = s;
            header = new Header(window);
            footer = new Footer(window, user);
            selected = null;

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
            calendarLabel.Text = "Consultations à venir";
            calendarLabel.TextAlign = ContentAlignment.MiddleLeft;
            calendarLabel.Font = new Font("Poppins", window.Height * 2 / 100);
            calendarLabel.ForeColor = UIColor.DARKBLUE;
            calendarLabel.Size = new Size(window.Width * 3 / 10, window.Height * 1 / 10);
            calendarLabel.Location = new Point(window.Width * 25 / 1000, window.Height / 10);
            window.Controls.Add(calendarLabel);
        }

        public void generateListBox()
        {
            consultOfDay = new ListBox();
            consultOfDay.Text = "";
            consultOfDay.Font = new Font("Poppins", window.Height * 1 / 100);
            consultOfDay.ForeColor = Color.Black;
            consultOfDay.BackColor = Color.White;
            consultOfDay.Location = new Point(window.Width * 275 / 1000, window.Height * 2 / 10);
            consultOfDay.Size = new Size(window.Width * 20 / 100, window.Height * 45 / 100);
            window.Controls.Add(consultOfDay);
            consultOfDay.SelectedIndexChanged += new EventHandler(rdvSelection);

            infosConsult = new ListBox();
            infosConsult.Text = "";
            infosConsult.Font = new Font("Poppins", window.Height * 1 / 100);
            infosConsult.ForeColor = Color.Black;
            infosConsult.BackColor = Color.White;
            infosConsult.Location = new Point(window.Width * 500 / 1000, window.Height * 2 / 10);
            infosConsult.Size = new Size(window.Width * 40 / 100, window.Height * 45 / 100);
            window.Controls.Add(infosConsult);



            calendar = new MonthCalendar();
            calendar.Location = new Point(window.Width * 25 / 1000, window.Height * 2 / 10);
            calendar.Size = new Size(window.Width * 20 / 100, window.Height * 45 / 100);
            calendar.DateSelected += new DateRangeEventHandler(dateSelection);
            window.Controls.Add(calendar);
        }


        public void generateButton()
        {
            modifConsult = new UIButton(UIColor.DARKBLUE, "Modifier Consultation", window.Width * 3 / 20);
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
        private void rdvSelection(object sender, EventArgs e)
        {
            infosConsult.Items.Clear();

            selected = (RENDEZ_VOUS)consultOfDay.SelectedItem;

            infosConsult.Items.Add(consultOfDay.SelectedItem);
            infosConsult.Items.Add(selected.CLIENT);
            foreach (ANIMAL animal in AppointmentController.getAnimalFromRDV(selected))
            {
                infosConsult.Items.Add(animal);
            }
            infosConsult.Items.Add(selected.RAISON);
            infosConsult.Items.Add(selected.TYPE_RDV.ToString());

        }

        private void dateSelection(object sender, DateRangeEventArgs e)
        {
            infosConsult.Items.Clear();
            consultOfDay.Items.Clear();

            DateTime selectedDate = new DateTime(e.Start.Year, e.Start.Month, e.Start.Day);

            if (DayController.getDay(selectedDate) == null)
            {
                DayController.addDay(selectedDate);
            }

            rdvOfDay = new List<RENDEZ_VOUS>(AppointmentController.getAppointmentsFromDate(selectedDate));

            foreach (RENDEZ_VOUS rdv in rdvOfDay)
            {
                consultOfDay.Items.Add(rdv);
            }
        }

        public void backClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceHome(window, user));
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
            if (selected!=null)
            {
                DialogResult mb = MessageBox.Show("Are you sure you want to Delete", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (mb == DialogResult.OK)
                {
                    AppointmentController.deleteAppointment(selected);
                    window.Controls.Clear();
                    this.load();
                }
            }
        }

        public void createConsultClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceAppointmentCreation(window, user));
        }

        #endregion

        public override void updateSize()
        {
            window.Controls.Clear();
            this.load();
        }
    }
}



