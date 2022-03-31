using System;
using System.Drawing;
using System.Windows.Forms;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.api.controllers;
using System.Collections.Generic;

namespace Mauxnimale_CE2.ui.diseasesAndCares
{
    internal class InterfaceUpdateCareOrDiseases : AInterface
    {
        readonly Header header;
        readonly Footer footer;

        readonly MALADIE disease;
        readonly SOIN care;

        #region DiseaseForm elements
        Label nameDiseaseForm;
        TextBox currentDiseaseName, newDiseaseName, researchCare;
        ListBox currentCares, newCares, allPossibleCares;
        UIButton validateDiseaseForm;
        #endregion

        #region CareForm elements
        Label nameCareForm;
        TextBox currentCaresName, newCaresName;
        UIButton validateCareForm;
        #endregion

        UIRoundButton backButton;

        /// <summary>
        /// Constructeur de l'interface
        /// </summary>
        /// <param name="window"></param>
        /// <param name="user"></param>
        public InterfaceUpdateCareOrDiseases(MainWindow window, SALARIE user, object o) : base(window,user)
        {
            header = new Header(window);
            footer = new Footer(window, user);
            if(o.GetType().ToString().Contains("MALADIE"))
            {
                disease = (MALADIE)o;
            } else if (o.GetType().ToString().Contains("SOIN"))
            {
                care = (SOIN)o;
            }
        }

        /// <summary>
        /// Permet de charger les éléments de la fenêtre.
        /// </summary>
        public override void load()
        {
            GenerateBackButton();
            if(disease != null)
            {
                header.load("Modification d'une maladie");
                footer.load();
                GenerateUpdateDiseaseForm();
                GenerateData();

            } else if(care != null)
            {
                header.load("Modification d'un soin");
                footer.load();
                GenerateUpdateCareForm();
            }
        }

        /// <summary>
        /// Permet d'ajouter les données possibles et actuelles des soins de la maladie.
        /// </summary>
        private void GenerateData()
        {
            foreach(SOIN care in disease.SOIN)
            {
                currentCares.Items.Add(care);
                newCares.Items.Add(care);
            }
            foreach(SOIN care in CareAndDiseaseController.AllCares())
            {
                allPossibleCares.Items.Add(care);
            }
        }

        /// <summary>
        /// Permet de générer les éléments du formulaire de modification d'un soin
        /// </summary>
        public void GenerateUpdateCareForm()
        {
            nameCareForm = new Label()
            {
                Text = "Formulaire modification d'un soin",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = UIColor.DARKBLUE,
                Size = new Size(window.Width * 4 / 10, window.Height * 1 / 10),
                Location = new Point(window.Width * 5 / 15, window.Height * 3 / 20)
            };
            window.Controls.Add(nameCareForm);

            newCaresName = new TextBox()
            {
                Text = care.DESCRIPTION,
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = Color.Black,
                Size = new Size(window.Width * 3 / 10, window.Height * 1 / 10),
                Location = new Point(window.Width * 27 / 50, window.Height * 5 / 20)
            };
            window.Controls.Add(newCaresName);

            currentCaresName = new TextBox()
            {
                Text = care.DESCRIPTION,
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = Color.Black,
                Size = new Size(window.Width * 3 / 10, window.Height * 1 / 10),
                Location = new Point(window.Width * 8 / 50, window.Height * 5 / 20),
                Enabled = false
            };
            window.Controls.Add(currentCaresName);

            validateCareForm = new UIButton(UIColor.ORANGE, "Modifier", window.Width * 15 / 100)
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Height = window.Height / 25,
                Location = new Point(window.Width * 18 / 50, window.Height * 6 / 20),
                Size = new Size(window.Width * 3 / 10, window.Height / 15),
            };
            validateCareForm.Click += new EventHandler(SubmitCareForm);
            window.Controls.Add(validateCareForm);
        }

        /// <summary>
        /// Permet de modifier un soin.
        /// </summary>
        /// <param name="sender">Bouton de validation</param>
        /// <param name="e">clic</param>
        private void SubmitCareForm(object sender, EventArgs e)
        {
            if (newCaresName.Text.Trim().Length != 0)
            {
                if(CareAndDiseaseController.UpdateCare(care, NormalizeName(newCaresName.Text))){
                    MessageBox.Show("Le soin a bien été modifié.", "Validation de modification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } else
                {
                    MessageBox.Show("Le soin existe déjà.", "Annulation de modification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// Permet de générer les éléments du formulaire de modification de maladie.
        /// </summary>
        private void GenerateUpdateDiseaseForm()
        {
            nameDiseaseForm = new Label()
            {
                Text = "Formulaire modification d'une maladie",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = UIColor.DARKBLUE,
                Size = new Size(window.Width * 4 / 10, window.Height * 1 / 10),
                Location = new Point(window.Width * 5 / 15, window.Height * 3 / 20)
            };
            window.Controls.Add(nameDiseaseForm);

            newDiseaseName = new TextBox()
            {
                Text = disease.NOMMALADIE,
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = Color.Black,
                Size = new Size(window.Width * 3 / 10, window.Height * 1 / 10),
                Location = new Point(window.Width * 27 / 50, window.Height * 5 / 20)
            };
            window.Controls.Add(newDiseaseName);


            researchCare = new TextBox()
            {
                Text = "Recherche...",
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = Color.Gray,
                Size = new Size(window.Width * 3 / 10, window.Height * 1 / 10),
                Location = new Point(window.Width * 27 / 50, window.Height * 6 / 20)
            };
            researchCare.TextChanged += new EventHandler(SearchCare);
            researchCare.GotFocus += new EventHandler(GotFocus);
            window.Controls.Add(researchCare);


            allPossibleCares = new ListBox()
            {
                Text = "Soins",
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = Color.Black,
                Size = new Size(window.Width * 3 / 10, window.Height * 2 / 10),
                Location = new Point(window.Width * 27 / 50, window.Height * 7 / 20)
            };
            allPossibleCares.SelectedValueChanged += new EventHandler(SelectCare);
            window.Controls.Add(allPossibleCares);

            newCares = new ListBox()
            {
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = Color.Gray,
                Size = new Size(window.Width * 3 / 10, window.Height * 2 / 10),
                Location = new Point(window.Width * 27 / 50, window.Height * 11 / 20)
            };
            newCares.SelectedValueChanged += new EventHandler(SelectCare);
            window.Controls.Add(newCares);

            currentDiseaseName = new TextBox()
            {
                Text = disease.NOMMALADIE,
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = Color.Black,
                Size = new Size(window.Width * 3 / 10, window.Height * 1 / 10),
                Location = new Point(window.Width * 8 / 50, window.Height * 5 / 20),
                Enabled = false
            };
            window.Controls.Add(currentDiseaseName);


            currentCares = new ListBox()
            {
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = Color.Gray,
                Size = new Size(window.Width * 3 / 10, window.Height * 2 / 10),
                Location = new Point(window.Width * 8 / 50, window.Height * 11 / 20),
                Enabled = false
            };
            window.Controls.Add(currentCares);


            validateDiseaseForm = new UIButton(UIColor.ORANGE, "Modifier", window.Width * 15 / 100)
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Height = window.Height / 25,
                Location = new Point(window.Width * 18 / 50, window.Height * 15 / 20),
                Size = new Size(window.Width * 3 / 10, window.Height * 1 / 15),
            };
            validateDiseaseForm.Click += new EventHandler(SubmitDiseaseForm);
            window.Controls.Add(validateDiseaseForm);
        }

        /// <summary>
        /// Méthode gérant le focus sur la barre de recherche.
        /// </summary>
        /// <param name="sender">Barre de recherche de soin</param>
        /// <param name="e">Changement de texte</param>
        private void GotFocus(object sender, EventArgs e)
        {
            if (sender.Equals(researchCare) && researchCare.Text == "Recherche...")
            {
                researchCare.Text = "";
                researchCare.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Permet de modifier une maladie si les informations sont correctement remplies.
        /// </summary>
        /// <param name="sender">Bouton de validation</param>
        /// <param name="e">clic</param>
        private void SubmitDiseaseForm(object sender, EventArgs e)
        {
            if(newDiseaseName.Text.Trim().Length != 0)
            {
                List<SOIN> cares = new List<SOIN>();
                foreach (SOIN care in newCares.Items)
                {
                    cares.Add(care);
                }
                if (CareAndDiseaseController.UpdateDisease(disease, NormalizeName(newDiseaseName.Text), cares))
                {
                    MessageBox.Show("La maladie à bien été modifiée.", "Confirmation de modification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } else
                {
                    MessageBox.Show("La maladie existe déjà dans la base.", "Anuulation de modification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// Permet de rechercher un soin dans la ComboBox.
        /// </summary>
        /// <param name="sender">ComboBox</param>
        /// <param name="e">Changement de text</param>
        private void SearchCare(object sender, EventArgs e)
        {
            if (researchCare.Text != "")
            {
                allPossibleCares.Items.Clear();
                allPossibleCares.Items.Add(" ");
                foreach (SOIN care in CareAndDiseaseController.SearchCaresByNames(researchCare.Text))
                {
                    allPossibleCares.Items.Add(care);
                }
            }
            else
            {
                GenerateData();
            }
        }

        /// <summary>
        /// Permet d'ajouter un soin à liste des soins choisis ou de le retirer de la liste.
        /// </summary>
        /// <param name="sender">Soit la Combox qui contient les soins possibles; soit la ListBox qui contient les soins choisis</param>
        /// <param name="e">clic sur un item</param>
        private void SelectCare(object sender, EventArgs e)
        {
            if (sender.Equals(allPossibleCares))
            {
                if (allPossibleCares.SelectedItem != " ")
                {
                    if (!newCares.Items.Contains(allPossibleCares.SelectedItem))
                    {
                        newCares.Items.Add((SOIN)allPossibleCares.SelectedItem);
                    }
                    else
                    {
                        MessageBox.Show("Ce soins est déjà lié à cette maladie.", "Soin déjà lié", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else if (sender.Equals(newCares))
            {
                newCares.Items.Remove(newCares.SelectedItem);
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
            window.switchInterface(new InterfaceDiseaseAndCares(window, user));
        }
        #endregion

        /// <summary>
        /// Méthode permettant de normaliser le prénom du nouveau client.
        /// C'est à dire première lettre en majuscule et le reste en minuscule.
        /// </summary>
        /// <returns>Le prénom normalisé</returns>
        private string NormalizeName(string name)
        {
            char[] nameLetter = name.ToCharArray();
            string nameWithCapital = "";
            string letter;
            bool firstLetter = true;
            foreach (char c in nameLetter)
            {
                if (firstLetter)
                {
                    letter = c.ToString().ToUpper();
                    firstLetter = false;
                }
                else
                {
                    letter = c.ToString().ToLower();
                }
                nameWithCapital += letter;
            }
            return nameWithCapital;
        }
    }
}
