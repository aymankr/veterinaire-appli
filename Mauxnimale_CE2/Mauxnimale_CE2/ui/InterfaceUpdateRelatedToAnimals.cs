using System;
using System.Drawing;
using System.Windows.Forms;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.api.controllers;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceUpdateRelatedToAnimals : AInterface
    {
        MainWindow window;
        AInterface interfaceWhoLaunchThisWindow;

        readonly RACE breed;
        readonly ANIMAL animal;
        readonly ESPECE specie;

        Header header;
        Footer footer;
        Label animalLabel, breedLabel, speciesLabel;

        #region Animal form elements and attribute
        TextBox animalNameBox, birthYearBox, sizeBox, weightBox, currentGender,currentBreed, CurrentOwner, currentSpecie;
        ComboBox genderCB, speciesAnimalFormCB, breedCB, possibleOwnerCB;
        UIButton validateAnimalForm;
        ESPECE selectedSpeciesAnimalForm;
        #endregion

        #region Species form elements
        TextBox specieNameBox;
        UIButton validateSpeciesForm;
        #endregion

        #region Breed form elements
        TextBox breedNameBox, currentSpecieBreedForm;
        ComboBox speciesBreedFormCB;
        UIButton validateBreedForm;
        ESPECE selectedSpeciesBreedForm;
        #endregion

        UIRoundButton backButton;

        public InterfaceUpdateRelatedToAnimals(MainWindow window, SALARIE s, object o, AInterface interfaceWhoLaunchThisWindow)
        {
            this.window = window;
            user = s;
            this.interfaceWhoLaunchThisWindow = interfaceWhoLaunchThisWindow;
            header = new Header(window);
            footer = new Footer(window, user);
            if (o.GetType().ToString().Contains("ANIMAL"))
            {
                animal = (ANIMAL)o;
            }
            else if (o.GetType().ToString().Contains("RACE"))
            {
                breed = (RACE)o;
            }
            else if (o.GetType().ToString().Contains("ESPECE"))
            {
                specie = (ESPECE)o;
            }
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
            //GenerateLabel();
            CreateBackButton();
            if (animal != null)
            {
                FormAnimalGenerateButton();
                FormAnimalGenerateTextBox();
                FormAnimalGenerateComboBox();
                AddIntoGenderCB();
                AddIntoPossibleOwnerCB();
            }
            else if (breed != null)
            {            
                //Génération des éléments du formulaire liés à l'ajout d'une espèce
                FormBreed();
            } else if (specie != null)
            {
                //génération des éléments du formulaire liés à l'ajout d'une espèce
                FormSpecies();
            }
            // Ajout des données dans les éléments
            AddIntoSpeciesCB();
        }

        #region Back button
        private void BackPage(object sender, EventArgs e)
        {   
            // TODO
            /**
            if(interfaceWhoLaunchThisWindow.GetType().ToString().Contains("InterfaceClient"))
            {
                window.Controls.Clear();
                window.switchInterface(new InterfaceClient(window, user));
            } else if (interfaceWhoLaunchThisWindow.GetType().ToString().Contains("InterfaceNewsRelatedToAnimals"))
            {
                window.Controls.Clear();
                window.switchInterface(new InterfaceNewsRelatedToAnimals(window, user, new InterfaceClient(window, user)));
            }*/
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
            animalLabel = new Label();
            animalLabel.Text = "Formulaire modification d'un animal";
            animalLabel.TextAlign = ContentAlignment.MiddleLeft;
            animalLabel.Font = new Font("Poppins", window.Height * 2 / 100);
            animalLabel.ForeColor = UIColor.DARKBLUE;
            animalLabel.Size = new Size(window.Width * 3 / 10, window.Height * 1 / 10);
            animalLabel.Location = new Point(window.Width * 5 / 50, window.Height / 10);
            window.Controls.Add(animalLabel);

            speciesLabel = new Label();
            speciesLabel.Text = "Formulaire modification d'une espèce";
            speciesLabel.TextAlign = ContentAlignment.MiddleLeft;
            speciesLabel.Font = new Font("Poppins", window.Height * 2 / 100);
            speciesLabel.ForeColor = UIColor.DARKBLUE;
            speciesLabel.Size = new Size(window.Width * 3 / 10, window.Height * 1 / 10);
            speciesLabel.Location = new Point(window.Width * 22 / 50, window.Height / 10);
            window.Controls.Add(speciesLabel);

            breedLabel = new Label();
            breedLabel.Text = "Formulaire modification d'une race";
            breedLabel.TextAlign = ContentAlignment.MiddleLeft;
            breedLabel.Font = new Font("Poppins", window.Height * 2 / 100);
            breedLabel.ForeColor = UIColor.DARKBLUE;
            breedLabel.Size = new Size(window.Width * 3 / 10, window.Height * 1 / 10);
            breedLabel.Location = new Point(window.Width * 22 / 50, window.Height * 6 / 20);
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
                Location = new Point(window.Width * 20 / 50, window.Height * 8 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                TabIndex = 4,
                ForeColor = Color.Black,
                Text = "Genre",
            };
            window.Controls.Add(genderCB);

            speciesAnimalFormCB = new ComboBox
            {
                Location = new Point(window.Width * 20 / 50, window.Height * 9 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                TabIndex = 5,
                ForeColor = Color.Black,
                Text = "Espèce"
            };
            window.Controls.Add(speciesAnimalFormCB);
            speciesAnimalFormCB.SelectedValueChanged += new EventHandler(SelectedSpecies);

            breedCB = new ComboBox
            {
                Location = new Point(window.Width * 20 / 50, window.Height * 10 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                ForeColor = Color.Black,
                TabIndex = 6,
                Text = "Race"
            };
            window.Controls.Add(breedCB);

            possibleOwnerCB = new ComboBox
            {
                Location = new Point(window.Width * 20 / 50, window.Height * 11 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                TabIndex = 7,
                ForeColor = Color.Black,
                Text = "Propriétaire"
            };
            window.Controls.Add(possibleOwnerCB);
        }
        #endregion

        #region Generation of validate button of form animal
        /// <summary>
        /// Methodes qui génére tous les bouttons
        /// </summary>
        private void FormAnimalGenerateButton()
        {
            validateAnimalForm = new UIButton(UIColor.ORANGE, "Modifier", window.Width * 15 / 100)
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Height = window.Height / 25,
                Location = new Point(window.Width * 20 / 50, window.Height * 12 / 20),
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
                Text = animal.NOM,
                ForeColor = Color.Black,
                Location = new Point(window.Width * 20 / 50, window.Height * 4 / 20),
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
                Text = animal.ANNEENAISSANCE,
                ForeColor = Color.Black,
                Location = new Point(window.Width * 20 / 50, window.Height * 5 / 20),
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
                Text = animal.TAILLE.ToString(),
                ForeColor = Color.Black,
                Location = new Point(window.Width * 20 / 50, window.Height * 6 / 20),
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
                Text = animal.POIDS.ToString(),
                ForeColor = Color.Black,
                Location = new Point(window.Width * 20 / 50, window.Height * 7 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                MaxLength = 4,
                TabIndex = 3
            };
            window.Controls.Add(weightBox);
            weightBox.GotFocus += new EventHandler(GetFocus);
            weightBox.LostFocus += new EventHandler(LostFocus);
            weightBox.KeyPress += new KeyPressEventHandler(KeyPress);

            currentGender = new TextBox
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                ForeColor = Color.Black,
                Location = new Point(window.Width * 12 / 50, window.Height * 9 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                Enabled = false
            };
            currentGender.Text = (animal.ESTMALE) ? "Male" : "Femelle";
            window.Controls.Add(currentGender);

            currentSpecie = new TextBox
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Text = animal.RACE.ESPECE.NOMESPECE,
                ForeColor = Color.Black,
                Location = new Point(window.Width * 12 / 50, window.Height * 9 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                Enabled = false
            };
            window.Controls.Add(currentSpecie);

            currentBreed = new TextBox
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Text = animal.RACE.NOMRACE,
                ForeColor = Color.Black,
                Location = new Point(window.Width * 12 / 50, window.Height * 10 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                Enabled = false
            };
            window.Controls.Add(currentBreed);

            CurrentOwner = new TextBox
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Text = animal.CLIENT.ToString(),
                ForeColor = Color.Black,
                Location = new Point(window.Width * 12 / 50, window.Height * 11 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                Enabled = false
            };
            window.Controls.Add(CurrentOwner);
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
            if (animalNameBox.Text.Trim().Length != 0 &&
                birthYearBox.Text.Trim().Length != 0 && birthYearBox.Text.Length == 4 &&
                sizeBox.Text.Trim().Length != 0 &&
                weightBox.Text.Trim().Length != 0)
            {
                bool isMale;
                RACE newBreed;
                CLIENT newOwner;
                if(genderCB.Text == "Genre")
                {
                    isMale = animal.ESTMALE;
                } else
                {
                    isMale = genderCB.Text == "Male";
                }
                if(breedCB.Text == "Race")
                {
                    newBreed = animal.RACE;
                } else
                {
                    newBreed = (RACE)breedCB.SelectedItem;
                }
                if (possibleOwnerCB.Text == "Propriétaire")
                {
                    newOwner = animal.CLIENT;
                } else
                {
                    newOwner = (CLIENT)possibleOwnerCB.SelectedItem;
                }
                if (AnimalController.UpdateAnimal(animal, newBreed, newOwner, NormalizeName(animalNameBox.Text), birthYearBox.Text, Int32.Parse(sizeBox.Text), Int32.Parse(weightBox.Text), isMale))
                {
                    MessageBox.Show("Les informations liées à cet animal ont été modifiées.", "Comfirmation de modification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    window.Controls.Clear();
                    load();
                } else
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
        private void AddIntoSpeciesCB()
        {
            foreach (ESPECE species in AnimalController.AllSpecies())
            {
                if(animal != null)
                {
                    speciesAnimalFormCB.Items.Add(species);
                } else if(breed != null)
                {
                    speciesBreedFormCB.Items.Add(species);
                }
            }
        }

        /// <summary>
        /// Méthode permettant d'ajouter les deux choix possible pour le sexe de l'animal dans la ComboBox associée
        /// </summary>
        private void AddIntoGenderCB()
        {
            genderCB.Items.Add("Male");
            genderCB.Items.Add("Femelle");
        }

        /// <summary>
        /// Méthode permettant d'ajouter les clients possible pour les lié à l'animal dans la ComBox associée
        /// </summary>
        public void AddIntoPossibleOwnerCB()
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
                Text = specie.NOMESPECE,
                ForeColor = Color.Black,
                Location = new Point(window.Width * 24 / 50, window.Height * 4 / 20),
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
                Location = new Point(window.Width * 24 / 50, window.Height * 5 / 20),
                TabIndex = 8
            };
            window.Controls.Add(validateSpeciesForm);
            validateSpeciesForm.Click += new EventHandler(SubmitSpecieForm);
        }

        private void SubmitSpecieForm(object sender, EventArgs e)
        {
            if (specieNameBox.Text.Trim().Length != 0 && specieNameBox.Text != "Nom de l'espèce")
            {
                if (AnimalController.UpdateSpecie(specie, NormalizeName(specieNameBox.Text)))
                {
                    MessageBox.Show("L'espèce a été ajoutée à la base de données avec succès", "Comfirmation d'ajout à la base de donnée", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
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

            currentSpecieBreedForm = new TextBox
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Text = breed.ESPECE.NOMESPECE,
                ForeColor = Color.Black,
                Location = new Point(window.Width * 15 / 50, window.Height * 8 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                Enabled = false,
            };
            window.Controls.Add(currentSpecieBreedForm);

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
                Text = breed.NOMRACE,
                ForeColor = Color.Black,
                Location = new Point(window.Width * 24 / 50, window.Height * 9 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                MaxLength = 128
            };
            window.Controls.Add(breedNameBox);
            breedNameBox.GotFocus += new EventHandler(GetFocus);
            breedNameBox.LostFocus += new EventHandler(LostFocus);

            validateBreedForm = new UIButton(UIColor.ORANGE, "Modifier", window.Width * 15 / 100)
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
            if(animal != null)
            {
                AddToBreedCB();
            }
        }

        private void SubmitBreedForm(object sender, EventArgs e)
        {
            if (selectedSpeciesBreedForm == null && breedNameBox.Text.Length != 0)
            {
                if (AnimalController.UpdateBreed(breed, breed.ESPECE,NormalizeName(breedNameBox.Text)))
                {
                    MessageBox.Show("La race à bien été modifiée avec succés", "Comfirmation de modification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("La race existe déjà", "Problème de modification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }else if (selectedSpeciesBreedForm != null && breedNameBox.Text.Length != 0)
            {
                if (AnimalController.UpdateBreed(breed, selectedSpeciesBreedForm, NormalizeName(breedNameBox.Text)))
                {
                    MessageBox.Show("La race à bien été modifiée avec succés", "Comfirmation de modification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("La race existe déjà", "Problème de modification", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

