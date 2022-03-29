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
    internal class InterfaceAppointmentManagment : AInterface
    {
        Header header;
        Footer footer;

        UIButton modifConsult, createOrdonnance, deleteConsult, createConsult, switchDisplay, modifOrdonnance;
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

        public InterfaceAppointmentManagment(MainWindow window, SALARIE user) : base(window, user)
        {
            header = new Header(window);
            footer = new Footer(window, base.user);
            selectedRdv = null;

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
            #endregion

            window.Controls.Add(consultOfDay);
            window.Controls.Add(animalsAtRDV);
            window.Controls.Add(prescription);
            window.Controls.Add(infosConsult);
            window.Controls.Add(calendar);
        }

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

            #region delete Button
            deleteConsult = new UIButton(UIColor.DARKBLUE, "Supprimer Consultation", window.Width * 3 / 20);
            deleteConsult.Location = new Point(window.Width * 9 / 40, window.Height * 14 / 20);
            deleteConsult.Click += new EventHandler(deleteConsultClick);
            deleteConsult.Enabled = false;
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
            window.Controls.Add(createConsult);
            window.Controls.Add(modifOrdonnance);
            window.Controls.Add(back);
        }

        #region eventHandler

        #region Selection
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
        public void EnableButtons()
        {
            if(selectedRdv != null)
            {
                if (selectedAnimal != null)
                {
                    if(ordonnance != null)
                    {
                        modifOrdonnance.Enabled = true;
                        createOrdonnance.Enabled = false;

                    }
                    else
                    {                 
                        createOrdonnance.Enabled = true;
                        modifOrdonnance.Enabled = false;
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
            }
        }

        public void backClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceHome(window, user));
        }

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

                prescription.Show();
                prescription.Enabled = true;
                animalsAtRDV.Show();
                animalsAtRDV.Enabled = true;
                modifOrdonnance.Show();
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

                modifConsult.Show();    
                modifConsult.Enabled=true;
                infosConsult.Show();
                infosConsult.Enabled = true;
            }
        }

        public void modifConsultClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceAppointmentModification(window, user, selectedRdv));
        }
       
        private void modifOrdonnanceClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfacePrescriptionCreation(window, user, ordonnance));
        }

        public void createOrdonanceClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfacePrescriptionCreation(window, user, selectedRdv,selectedAnimal));
        }

        public void deleteConsultClick(object sender, EventArgs e)
        {
            if (selectedRdv!=null)
            {
                DialogResult mb = MessageBox.Show("Are you sure you want to Delete", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (mb == DialogResult.OK)
                {
                    AppointmentController.deleteAppointment(selectedRdv);
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

        #endregion
    }
}



