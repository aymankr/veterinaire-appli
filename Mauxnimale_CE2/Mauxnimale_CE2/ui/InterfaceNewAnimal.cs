using System;
using System.Drawing;
using System.Windows.Forms;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.api.controllers;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceNewAnimal : AInterface
    {
        MainWindow window;

        Header header;
        Footer footer;

        #region Animal form elements and attribute
        TextBox animalNameBox, birthYearBox, sizeBox, weightBox;
        ComboBox isMaleCB, speciesAnimalFormCB, breedCB, clientCB;
        UIButton validateAnimalForm;
        ESPECE selectedSpeciesAnimalForm;
        #endregion

        #region Species form elements
        TextBox specieNameBox;
        UIButton validateSpeciesForm;
        #endregion

        #region Breed form elements
        TextBox breedNameBox;
        ComboBox speciesBreedFormCB;
        UIButton validateBreedForm;
        ESPECE selectedSpeciesBreedForm;
        #endregion

        UIRoundButton backButton;

        public InterfaceNewAnimal(MainWindow window, SALARIE s)
        {
            this.window = window;
            user = s;
            header = new Header(window);
            footer = new Footer(window, user);
        }

        /// <summary>
        /// Méthode permettant de 
        /// </summary>
        public override void load()
        {
            // Génération du haut et du bas de la page
            header.load("Mauxnimale - Page de gestion des clients");
            footer.load();
            // Génération des éléments du formulaire liés à l'ajout d'un animal
            CreateBackButton();
            FormAnimalGenerateButton();
            FormAnimalGenerateTextBox();
            FormAnimalGenerateComboBox();
            //génération des éléments du formulaire liés à l'ajout d'une espèce
            FormSpecies();
            //Génération des éléments du formulaire liés à l'ajout d'une espèce
            FormBreed();
            // Ajout des données dans les éléments
            AddToIsMaleCB();
            AddToSpeciesCB();
            AddToClientCB();
        }

        #region Back button
        private void BackPage(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceClient(window, user));
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

        public override void updateSize()
        {
            if (window.WindowState != FormWindowState.Minimized)
            {
                window.Controls.Clear();
                this.load();
            }
        }

        #region ANIMAL FORM

        #region Generation of ComboBoxes of form animal
        /// <summary>
        /// Méthode permettant de générer les ComboBox liés au formulaire lié à l'ajout d'un animal
        /// </summary>
        private void FormAnimalGenerateComboBox()
        {
            isMaleCB = new ComboBox
            {
                Location = new Point(window.Width * 12 / 50, window.Height * 7 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                TabIndex = 4,
                ForeColor = Color.Black,
                Text = "Sexe"
            };
            window.Controls.Add(isMaleCB);

            speciesAnimalFormCB = new ComboBox
            {
                Location = new Point(window.Width * 12 / 50, window.Height * 8 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                TabIndex = 5,
                ForeColor = Color.Black,
                Text = "Espèce"
            };
            window.Controls.Add(speciesAnimalFormCB);
            speciesAnimalFormCB.SelectedValueChanged += new EventHandler(SelectedSpecies);

            breedCB = new ComboBox
            {
                Location = new Point(window.Width * 12 / 50, window.Height * 9 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                TabIndex = 6
            };
            isMaleCB.ForeColor = Color.Black;
            breedCB.Text = "Race";
            window.Controls.Add(breedCB);

            clientCB = new ComboBox
            {
                Location = new Point(window.Width * 12 / 50, window.Height * 10 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                TabIndex = 7
            };
            isMaleCB.ForeColor = Color.Black;
            clientCB.Text = "Propriétaire";
            window.Controls.Add(clientCB);
        }
        #endregion

        #region Generation of validate button of form animal
        /// <summary>
        /// Methodes qui génére tous les bouttons
        /// </summary>
        private void FormAnimalGenerateButton()
        {
            validateAnimalForm = new UIButton(UIColor.ORANGE, "Ajouter", window.Width * 15 / 100)
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Height = window.Height / 25,
                Location = new Point(window.Width * 12 / 50, window.Height * 11 / 20),
                TabIndex = 8
            };
            window.Controls.Add(validateAnimalForm);
            validateAnimalForm.Click += new EventHandler(SubmitAnimalForm);

        }
        #endregion

        #region Generation of TextBox of form animal
        /// <summary>
        /// Méthode permettant de générer les TextBox de l'ihm
        /// </summary>
        private void FormAnimalGenerateTextBox()
        {
            animalNameBox = new TextBox
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Text = "Nom",
                ForeColor = Color.Gray,
                BackColor = Color.White,
                Location = new Point(window.Width * 12 / 50, window.Height * 3 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                MaxLength = 128,
                TabIndex = 0
            };
            window.Controls.Add(animalNameBox);
            animalNameBox.GotFocus += new EventHandler(GetFocus);
            animalNameBox.LostFocus += new EventHandler(LostFocus);

            birthYearBox = new TextBox
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Text = "Année de naissance",
                ForeColor = Color.Gray,
                BackColor = Color.White,
                Location = new Point(window.Width * 12 / 50, window.Height * 4 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                MaxLength = 4,
                TabIndex = 1
            };
            window.Controls.Add(birthYearBox);
            birthYearBox.GotFocus += new EventHandler(GetFocus);
            birthYearBox.LostFocus += new EventHandler(LostFocus);
            birthYearBox.KeyPress += new KeyPressEventHandler(KeyPress);


            sizeBox = new TextBox
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Text = "Taille",
                ForeColor = Color.Gray,
                BackColor = Color.White,
                Location = new Point(window.Width * 12 / 50, window.Height * 5 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                MaxLength = 4,
                TabIndex = 2
            };
            window.Controls.Add(sizeBox);
            sizeBox.GotFocus += new EventHandler(GetFocus);
            sizeBox.LostFocus += new EventHandler(LostFocus);
            sizeBox.KeyPress += new KeyPressEventHandler(KeyPress);


            weightBox = new TextBox
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Text = "Poids",
                ForeColor = Color.Gray,
                BackColor = Color.White,
                Location = new Point(window.Width * 12 / 50, window.Height * 6 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                MaxLength = 4,
                TabIndex = 3
            };
            window.Controls.Add(weightBox);
            weightBox.GotFocus += new EventHandler(GetFocus);
            weightBox.LostFocus += new EventHandler(LostFocus);
            weightBox.KeyPress += new KeyPressEventHandler(KeyPress);

        }
        #endregion

        #region EventHandler animal form

        #region prevents the user from entering letters
        /// <summary>
        /// Méthode qui permet de n'entrer que des chiffres dans le champ du numéro de téléphone.
        /// </summary>
        /// <param name="sender">Champ du numéro de téléphone</param>
        /// <param name="e">Touche préssée</param>
        private void KeyPress(object sender, KeyPressEventArgs e)
        {
            // On ne fait rien si la touche Delete du clavier est préssé afin de pouvoir supprimer le contenu de la TextBox
            if (e.KeyChar != (char)Keys.Back)
            {
                e.Handled = !char.IsDigit(e.KeyChar);
            }
        }
        #endregion

        #region Select item in species list
        /// <summary>
        /// Méthode répondant à l'évenement de sélection d'un objet dans la Combobox
        /// Permet de récupérer et de garder l'objet sélectionné
        /// </summary>
        /// <param name="sender">Liste qui reçoit le clic</param>
        /// <param name="e">le click</param>
        private void SelectedSpecies(object sender, EventArgs e)
        {
            selectedSpeciesAnimalForm = speciesAnimalFormCB.SelectedItem == " " ? null : (ESPECE)speciesAnimalFormCB.SelectedItem;
            AddToBreedCB();
        }
        #endregion

        #region Register a new animal (click on validate button)
        private void SubmitAnimalForm(object sender, EventArgs e)
        {
            if (isMaleCB.Text != null && isMaleCB.Text != "Sexe" &&
                breedCB.SelectedItem != null && breedCB.Text != "Race" &&
                clientCB.SelectedItem != null && clientCB.Text != "Propriétaire" &&
                animalNameBox.Text != null && animalNameBox.Text != "Nom" &&
                birthYearBox.Text != null && birthYearBox.Text != "Année de naissance" &&
                sizeBox.Text != null && animalNameBox.Text != "Taille" &&
                weightBox.Text != null && weightBox.Text != "Poids")
            {
                RACE breed = (RACE)breedCB.SelectedItem;
                CLIENT owner = (CLIENT)clientCB.SelectedItem;
                bool isMale = isMaleCB.Text == "M";
                if (AnimalController.registerAnimal(breed, owner, NormalizeName(animalNameBox.Text), birthYearBox.Text, Int32.Parse(sizeBox.Text), Int32.Parse(weightBox.Text), isMale))
                {
                    MessageBox.Show("L'animal à bien été ajouté à la base.", "Comfirmation d'ajout à la base", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cet animal existe déjà", "Problème d'insertion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        #endregion

        #endregion

        #region Add data in comboBox
        /// <summary>
        /// Méthode permettant d'ajouter les espèce dans les ComboBox associées
        /// </summary>
        private void AddToSpeciesCB()
        {
            foreach (ESPECE species in AnimalController.AllSpecies())
            {
                speciesAnimalFormCB.Items.Add(species);
                speciesBreedFormCB.Items.Add(species);
            }
        }

        /// <summary>
        /// Méthode permettant d'ajouter les deux choix possible pour le sexe de l'animal dans la ComboBox associée
        /// </summary>
        private void AddToIsMaleCB()
        {
            isMaleCB.Items.Add("M");
            isMaleCB.Items.Add("F");
        }

        /// <summary>
        /// Méthode permettant d'ajouter les clients possible pour les lié à l'animal dans la ComBox associée
        /// </summary>
        public void AddToClientCB()
        {
            foreach (CLIENT c in ClientController.AllClient())
            {
                clientCB.Items.Add(c);
            }
        }

        /// <summary>
        /// Méthode permettant d'ajouter les races liées à une espèce
        /// </summary>
        public void AddToBreedCB()
        {
            breedCB.Items.Clear();
            breedCB.Text = "Race";
            breedCB.SelectedItem = null;
            if (selectedSpeciesAnimalForm != null)
            {
                foreach (RACE r in AnimalController.BreedsWithSpecie((ESPECE)speciesAnimalFormCB.SelectedItem))
                {
                    breedCB.Items.Add(r);
                }
            }
        }
        #endregion
        #endregion

        #region SPECIES FORM
        /// <summary>
        /// Méthode permettant de générer les éléments du formulaire lié à l'ajout d'une espèce
        /// </summary>
        private void FormSpecies()
        {
            specieNameBox = new TextBox
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Text = "Nom de l'espèce",
                ForeColor = Color.Gray,
                Location = new Point(window.Width * 24 / 50, window.Height * 3 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                MaxLength = 128
            };
            window.Controls.Add(specieNameBox);
            specieNameBox.GotFocus += new EventHandler(GetFocus);
            specieNameBox.LostFocus += new EventHandler(LostFocus);

            validateSpeciesForm = new UIButton(UIColor.ORANGE, "Ajouter", window.Width * 15 / 100)
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Height = window.Height / 25,
                Location = new Point(window.Width * 24 / 50, window.Height * 4 / 20),
                TabIndex = 8
            };
            window.Controls.Add(validateSpeciesForm);
            validateSpeciesForm.Click += new EventHandler(SubmitSpecieForm);
        }

        private void SubmitSpecieForm(object sender, EventArgs e)
        {
            if(specieNameBox.Text.Trim() != null && specieNameBox.Text != "Nom de l'espèce")
            {
                if (AnimalController.AddSpecie(NormalizeName(specieNameBox.Text.Trim())))
                {
                    MessageBox.Show("L'espèce a été ajoutée à la base de données avec succès", "Comfirmation d'ajout à la base de donnée", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } else
                {
                    MessageBox.Show("Ce nom d'espèce existe déjà", "Problème d'insertion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region BREED FORM
        /// <summary>
        /// Méthode permettant de générer les éléments du formulaire lié à l'ajout d'une race
        /// </summary>
        private void FormBreed()
        {
            speciesBreedFormCB = new ComboBox
            {
                Location = new Point(window.Width * 24 / 50, window.Height * 6 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                ForeColor = Color.Black,
                Text = "Espèce"
            };
            window.Controls.Add(speciesBreedFormCB);
            speciesBreedFormCB.SelectedValueChanged += new EventHandler(BreedFormSelectedSpecies);

            breedNameBox = new TextBox
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Text = "Nom de la race",
                ForeColor = Color.Gray,
                Location = new Point(window.Width * 24 / 50, window.Height * 7 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                MaxLength = 128
            };
            window.Controls.Add(breedNameBox);
            breedNameBox.GotFocus += new EventHandler(GetFocus);
            breedNameBox.LostFocus += new EventHandler(LostFocus);

            validateBreedForm = new UIButton(UIColor.ORANGE, "Ajouter", window.Width * 15 / 100)
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Height = window.Height / 25,
                Location = new Point(window.Width * 24 / 50, window.Height * 8 / 20)
            };
            window.Controls.Add(validateBreedForm);
            validateBreedForm.Click += new EventHandler(SubmitBreedForm);
        }

        /// <summary>
        /// Méthode répondant à l'évènement de click sur un objet de la liste des espèces du formulaire de race
        /// </summary>
        /// <param name="sender">Liste qui reçoit le clic</param>
        /// <param name="e">Le clic</param>
        private void BreedFormSelectedSpecies(object sender, EventArgs e)
        {
            selectedSpeciesBreedForm = speciesBreedFormCB.SelectedItem == " " ? null : (ESPECE)speciesBreedFormCB.SelectedItem;
            AddToBreedCB();
        }

        private void SubmitBreedForm(object sender, EventArgs e)
        {
            if (selectedSpeciesBreedForm != null && breedNameBox.Text.Length != 0)
            {
                if(AnimalController.AddBreed(selectedSpeciesBreedForm, NormalizeName(breedNameBox.Text)))
                {
                    MessageBox.Show("La race à bien été ajoutée à la base avec succés", "Comfirmation d'ajout à la base de donnée", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }else
                {
                    MessageBox.Show("La race existe déjà", "Problème d'insertion à la base de donnée", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
        #endregion

        #region focus event
        private void LostFocus(object sender, EventArgs e)
        {
            if (sender.Equals(weightBox) && weightBox.Text.Length == 0)
            {
                weightBox.Text = "Poids";
                weightBox.ForeColor = Color.Gray;
            }
            if (sender.Equals(animalNameBox) && animalNameBox.Text.Length == 0)
            {
                animalNameBox.Text = "Nom";
                animalNameBox.ForeColor = Color.Gray;
            }
            if (sender.Equals(sizeBox) && sizeBox.Text.Length == 0)
            {
                sizeBox.Text = "Taille";
                sizeBox.ForeColor = Color.Gray;
            }
            if (sender.Equals(birthYearBox) && birthYearBox.Text.Length == 0)
            {
                birthYearBox.Text = "Année de naissance";
                birthYearBox.ForeColor = Color.Gray;
            }
            if (sender.Equals(specieNameBox) && specieNameBox.Text.Length == 0)
            {
                specieNameBox.Text = "Nom de l'espèce";
                specieNameBox.ForeColor = Color.Gray;
            }
            if (sender.Equals(breedNameBox) && breedNameBox.Text.Length == 0)
            {
                breedNameBox.Text = "Nom de la race";
                breedNameBox.ForeColor = Color.Gray;
            }
        }

        private void GetFocus(object sender, EventArgs e)
        {
            if (sender.Equals(weightBox) && weightBox.Text == "Poids")
            {
                weightBox.Text = "";
                weightBox.ForeColor = Color.Black;
            }
            if (sender.Equals(animalNameBox) && animalNameBox.Text == "Nom")
            {
                animalNameBox.Text = "";
                animalNameBox.ForeColor = Color.Black;
            }
            if (sender.Equals(sizeBox) && sizeBox.Text == "Taille")
            {
                sizeBox.Text = "";
                sizeBox.ForeColor = Color.Black;
            }
            if (sender.Equals(birthYearBox) && birthYearBox.Text == "Année de naissance")
            {
                birthYearBox.Text = "";
                birthYearBox.ForeColor = Color.Black;
            }
            if (sender.Equals(specieNameBox) && specieNameBox.Text == "Nom de l'espèce")
            {
                specieNameBox.Text = "";
                specieNameBox.ForeColor = Color.Black;
            }
            if (sender.Equals(breedNameBox) && breedNameBox.Text == "Nom de la race")
            {
                breedNameBox.Text = "";
                breedNameBox.ForeColor = Color.Black;
            }
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
