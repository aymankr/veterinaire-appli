using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api;
using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.api.controllers;

namespace Mauxnimale_CE2.ui.appointments
{
    internal class InterfaceAppointmentManagment : AInterface
    {
        Header header;
        Footer footer;

        UIButton modifConsult, createOrdonnance, deleteConsult, createConsult, switchDisplay, modifOrdonnance, deletePrescri;
        UIRoundButton back;
        MonthCalendar calendar;

        DateTime selectedDate;
        List<RENDEZ_VOUS> rdvOfDay;
        RENDEZ_VOUS selectedRdv;
        ANIMAL selectedAnimal;
        ORDONNANCE ordonnance;

        Label calendarLabel;
        ListBox consultOfDay;
        ComboBox animalsAtRDV;
        TextBox infosConsult, prescription;

        bool prescriptionMode = false;

        /// <summary>
        /// Constructeur de l'interface
        /// </summary>
        /// <param name="window"></param>
        /// <param name="user"></param>
        public InterfaceAppointmentManagment(MainWindow window, SALARIE user) : base(window, user)
        {
            header = new Header(window);
            footer = new Footer(window, base.user);
            selectedRdv = null;

        }

        /// <summary>
        /// Génère l'interface
        /// </summary>
        public override void load()
        {
            header.load("Mauxnimale - Gestion des Consultations");
            footer.load();
            generateButton();
            generateLabels();
            generateListBox();
        }

        /// <summary>
        /// Génère les labels
        /// </summary>
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

        /// <summary>
        /// Génère les différentes box de l'interface
        /// </summary>
        public void generateListBox()
        {

            #region ConsultOfDayBox
            consultOfDay = new ListBox();
            consultOfDay.Text = "";
            consultOfDay.Font = new Font("Poppins", window.Height * 1 / 100);
            consultOfDay.ForeColor = Color.Black;
            consultOfDay.BackColor = Color.White;
            consultOfDay.Location = new Point(window.Width * 275 / 1000, window.Height * 2 / 10);
            consultOfDay.Size = new Size(window.Width * 20 / 100, window.Height * 45 / 100);
            consultOfDay.SelectedIndexChanged += new EventHandler(rdvSelection);
            #endregion

            #region animalsAtRDV

            animalsAtRDV = new ComboBox();
            animalsAtRDV.Text = "";
            animalsAtRDV.Font = new Font("Poppins", window.Height * 1 / 100);
            animalsAtRDV.ForeColor = Color.Black;
            animalsAtRDV.BackColor = Color.White;
            animalsAtRDV.DropDownStyle = ComboBoxStyle.DropDownList;
            animalsAtRDV.Location = new Point(window.Width * 500 / 1000, window.Height * 2 / 10);
            animalsAtRDV.Size = new Size(window.Width * 40 / 100, window.Height * 5 / 100);
            animalsAtRDV.SelectedIndexChanged += new EventHandler(AnimalComboBoxSearch);
            animalsAtRDV.Enabled = false;
            animalsAtRDV.Hide();
            #endregion

            #region InfosConsult

            infosConsult = new TextBox();
            infosConsult.ReadOnly = true;   
            infosConsult.Text = "";
            infosConsult.Font = new Font("Poppins", window.Height * 1 / 100);
            infosConsult.ForeColor = Color.Black;
            infosConsult.BackColor = Color.White;
            infosConsult.Multiline = true;
            infosConsult.Location = new Point(window.Width * 500 / 1000, window.Height * 2 / 10);
            infosConsult.Size = new Size(window.Width * 40 / 100, window.Height * 45 / 100);
            #endregion

            #region Prescription

            prescription = new TextBox();
            prescription.ReadOnly = true;
            prescription.Text = "";
            prescription.Font = new Font("Poppins", window.Height * 1 / 100);
            prescription.ForeColor = Color.Black;
            prescription.BackColor = Color.White;
            prescription.Multiline = true;
            prescription.Location = new Point(window.Width * 500 / 1000, window.Height * 5 / 20);
            prescription.Size = new Size(window.Width * 40 / 100, window.Height * 40 / 100);
            prescription.Enabled = false;
            prescription.Hide();

            #endregion

            #region calendar
            calendar = new MonthCalendar();
            calendar.Location = new Point(window.Width * 25 / 1000, window.Height * 2 / 10);
            calendar.Size = new Size(window.Width * 20 / 100, window.Height * 45 / 100);
            calendar.DateSelected += new DateRangeEventHandler(dateSelection);
            selectedDate = DateTime.Now;
            List<DateTime> dateRDVs = new List<DateTime>(); 
            foreach(RENDEZ_VOUS rdv in AppointmentController.getAllRDV())
            {
                dateRDVs.Add(rdv.JOURNEE.DATE);
            }
            calendar.BoldedDates = dateRDVs.ToArray();
            #endregion

            window.Controls.Add(consultOfDay);
            window.Controls.Add(animalsAtRDV);
            window.Controls.Add(prescription);
            window.Controls.Add(infosConsult);
            window.Controls.Add(calendar);
        }

        /// <summary>
        /// Génère les boutons de l'interface
        /// </summary>
        public void generateButton()
        {

            #region Modif Button
            switchDisplay = new UIButton(UIColor.DARKBLUE, "Afficher Ordonnances", window.Width * 3 / 20);
            switchDisplay.Location = new Point(window.Width * 30 / 40, window.Height * 14 / 20);
            switchDisplay.Click += new EventHandler(SwitchDisplauClick);
            switchDisplay.Enabled = false;
            #endregion

            #region Modif Button
            modifConsult = new UIButton(UIColor.DARKBLUE, "Modifier Consultation", window.Width * 3 / 20);
            modifConsult.Location = new Point(window.Width * 16 / 40, window.Height * 14 / 20);
            modifConsult.Click += new EventHandler(modifConsultClick);
            modifConsult.Enabled = false;
            #endregion

            #region modifOrdonnance
            modifOrdonnance = new UIButton(UIColor.DARKBLUE, "Modifier Ordonnance", window.Width * 3 / 20);
            modifOrdonnance.Location = new Point(window.Width * 16 / 40, window.Height * 14 / 20);
            modifOrdonnance.Click += new EventHandler(modifOrdonnanceClick);
            modifOrdonnance.Enabled = false;
            modifOrdonnance.Hide();
            #endregion

            #region CreateOrdoButton
            createOrdonnance = new UIButton(UIColor.DARKBLUE, "Créer ordonance", window.Width * 3 / 20);
            createOrdonnance.Location = new Point(window.Width * 23 / 40, window.Height * 14 / 20);
            createOrdonnance.Click += new EventHandler(createOrdonanceClick);
            createOrdonnance.Enabled = false;    
            #endregion

            #region deleteConsult Button
            deleteConsult = new UIButton(UIColor.DARKBLUE, "Supprimer Consultation", window.Width * 3 / 20);
            deleteConsult.Location = new Point(window.Width * 9 / 40, window.Height * 14 / 20);
            deleteConsult.Click += new EventHandler(deleteConsultClick);
            deleteConsult.Enabled = false;
            #endregion

            #region deletePrescri Button
            deletePrescri = new UIButton(UIColor.DARKBLUE, "Supprimer Ordonnance", window.Width * 3 / 20);
            deletePrescri.Location = new Point(window.Width * 9 / 40, window.Height * 14 / 20);
            deletePrescri.Click += new EventHandler(deletePrescriClick);
            deletePrescri.Enabled = false;
            deletePrescri.Hide();
            #endregion

            #region createButton
            createConsult = new UIButton(UIColor.DARKBLUE, "Créer Consultation", window.Width * 3 / 20);
            createConsult.Location = new Point(window.Width * 2 / 40, window.Height * 14 / 20);
            createConsult.Click += new EventHandler(createConsultClick);
            #endregion

            #region Back Button
            back = new UIRoundButton(window.Width / 20, "<");
            back.Location = new Point(window.Width * 9 / 10, window.Height / 10);
            back.Click += new EventHandler(backClick);
            #endregion

            window.Controls.Add(switchDisplay);
            window.Controls.Add(modifConsult);
            window.Controls.Add(createOrdonnance);
            window.Controls.Add(deleteConsult);
            window.Controls.Add(deletePrescri);
            window.Controls.Add(createConsult);
            window.Controls.Add(modifOrdonnance);
            window.Controls.Add(back);
        }


        #region eventHandler

        #region Selection
        
        /// <summary>
        /// Permet de sélectionner une journée pour voir les rendez vous de cette journée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateSelection(object sender, DateRangeEventArgs e)
        {
            infosConsult.Clear();
            consultOfDay.Items.Clear();
            prescription.Clear();
            animalsAtRDV.Items.Clear();
            selectedAnimal = null;
            selectedRdv = null;

            selectedDate = new DateTime(e.Start.Year, e.Start.Month, e.Start.Day);

            if (DayController.getDay(selectedDate) == null)
            {
                DayController.addDay(selectedDate);
            }

            rdvOfDay = new List<RENDEZ_VOUS>(AppointmentController.getAppointmentsFromDate(selectedDate));

            foreach (RENDEZ_VOUS rdv in rdvOfDay)
            {
                consultOfDay.Items.Add(rdv);
            }
            EnableButtons();
        }


        /// <summary>
        /// Permet de sélectionner un rendez vous et d'afficher les informations de ce rendez vous et ses ordonnance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdvSelection(object sender, EventArgs e)
        {
            infosConsult.Clear();
            prescription.Clear();
            animalsAtRDV.Items.Clear();
            selectedAnimal = null;

            selectedRdv = (RENDEZ_VOUS)consultOfDay.SelectedItem;

            if(selectedRdv != null)
            {

                #region description Rdv
                infosConsult.AppendText("Horraire : " + selectedRdv.HEUREDEBUT + " à " + selectedRdv.HEUREFIN + Environment.NewLine);
                infosConsult.AppendText("Client : " + selectedRdv.CLIENT + Environment.NewLine);
                infosConsult.AppendText("Type de rendez-vous : " + selectedRdv.TYPE_RDV.ToString() + Environment.NewLine);
                infosConsult.AppendText("Animaux :" + Environment.NewLine);

                foreach (ANIMAL animal in AppointmentController.getAnimalFromRDV(selectedRdv))
                {
                    infosConsult.AppendText("    " + animal + Environment.NewLine);
                    animalsAtRDV.Items.Add(animal);
                }


                infosConsult.AppendText("Description : " + Environment.NewLine);
                infosConsult.AppendText(selectedRdv.RAISON + Environment.NewLine);

                #endregion
            }
            EnableButtons();

        }
        
        /// <summary>
        /// Permet d'afficher l'ordonnance d'un animal pour un rendez vous
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimalComboBoxSearch(object sender, EventArgs e)
        {
            selectedAnimal = (ANIMAL)animalsAtRDV.SelectedItem;
            prescription.Clear();
            ordonnance = PrescriptionController.GetORDONNANCEFromRDVAndAnimal(selectedRdv, selectedAnimal);
            if(ordonnance == null)
            {
                prescription.AppendText("PAS D'ORDONNANCE POUR LE MOMENT");
            }else{
                prescription.AppendText("Date : " + ordonnance.RENDEZ_VOUS.JOURNEE + Environment.NewLine);
                prescription.AppendText("Horraire : " + ordonnance.RENDEZ_VOUS.HEUREDEBUT + " à " + ordonnance.RENDEZ_VOUS.HEUREFIN + Environment.NewLine);
                prescription.AppendText("Client : " + ordonnance.RENDEZ_VOUS.CLIENT + Environment.NewLine);
                prescription.AppendText("Animal : " + ordonnance.ANIMAL + Environment.NewLine);
                prescription.AppendText("Soins effectués: " + ordonnance.ANIMAL + Environment.NewLine);
                foreach(LIEN_SOIN care in ordonnance.LIEN_SOIN)
                {
                    prescription.AppendText("    " + care.SOIN + Environment.NewLine);
                }
                prescription.AppendText("Diagnostique : " + ordonnance.DIAGNOSTIQUE + Environment.NewLine);
                prescription.AppendText("Prescription : " + ordonnance.PRESCRIPTION + Environment.NewLine);
                prescription.AppendText("Médicaments prescrits : " + Environment.NewLine);
                foreach (PRODUITLIES product in ordonnance.PRODUITLIES)
                {
                    prescription.AppendText("    " + product.PRODUIT.ToString() + " x " + product.QUANTITEPRODUITS + Environment.NewLine);
                }

            }
            EnableButtons();


        }

        #endregion

        #region Buttons

        /// <summary>
        /// Permet d'afficher les boutons correspondants et possiblement cliquables dans la situation actuelle de l'interface
        /// </summary>
        public void EnableButtons()
        {
            if(selectedRdv != null)
            {
                if (selectedAnimal != null)
                {
                    if(ordonnance != null)
                    {
                        modifOrdonnance.Enabled = true;
                        deletePrescri.Enabled = true;
                        createOrdonnance.Enabled = false;

                    }
                    else
                    {                 
                        modifOrdonnance.Enabled = false;
                        deletePrescri.Enabled = false;
                        createOrdonnance.Enabled = true;
                    }
                }
                deleteConsult.Enabled = true;
                modifConsult.Enabled = true;
                switchDisplay.Enabled = true;
            }
            else 
            {
                createOrdonnance.Enabled = false;
                deleteConsult.Enabled = false;
                modifConsult.Enabled = false;
                switchDisplay.Enabled = false;
                modifOrdonnance.Enabled = false;
                deletePrescri.Enabled = false;
            }
        }

        /// <summary>
        /// Renvoie vers l'interface Home
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void backClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceHome(window, user));
        }

        /// <summary>
        /// Change le mode de l'interfacepour afficher les ordonnace ou les infos du rdv
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwitchDisplauClick(object sender, EventArgs e)
        {
            prescriptionMode = !prescriptionMode;
            if (prescriptionMode)
            {
                switchDisplay.Text = "Afficher RDV";

                infosConsult.Hide();
                infosConsult.Enabled = false;
                modifConsult.Hide();
                modifConsult.Enabled = false;
                deleteConsult.Hide();
                deleteConsult.Enabled = false;

                prescription.Show();
                prescription.Enabled = true;
                animalsAtRDV.Show();
                animalsAtRDV.Enabled = true;
                modifOrdonnance.Show();
                deletePrescri.Show();
            }
            else
            {
                switchDisplay.Text = "Afficher Ordonnances";

                prescription.Hide();
                prescription.Enabled = false;
                animalsAtRDV.Hide();
                animalsAtRDV.Enabled = false;
                modifOrdonnance.Hide();
                modifOrdonnance.Enabled = false;
                deletePrescri.Hide();
                deletePrescri.Enabled = false;

                modifConsult.Show();    
                modifConsult.Enabled=true;
                infosConsult.Show();
                infosConsult.Enabled = true;
                deleteConsult.Show();
                deleteConsult.Enabled=true;

            }
        }

        /// <summary>
        /// Renvoie vers l'interface de modification de la consultation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void modifConsultClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceAppointmentModification(window, user, selectedRdv));
        }
       
        /// <summary>
        /// Renvoie vers l'interface de modification d'une ordonnace
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void modifOrdonnanceClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfacePrescriptionCreation(window, user, ordonnance));
        }

        /// <summary>
        /// Supprime l'ordonnance sélectionnée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void deletePrescriClick(object sender, EventArgs e)
        {
            DialogResult mb = MessageBox.Show("Voulez vous vraiment supprimer cette ordonnance ?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (mb == DialogResult.OK)
            {
                PrescriptionController.DeletePrescription(ordonnance);
                window.Controls.Clear();
                this.load();
            }
        }

        /// <summary>
        /// Crée une ordonnance pour l'animal sélectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void createOrdonanceClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfacePrescriptionCreation(window, user, selectedRdv,selectedAnimal));
        }


        /// <summary>
        /// Supprime le rendez vous sélectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void deleteConsultClick(object sender, EventArgs e)
        {
            if (selectedRdv!=null)
            {
                DialogResult mb = MessageBox.Show("Voulez vous vraiment supprimer ce rendez vous et toutes informations liées (ses ordonnances)", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (mb == DialogResult.OK)
                {
                    AppointmentController.deleteAppointment(selectedRdv);
                    window.Controls.Clear();
                    this.load();
                }
            }
        }

        /// <summary>
        /// Renvoie vers la page de création d'une consultation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void createConsultClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceAppointmentCreation(window, user));
        }
        #endregion

        #endregion
    }
}



