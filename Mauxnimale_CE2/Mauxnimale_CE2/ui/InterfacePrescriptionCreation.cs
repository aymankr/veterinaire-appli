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

        UIButton createConsult;
        UIRoundButton back, medsPlus, medsMinus, carePlus, careMinus;

        Label medsLabel, prescriptionLabel, diagnosticLabel, careLabel, descriptionLabel;
        ComboBox careComboBox, medsComboBox;
        RichTextBox prescriptionTextBox, diagTextBox;
        TextBox descriptionTextBox;



        RENDEZ_VOUS rdv;
        ANIMAL animal;
        string prescription, diagnostic;

        Dictionary<PRODUIT,int> products = new Dictionary<PRODUIT, int>();
        List<SOIN> cares = new List<SOIN>();
        PRODUIT selectedProduct;
        SOIN selectedCare;


        public InterfacePrescriptionCreation(MainWindow window, SALARIE user, RENDEZ_VOUS rdv, ANIMAL animal) : base(window, user)
        {
            header = new Header(window);
            footer = new Footer(window, base.user);
            this.rdv = rdv;
            this.animal = animal;
        }

        public InterfacePrescriptionCreation(MainWindow window, SALARIE user, ORDONNANCE ordonnance) : base(window, user)
        {
            header = new Header(window);
            footer = new Footer(window, base.user);
            this.rdv = ordonnance.RENDEZ_VOUS;
            this.animal = ordonnance.ANIMAL;
        }



        public override void load()
        {
            header.load("Mauxnimale - Création d'une Ordonnance");
            footer.load();
            generateButton();
            generateLabels();
            generateBox();
            setDescription();
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
            /*foreach(SOIN soin in CareController.getAllCares())
            {
                careComboBox.Items.Add(soin);
            }*/
            #endregion

            #region medsBox
            medsComboBox = new ComboBox();
            medsComboBox.Size = new Size(window.Width * 20 / 100, window.Height * 3 / 20);
            medsComboBox.Location = new Point(window.Width * 50 / 1000, window.Height * 5 / 20);
            medsComboBox.TextChanged += new EventHandler(MedsComboSearch);
            medsComboBox.GotFocus += new EventHandler(MedsComboSearch);
            medsComboBox.SelectedIndexChanged += new EventHandler(MedsComboSearch);
            foreach(PRODUIT med in ProductController.getProducts())
            {
                medsComboBox.Items.Add(med);
            }
            #endregion

            #region PrescriptonBox
            prescriptionTextBox = new RichTextBox();
            prescriptionTextBox.Size = new Size(window.Width * 20 / 100, window.Height * 3 / 20);
            prescriptionTextBox.Location = new Point(window.Width * 50 / 1000, window.Height * 7 / 20);
            prescriptionTextBox.TextChanged += new EventHandler(prescriptionTextBoxChanged);
            prescription = "";
            #endregion
            
            #region DiagnosticBox
            diagTextBox= new RichTextBox();
            diagTextBox.Size = new Size(window.Width * 20 / 100, window.Height * 3 / 20);
            diagTextBox.Location = new Point(window.Width * 270 / 1000, window.Height * 7 / 20);
            diagTextBox.TextChanged += new EventHandler(diagTextBoxChanged);
            diagnostic = "";
            #endregion

            #region descriptionBox

            descriptionTextBox = new TextBox();
            descriptionTextBox.ReadOnly = true;
            descriptionTextBox.Text = "";
            descriptionTextBox.Font = new Font("Poppins", window.Height * 1 / 100);
            descriptionTextBox.ForeColor = Color.Black;
            descriptionTextBox.BackColor = Color.White;
            descriptionTextBox.Multiline = true;
            descriptionTextBox.Size = new Size(window.Width * 45 / 100, window.Height * 60 / 100);
            descriptionTextBox.Location = new Point(window.Width * 490 / 1000, window.Height * 9 / 40);
            

            #endregion



            window.Controls.Add(careComboBox);
            window.Controls.Add(medsComboBox);
            window.Controls.Add(descriptionTextBox);
            window.Controls.Add(prescriptionTextBox);
            window.Controls.Add(diagTextBox);
        }

        private void setDescription()
        {
            descriptionTextBox.Clear();

            descriptionTextBox.AppendText("Date : " + rdv.JOURNEE + Environment.NewLine);

            descriptionTextBox.AppendText("Horraire : " + rdv.HEUREDEBUT.ToString() + " à " + rdv.HEUREFIN.ToString() + Environment.NewLine);
            descriptionTextBox.AppendText("Client : " + rdv.CLIENT + Environment.NewLine);
            descriptionTextBox.AppendText("Animal : " + animal + Environment.NewLine);
            foreach(SOIN care in cares)
            {
                descriptionTextBox.AppendText("    " + care);
            }
            descriptionTextBox.AppendText("Diagnostique : " + diagnostic + Environment.NewLine);
            descriptionTextBox.AppendText("Prescription : " + prescription + Environment.NewLine);
            descriptionTextBox.AppendText("Médicaments prescrits : " + Environment.NewLine);
            foreach (KeyValuePair<PRODUIT,int> product  in products)
            {
                descriptionTextBox.AppendText("    " + product.Key.ToString() + " x " + product.Value + Environment.NewLine);
            }

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
            setDescription();
        }

        private void prescriptionTextBoxChanged(object sender, EventArgs e)
        {
            Console.WriteLine(prescription);
            prescription = prescriptionTextBox.Text;
            setDescription();
        }

        private void MedsComboSearch(object sender, EventArgs e)
        {
            
            selectedProduct = (PRODUIT)medsComboBox.SelectedItem;
            
            if(selectedProduct == null)
            {
                medsComboBox.Items.Clear();
            }
            
            if (medsComboBox.Text.Length == 0)
            {
                ICollection<PRODUIT> produits = ProductController.getProducts();
                foreach(PRODUIT product in produits)
                {
                    medsComboBox.Items.Add(product);
                }
            }
            else
            {
                List<PRODUIT> produits = ProductController.getByName(medsComboBox.Text);
                foreach (PRODUIT product in produits)
                {
                    medsComboBox.Items.Add(product);
                }
                medsComboBox.Select(medsComboBox.Text.Length, 0);
            }
             
        }

        private void careComboSearch(object sender, EventArgs e)
        {
            selectedCare = (SOIN)careComboBox.SelectedItem;
            if (selectedCare == null)
            {
                careComboBox.Items.Clear();
            }

            if (careComboBox.Text.Length == 0)
            {
                ICollection<PRODUIT> produits = ProductController.getProducts();
                foreach (PRODUIT product in produits)
                {
                    careComboBox.Items.Add(product);
                }
            }
            else
            {
                List<PRODUIT> produits = ProductController.getByName(careComboBox.Text);
                foreach (PRODUIT product in produits)
                {
                    careComboBox.Items.Add(product);
                }
            }
            careComboBox.Select(careComboBox.Text.Length, 0);
        }


        #endregion

        #region Buttons

        private void careMinusClick(object sender, EventArgs e)
        {
            if (cares.Contains(selectedCare))
            {
                cares.Remove(selectedCare);
            }
            setDescription();
        }

        private void carePlusClick(object sender, EventArgs e)
        {
            if (!cares.Contains(selectedCare))
            {
                cares.Add(selectedCare);
            }
            setDescription();
        }

        private void medsMinusClick(object sender, EventArgs e)
        {
            if (products.ContainsKey(selectedProduct))
            {
                products[selectedProduct]--;
                if (products[selectedProduct] <= 0)
                {
                    products.Remove(selectedProduct);
                }
            }
            setDescription();
        }

        private void medsPlusClick(object sender, EventArgs e)
        {
            if (products.ContainsKey(selectedProduct))
            {
                products[selectedProduct]++;

            }
            else
            {
                products[selectedProduct] = 1;
            }
            setDescription();
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
