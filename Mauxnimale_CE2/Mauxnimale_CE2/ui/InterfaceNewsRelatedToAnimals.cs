using System;
using System.Drawing;
using System.Windows.Forms;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.api.controllers;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceNewsRelatedToAnimals : AInterface
    {
        MainWindow window;
        AInterface interfaceWhoLauchThisWindow;

        Header header;
        Footer footer;
        Label animalLabel, breedLabel, speciesLabel;

        #region Animal form elements and attribute
        TextBox animalNameBox, birthYearBox, sizeBox, weightBox;
        ComboBox genderCB, speciesAnimalFormCB, possibleBreedCB, possibleOwnerCB;
        UIButton validateAnimalForm;
        ESPECE selectedSpeciesAnimalForm;
        #endregion

        #region Species form elements
        TextBox specieNameBox;
        UIButton validateSpeciesForm, updateSpecie;
        ListBox allSpecies;
        TextBox researchSpecies;
        #endregion

        #region Breed form elements
        TextBox breedNameBox;
        ComboBox speciesBreedFormCB;
        UIButton validateBreedForm;
        ESPECE selectedSpeciesBreedForm;
        #endregion

        UIRoundButton backButton;

        public InterfaceNewsRelatedToAnimals(MainWindow window, SALARIE s, AInterface interfaceWhoLauchThisWindow)
        {
            this.window = window;
            user = s;
            this.interfaceWhoLauchThisWindow = interfaceWhoLauchThisWindow;
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
            GenerateLabel();
            CreateBackButton();
            FormAnimalGenerateButton();
            FormAnimalGenerateTextBox();
            FormAnimalGenerateComboBox();
            //génération des éléments du formulaire liés à l'ajout d'une espèce
            FormSpecies();
            //Génération des éléments du formulaire liés à l'ajout d'une espèce
            //FormBreed();
            // Ajout des données dans les éléments
            AddToIsMaleCB();
            AddToSpeciesCB();
            AddToClientCB();
        }

        #region Back button
        private void BackPage(object sender, EventArgs e)
        {
            // Permet de retourne sur l'interface qui avait lancée cette interface
            if (interfaceWhoLauchThisWindow.GetType().ToString().Split('.')[2] == "InterfaceClient")
            {
                window.Controls.Clear();
                window.switchInterface(new InterfaceClient(window, user));
            }
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

        #region Label
        private void GenerateLabel()
        {
            animalLabel = new Label
            {
                Text = "Formulaire ajout d'un animal",
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = UIColor.DARKBLUE,
                Size = new Size(window.Width * 3 / 10, window.Height * 1 / 10),
                Location = new Point(window.Width * 5 / 50, window.Height / 10)
            };
            window.Controls.Add(animalLabel);

            speciesLabel = new Label
            {
                Text = "Informations liées aux espèces",
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = UIColor.DARKBLUE,
                Size = new Size(window.Width * 3 / 10, window.Height / 10),
                Location = new Point(window.Width * 20 / 50, window.Height * 2 / 12)
            };
            window.Controls.Add(speciesLabel);

            breedLabel = new Label
            {
                Text = "Informations liées aux races",
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = UIColor.DARKBLUE,
                Size = new Size(window.Width * 3 / 10, window.Height * 1 / 10),
                Location = new Point(window.Width * 22 / 50, window.Height * 11 / 20)
            };
            window.Controls.Add(breedLabel);
        }
        #endregion

        #region ANIMAL FORM
        #region Generation of ComboBoxes of form animal
        /// <summary>
        /// Méthode permettant de générer les ComboBox liés au formulaire lié à l'ajout d'un animal
        /// </summary>
        private void FormAnimalGenerateComboBox()
        {
            genderCB = new ComboBox
            {
                Location = new Point(window.Width * 7 / 50, window.Height * 8 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                TabIndex = 4,
                ForeColor = Color.Black,
                Text = "Sexe"
            };
            window.Controls.Add(genderCB);

            speciesAnimalFormCB = new ComboBox
            {
                Location = new Point(window.Width * 7 / 50, window.Height * 9 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                TabIndex = 5,
                ForeColor = Color.Black,
                Text = "Espèce"
            };
            window.Controls.Add(speciesAnimalFormCB);
            speciesAnimalFormCB.SelectedValueChanged += new EventHandler(SelectedSpecies);

            possibleBreedCB = new ComboBox
            {
                Location = new Point(window.Width * 7 / 50, window.Height * 10 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                TabIndex = 6
            };
            genderCB.ForeColor = Color.Black;
            possibleBreedCB.Text = "Race";
            window.Controls.Add(possibleBreedCB);

            possibleOwnerCB = new ComboBox
            {
                Location = new Point(window.Width * 7 / 50, window.Height * 11 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                TabIndex = 7
            };
            genderCB.ForeColor = Color.Black;
            possibleOwnerCB.Text = "Propriétaire";
            window.Controls.Add(possibleOwnerCB);
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
                Location = new Point(window.Width * 7 / 50, window.Height * 12 / 20),
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
                Location = new Point(window.Width * 7 / 50, window.Height * 4 / 20),
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
                Location = new Point(window.Width * 7 / 50, window.Height * 5 / 20),
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
                Location = new Point(window.Width * 7 / 50, window.Height * 6 / 20),
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
                Location = new Point(window.Width * 7 / 50, window.Height * 7 / 20),
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
            if (genderCB.Text != "Genre" &&
                possibleBreedCB.Text != "Race" &&
                possibleOwnerCB.Text != "Propriétaire" &&
                animalNameBox.Text.Trim().Length != 0 &&
                birthYearBox.Text.Trim().Length != 0 && birthYearBox.Text.Length == 4 &&
                sizeBox.Text.Trim().Length != 0 &&
                weightBox.Text.Trim().Length != 0)
            {
                RACE breed = (RACE)possibleBreedCB.SelectedItem;
                CLIENT owner = (CLIENT)possibleOwnerCB.SelectedItem;
                bool isMale = genderCB.Text == "Male";
                if (AnimalController.registerAnimal(breed, owner, NormalizeName(animalNameBox.Text), birthYearBox.Text, Int32.Parse(sizeBox.Text), Int32.Parse(weightBox.Text), isMale))
                {
                    // On vide tous les champs du formulaire
                    genderCB.Text = "";
                    possibleBreedCB.Text = "Race";
                    possibleOwnerCB.Text = "Propriétaire";
                    animalNameBox.Text = "Nom";
                    birthYearBox.Text = "Année de naissance";
                    sizeBox.Text = "Taille";
                    weightBox.Text = "Poids";
                    MessageBox.Show("L'animal à bien été ajouté à la base.", "Comfirmation d'ajout à la base", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cet animal existe déjà", "Problème d'insertion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            } else
            {
                MessageBox.Show("Des champs ne sont pas remplis ou mal remplis", "Problème de formulaire", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                //speciesBreedFormCB.Items.Add(species);
                allSpecies.Items.Add(species);
            }
        }

        /// <summary>
        /// Méthode permettant d'ajouter les deux choix possible pour le sexe de l'animal dans la ComboBox associée
        /// </summary>
        private void AddToIsMaleCB()
        {
            genderCB.Items.Add("Male");
            genderCB.Items.Add("Femelle");
        }

        /// <summary>
        /// Méthode permettant d'ajouter les clients possible pour les lié à l'animal dans la ComBox associée
        /// </summary>
        public void AddToClientCB()
        {
            foreach (CLIENT c in ClientController.AllClient())
            {
                possibleOwnerCB.Items.Add(c);
            }
        }

        /// <summary>
        /// Méthode permettant d'ajouter les races liées à une espèce
        /// </summary>
        public void AddToBreedCB()
        {
            possibleBreedCB.Items.Clear();
            possibleBreedCB.Text = "Race";
            possibleBreedCB.SelectedItem = null;
            if (selectedSpeciesAnimalForm != null)
            {
                foreach (RACE r in AnimalController.BreedsWithSpecie((ESPECE)speciesAnimalFormCB.SelectedItem))
                {
                    possibleBreedCB.Items.Add(r);
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
                Location = new Point(window.Width * 24 / 50, window.Height * 6 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                MaxLength = 128
            };
            window.Controls.Add(specieNameBox);
            specieNameBox.BringToFront();   
            specieNameBox.GotFocus += new EventHandler(GetFocus);
            specieNameBox.LostFocus += new EventHandler(LostFocus);

            validateSpeciesForm = new UIButton(UIColor.ORANGE, "Ajouter", window.Width * 8 / 100)
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Height = window.Height / 25,
                Location = new Point(window.Width * 24 / 50, window.Height * 7 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
            };
            window.Controls.Add(validateSpeciesForm);
            validateSpeciesForm.BringToFront();
            validateSpeciesForm.Click += new EventHandler(SubmitSpecieForm);

            allSpecies = new ListBox()
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Location = new Point(window.Width * 35 / 50, window.Height * 6 / 20),
                Size = new Size(window.Width * 10 / 50, window.Height * 3 / 20),
            };
            window.Controls.Add(allSpecies);
            allSpecies.BringToFront();
            allSpecies.SelectedIndexChanged += new EventHandler(SelectedSpeciesForUpdate);

            researchSpecies = new TextBox()
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Text = "Recherche",
                ForeColor = Color.Gray,
                Location = new Point(window.Width * 35 / 50, window.Height * 5 / 20),
                Size = new Size(window.Width * 10 / 50, window.Height * 3 / 20),
            };
            window.Controls.Add(researchSpecies);
            researchSpecies.GotFocus += new EventHandler(ResearchGotFocus);
            researchSpecies.TextChanged += new EventHandler(ResearchByName);

            updateSpecie = new UIButton(UIColor.ORANGE, "Modifier", window.Width * 8 / 100)
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Height = window.Height / 25,
                Location = new Point(window.Width * 35 / 50, window.Height * 9 / 20),
                Size = new Size(window.Width * 10 / 50, window.Height * 5 / 100),
            };
            updateSpecie.BringToFront();
            updateSpecie.Click += new EventHandler(SubmitUpdateSpecie);
        }

        private void SubmitUpdateSpecie(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceUpdateRelatedToAnimals(window, user, allSpecies.SelectedItem, this));
        }

        private void SelectedSpeciesForUpdate(object sender, EventArgs e)
        {
            if(allSpecies.SelectedItem != null && allSpecies.SelectedItem != " ")
            {
                window.Controls.Add(updateSpecie);
            } else
            {
                window.Controls.Remove(updateSpecie);
            }
            window.Refresh();
        }

        private void ResearchByName(object sender, EventArgs e)
        {
            if (sender.Equals(researchSpecies))
            {
                allSpecies.Items.Clear();
                allSpecies.SelectedItems.Clear();
                allSpecies.Items.Add(" ");
                foreach (ESPECE s in AnimalController.ResearchSpeciesByName(researchSpecies.Text))
                {
                    allSpecies.Items.Add(s);
                }
            }
        }

        private void ResearchGotFocus(object sender, EventArgs e)
        {
            if (sender.Equals(researchSpecies) && researchSpecies.Text == "Recherche")
            {
                researchSpecies.Text = "";
                researchSpecies.ForeColor = Color.Black;
            }
        }

        private void SubmitSpecieForm(object sender, EventArgs e)
        {
            if(specieNameBox.Text.Trim() != null && specieNameBox.Text != "Nom de l'espèce")
            {
                if (AnimalController.AddSpecie(NormalizeName(specieNameBox.Text)))
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
                Location = new Point(window.Width * 24 / 50, window.Height * 8 / 20),
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
                Location = new Point(window.Width * 24 / 50, window.Height * 9 / 20),
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
                Location = new Point(window.Width * 24 / 50, window.Height * 10 / 20)
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
