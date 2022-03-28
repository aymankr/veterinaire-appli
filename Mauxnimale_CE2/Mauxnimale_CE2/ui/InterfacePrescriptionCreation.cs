using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api;
using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.api.controllers;

namespace Mauxnimale_CE2.ui
{
    internal class InterfacePrescriptionCreation : AInterface
    {
        Header header;
        Footer footer;

        UIButton newClient, newAnimal, createConsult;
        UIRoundButton back;

        TYPE_RDV selectedType;
        CLIENT selectedClient;
        HashSet<ANIMAL> animalsInRDV;
        JOURNEE selectedJOURNEE;
        String description = "";
        TimeSpan RDVStart;
        TimeSpan RDVEnd;



        Label medsLabel, prescriptionLabel, diagnosticLabel, careLabel, descriptionLabel;
        ComboBox careComboBox, medsComboBox;
        RichTextBox prescriptionTextBox, diagTextBox, descriptionTexBox;



        public InterfacePrescriptionCreation(MainWindow window, SALARIE user) : base(window, user)
        {
            header = new Header(window);
            footer = new Footer(window, base.user);

        }



        public override void load()
        {
            header.load("Mauxnimale - Création d'une Ordonnance");
            footer.load();
            generateButton();
            generateLabels();
            generateBox();
        }


        #region Generation
        public void generateLabels()
        {
            #region medsLabel
            medsLabel = new Label();
            medsLabel.Text = "Sélectionnez un médicament et sa quantité";
            medsLabel.TextAlign = ContentAlignment.MiddleLeft;
            medsLabel.Font = new Font("Poppins", window.Height * 2 / 100);
            medsLabel.ForeColor = UIColor.DARKBLUE;
            medsLabel.Size = new Size(window.Width * 3 / 10, window.Height * 1 / 20);
            medsLabel.Location = new Point(window.Width * 50 / 1000, window.Height * 4 / 20);
            #endregion

            #region PrescriptionLabel
            prescriptionLabel = new Label();
            prescriptionLabel.Text = "Prescription";
            prescriptionLabel.TextAlign = ContentAlignment.MiddleLeft;
            prescriptionLabel.Font = new Font("Poppins", window.Height * 2 / 100);
            prescriptionLabel.ForeColor = UIColor.DARKBLUE;
            prescriptionLabel.Size = new Size(window.Width * 2 / 10, window.Height * 1 / 20);
            prescriptionLabel.Location = new Point(window.Width * 50 / 1000, window.Height * 6 / 20);
            #endregion

            #region DiagnosticlLabel
            diagnosticLabel = new Label();
            diagnosticLabel.Text = "Diagnostique";
            diagnosticLabel.TextAlign = ContentAlignment.MiddleLeft;
            diagnosticLabel.Font = new Font("Poppins", window.Height * 2 / 100);
            diagnosticLabel.ForeColor = UIColor.DARKBLUE;
            diagnosticLabel.Size = new Size(window.Width * 2 / 10, window.Height * 1 / 20);
            diagnosticLabel.Location = new Point(window.Width * 50 / 1000, window.Height * 8 / 20);
            #endregion

            #region CareLabel
            careLabel = new Label();
            careLabel.Text = "Soin effectué";
            careLabel.TextAlign = ContentAlignment.MiddleLeft;
            careLabel.Font = new Font("Poppins", window.Height * 2 / 100);
            careLabel.ForeColor = UIColor.DARKBLUE;
            careLabel.Size = new Size(window.Width * 2 / 10, window.Height * 1 / 20);
            careLabel.Location = new Point(window.Width * 50 / 1000, window.Height * 10 / 20);
            #endregion

            #region descriptionLabel
            descriptionLabel = new Label();
            descriptionLabel.Text = "Description :";
            descriptionLabel.TextAlign = ContentAlignment.MiddleLeft;
            descriptionLabel.Font = new Font("Poppins", window.Height * 2 / 100);
            descriptionLabel.ForeColor = UIColor.DARKBLUE;
            descriptionLabel.Size = new Size(window.Width * 3 / 10, window.Height * 1 / 20);
            descriptionLabel.Location = new Point(window.Width * 425 / 1000, window.Height * 7 / 40);
            #endregion

            window.Controls.Add(medsLabel);
            window.Controls.Add(prescriptionLabel);
            window.Controls.Add(diagnosticLabel);
            window.Controls.Add(careLabel);
            window.Controls.Add(descriptionLabel);
        }

        public void generateBox()
        {
            #region careBox
            careComboBox = new ComboBox();
            careComboBox.Size = new Size(window.Width * 20 / 100, window.Height * 3 / 20);
            careComboBox.Location = new Point(window.Width * 50 / 1000, window.Height * 11 / 20);
            careComboBox.TextChanged += new EventHandler(ClientComboBoxSearch);
            careComboBox.SelectedIndexChanged += new EventHandler(ClientComboBoxSearch);
            List<CLIENT> clients = ClientController.AllClient();
            foreach (CLIENT client in clients)
            {
                careComboBox.Items.Add(client);
            }
            #endregion

            #region medsBox
            medsComboBox = new ComboBox();
            medsComboBox.Size = new Size(window.Width * 20 / 100, window.Height * 3 / 20);
            medsComboBox.Location = new Point(window.Width * 50 / 1000, window.Height * 5 / 20);
            medsComboBox.TextChanged += new EventHandler(AnimalComboBoxSearch);
            medsComboBox.GotFocus += new EventHandler(AnimalComboBoxFocus);
            medsComboBox.SelectedIndexChanged += new EventHandler(AnimalComboBoxSearch);
            #endregion

            #region descriptionBox
            descriptionTexBox = new RichTextBox();
            descriptionTexBox.Size = new Size(window.Width * 45 / 100, window.Height * 60 / 100);
            descriptionTexBox.Location = new Point(window.Width * 425 / 1000, window.Height * 9 / 40);
            descriptionTexBox.TextChanged += new EventHandler(DescriptionTexBoxChanged);
            #endregion
            
            #region PrescriptonBox
            prescriptionTextBox = new RichTextBox();
            prescriptionTextBox.Size = new Size(window.Width * 20 / 100, window.Height * 3 / 20);
            prescriptionTextBox.Location = new Point(window.Width * 50 / 1000, window.Height * 7 / 20);
            prescriptionTextBox.TextChanged += new EventHandler(prescriptionTextBoxChanged);
            #endregion
            
            #region DiagnosticBox
            diagTextBox= new RichTextBox();
            diagTextBox.Size = new Size(window.Width * 20 / 100, window.Height * 3 / 20);
            diagTextBox.Location = new Point(window.Width * 50 / 1000, window.Height * 9 / 20);
            diagTextBox.TextChanged += new EventHandler(diagTextBoxChanged);
            #endregion

            
            window.Controls.Add(careComboBox);
            window.Controls.Add(medsComboBox);
            window.Controls.Add(descriptionTexBox);
            window.Controls.Add(prescriptionTextBox);
            window.Controls.Add(diagTextBox);
        }

        private void diagTextBoxChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void prescriptionTextBoxChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void generateButton()
        {
            newClient = new UIButton(UIColor.DARKBLUE, "Nouveau Client", window.Width * 3 / 20);
            newClient.Location = new Point(window.Width * 8 / 15, window.Height * 14 / 20);
            window.Controls.Add(newClient);

            newAnimal = new UIButton(UIColor.DARKBLUE, "Nouvel Animal", window.Width * 3 / 20);
            newAnimal.Location = new Point(window.Width * 11 / 15, window.Height * 14 / 20);
            window.Controls.Add(newAnimal);

            createConsult = new UIButton(UIColor.DARKBLUE, "Créer Consultation", window.Width * 3 / 20);
            createConsult.Location = new Point(window.Width * 2 / 15, window.Height * 14 / 20);
            createConsult.Enabled = false;
            window.Controls.Add(createConsult);

            back = new UIRoundButton(window.Width / 20, "<");
            back.Location = new Point(window.Width * 9 / 10, window.Height / 10);
            window.Controls.Add(back);
            back.Click += new EventHandler(backClick);


            newClient.Click += new EventHandler(NewClientClick);
            newAnimal.Click += new EventHandler(NewAnimalClick);
            createConsult.Click += new EventHandler(createConsultClick);
        }

        #endregion



        #region eventHandler

        #region Selection
        private void DescriptionTexBoxChanged(object sender, EventArgs e)
        {
        }

        private void EndTimePickerChanged(object sender, EventArgs e)
        {
        }

        private void StartTimePickerChanged(object sender, EventArgs e)
        {
           
        }

        private void SelectedAnimalsSearch(object sender, EventArgs e)
        {
        }

        private void AnimalComboBoxFocus(object sender, EventArgs e)
        {
           
        }

        private void AnimalComboBoxSearch(object sender, EventArgs e)
        {

        }

        private void ClientComboBoxSearch(object sender, EventArgs e)
        {
            
        }

        private void AppointmentTypeComboBoxSelected(object sender, EventArgs e)
        {
            
        }

        private void dateSelection(object sender, DateRangeEventArgs e)
        {

        }

        private void addButtons()
        {

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
            AppointmentController.addAppointment(selectedType, selectedClient, animalsInRDV, selectedJOURNEE, description, RDVStart, RDVEnd);
            window.Controls.Clear();
            window.switchInterface(new InterfaceAppointmentManagment(window, user));
        }
        #endregion


        #endregion


    }
}
