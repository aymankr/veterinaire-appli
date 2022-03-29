using System;
using System.Drawing;
using System.Windows.Forms;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.api.controllers;


namespace Mauxnimale_CE2.ui
{
    class InterfaceDiseaseAndCares : AInterface
    {
        private readonly Header header;
        private readonly Footer footer;

        private UIRoundButton backButton;

        private TextBox researchDisease, researchCare;
        private ListBox diseasesLB, caresLB;
        private UIButton newDisease, updateDisease, deleteDisease, newCare, updateCare, deleteCare;

        private MALADIE selectedDisease;
        private SOIN selectedCare;

        public InterfaceDiseaseAndCares(MainWindow window, SALARIE user) : base(window, user)
        {
            header = new Header(window);
            footer = new Footer(window, base.user);
        }

        public override void load()
        {
            header.load("Gestions des maladies et soins");
            footer.load();
            GenerateListBox();
            GenerateTextBox();
            GenerateButton();
            AddDataInDiseases();
            AddDataInCares();


        }

        private void AddDataInDiseases()
        {
            diseasesLB.Items.Clear();
            diseasesLB.Items.Add(" ");
            foreach(MALADIE disease in CareAndDiseaseController.AllDiseases())
            {
                diseasesLB.Items.Add(disease);
            }
        }
        
        private void AddDataInCares()
        {
            if(selectedDisease == null)
            {
            caresLB.Items.Clear();
            caresLB.Items.Add(" ");
            foreach (SOIN care in CareAndDiseaseController.AllCares())
            {
                caresLB.Items.Add(care);
            }
            } else
            {
                caresLB.Items.Clear();
                caresLB.Items.Add(" ");
                foreach (SOIN care in CareAndDiseaseController.ListCaresByDisease(selectedDisease))
                {
                    caresLB.Items.Add(care);
                }
            }
        }

        public void GenerateButton()
        {
            newDisease = new UIButton(UIColor.ORANGE, "Nouveau", window.Width / 8)
            {
                Height = window.Height / 25,
                Location = new Point(window.Width * 20 / 1000, window.Height * 10 / 15)
            };
            newDisease.Click += new EventHandler(OpenNewCareAndDiseseaseInterface);
            window.Controls.Add(newDisease);

            deleteDisease = new UIButton(UIColor.ORANGE, "Supprimer", window.Width / 8)
            {
                Height = window.Height / 25,
                Location = new Point(window.Width * 295 / 1000, window.Height * 10 / 15)
            };
            deleteDisease.Click += new EventHandler(DeleteDisease);

            updateDisease = new UIButton(UIColor.ORANGE, "Modifier", window.Width / 8)
            {
                Height = window.Height / 25,
                Location = new Point(window.Width * 157 / 1000, window.Height * 10 / 15)
            };
            updateDisease.Click += new EventHandler(OpenUpdateCareAndDiseaseInterface);

            newCare = new UIButton(UIColor.ORANGE, "Nouveau", window.Width / 8)
            {
                Height = window.Height / 25,
                Location = new Point(window.Width * 520 / 1000, window.Height * 10 / 15)
            };
            newCare.Click += new EventHandler(OpenNewCareAndDiseseaseInterface);
            window.Controls.Add(newCare);

            deleteCare = new UIButton(UIColor.ORANGE, "Supprimer", window.Width / 8)
            {
                Height = window.Height / 25,
                Location = new Point(window.Width * 789 / 1000, window.Height * 10 / 15)
            };
            deleteCare.Click += new EventHandler(DeleteCare);

            updateCare = new UIButton(UIColor.ORANGE, "Modifier", window.Width / 8)
            {
                Height = window.Height / 25,
                Location = new Point(window.Width * 655 / 1000, window.Height * 10 / 15)
            };
            updateCare.Click += new EventHandler(OpenUpdateCareAndDiseaseInterface);
        }

        /// <summary>
        /// Permet de générer toutes les ComboBox de l'interface.s
        /// </summary>
        public void GenerateListBox()
        {
            diseasesLB = new ListBox
            {
                Font = new Font("Poppins", window.Height / 100),
                ForeColor = Color.Black,
                BackColor = Color.White,
                Location = new Point(window.Width * 20 / 1000, window.Height * 2 / 14),
                Size = new Size(window.Width * 40 / 100, window.Height * 50 / 100)
            };
            window.Controls.Add(diseasesLB);
            diseasesLB.SelectedIndexChanged += new EventHandler(DiseaseSelection);

            caresLB = new ListBox
            {
                Font = new Font("Poppins", window.Height / 100),
                ForeColor = Color.Black,
                BackColor = Color.White,
                Location = new Point(window.Width * 520 / 1000, window.Height * 2 / 14),
                Size = new Size(window.Width * 40 / 100, window.Height * 50 / 100)
            };
            window.Controls.Add(caresLB);
            caresLB.SelectedIndexChanged += new EventHandler(CareSelection);
        }

        private void GenerateTextBox()
        {
            researchDisease = new TextBox
            {
                Text = "Recherche...",
                Font = new Font("Poppins", window.Height * 1 / 100),
                ForeColor = Color.Gray,
                BackColor = Color.White,
                Location = new Point(window.Width * 20 / 1000, window.Height / 10),
                Size = new Size(window.Width * 40 / 100, window.Height * 50 / 100)
            };
            window.Controls.Add(researchDisease);
            researchDisease.TextChanged += new EventHandler(Research);
            researchDisease.GotFocus += new EventHandler(GotFocus);

            researchCare = new TextBox
            {
                Text = "Recherche...",
                Font = new Font("Poppins", window.Height * 1 / 100),
                ForeColor = Color.Gray,
                BackColor = Color.White,
                Location = new Point(window.Width * 520 / 1000, window.Height / 10),
                Size = new Size(window.Width * 40 / 100, window.Height * 50 / 100)
            };
            window.Controls.Add(researchCare);
            researchCare.TextChanged += new EventHandler(Research);
            researchCare.GotFocus += new EventHandler(GotFocus);
        }

        private void GotFocus(object sender, EventArgs e)
        {
            if(sender.Equals(researchCare) && researchCare.Text == "Recherche...")
            {
                researchCare.Text = "";
                researchCare.ForeColor = Color.Black;
            }
            if (sender.Equals(researchDisease) && researchDisease.Text == "Recherche...")
            {
                researchDisease.Text = "";
                researchDisease.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Permet de cherche une maladie ou un soin avec leur barre de recherche
        /// </summary>
        /// <param name="sender">Barre de recherche</param>
        /// <param name="e">changement du text</param>
        private void Research(object sender, EventArgs e)
        {
            if (sender.Equals(researchDisease))
            {
                if(researchDisease.Text != "")
                {
                diseasesLB.Items.Clear();
                diseasesLB.Items.Add(" ");
                foreach (MALADIE disease in CareAndDiseaseController.SearchDiseasesByName(researchDisease.Text))
                {
                    diseasesLB.Items.Add(disease);
                }
                } else
                {
                    AddDataInDiseases();
                }
            }
            if (sender.Equals(researchCare)){
                if (researchCare.Text != "")
                {
                caresLB.Items.Clear();
                caresLB.Items.Add(" ");
                foreach (SOIN care in CareAndDiseaseController.SearchCaresByNames(researchCare.Text))
                {
                    caresLB.Items.Add(care);
                }
                } else
                {
                    AddDataInCares();
                }
            }
        }

        private void CareSelection(object sender, EventArgs e)
        {
            selectedCare = (caresLB.SelectedItem != " ") ? (SOIN)caresLB.SelectedItem : null;
            if (selectedCare != null)
            {
                window.Controls.Add(updateCare);
                window.Controls.Add(deleteCare);
            } else
            {
                window.Controls.Remove(updateCare);
                window.Controls.Remove(deleteCare);
            }
            window.Refresh();
        }

        private void DiseaseSelection(object sender, EventArgs e)
        {
            selectedDisease = (diseasesLB.SelectedItem != " ") ? (MALADIE)diseasesLB.SelectedItem : null;

            if (selectedDisease != null)
            {
                window.Controls.Add(updateDisease);
                window.Controls.Add(deleteDisease);
                caresLB.SelectedItem = null;
                AddDataInCares();
            }
            else
            {
                window.Controls.Remove(updateDisease);
                window.Controls.Remove(deleteDisease);
                AddDataInCares();
            }
            window.Refresh();
        }
        
        private void DeleteCare(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Êtes vous sure de vouloir supprimer ce soin. Cette action sera IRRÉVERSIBLE.", "Demande de comfirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if(result == DialogResult.OK)
            {
                CareAndDiseaseController.RemoveCare(selectedCare);
                MessageBox.Show("Le soin a été supprimé avec succès.", "Validation de suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void OpenUpdateCareAndDiseaseInterface(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DeleteDisease(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Êtes vous sure de vouloir supprimer cette maladie. Cette action sera IRRÉVERSIBLE.", "Demande de comfirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                CareAndDiseaseController.RemoveDisease(selectedDisease);
                MessageBox.Show("La maladie a été supprimée avec succès.", "Validation de suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void OpenNewCareAndDiseseaseInterface(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}
