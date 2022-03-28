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
        UIRoundButton back, medsPlus, medsMinus, carePlus, careMinus;

        Label medsLabel, prescriptionLabel, diagnosticLabel, careLabel, descriptionLabel;
        ComboBox careComboBox, medsComboBox;
        RichTextBox prescriptionTextBox, diagTextBox;
        TextBox descriptionTexBox;



        String description, prescription, diagnostic = "";
        RENDEZ_VOUS rdv;


        public InterfacePrescriptionCreation(MainWindow window, SALARIE user, RENDEZ_VOUS rdv) : base(window, user)
        {
            header = new Header(window);
            footer = new Footer(window, base.user);
            this.rdv = rdv;

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
            medsLabel.Size = new Size(window.Width * 4 / 10, window.Height * 1 / 20);
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
            diagnosticLabel.Location = new Point(window.Width * 270 / 1000, window.Height * 6 / 20);
            #endregion

            #region CareLabel
            careLabel = new Label();
            careLabel.Text = "Soin effectué";
            careLabel.TextAlign = ContentAlignment.MiddleLeft;
            careLabel.Font = new Font("Poppins", window.Height * 2 / 100);
            careLabel.ForeColor = UIColor.DARKBLUE;
            careLabel.Size = new Size(window.Width * 2 / 10, window.Height * 1 / 20);
            careLabel.Location = new Point(window.Width * 50 / 1000, window.Height * 11 / 20);
            #endregion

            #region descriptionLabel
            descriptionLabel = new Label();
            descriptionLabel.Text = "Description :";
            descriptionLabel.TextAlign = ContentAlignment.MiddleLeft;
            descriptionLabel.Font = new Font("Poppins", window.Height * 2 / 100);
            descriptionLabel.ForeColor = UIColor.DARKBLUE;
            descriptionLabel.Size = new Size(window.Width * 3 / 10, window.Height * 1 / 20);
            descriptionLabel.Location = new Point(window.Width * 490 / 1000, window.Height * 7 / 40);
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
            careComboBox.Location = new Point(window.Width * 50 / 1000, window.Height * 12 / 20);
            careComboBox.TextChanged += new EventHandler(careComboSearch);
            careComboBox.SelectedIndexChanged += new EventHandler(careComboSearch);
            #endregion

            #region medsBox
            medsComboBox = new ComboBox();
            medsComboBox.Size = new Size(window.Width * 20 / 100, window.Height * 3 / 20);
            medsComboBox.Location = new Point(window.Width * 50 / 1000, window.Height * 5 / 20);
            medsComboBox.TextChanged += new EventHandler(MedsComboSearch);
            medsComboBox.GotFocus += new EventHandler(MedsComboSearch);
            medsComboBox.SelectedIndexChanged += new EventHandler(MedsComboSearch);
            #endregion

            #region descriptionBox

            descriptionTexBox.TextChanged += new EventHandler(DescriptionTexBoxChanged);
            descriptionTexBox = new TextBox();
            descriptionTexBox.ReadOnly = true;
            descriptionTexBox.Text = "";
            descriptionTexBox.Font = new Font("Poppins", window.Height * 1 / 100);
            descriptionTexBox.ForeColor = Color.Black;
            descriptionTexBox.BackColor = Color.White;
            descriptionTexBox.Multiline = true;
            descriptionTexBox.Size = new Size(window.Width * 45 / 100, window.Height * 60 / 100);
            descriptionTexBox.Location = new Point(window.Width * 490 / 1000, window.Height * 9 / 40);
            setDescription();

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
            diagTextBox.Location = new Point(window.Width * 270 / 1000, window.Height * 7 / 20);
            diagTextBox.TextChanged += new EventHandler(diagTextBoxChanged);
            #endregion

            
            window.Controls.Add(careComboBox);
            window.Controls.Add(medsComboBox);
            window.Controls.Add(descriptionTexBox);
            window.Controls.Add(prescriptionTextBox);
            window.Controls.Add(diagTextBox);
        }

        private void setDescription()
        {
            descriptionTexBox.AppendText("Date :" + DateTime.Now);
            descriptionTexBox.AppendText("Client :" + rdv.CLIENT);
        }

        public void generateButton()
        {
            createConsult = new UIButton(UIColor.DARKBLUE, "Créer Consultation", window.Width * 3 / 20);
            createConsult.Location = new Point(window.Width * 2 / 15, window.Height * 14 / 20);
            createConsult.Enabled = false;
            window.Controls.Add(createConsult);

            #region RoudButtons

            back = new UIRoundButton(window.Width / 20, "<");
            back.Location = new Point(window.Width * 9 / 10, window.Height / 10);
            window.Controls.Add(back);
            back.Click += new EventHandler(backClick);

            medsPlus = new UIRoundButton(window.Width / 45, "+");
            medsPlus.Location = new Point(window.Width * 270 / 1000, window.Height * 5 / 20);
            window.Controls.Add(medsPlus);
            medsPlus.Click += new EventHandler(medsPlusClick);
            
            medsMinus = new UIRoundButton(window.Width / 45, "-");
            medsMinus.Location = new Point(window.Width * 300 / 1000, window.Height * 5 / 20);
            window.Controls.Add(medsMinus);
            medsMinus.Click += new EventHandler(medsMinusClick);

            carePlus = new UIRoundButton(window.Width / 45, "+");
            carePlus.Location = new Point(window.Width * 270 / 1000, window.Height * 12 / 20);
            window.Controls.Add(carePlus);
            carePlus.Click += new EventHandler(carePlusClick);

            careMinus = new UIRoundButton(window.Width / 45, "-");
            careMinus.Location = new Point(window.Width * 300 / 1000, window.Height * 12 / 20);
            window.Controls.Add(careMinus);
            careMinus.Click += new EventHandler(careMinusClick);
            #endregion


            createConsult.Click += new EventHandler(createConsultClick);
        }


        #endregion



        #region eventHandler

        #region Selection
        private void diagTextBoxChanged(object sender, EventArgs e)
        {
            diagnostic = diagTextBox.Text;
        }

        private void prescriptionTextBoxChanged(object sender, EventArgs e)
        {
            prescription = prescriptionTextBox.Text;
        }

        private void DescriptionTexBoxChanged(object sender, EventArgs e)
        {
            description = descriptionTexBox.Text;
        }

        private void MedsComboSearch(object sender, EventArgs e)
        {

        }

        private void careComboSearch(object sender, EventArgs e)
        {
            
        }


        #endregion

        #region Buttons

        private void careMinusClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void carePlusClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void medsMinusClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void medsPlusClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void backClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceAppointmentManagment(window, user));
        }

        public void createConsultClick(object sender, EventArgs e)
        {

            window.Controls.Clear();
            window.switchInterface(new InterfaceAppointmentManagment(window, user));
        }
        #endregion


        #endregion


    }
}
