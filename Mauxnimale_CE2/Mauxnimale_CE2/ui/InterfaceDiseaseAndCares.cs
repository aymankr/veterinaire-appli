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

        /// <summary>
        /// Permet de générer tous les éléments de la fenêtre.
        /// </summary>
        public override void load()
        {
            header.load("Gestions des maladies et soins");
            footer.load();
            GenerateBackButton();
            GenerateListBox();
            GenerateTextBox();
            GenerateButton();
            AddDataInDiseases();
            AddDataInCares();
        }


        /// <summary>
        /// Pemet d'afficher toutes les maladies.
        /// </summary>
        private void AddDataInDiseases()
        {
            diseasesLB.Items.Clear();
            diseasesLB.Items.Add(" ");
            foreach(MALADIE disease in CareAndDiseaseController.AllDiseases())
            {
                diseasesLB.Items.Add(disease);
            }
        }
        
        /// <summary>
        /// Permet d'ajouter tous les soin si l'utilisateur n'a pas choisi de maladie.
        /// Permet d'ajouter tous les soins associées à une maladie si l'utilisateur en a choisi une.
        /// </summary>
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

        /// <summary>
        /// Pemrete de générer les boutons
        /// </summary>
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
        /// Permet de générer toutes les ComboBox de l'interface
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

        /// <summary>
        /// Permet de générer les TextBox, alias les barres de recherches.
        /// </summary>
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

        /// <summary>
        /// Permet de changer le texte quand l'utilisateur clic sur les barres de recherche.
        /// </summary>
        /// <param name="sender">Les barres de recherches</param>
        /// <param name="e">Prise de focus</param>
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

        /// <summary>
        /// Permet d'afficher les boutons de suppression liés à un soin.
        /// </summary>
        /// <param name="sender">Liste contenant les soins</param>
        /// <param name="e">Sélection d'un soin</param>
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

        /// <summary>
        /// Permet d'afficher les boutons de suppression liés à une maladie.
        /// </summary>
        /// <param name="sender">Liste contenant les maladies</param>
        /// <param name="e">Sélection d'un soin</param>
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
        
        /// <summary>
        /// Permet de supprimer un soin.
        /// </summary>
        /// <param name="sender">Bouton de suppression</param>
        /// <param name="e">clic</param>
        private void DeleteCare(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Êtes vous sure de vouloir supprimer ce soin. Cette action sera IRRÉVERSIBLE.", "Demande de comfirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if(result == DialogResult.OK)
            {
                CareAndDiseaseController.RemoveCare(selectedCare);
                MessageBox.Show("Le soin a été supprimé avec succès.", "Validation de suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AddDataInCares();
                AddDataInDiseases();
            }
        }

        /// <summary>
        /// Permet de supprimer une maladie.
        /// </summary>
        /// <param name="sender">Bouton de suppression</param>
        /// <param name="e">clic</param>
        private void DeleteDisease(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Êtes vous sure de vouloir supprimer cette maladie. Cette action sera IRRÉVERSIBLE.", "Demande de comfirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                CareAndDiseaseController.RemoveDisease(selectedDisease);
                MessageBox.Show("La maladie a été supprimée avec succès.", "Validation de suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AddDataInDiseases();
                AddDataInCares();
            }
        }

        /// <summary>
        /// Permet de lancer l'interface d'ajout de soins et maladies.
        /// </summary>
        /// <param name="sender">Tous les boutons "nouveau"</param>
        /// <param name="e">clic</param>
        private void OpenNewCareAndDiseseaseInterface(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceNewCaresAndDiseases(window, user));
        }

        /// <summary>
        /// Permet de lancer l'interface de modification suivant l'élément choisi.
        /// </summary>
        /// <param name="sender">Bouton de modification d'une maladie ou d'un soin</param>
        /// <param name="e">clic</param>
        private void OpenUpdateCareAndDiseaseInterface(object sender, EventArgs e)
        {
            window.Controls.Clear();
            if (sender.Equals(updateDisease))
            {
                window.switchInterface(new InterfaceUpdateCareOrDiseases(window, user, selectedDisease));
            } else if (sender.Equals(updateCare))
            {
                window.switchInterface(new InterfaceUpdateCareOrDiseases(window, user, selectedCare));
            }
        }

        #region BackButton
        private void GenerateBackButton()
        {
            backButton = new UIRoundButton(window.Width / 20, "<")
            {
                Location = new Point(window.Width * 9 / 10, window.Height / 10)
            };
            window.Controls.Add(backButton);
            backButton.Click += new EventHandler(BackPage);
        }

        private void BackPage(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceHome(window, user));
        }
        #endregion
    }
}
