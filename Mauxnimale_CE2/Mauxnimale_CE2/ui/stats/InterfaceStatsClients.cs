using System;
using System.Drawing;
using System.Windows.Forms;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.api.controllers;


namespace Mauxnimale_CE2.ui
{
    internal class InterfaceStatsClient : AInterface
    {
        readonly Header header;
        readonly Footer footer;

        Label nbCLientLabel, nbAnimalLabel, nbBillsLabel, nbRDVLabel;
        TextBox totalClient, totalAnimal, totalBills, totalRDV;

        UIRoundButton backButton;

        public InterfaceStatsClient(MainWindow window, SALARIE user) : base(window, user)
        {
            header = new Header(window);
            footer = new Footer(window, user);
        }

        public override void load()
        {
            header.load("Statistique de la clientèle");
            footer.load();

            CreateBackButton();
            GenerateStats();
        }

        /// <summary>
        /// Permet de générer les éléments de la fenêtre.
        /// </summary>
        private void GenerateStats()
        {
            nbCLientLabel = new Label()
            {
                Text = "Nombre total de clients",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = UIColor.DARKBLUE,
                Size = new Size(window.Width * 2 / 10, window.Height * 1 / 10),
                Location = new Point(window.Width * 2 / 15, window.Height * 2 / 20)
            };
            window.Controls.Add(nbCLientLabel);

            totalClient = new TextBox()
            {
                Font = new Font("Poppins", window.Height * 4 / 100),
                Location = new Point(window.Width * 2 / 10, window.Height * 4 / 20),
                Size = new Size(window.Width * 2 / 10, window.Height * 2 / 20),
                Enabled = false,
                TextAlign = HorizontalAlignment.Center,
                Text = ClientController.AllClient().Count.ToString()
            };
            totalClient.BringToFront();
            window.Controls.Add(totalClient);

            nbAnimalLabel = new Label()
            {
                Text = "Nombre total d'animaux",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = UIColor.DARKBLUE,
                Location = new Point(window.Width * 2 / 15, window.Height * 5 / 20),
                Size = new Size(window.Width * 2 / 10, window.Height * 1 / 10)
            };
            window.Controls.Add(nbAnimalLabel);

            totalAnimal = new TextBox()
            {
                Font = new Font("Poppins", window.Height * 4 / 100),
                Location = new Point(window.Width * 2 / 10, window.Height * 7 / 20),
                Size = new Size(window.Width * 2 / 10, window.Height * 2 / 20),
                Enabled = false,
                TextAlign = HorizontalAlignment.Center,
                Text = AnimalController.AllAnimals().Count.ToString()
            };
            totalAnimal.BringToFront();
            window.Controls.Add(totalAnimal);

            nbBillsLabel = new Label()
            {
                Text = "Nombre total de factures",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = UIColor.DARKBLUE,
                Location = new Point(window.Width / 20, window.Height * 8 / 20),
                Size = new Size(window.Width * 4 / 10, window.Height * 1 / 10)
            };
            window.Controls.Add(nbBillsLabel);

            totalBills = new TextBox()
            {
                Font = new Font("Poppins", window.Height * 4 / 100),
                Location = new Point(window.Width * 2 / 10, window.Height * 10 / 20),
                Size = new Size(window.Width * 2 / 10, window.Height * 2 / 20),
                Enabled = false,
                TextAlign = HorizontalAlignment.Center,
                Text = InvoiceController.allInvoices().Count.ToString()
            };
            totalBills.BringToFront();
            window.Controls.Add(totalBills);

            nbRDVLabel = new Label()
            {
                Text = "Nombre total de factures",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = UIColor.DARKBLUE,
                Location = new Point(window.Width / 20, window.Height * 11 / 20),
                Size = new Size(window.Width * 4 / 10, window.Height * 1 / 10)
            };
            window.Controls.Add(nbRDVLabel);

            totalRDV = new TextBox()
            {
                Font = new Font("Poppins", window.Height * 4 / 100),
                Location = new Point(window.Width * 2 / 10, window.Height * 13 / 20),
                Size = new Size(window.Width * 2 / 10, window.Height * 2 / 20),
                Enabled = false,
                TextAlign = HorizontalAlignment.Center,
                Text = AppointmentController.AllAppointment().Count.ToString()
            };
            totalRDV.BringToFront();
            window.Controls.Add(totalRDV);
        }

        #region Back button
        private void BackPage(object sender, EventArgs e)
        {
            // Permet de retourne sur l'interface qui avait lancée cette interface
            window.Controls.Clear();
            window.switchInterface(new InterfaceStatsPage(window, user));
        }

        private void CreateBackButton()
        {
            backButton = new UIRoundButton(window.Width / 20, "<")
            {
                Location = new Point(window.Width * 9 / 10, window.Height / 10)
            };
            window.Controls.Add(backButton);
            backButton.Click += new EventHandler(BackPage);
        }
        #endregion
    }
}
