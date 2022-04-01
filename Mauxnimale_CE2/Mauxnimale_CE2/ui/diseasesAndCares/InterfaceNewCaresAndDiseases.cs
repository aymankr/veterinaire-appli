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
    internal class InterfaceNewCaresAndDiseases : AInterface
    {
        readonly Header header;
        readonly Footer footer;

        #region DiseaseForm
        Label nameDiseaseForm;
        TextBox diseaseName, researchCare;
        ListBox allPossibleCares, allSelectedCares;
        UIButton validateDiseaseForm;
        #endregion

        #region CareForm
        Label nameCareForm;
        TextBox careDescription;
        UIButton validateCareForm;
        #endregion

        UIRoundButton backButton;

        /// <summary>
        /// Constructeur de l'interface
        /// </summary>
        /// <param name="window"></param>
        /// <param name="user"></param>
        public InterfaceNewCaresAndDiseases(MainWindow window, SALARIE user) : base(window, user)
        {
            header = new Header(window);
            footer = new Footer(window, base.user);
        }

        /// <summary>
        /// Permet de générer les éléments de la fenêtre.
        /// </summary>
        public override void load()
        {
            header.load("Ajout d'une maladie ou d'un soin");
            footer.load();
            GenerateBackButton();
            GenerateDiseaseForm();
            AddDataInCaresList();

            GenerateCaresForm();
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
        /// Permet de générer les élémetns du formulaire d'ajout d'une maladie.
        /// </summary>
        private void GenerateDiseaseForm()
        {
            nameDiseaseForm = new Label()
            {
                Text = "Formulaire ajout d'une maladie",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = UIColor.DARKBLUE,
                Size = new Size(window.Width * 3 / 10, window.Height * 1 / 10),
                Location = new Point(window.Width * 2  / 15, window.Height * 3 / 20)
            };
            window.Controls.Add(nameDiseaseForm);

            diseaseName = new TextBox()
            {
                Text = "Nom",
                Font = new Font("Poppins", window.Height * 2 / 100), 
                ForeColor = Color.Gray,
                Size = new Size(window.Width * 3 / 10, window.Height * 1 / 10),
                Location = new Point(window.Width * 7 / 50, window.Height * 5 / 20)
            };
            diseaseName.GotFocus += new EventHandler(GotFocus);
            window.Controls.Add(diseaseName);

            researchCare = new TextBox()
            {
                Text = "Recherche...",
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = Color.Gray,
                Size = new Size(window.Width * 3 / 10, window.Height * 1 / 10),
                Location = new Point(window.Width * 7 / 50, window.Height * 6 / 20)
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
                Location = new Point(window.Width * 7 / 50, window.Height * 7 / 20)
            };
            allPossibleCares.SelectedValueChanged += new EventHandler(SelectCare);
            window.Controls.Add(allPossibleCares);

            allSelectedCares = new ListBox()
            {
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = Color.Gray,
                Size = new Size(window.Width * 3 / 10, window.Height * 2 / 10),
                Location = new Point(window.Width * 7 / 50, window.Height * 11 / 20)
            };
            allSelectedCares.SelectedValueChanged += new EventHandler(SelectCare);
            window.Controls.Add(allSelectedCares);

            validateDiseaseForm = new UIButton(UIColor.ORANGE, "Ajouter", window.Width * 15 / 100)
            {
                Font = new Font("Poppins", window.Height * 2 / 100),
                Height = window.Height / 25,
                Location = new Point(window.Width * 7 / 50, window.Height * 15 / 20),
                Size = new Size(window.Width * 3 / 10, window.Height * 1 / 15),
            };
            validateDiseaseForm.Click += new EventHandler(SubmitDiseaseForm);
            window.Controls.Add(validateDiseaseForm);
        }

        /// <summary>
        /// Permet de rechercher un soin dans la ComboBox
        /// </summary>
        /// <param name="sender">ComboBox</param>
        /// <param name="e">Changement de text</param>
        private void SearchCare(object sender, EventArgs e)
        {
            if(researchCare.Text != "")
            {
                allPossibleCares.Items.Clear();
                allPossibleCares.Items.Add(" ");
                foreach(SOIN care in CareAndDiseaseController.SearchCaresByNames(researchCare.Text))
                {
                    allPossibleCares.Items.Add(care);
                }
            } else
            {
                AddDataInCaresList();
            }
        }

        /// <summary>
        /// Permet de soumettre le formulaire et si les informations sont valides alors la maladie est ajoutée
        /// </summary>
        /// <param name="sender">Bouton de validation</param>
        /// <param name="e">Clic</param>
        private void SubmitDiseaseForm(object sender, EventArgs e)
        {
            if (diseaseName.Text.Trim().Length != 0)
            {
                List<SOIN> cares = new List<SOIN>();
                foreach(SOIN care in allSelectedCares.Items)
                {
                    cares.Add(care);
                }
                if(CareAndDiseaseController.AddDisease(NormalizeName(diseaseName.Text), cares))
                {
                    MessageBox.Show("La maladie a été ajoutée à la base.", "Confirmation d'ajout", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } else
                {
                    MessageBox.Show("La maladie est déjà présente dans la base.", "Annulation d'ajout", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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
                if(allPossibleCares.SelectedItem != " " && allPossibleCares.SelectedItem != null)
                {
                    if (!allSelectedCares.Items.Contains(allPossibleCares.SelectedItem))
                    {
                        allSelectedCares.Items.Add((SOIN)allPossibleCares.SelectedItem);
                    } else
                    {
                        MessageBox.Show("Ce soins est déjà lié à cette maladie.", "Soin déjà lié", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                    }
                }
            } else if (sender.Equals(allSelectedCares) && allPossibleCares.SelectedItem != null)
            {
                allSelectedCares.Items.Remove(allSelectedCares.SelectedItem);
            }
        }

        /// <summary>
        /// Permet de changer le texte de la TextBox du nom de la maladie quand l'utilisateur clic dessus pour la première fois.
        /// </summary>
        /// <param name="sender">TextBox du nom de la maladie</param>
        /// <param name="e">prise de focus</param>
        private void GotFocus(object sender, EventArgs e)
        {
            if(sender.Equals(diseaseName) && diseaseName.Text == "Nom")
            {
                diseaseName.Text = "";
                diseaseName.ForeColor = Color.Black;
            } 
            else if (sender.Equals(careDescription) &&  careDescription.Text == "Nom")
            {
                careDescription.Text = "";
                careDescription.ForeColor = Color.Black;
            }
            else if (sender.Equals(researchCare) && researchCare.Text == "Recherche...")
            {
                researchCare.Text = "";
                researchCare.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Permet d'ajouter tous les soins dans la ComboBox
        /// </summary>
        private void AddDataInCaresList()
        {
            allPossibleCares.Items.Clear();
            allPossibleCares.Items.Add(" ");
            foreach (SOIN care in CareAndDiseaseController.AllCares())
            {
                allPossibleCares.Items.Add(care);
            }
        }

        /// <summary>
        /// Permet de générer les éléments du formulaire d'ajout d'un soin
        /// </summary>
        private void GenerateCaresForm()
        {
            nameCareForm = new Label()
            {
                Text = "Formulaire ajout d'un soin",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = UIColor.DARKBLUE,
                Size = new Size(window.Width * 3 / 10, window.Height * 1 / 10),
                Location = new Point(window.Width * 9 / 15, window.Height * 3 / 20)
            };
            window.Controls.Add(nameCareForm);

            careDescription = new TextBox()
            {
                Text = "Nom",
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = Color.Gray,
                Size = new Size(window.Width * 3 / 10, window.Height * 1 / 10),
                Location = new Point(window.Width * 30 / 50, window.Height * 5 / 20)
            };
            careDescription.GotFocus += new EventHandler(GotFocus);
            window.Controls.Add(careDescription);

            validateCareForm = new UIButton(UIColor.ORANGE, "Ajouter", window.Width * 15 / 100)
            {
                Font = new Font("Poppins", window.Height * 2 / 100),
                Height = window.Height / 25,
                Location = new Point(window.Width * 30 / 50, window.Height * 6 / 20),
                Size = new Size(window.Width * 3 / 10, window.Height * 1 / 15),
            };
            validateCareForm.Click += new EventHandler(SubmitCareForm);
            window.Controls.Add(validateCareForm);
        }

        /// <summary>
        /// Permet d'ajouter un soin à la base
        /// </summary>
        /// <param name="sender">Bouton de validation</param>
        /// <param name="e">clic</param>
        private void SubmitCareForm(object sender, EventArgs e)
        {
            if(careDescription.Text != "Nom" && careDescription.Text.Trim().Length != 0)
            {
                if (CareAndDiseaseController.AddCare(NormalizeName(careDescription.Text)))
                {
                    MessageBox.Show("Le soin à été ajouté à la base.", "Comfirmation d'ajout", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AddDataInCaresList();
                } else
                {
                    MessageBox.Show("Le soin est déjà présent dans la base.", "Annulation d'ajout", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

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
