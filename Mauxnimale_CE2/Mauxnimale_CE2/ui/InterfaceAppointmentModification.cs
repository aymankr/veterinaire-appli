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
    internal class InterfaceAppointmentModification : AInterface
    {
        MainWindow window;


        Header header;
        Footer footer;

        UIButton newClient, newAnimal, modifConsult;
        UIRoundButton back;
        MonthCalendar calendar;
        DateTime selectedDate;


        RENDEZ_VOUS rdv;

        TYPE_RDV selectedType;
        CLIENT selectedClient;
        ANIMAL selectedAnimal;
        JOURNEE selectedJOURNEE;
        String description = "";
        TimeSpan RDVStart;
        TimeSpan RDVEnd;



        Label calendarLabel, clientLabel, animalLabel, appointmentTypeLabel, timeLabel, descriptionLabel;

        ComboBox clientComboBox, animalComboBox, appointmentTypeComboBox;
        DateTimePicker startTimePicker, endTimePicker;
        RichTextBox descriptionTexBox;


        public InterfaceAppointmentModification(MainWindow window, SALARIE s, RENDEZ_VOUS rdv)
        {
            this.rdv = rdv;
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
            generateBox();
        }

        #region Generation
        public void generateLabels()
        {
            #region calendarLabel
            calendarLabel = new Label();
            calendarLabel.Text = "Sélectionnez une date";
            calendarLabel.TextAlign = ContentAlignment.MiddleLeft;
            calendarLabel.Font = new Font("Poppins", window.Height * 2 / 100);
            calendarLabel.ForeColor = UIColor.DARKBLUE;
            calendarLabel.Size = new Size(window.Width * 3 / 10, window.Height * 1 / 10);
            calendarLabel.Location = new Point(window.Width * 25 / 1000, window.Height / 10);
            #endregion

            #region clientLabel
            clientLabel = new Label();
            clientLabel.Text = "Choisissez un Client";
            clientLabel.TextAlign = ContentAlignment.MiddleLeft;
            clientLabel.Font = new Font("Poppins", window.Height * 2 / 100);
            clientLabel.ForeColor = UIColor.DARKBLUE;
            clientLabel.Size = new Size(window.Width * 3 / 10, window.Height * 1 / 20);
            clientLabel.Location = new Point(window.Width * 350 / 1000, window.Height * 5 / 40);
            #endregion

            #region animalLabel
            animalLabel = new Label();
            animalLabel.Text = "Choisissez un Animal";
            animalLabel.TextAlign = ContentAlignment.MiddleLeft;
            animalLabel.Font = new Font("Poppins", window.Height * 2 / 100);
            animalLabel.ForeColor = UIColor.DARKBLUE;
            animalLabel.Size = new Size(window.Width * 3 / 10, window.Height * 1 / 20);
            animalLabel.Location = new Point(window.Width * 650 / 1000, window.Height * 5 / 40);
            #endregion

            #region typeLabel
            appointmentTypeLabel = new Label();
            appointmentTypeLabel.Text = "Choisissez le type du rendez-vous";
            appointmentTypeLabel.TextAlign = ContentAlignment.MiddleLeft;
            appointmentTypeLabel.Font = new Font("Poppins", window.Height * 2 / 100);
            appointmentTypeLabel.ForeColor = UIColor.DARKBLUE;
            appointmentTypeLabel.Size = new Size(window.Width * 3 / 10, window.Height * 1 / 20);
            appointmentTypeLabel.Location = new Point(window.Width * 350 / 1000, window.Height * 10 / 40);
            #endregion

            #region timeLabel
            timeLabel = new Label();
            timeLabel.Text = "Choisissez l'heure du rendez-vous";
            timeLabel.TextAlign = ContentAlignment.MiddleLeft;
            timeLabel.Font = new Font("Poppins", window.Height * 2 / 100);
            timeLabel.ForeColor = UIColor.DARKBLUE;
            timeLabel.Size = new Size(window.Width * 3 / 10, window.Height * 1 / 20);
            timeLabel.Location = new Point(window.Width * 650 / 1000, window.Height * 10 / 40);
            #endregion

            #region descriptionLabel
            descriptionLabel = new Label();
            descriptionLabel.Text = "Description :";
            descriptionLabel.TextAlign = ContentAlignment.MiddleLeft;
            descriptionLabel.Font = new Font("Poppins", window.Height * 2 / 100);
            descriptionLabel.ForeColor = UIColor.DARKBLUE;
            descriptionLabel.Size = new Size(window.Width * 3 / 10, window.Height * 1 / 10);
            descriptionLabel.Location = new Point(window.Width * 350 / 1000, window.Height * 7 / 20);
            #endregion

            window.Controls.Add(calendarLabel);
            window.Controls.Add(clientLabel);
            window.Controls.Add(animalLabel);
            window.Controls.Add(appointmentTypeLabel);
            window.Controls.Add(timeLabel);
            window.Controls.Add(descriptionLabel);
        }

        public void generateBox()
        {
            #region clientBox
            clientComboBox = new ComboBox();
            clientComboBox.Size = new Size(window.Width * 20 / 100, window.Height * 3 / 20);
            clientComboBox.Location = new Point(window.Width * 350 / 1000, window.Height * 8 / 40);
            clientComboBox.TextChanged += new EventHandler(ClientComboBoxSearch);
            clientComboBox.SelectedIndexChanged += new EventHandler(ClientComboBoxSearch);
            List<CLIENT> clients = ClientController.AllClient();
            foreach (CLIENT client in clients)
            {
                clientComboBox.Items.Add(client);
            }
            clientComboBox.SelectedIndex = clientComboBox.Items.IndexOf(rdv.CLIENT);
            selectedClient = (CLIENT) clientComboBox.SelectedItem;
            #endregion

            #region animalBox
            animalComboBox = new ComboBox();
            animalComboBox.Size = new Size(window.Width * 20 / 100, window.Height * 3 / 20);
            animalComboBox.Location = new Point(window.Width * 650 / 1000, window.Height * 8 / 40);
            animalComboBox.TextChanged += new EventHandler(AnimalComboBoxSearch);
            animalComboBox.GotFocus += new EventHandler(AnimalComboBoxSearch);
            animalComboBox.SelectedIndexChanged += new EventHandler(AnimalComboBoxSearch);
            List<ANIMAL> animaux = ClientController.ListOfAnimal(selectedClient);
            foreach (ANIMAL animal in animaux)
            {
                animalComboBox.Items.Add(animal);
            }
            animalComboBox.SelectedIndex = animalComboBox.Items.IndexOf(rdv.ANIMAL);
            selectedAnimal = (ANIMAL) animalComboBox.SelectedItem;
            #endregion

            #region typeBox
            appointmentTypeComboBox = new ComboBox();
            appointmentTypeComboBox.Size = new Size(window.Width * 20 / 100, window.Height * 6 / 20);
            appointmentTypeComboBox.Location = new Point(window.Width * 350 / 1000, window.Height * 13 / 40);
            appointmentTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            appointmentTypeComboBox.SelectedIndexChanged += new EventHandler(AppointmentTypeComboBoxSelected);
            List<TYPE_RDV> types = AppointmentController.GetAllRDVType();
            foreach (TYPE_RDV type in types)
            {
                appointmentTypeComboBox.Items.Add(type);
            }
            appointmentTypeComboBox.SelectedIndex = appointmentTypeComboBox.Items.IndexOf(rdv.TYPE_RDV);
            #endregion

            #region timeBox

            startTimePicker = new DateTimePicker();
            startTimePicker.Size = new Size(window.Width * 10 / 100, window.Height * 6 / 20);
            startTimePicker.Location = new Point(window.Width * 650 / 1000, window.Height * 13 / 40);
            startTimePicker.Format = DateTimePickerFormat.Custom;
            startTimePicker.CustomFormat = "HH:mm";
            startTimePicker.ShowUpDown = true;
            startTimePicker.ValueChanged += new EventHandler(StartTimePickerChanged);
            startTimePicker.Value = new DateTime() + rdv.HEUREDEBUT;
            

            endTimePicker = new DateTimePicker();
            endTimePicker.Size = new Size(window.Width * 10 / 100, window.Height * 6 / 20);
            endTimePicker.Location = new Point(window.Width * 800 / 1000, window.Height * 13 / 40);
            endTimePicker.Format = DateTimePickerFormat.Custom;
            endTimePicker.CustomFormat = "HH:mm";
            endTimePicker.ShowUpDown = true;
            endTimePicker.ValueChanged += new EventHandler(EndTimePickerChanged);
            startTimePicker.Value = new DateTime() + rdv.HEUREFIN;
            #endregion

            #region descriptionBox
            descriptionTexBox = new RichTextBox();
            descriptionTexBox.Size = new Size(window.Width * 60 / 100, window.Height * 20 / 100);
            descriptionTexBox.Location = new Point(window.Width * 350 / 1000, window.Height * 18 / 40);
            descriptionTexBox.TextChanged += new EventHandler(DescriptionTexBoxChanged);
            #endregion

            #region calendar
            calendar = new MonthCalendar();
            calendar.Location = new Point(window.Width * 25 / 1000, window.Height * 2 / 10);
            calendar.Size = new Size(window.Width * 20 / 100, window.Height * 45 / 100);
            calendar.DateSelected += new DateRangeEventHandler(dateSelection);
            selectedJOURNEE = DayController.getDay(selectedDate);
            #endregion

            window.Controls.Add(calendar);
            window.Controls.Add(clientComboBox);
            window.Controls.Add(appointmentTypeComboBox);
            window.Controls.Add(animalComboBox);
            window.Controls.Add(startTimePicker);
            window.Controls.Add(endTimePicker);
            window.Controls.Add(descriptionTexBox);
        }

        public void generateButton()
        {
            modifConsult = new UIButton(UIColor.DARKBLUE, "Créer Consultation", window.Width * 3 / 20);
            modifConsult.Location = new Point(window.Width * 2 / 15, window.Height * 14 / 20);
            modifConsult.Enabled = false;
            modifConsult.Click += new EventHandler(createConsultClick);


            back = new UIRoundButton(window.Width / 20, "<");
            back.Location = new Point(window.Width * 9 / 10, window.Height / 10);
            back.Click += new EventHandler(backClick);


            window.Controls.Add(modifConsult);
            window.Controls.Add(back);
        }

        #endregion

        #region eventHandler

        #region Selection
        private void DescriptionTexBoxChanged(object sender, EventArgs e)
        {
            description = descriptionTexBox.Text;
            addButtons();
        }

        private void EndTimePickerChanged(object sender, EventArgs e)
        {
            if (endTimePicker.Value <= startTimePicker.Value)
            {
                endTimePicker.Value = startTimePicker.Value;
            }

            DateTime tmp = endTimePicker.Value;
            RDVEnd = new TimeSpan(tmp.Hour, tmp.Minute, 00);
            addButtons();
        }

        private void StartTimePickerChanged(object sender, EventArgs e)
        {
            DateTime tmp = startTimePicker.Value;
            RDVStart = new TimeSpan(tmp.Hour, tmp.Minute, 00);
            addButtons();
        }

        private void AnimalComboBoxSearch(object sender, EventArgs e)
        {

            if (selectedClient != null)
            {
                selectedAnimal = (ANIMAL)animalComboBox.SelectedItem;
                if (selectedAnimal == null)
                {
                    animalComboBox.Items.Clear();
                }

                if (animalComboBox.Text.Length == 0)
                {
                    List<ANIMAL> animaux = ClientController.ListOfAnimal(selectedClient);
                    foreach (ANIMAL animal in animaux)
                    {
                        animalComboBox.Items.Add(animal);
                    }
                }
                else
                {
                    animalComboBox.Select(animalComboBox.Text.Length, 0);
                    List<ANIMAL> animaux = ClientController.ListAnimalByName(selectedClient, animalComboBox.Text);
                    foreach (ANIMAL animal in animaux)
                    {
                        animalComboBox.Items.Add(animal);
                    }
                }
            }
            addButtons();
        }

        private void ClientComboBoxSearch(object sender, EventArgs e)
        {
            selectedClient = (CLIENT)clientComboBox.SelectedItem;
            selectedAnimal = null;
            animalComboBox.Items.Clear();
            animalComboBox.Text = "";
            if (selectedClient == null)
            {
                clientComboBox.Items.Clear();
            }

            if (clientComboBox.Text.Length == 0)
            {
                List<CLIENT> clients = ClientController.AllClient();
                foreach (CLIENT client in clients)
                {
                    clientComboBox.Items.Add(client);
                }
            }
            else
            {
                List<CLIENT> clients = ClientController.ResearhByName(clientComboBox.Text);
                foreach (CLIENT client in clients)
                {
                    clientComboBox.Items.Add(client);
                }
            }
            clientComboBox.Select(clientComboBox.Text.Length, 0);
            addButtons();
        }


        private void AppointmentTypeComboBoxSelected(object sender, EventArgs e)
        {
            selectedType = (TYPE_RDV)appointmentTypeComboBox.SelectedItem;
            addButtons();
        }

        private void dateSelection(object sender, DateRangeEventArgs e)
        {

            selectedDate = new DateTime(e.Start.Year, e.Start.Month, e.Start.Day);


            if (DayController.getDay(selectedDate) == null)
            {
                DayController.addDay(selectedDate);
                selectedJOURNEE = DayController.getDay(selectedDate);
            }

            selectedJOURNEE = DayController.getDay(selectedDate);
            addButtons();
        }

        private void addButtons()
        {
            if (selectedType != null && selectedClient != null && selectedAnimal != null && selectedJOURNEE != null && RDVStart != null && RDVEnd != null)
            {
                modifConsult.Enabled = true;
            }
            else
            {
                modifConsult.Enabled = false;
            }

        }


        #endregion

        #region Buttons
        public void backClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceAppointmentManagment(window, user));
        }

        public void NewClientClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            //form.changerClasse(new Interface...());
        }

        public void NewAnimalClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            //form.changerClasse(new Interface...());
        }

        public void createConsultClick(object sender, EventArgs e)
        {
            //AppointmentController.addAppointment(selectedType, selectedClient, selectedAnimal, selectedJOURNEE, description, RDVStart, RDVEnd);
            window.Controls.Clear();
            window.switchInterface(new InterfaceAppointmentManagment(window, user));
        }
        #endregion


        #endregion
        public override void updateSize()
        {
            if (window.WindowState != FormWindowState.Minimized)
            {
                window.Controls.Clear();
                this.load();
            }
        }

    }
}



