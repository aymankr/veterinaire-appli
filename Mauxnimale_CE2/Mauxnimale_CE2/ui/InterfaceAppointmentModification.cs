using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api;
using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.api.controllers;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceAppointmentModification: AInterface
    {
        Header header;
        Footer footer;

        UIButton modifConsultButton;
        UIRoundButton back;
        MonthCalendar calendar;

        DateTime selectedDate;
        TYPE_RDV selectedType;
        CLIENT selectedClient;
        HashSet<ANIMAL> animalsInRDV;
        JOURNEE selectedJOURNEE;
        String description = "";
        TimeSpan RDVStart;
        TimeSpan RDVEnd;

        RENDEZ_VOUS rdv;



        Label calendarLabel, clientLabel, animalLabel, appointmentTypeLabel, timeLabel, descriptionLabel, selectedAnimalsLabel;

        ComboBox clientComboBox, animalComboBox, appointmentTypeComboBox, selectedAnimals;
        DateTimePicker startTimePicker, endTimePicker;
        RichTextBox descriptionTexBox;


        public InterfaceAppointmentModification(MainWindow window, SALARIE user, RENDEZ_VOUS rdv) : base(window, user)
        {
            header = new Header(window);
            footer = new Footer(window, base.user);
            this.rdv = rdv;
            animalsInRDV = new HashSet<ANIMAL>();
        }

        public override void load()
        {
            header.load("Mauxnimale - Modification d'une consultations");
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
            calendarLabel.Size = new Size(window.Width * 2 / 10, window.Height * 1 / 10);
            calendarLabel.Location = new Point(window.Width * 25 / 1000, window.Height / 10);
            #endregion

            #region clientLabel
            clientLabel = new Label();
            clientLabel.Text = "Client";
            clientLabel.TextAlign = ContentAlignment.MiddleLeft;
            clientLabel.Font = new Font("Poppins", window.Height * 2 / 100);
            clientLabel.ForeColor = UIColor.DARKBLUE;
            clientLabel.Size = new Size(window.Width * 2 / 10, window.Height * 1 / 20);
            clientLabel.Location = new Point(window.Width * 270 / 1000, window.Height * 5 / 40);
            #endregion

            #region animalLabel
            animalLabel = new Label();
            animalLabel.Text = "Animaux";
            animalLabel.TextAlign = ContentAlignment.MiddleLeft;
            animalLabel.Font = new Font("Poppins", window.Height * 2 / 100);
            animalLabel.ForeColor = UIColor.DARKBLUE;
            animalLabel.Size = new Size(window.Width * 2 / 10, window.Height * 1 / 20);
            animalLabel.Location = new Point(window.Width * 500 / 1000, window.Height * 5 / 40);
            #endregion

            #region typeLabel
            appointmentTypeLabel = new Label();
            appointmentTypeLabel.Text = "Type du rendez-vous";
            appointmentTypeLabel.TextAlign = ContentAlignment.MiddleLeft;
            appointmentTypeLabel.Font = new Font("Poppins", window.Height * 2 / 100);
            appointmentTypeLabel.ForeColor = UIColor.DARKBLUE;
            appointmentTypeLabel.Size = new Size(window.Width * 2 / 10, window.Height * 1 / 20);
            appointmentTypeLabel.Location = new Point(window.Width * 270 / 1000, window.Height * 10 / 40);
            #endregion

            #region Selected Animals Label
            selectedAnimalsLabel = new Label();
            selectedAnimalsLabel.Text = "Annimaux au RDV";
            selectedAnimalsLabel.TextAlign = ContentAlignment.MiddleLeft;
            selectedAnimalsLabel.Font = new Font("Poppins", window.Height * 2 / 100);
            selectedAnimalsLabel.ForeColor = UIColor.DARKBLUE;
            selectedAnimalsLabel.Size = new Size(window.Width * 2 / 10, window.Height * 1 / 20);
            selectedAnimalsLabel.Location = new Point(window.Width * 750 / 1000, window.Height * 8 / 40);
            #endregion

            #region timeLabel
            timeLabel = new Label();
            timeLabel.Text = "Heure du rendez-vous";
            timeLabel.TextAlign = ContentAlignment.MiddleLeft;
            timeLabel.Font = new Font("Poppins", window.Height * 2 / 100);
            timeLabel.ForeColor = UIColor.DARKBLUE;
            timeLabel.Size = new Size(window.Width * 2 / 10, window.Height * 1 / 20);
            timeLabel.Location = new Point(window.Width * 500 / 1000, window.Height * 10 / 40);
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

            window.Controls.Add(selectedAnimalsLabel);
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
            clientComboBox.Location = new Point(window.Width * 270 / 1000, window.Height * 8 / 40);
            List<CLIENT> clients = ClientController.AllClient();
            foreach (CLIENT client in clients)
            {
                clientComboBox.Items.Add(client);
            }
            clientComboBox.SelectedIndex = clientComboBox.Items.IndexOf(ClientController.GetClientFromID(rdv.IDCLIENT));
            selectedClient = ClientController.GetClientFromID((int)rdv.IDCLIENT);   
            clientComboBox.TextChanged += new EventHandler(ClientComboBoxSearch);
            clientComboBox.SelectedIndexChanged += new EventHandler(ClientComboBoxSearch);
            #endregion

            #region animalBox
            animalComboBox = new ComboBox();
            animalComboBox.Size = new Size(window.Width * 20 / 100, window.Height * 3 / 20);
            animalComboBox.Location = new Point(window.Width * 500 / 1000, window.Height * 8 / 40);
            animalComboBox.TextChanged += new EventHandler(AnimalComboBoxSearch);
            animalComboBox.GotFocus += new EventHandler(AnimalComboBoxFocus);
            animalComboBox.SelectedIndexChanged += new EventHandler(AnimalComboBoxSearch);
            #endregion

            #region selectedAnimals
            selectedAnimals = new ComboBox();
            selectedAnimals.Size = new Size(window.Width * 20 / 100, window.Height * 6 / 20);
            selectedAnimals.Location = new Point(window.Width * 750 / 1000, window.Height * 10 / 40);
            foreach(ANIMAL animal in rdv.ANIMAL)
            {
                selectedAnimals.Items.Add(animal);
                animalsInRDV.Add(animal);
            }
            selectedAnimals.TextChanged += new EventHandler(AnimalSelectionComboBoxSearch);
            selectedAnimals.GotFocus += new EventHandler(AnimalSelectionComboBoxSearch);
            selectedAnimals.SelectedIndexChanged += new EventHandler(AnimalSelectionComboBoxSearch);
            #endregion

            #region typeBox
            appointmentTypeComboBox = new ComboBox();
            appointmentTypeComboBox.Size = new Size(window.Width * 20 / 100, window.Height * 6 / 20);
            appointmentTypeComboBox.Location = new Point(window.Width * 270 / 1000, window.Height * 13 / 40);
            appointmentTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            List<TYPE_RDV> types = AppointmentController.GetAllRDVType();
            foreach (TYPE_RDV type in types)
            {
                appointmentTypeComboBox.Items.Add(type);
            }
            appointmentTypeComboBox.SelectedItem = rdv.TYPE_RDV;
            selectedType = rdv.TYPE_RDV;
            appointmentTypeComboBox.SelectedIndexChanged += new EventHandler(AppointmentTypeComboBoxSelected);
            #endregion

            #region timeBox
            startTimePicker = new DateTimePicker();
            startTimePicker.Size = new Size(window.Width * 10 / 100, window.Height * 6 / 20);
            startTimePicker.Location = new Point(window.Width * 500 / 1000, window.Height * 13 / 40);
            startTimePicker.Format = DateTimePickerFormat.Custom;
            startTimePicker.CustomFormat = "HH:mm";
            startTimePicker.ShowUpDown = true;
            startTimePicker.MinDate = new DateTime();
            startTimePicker.Text = rdv.HEUREDEBUT.ToString();
            DateTime tmp = startTimePicker.Value;
            RDVStart = rdv.HEUREDEBUT;
            startTimePicker.ValueChanged += new EventHandler(StartTimePickerChanged);

            endTimePicker = new DateTimePicker();
            endTimePicker.Size = new Size(window.Width * 10 / 100, window.Height * 6 / 20);
            endTimePicker.Location = new Point(window.Width * 625 / 1000, window.Height * 13 / 40);
            endTimePicker.Format = DateTimePickerFormat.Custom;
            endTimePicker.CustomFormat = "HH:mm";
            endTimePicker.ShowUpDown = true;
            endTimePicker.MinDate = new DateTime();
            endTimePicker.Text = rdv.HEUREFIN.ToString();
            tmp = endTimePicker.Value;
            RDVEnd = rdv.HEUREFIN;
            endTimePicker.ValueChanged += new EventHandler(EndTimePickerChanged);
            #endregion

            #region descriptionBox
            descriptionTexBox = new RichTextBox();
            descriptionTexBox.Size = new Size(window.Width * 60 / 100, window.Height * 20 / 100);
            descriptionTexBox.Location = new Point(window.Width * 350 / 1000, window.Height * 18 / 40);
            descriptionTexBox.Text = rdv.RAISON;
            description = rdv.RAISON;
            descriptionTexBox.TextChanged += new EventHandler(DescriptionTexBoxChanged);
            #endregion

            #region calendar
            calendar = new MonthCalendar();
            calendar.Location = new Point(window.Width * 25 / 1000, window.Height * 2 / 10);
            calendar.Size = new Size(window.Width * 20 / 100, window.Height * 45 / 100);
            calendar.SetDate(rdv.JOURNEE.DATE);
            selectedDate = rdv.JOURNEE.DATE;
            selectedJOURNEE = DayController.getDay(selectedDate);
            calendar.DateSelected += new DateRangeEventHandler(dateSelection);
            #endregion

            window.Controls.Add(calendar);
            window.Controls.Add(clientComboBox);
            window.Controls.Add(appointmentTypeComboBox);
            window.Controls.Add(selectedAnimals);
            window.Controls.Add(animalComboBox);
            window.Controls.Add(startTimePicker);
            window.Controls.Add(endTimePicker);
            window.Controls.Add(descriptionTexBox);
        }


        public void generateButton()
        {

            modifConsultButton = new UIButton(UIColor.DARKBLUE, "Modifier Consultation", window.Width * 3 / 20);
            modifConsultButton.Location = new Point(window.Width * 2 / 15, window.Height * 14 / 20);
            modifConsultButton.Enabled = false;
            window.Controls.Add(modifConsultButton);

            back = new UIRoundButton(window.Width / 20, "<");
            back.Location = new Point(window.Width * 9 / 10, window.Height / 10);
            window.Controls.Add(back);
            back.Click += new EventHandler(backClick);

            modifConsultButton.Click += new EventHandler(ModifConsultClick);
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

        private void AnimalSelectionComboBoxSearch(object sender, EventArgs e)
        {

            ANIMAL selectedAnimal = (ANIMAL)selectedAnimals.SelectedItem;

            if (selectedAnimal == null)
            {
                selectedAnimals.Items.Clear();
            }
            else if (animalsInRDV.Contains(selectedAnimal))
            {
                Console.WriteLine("wesh");
                animalsInRDV.Remove(selectedAnimal);
            }

            selectedAnimals.Items.Clear();



            if (selectedAnimals.Text.Length == 0)
            {
                List<ANIMAL> animaux = ClientController.ListOfAnimal(selectedClient);
                foreach (ANIMAL animal in animalsInRDV)
                {
                    selectedAnimals.Items.Add(animal);
                    Console.WriteLine(animal);
                }

            }
            else
            {
                selectedAnimals.Select(selectedAnimals.Text.Length, 0);
                foreach (ANIMAL animal in animalsInRDV)
                {

                    selectedAnimals.Items.Add(animal);
                    Console.WriteLine(animal);
                }
            }

        }

        private void AnimalComboBoxFocus(object sender, EventArgs e)
        {
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

        private void AnimalComboBoxSearch(object sender, EventArgs e)
        {

            if (selectedClient != null)
            {
                ANIMAL selectedAnimal = (ANIMAL)animalComboBox.SelectedItem;

                if (selectedAnimal == null)
                {
                    animalComboBox.Items.Clear();
                }
                else if (!animalsInRDV.Contains(selectedAnimal))
                {
                    animalsInRDV.Add(selectedAnimal);
                }
                selectedAnimals.Items.Clear();
                foreach (ANIMAL animal in animalsInRDV)
                {
                    Console.WriteLine(animal);
                    selectedAnimals.Items.Add(animal);
                }

                AnimalComboBoxFocus(sender, e);
            }
            addButtons();
        }

        private void ClientComboBoxSearch(object sender, EventArgs e)
        {
            selectedClient = (CLIENT)clientComboBox.SelectedItem;
            animalsInRDV = new HashSet<ANIMAL>();
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
            if (selectedType != null && selectedClient != null && animalsInRDV != null && selectedJOURNEE != null && RDVStart != null && RDVEnd != null)
            {
                modifConsultButton.Enabled = true;
            }
            else
            {
                modifConsultButton.Enabled = false;
            }

        }


        #endregion

        #region Buttons
        public void backClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceAppointmentManagment(window, user));
        }

        public void ModifConsultClick(object sender, EventArgs e)
        {
            AppointmentController.UpdateAppointment(rdv, selectedType, selectedClient, animalsInRDV, selectedJOURNEE, description, RDVStart, RDVEnd);
            window.Controls.Clear();
            window.switchInterface(new InterfaceAppointmentManagment(window, user));
        }
        #endregion


        #endregion
    }
}



