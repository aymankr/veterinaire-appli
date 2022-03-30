using System;
using System.Drawing;
using System.Windows.Forms;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.api.controllers;
namespace Mauxnimale_CE2.ui
{
    internal class InterfaceUpdateCareOrDiseases : AInterface
    {
        Header header;
        Footer footer;

        MALADIE disease;
        SOIN care;

        #region DiseaseForm elements
        Label nameDiseaseForm;
        TextBox currentDiseaseName, newDiseaseName;
        ComboBox allPossibleCares;
        ListBox currentCares, newCares;
        UIButton validateDiseaseForm;
        #endregion

        UIRoundButton backButton;

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
            }
        }

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

            allPossibleCares = new ComboBox()
            {
                Text = "Soins",
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = Color.Gray,
                Size = new Size(window.Width * 3 / 10, window.Height * 1 / 10),
                Location = new Point(window.Width * 27 / 50, window.Height * 6 / 20)
            };
            allPossibleCares.SelectedValueChanged += new EventHandler(SelectCare);
            allPossibleCares.TextChanged += new EventHandler(SearchCare);
            window.Controls.Add(allPossibleCares);

            newCares = new ListBox()
            {
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = Color.Gray,
                Size = new Size(window.Width * 3 / 10, window.Height * 2 / 10),
                Location = new Point(window.Width * 27 / 50, window.Height * 7 / 20)
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
                Location = new Point(window.Width * 8 / 50, window.Height * 7 / 20),
                Enabled = false
            };
            window.Controls.Add(currentCares);


            validateDiseaseForm = new UIButton(UIColor.ORANGE, "Modifier", window.Width * 15 / 100)
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Height = window.Height / 25,
                Location = new Point(window.Width * 18 / 50, window.Height * 11 / 20),
                Size = new Size(window.Width * 3 / 10, window.Height * 1 / 15),
            };
            validateDiseaseForm.Click += new EventHandler(SubmitDiseaseForm);
            window.Controls.Add(validateDiseaseForm);
        }

        private void SubmitDiseaseForm(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Permet de rechercher un soin dans la ComboBox
        /// </summary>
        /// <param name="sender">ComboBox</param>
        /// <param name="e">Changement de text</param>
        private void SearchCare(object sender, EventArgs e)
        {
            allPossibleCares.ForeColor = Color.Black;
            allPossibleCares.Items.Clear();
            allPossibleCares.Items.Add(" ");
            foreach (SOIN care in CareAndDiseaseController.ResearchCareByName(allPossibleCares.Text))
            {
                allPossibleCares.Items.Add(care);
            }
            allPossibleCares.Select(allPossibleCares.Text.Length, 0);
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
    }
}
