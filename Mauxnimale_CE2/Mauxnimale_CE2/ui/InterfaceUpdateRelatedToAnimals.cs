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
        private AInterface originInterface;

        private readonly RACE breed;
        private readonly ANIMAL animal;
        private readonly ESPECE specie;

        private Header header;
        private Footer footer;
        private Label animalLabel, breedLabel, speciesLabel;

        #region Animal form elements and attribute
        TextBox animalFormNameBox, animalFormBirthYearBox, animalFormSizeBox, animalFormWeigtBox, currentGender,currentBreed, currentOwner, currentSpecie;
        ComboBox animalFormGenderCB, animalFormSpeciesCB, animalFormPossibleBreedCB, animalFormOwner;
        UIButton animalFormValidate;
        ESPECE animalFormSelectedSpecie;
        #endregion

        #region Species form elements
        TextBox specieFormNameBox;
        UIButton specieFormValidate;
        #endregion

        #region Breed form elements
        TextBox breedFormNameBox, currentSpecieBreedForm;
        ComboBox breedFormSpeciesCB;
        UIButton breedFormValidate;
        ESPECE breedFormSelectedSpecie;
        #endregion

        UIRoundButton backButton;

        public InterfaceUpdateRelatedToAnimals(MainWindow window, SALARIE user, object o, AInterface originInterface) : base(window, user)
        {
            this.originInterface = originInterface;
            header = new Header(window);
            footer = new Footer(window, base.user);
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
            GenerateLabel();
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
            if(originInterface.GetType().ToString().Contains("InterfaceClient"))
            {
                window.Controls.Clear();
                window.switchInterface(new InterfaceClient(window, user));
            } else if (originInterface.GetType().ToString().Contains("InterfaceNewsRelatedToAnimals"))
            {
                window.Controls.Clear();
                window.switchInterface(new InterfaceNewsRelatedToAnimals(window, user, new InterfaceClient(window, user)));
            }
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

        #region Label
        private void GenerateLabel()
        {
            if(animal != null)
            {
                animalLabel = new Label
                {
                    Text = "Formulaire modification d'un animal",
                    TextAlign = ContentAlignment.MiddleLeft,
                    Font = new Font("Poppins", window.Height * 2 / 100),
                    ForeColor = UIColor.DARKBLUE,
                    Size = new Size(window.Width * 3 / 10, window.Height * 1 / 10),
                    Location = new Point(window.Width * 5 / 50, window.Height / 10)
                };
                window.Controls.Add(animalLabel);
            }

            if(specie != null)
            {
                speciesLabel = new Label
                {
                    Text = "Formulaire modification d'une espèce",
                    TextAlign = ContentAlignment.MiddleLeft,
                    Font = new Font("Poppins", window.Height * 2 / 100),
                    ForeColor = UIColor.DARKBLUE,
                    Size = new Size(window.Width * 3 / 10, window.Height * 1 / 10),
                    Location = new Point(window.Width * 22 / 50, window.Height / 10)
                };
                window.Controls.Add(speciesLabel);
            }

            if(breed != null)
            {
                breedLabel = new Label
                {
                    Text = "Formulaire modification d'une race",
                    TextAlign = ContentAlignment.MiddleLeft,
                    Font = new Font("Poppins", window.Height * 2 / 100),
                    ForeColor = UIColor.DARKBLUE,
                    Size = new Size(window.Width * 3 / 10, window.Height * 1 / 10),
                    Location = new Point(window.Width * 22 / 50, window.Height * 6 / 20)
                };
                window.Controls.Add(breedLabel);
            }
        }
        #endregion

        #region ANIMAL FORM
        #region Generation of ComboBoxes of form animal
        /// <summary>
        /// Méthode permettant de générer les ComboBox liés au formulaire lié à l'ajout d'un animal
        /// </summary>
        private void FormAnimalGenerateComboBox()
        {
            animalFormGenderCB = new ComboBox
            {
                Location = new Point(window.Width * 20 / 50, window.Height * 8 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                TabIndex = 4,
                ForeColor = Color.Black,
                Text = "Genre",
            };
            window.Controls.Add(animalFormGenderCB);

            animalFormSpeciesCB = new ComboBox
            {
                Location = new Point(window.Width * 20 / 50, window.Height * 9 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                TabIndex = 5,
                ForeColor = Color.Black,
                Text = "Espèce"
            };
            window.Controls.Add(animalFormSpeciesCB);
            animalFormSpeciesCB.SelectedValueChanged += new EventHandler(SelectedSpecies);

            animalFormPossibleBreedCB = new ComboBox
            {
                Location = new Point(window.Width * 20 / 50, window.Height * 10 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                ForeColor = Color.Black,
                TabIndex = 6,
                Text = "Race"
            };
            window.Controls.Add(animalFormPossibleBreedCB);

            animalFormOwner = new ComboBox
            {
                Location = new Point(window.Width * 20 / 50, window.Height * 11 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                TabIndex = 7,
                ForeColor = Color.Black,
                Text = "Propriétaire"
            };
            window.Controls.Add(animalFormOwner);
        }
        #endregion

        #region Generation of validate button of form animal
        /// <summary>
        /// Methodes qui génére tous les bouttons
        /// </summary>
        private void FormAnimalGenerateButton()
        {
            animalFormValidate = new UIButton(UIColor.ORANGE, "Modifier", window.Width * 15 / 100)
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Height = window.Height / 25,
                Location = new Point(window.Width * 20 / 50, window.Height * 12 / 20),
                TabIndex = 8
            };
            window.Controls.Add(animalFormValidate);
            animalFormValidate.Click += new EventHandler(SubmitAnimalForm);

        }
        #endregion

        #region Generation of TextBox of form animal
        /// <summary>
        /// Méthode permettant de générer les TextBox de l'ihm
        /// </summary>
        private void FormAnimalGenerateTextBox()
        {
            animalFormNameBox = new TextBox
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Text = animal.NOM,
                ForeColor = Color.Black,
                Location = new Point(window.Width * 20 / 50, window.Height * 4 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                MaxLength = 128,
                TabIndex = 0
            };
            window.Controls.Add(animalFormNameBox);
            animalFormNameBox.GotFocus += new EventHandler(GetFocus);
            animalFormNameBox.LostFocus += new EventHandler(LostFocus);

            animalFormBirthYearBox = new TextBox
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Text = animal.ANNEENAISSANCE,
                ForeColor = Color.Black,
                Location = new Point(window.Width * 20 / 50, window.Height * 5 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                MaxLength = 4,
                TabIndex = 1
            };
            window.Controls.Add(animalFormBirthYearBox);
            animalFormBirthYearBox.GotFocus += new EventHandler(GetFocus);
            animalFormBirthYearBox.LostFocus += new EventHandler(LostFocus);
            animalFormBirthYearBox.KeyPress += new KeyPressEventHandler(KeyPress);


            animalFormSizeBox = new TextBox
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Text = animal.TAILLE.ToString(),
                ForeColor = Color.Black,
                Location = new Point(window.Width * 20 / 50, window.Height * 6 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                MaxLength = 4,
                TabIndex = 2
            };
            window.Controls.Add(animalFormSizeBox);
            animalFormSizeBox.GotFocus += new EventHandler(GetFocus);
            animalFormSizeBox.LostFocus += new EventHandler(LostFocus);
            animalFormSizeBox.KeyPress += new KeyPressEventHandler(KeyPress);


            animalFormWeigtBox = new TextBox
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Text = animal.POIDS.ToString(),
                ForeColor = Color.Black,
                Location = new Point(window.Width * 20 / 50, window.Height * 7 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                MaxLength = 4,
                TabIndex = 3
            };
            window.Controls.Add(animalFormWeigtBox);
            animalFormWeigtBox.GotFocus += new EventHandler(GetFocus);
            animalFormWeigtBox.LostFocus += new EventHandler(LostFocus);
            animalFormWeigtBox.KeyPress += new KeyPressEventHandler(KeyPress);

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

            currentOwner = new TextBox
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Text = animal.CLIENT.ToString(),
                ForeColor = Color.Black,
                Location = new Point(window.Width * 12 / 50, window.Height * 11 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                Enabled = false
            };
            window.Controls.Add(currentOwner);
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
            animalFormSelectedSpecie = animalFormSpeciesCB.SelectedItem == " " ? null : (ESPECE)animalFormSpeciesCB.SelectedItem;
            AddToBreedCB();
        }
        #endregion

        #region Register a new animal (click on validate button)
        private void SubmitAnimalForm(object sender, EventArgs e)
        {
            if (animalFormNameBox.Text.Trim().Length != 0 &&
                animalFormBirthYearBox.Text.Trim().Length != 0 && animalFormBirthYearBox.Text.Length == 4 &&
                animalFormSizeBox.Text.Trim().Length != 0 &&
                animalFormWeigtBox.Text.Trim().Length != 0)
            {
                bool isMale;
                RACE newBreed;
                CLIENT newOwner;
                if(animalFormGenderCB.Text == "Genre")
                {
                    isMale = animal.ESTMALE;
                } else
                {
                    isMale = animalFormGenderCB.Text == "Male";
                }
                if(animalFormPossibleBreedCB.Text == "Race")
                {
                    newBreed = animal.RACE;
                } else
                {
                    newBreed = (RACE)animalFormPossibleBreedCB.SelectedItem;
                }
                if (animalFormOwner.Text == "Propriétaire")
                {
                    newOwner = animal.CLIENT;
                } else
                {
                    newOwner = (CLIENT)animalFormOwner.SelectedItem;
                }
                if (AnimalController.UpdateAnimal(animal, newBreed, newOwner, NormalizeName(animalFormNameBox.Text), animalFormBirthYearBox.Text, Int32.Parse(animalFormSizeBox.Text), Int32.Parse(animalFormWeigtBox.Text), isMale))
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
                    animalFormSpeciesCB.Items.Add(species);
                } else if(breed != null)
                {
                    breedFormSpeciesCB.Items.Add(species);
                }
            }
        }

        /// <summary>
        /// Méthode permettant d'ajouter les deux choix possible pour le sexe de l'animal dans la ComboBox associée
        /// </summary>
        private void AddIntoGenderCB()
        {
            animalFormGenderCB.Items.Add("Male");
            animalFormGenderCB.Items.Add("Femelle");
        }

        /// <summary>
        /// Méthode permettant d'ajouter les clients possible pour les lié à l'animal dans la ComBox associée
        /// </summary>
        public void AddIntoPossibleOwnerCB()
        {
            foreach (CLIENT c in ClientController.AllClient())
            {
                animalFormOwner.Items.Add(c);
            }
        }

        /// <summary>
        /// Méthode permettant d'ajouter les races liées à une espèce
        /// </summary>
        public void AddToBreedCB()
        {
            animalFormPossibleBreedCB.Items.Clear();
            animalFormPossibleBreedCB.Text = "Race";
            animalFormPossibleBreedCB.SelectedItem = null;
            if (animalFormSelectedSpecie != null)
            {
                foreach (RACE r in AnimalController.BreedsWithSpecie((ESPECE)animalFormSpeciesCB.SelectedItem))
                {
                    animalFormPossibleBreedCB.Items.Add(r);
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
            specieFormNameBox = new TextBox
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Text = specie.NOMESPECE,
                ForeColor = Color.Black,
                Location = new Point(window.Width * 24 / 50, window.Height * 4 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                MaxLength = 128
            };
            window.Controls.Add(specieFormNameBox);
            specieFormNameBox.GotFocus += new EventHandler(GetFocus);
            specieFormNameBox.LostFocus += new EventHandler(LostFocus);

            specieFormValidate = new UIButton(UIColor.ORANGE, "Ajouter", window.Width * 15 / 100)
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Height = window.Height / 25,
                Location = new Point(window.Width * 24 / 50, window.Height * 5 / 20),
                TabIndex = 8
            };
            window.Controls.Add(specieFormValidate);
            specieFormValidate.Click += new EventHandler(SubmitSpecieForm);
        }

        private void SubmitSpecieForm(object sender, EventArgs e)
        {
            if (specieFormNameBox.Text.Trim().Length != 0 && specieFormNameBox.Text != "Nom de l'espèce")
            {
                if (AnimalController.UpdateSpecie(specie, NormalizeName(specieFormNameBox.Text)))
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

            breedFormSpeciesCB = new ComboBox
            {
                Location = new Point(window.Width * 24 / 50, window.Height * 8 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                ForeColor = Color.Black,
                Text = "Espèce"
            };
            window.Controls.Add(breedFormSpeciesCB);
            breedFormSpeciesCB.SelectedValueChanged += new EventHandler(BreedFormSelectedSpecies);

            breedFormNameBox = new TextBox
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Text = breed.NOMRACE,
                ForeColor = Color.Black,
                Location = new Point(window.Width * 24 / 50, window.Height * 9 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                MaxLength = 128
            };
            window.Controls.Add(breedFormNameBox);
            breedFormNameBox.GotFocus += new EventHandler(GetFocus);
            breedFormNameBox.LostFocus += new EventHandler(LostFocus);

            breedFormValidate = new UIButton(UIColor.ORANGE, "Modifier", window.Width * 15 / 100)
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Height = window.Height / 25,
                Location = new Point(window.Width * 24 / 50, window.Height * 10 / 20)
            };
            window.Controls.Add(breedFormValidate);
            breedFormValidate.Click += new EventHandler(SubmitBreedForm);
        }

        /// <summary>
        /// Méthode répondant à l'évènement de click sur un objet de la liste des espèces du formulaire de race
        /// </summary>
        /// <param name="sender">Liste qui reçoit le clic</param>
        /// <param name="e">Le clic</param>
        private void BreedFormSelectedSpecies(object sender, EventArgs e)
        {
            breedFormSelectedSpecie = breedFormSpeciesCB.SelectedItem == " " ? null : (ESPECE)breedFormSpeciesCB.SelectedItem;
            // on met à jours les 
            if(animal != null)
            {
                AddToBreedCB();
            }
        }

        /// <summary>
        /// Permet d'ajouter une race à la base si les informations rentrées sont correcte.
        /// </summary>
        /// <param name="sender">Bouton ajouter du formulaire</param>
        /// <param name="e">Le clic</param>
        private void SubmitBreedForm(object sender, EventArgs e)
        {
            if (breedFormSelectedSpecie == null && breedFormNameBox.Text.Length != 0)
            {
                if (AnimalController.UpdateBreed(breed, breed.ESPECE,NormalizeName(breedFormNameBox.Text)))
                {
                    MessageBox.Show("La race à bien été modifiée avec succés", "Comfirmation de modification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("La race existe déjà", "Problème de modification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }else if (breedFormSelectedSpecie != null && breedFormNameBox.Text.Length != 0)
            {
                if (AnimalController.UpdateBreed(breed, breedFormSelectedSpecie, NormalizeName(breedFormNameBox.Text)))
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
        /// <summary>
        /// Méthode qui répond à l'évenement une TextBox qui perd le focus de l'utilisateur.
        /// </summary>
        /// <param name="sender">La TextBox qui perd le focus</param>
        /// <param name="e">Perte du focus</param>
        private void LostFocus(object sender, EventArgs e)
        {
            if (sender.Equals(animalFormWeigtBox) && animalFormWeigtBox.Text.Length == 0)
            {
                animalFormWeigtBox.Text = "Poids";
                animalFormWeigtBox.ForeColor = Color.Gray;
            }
            if (sender.Equals(animalFormNameBox) && animalFormNameBox.Text.Length == 0)
            {
                animalFormNameBox.Text = "Nom";
                animalFormNameBox.ForeColor = Color.Gray;
            }
            if (sender.Equals(animalFormSizeBox) && animalFormSizeBox.Text.Length == 0)
            {
                animalFormSizeBox.Text = "Taille";
                animalFormSizeBox.ForeColor = Color.Gray;
            }
            if (sender.Equals(animalFormBirthYearBox) && animalFormBirthYearBox.Text.Length == 0)
            {
                animalFormBirthYearBox.Text = "Année de naissance";
                animalFormBirthYearBox.ForeColor = Color.Gray;
            }
            if (sender.Equals(specieFormNameBox) && specieFormNameBox.Text.Length == 0)
            {
                specieFormNameBox.Text = "Nom de l'espèce";
                specieFormNameBox.ForeColor = Color.Gray;
            }
            if (sender.Equals(breedFormNameBox) && breedFormNameBox.Text.Length == 0)
            {
                breedFormNameBox.Text = "Nom de la race";
                breedFormNameBox.ForeColor = Color.Gray;
            }
        }

        /// <summary>
        /// Méthode qui répond à l'évenement une TextBox qui gagne le focus de l'utilisateur.
        /// </summary>
        /// <param name="sender">La TextBox qui gagne le focus</param>
        /// <param name="e">Acquisition du focus</param>
        private void GetFocus(object sender, EventArgs e)
        {
            if (sender.Equals(animalFormWeigtBox) && animalFormWeigtBox.Text == "Poids")
            {
                animalFormWeigtBox.Text = "";
                animalFormWeigtBox.ForeColor = Color.Black;
            }
            if (sender.Equals(animalFormNameBox) && animalFormNameBox.Text == "Nom")
            {
                animalFormNameBox.Text = "";
                animalFormNameBox.ForeColor = Color.Black;
            }
            if (sender.Equals(animalFormSizeBox) && animalFormSizeBox.Text == "Taille")
            {
                animalFormSizeBox.Text = "";
                animalFormSizeBox.ForeColor = Color.Black;
            }
            if (sender.Equals(animalFormBirthYearBox) && animalFormBirthYearBox.Text == "Année de naissance")
            {
                animalFormBirthYearBox.Text = "";
                animalFormBirthYearBox.ForeColor = Color.Black;
            }
            if (sender.Equals(specieFormNameBox) && specieFormNameBox.Text == "Nom de l'espèce")
            {
                specieFormNameBox.Text = "";
                specieFormNameBox.ForeColor = Color.Black;
            }
            if (sender.Equals(breedFormNameBox) && breedFormNameBox.Text == "Nom de la race")
            {
                breedFormNameBox.Text = "";
                breedFormNameBox.ForeColor = Color.Black;
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

