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
        private readonly AInterface originInterface;
        private readonly Header header;
        private readonly Footer footer;

        private Label animalLabel, breedLabel, speciesLabel;

        #region Animal form elements and attribute
        TextBox animalFormNameBox, animalFormBirthYearBox, animalFormSizeBox, animalFormWeightBox;
        ComboBox animalFormGenderCB, animalFormSpeciesCB, animalFormPossibleBreed, animalFormPossibleOwner;
        UIButton animalFormValidate;
        ESPECE animalFormSelectedSpecie;
        #endregion

        #region Species form elements
        TextBox specieFormNameBox, specieFormResearchBar;
        UIButton specieFormValidate, specieFormUpdate, specieFormDelete;
        ListBox specieFormAllSpecieLB;
        #endregion

        #region Breed form elements
        TextBox breedFormNameBox, breedFormResearchBar;
        ListBox breedFromBreedLB;
        ComboBox breedFormSpeciesCB;
        UIButton breedFormValidate, breedFormUpdate, breedFormDelete;
        ESPECE breedFormSelectedSpecie;
        RACE breedFormSelectedBreed;
        #endregion

        UIRoundButton backButton;

        /// <summary>
        /// Constructeur de l'interface
        /// </summary>
        /// <param name="window"></param>
        /// <param name="user"></param>
        public InterfaceNewsRelatedToAnimals(MainWindow window, SALARIE user, AInterface originInterface) : base(window, user)
        {
            this.originInterface = originInterface;
            header = new Header(window);
            footer = new Footer(window, base.user);
        }

        /// <summary>
        /// Méthode permettant de d'afficher et remplir les objets de la fenêtre.
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
            // Génération des éléments du formulaire liés à l'ajout et la modification d'une espèce
            FormSpecies();
            //Génération des éléments du formulaire liés à l'ajout et de modification d'une race
            FormBreed();
            // Ajout des données dans les éléments
            InitGenderCB();
            AddDataInCBandB();
            InitClientCB();
        }

        #region Back button
        private void BackPage(object sender, EventArgs e)
        {
            // Permet de retourne sur l'interface qui avait lancée cette interface
            if (originInterface.GetType().ToString().Split('.')[2] == "InterfaceClient")
            {
                window.Controls.Clear();
                window.switchInterface(new InterfaceClient(window, user));
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
        /// <summary>
        /// Permet de générer les labels des différents formulaire de la fenêtre
        /// </summary>
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
                Location = new Point(window.Width * 22 / 50, window.Height * 9 / 20)
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
            animalFormGenderCB = new ComboBox
            {
                Location = new Point(window.Width * 7 / 50, window.Height * 8 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                TabIndex = 4,
                ForeColor = Color.Black,
                Text = "Sexe"
            };
            window.Controls.Add(animalFormGenderCB);

            animalFormSpeciesCB = new ComboBox
            {
                Location = new Point(window.Width * 7 / 50, window.Height * 9 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                TabIndex = 5,
                ForeColor = Color.Black,
                Text = "Espèce"
            };
            window.Controls.Add(animalFormSpeciesCB);
            animalFormSpeciesCB.SelectedValueChanged += new EventHandler(SelectedSpecies);

            animalFormPossibleBreed = new ComboBox
            {
                Location = new Point(window.Width * 7 / 50, window.Height * 10 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                TabIndex = 6
            };
            animalFormGenderCB.ForeColor = Color.Black;
            animalFormPossibleBreed.Text = "Race";
            window.Controls.Add(animalFormPossibleBreed);

            animalFormPossibleOwner = new ComboBox
            {
                Location = new Point(window.Width * 7 / 50, window.Height * 11 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                TabIndex = 7
            };
            animalFormGenderCB.ForeColor = Color.Black;
            animalFormPossibleOwner.Text = "Propriétaire";
            window.Controls.Add(animalFormPossibleOwner);
        }
        #endregion

        #region Generation of validate button of form animal
        /// <summary>
        /// Methodes qui génére tous les bouttons
        /// </summary>
        private void FormAnimalGenerateButton()
        {
            animalFormValidate = new UIButton(UIColor.ORANGE, "Ajouter", window.Width * 15 / 100)
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Height = window.Height / 25,
                Location = new Point(window.Width * 7 / 50, window.Height * 12 / 20),
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
                Text = "Nom",
                ForeColor = Color.Gray,
                BackColor = Color.White,
                Location = new Point(window.Width * 7 / 50, window.Height * 4 / 20),
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
                Text = "Année de naissance",
                ForeColor = Color.Gray,
                BackColor = Color.White,
                Location = new Point(window.Width * 7 / 50, window.Height * 5 / 20),
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
                Text = "Taille",
                ForeColor = Color.Gray,
                BackColor = Color.White,
                Location = new Point(window.Width * 7 / 50, window.Height * 6 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                MaxLength = 4,
                TabIndex = 2
            };
            window.Controls.Add(animalFormSizeBox);
            animalFormSizeBox.GotFocus += new EventHandler(GetFocus);
            animalFormSizeBox.LostFocus += new EventHandler(LostFocus);
            animalFormSizeBox.KeyPress += new KeyPressEventHandler(KeyPress);


            animalFormWeightBox = new TextBox
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
            window.Controls.Add(animalFormWeightBox);
            animalFormWeightBox.GotFocus += new EventHandler(GetFocus);
            animalFormWeightBox.LostFocus += new EventHandler(LostFocus);
            animalFormWeightBox.KeyPress += new KeyPressEventHandler(KeyPress);

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
            if (animalFormGenderCB.Text != "Genre" &&
                animalFormPossibleBreed.Text != "Race" &&
                animalFormPossibleOwner.Text != "Propriétaire" &&
                animalFormNameBox.Text.Trim().Length != 0 &&
                animalFormBirthYearBox.Text.Trim().Length != 0 && animalFormBirthYearBox.Text.Length == 4 &&
                animalFormSizeBox.Text.Trim().Length != 0 &&
                animalFormWeightBox.Text.Trim().Length != 0)
            {
                RACE breed = (RACE)animalFormPossibleBreed.SelectedItem;
                CLIENT owner = (CLIENT)animalFormPossibleOwner.SelectedItem;
                bool isMale = animalFormGenderCB.Text == "Male";
                if (AnimalController.RegisterAnimal(breed, owner, NormalizeName(animalFormNameBox.Text), animalFormBirthYearBox.Text, Int32.Parse(animalFormSizeBox.Text), Int32.Parse(animalFormWeightBox.Text), isMale))
                {
                    // On vide tous les champs du formulaire
                    animalFormGenderCB.Text = "Genre";
                    animalFormPossibleBreed.Text = "Race";
                    animalFormPossibleOwner.Text = "Propriétaire";
                    animalFormNameBox.Text = "Nom";
                    animalFormBirthYearBox.Text = "Année de naissance";
                    animalFormSizeBox.Text = "Taille";
                    animalFormWeightBox.Text = "Poids";
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

        #region Add data in comboBox and listbox
        /// <summary>
        /// Méthode permettant d'ajouter les données dans les ComboBox et ListBox associées
        /// </summary>
        private void AddDataInCBandB()
        {
            animalFormSpeciesCB.Items.Clear();
            breedFormSpeciesCB.Items.Clear();
            specieFormAllSpecieLB.Items.Clear();
            foreach (ESPECE species in AnimalController.AllSpecies())
            {
                animalFormSpeciesCB.Items.Add(species);
                breedFormSpeciesCB.Items.Add(species);
                specieFormAllSpecieLB.Items.Add(species);
            }
            breedFromBreedLB.Items.Add(" ");
            foreach (RACE breed in AnimalController.AllBreeds())
            {
                breedFromBreedLB.Items.Add(breed);
            }
        }

        /// <summary>
        /// Méthode permettant d'ajouter les deux choix possible pour le genre de l'animal dans la ComboBox associée
        /// </summary>
        private void InitGenderCB()
        {
            animalFormGenderCB.Items.Add("Male");
            animalFormGenderCB.Items.Add("Femelle");
        }

        /// <summary>
        /// Méthode permettant d'ajouter les clients possible pour les lié à l'animal dans la ComBox associée
        /// </summary>
        public void InitClientCB()
        {
            foreach (CLIENT c in ClientController.AllClient())
            {
                animalFormPossibleOwner.Items.Add(c);
            }
        }

        /// <summary>
        /// Méthode permettant d'ajouter les races liées à une espèce
        /// </summary>
        public void AddToBreedCB()
        {
            animalFormPossibleBreed.Items.Clear();
            animalFormPossibleBreed.Text = "Race";
            animalFormPossibleBreed.SelectedItem = null;
            if (animalFormSelectedSpecie != null)
            {
                foreach (RACE r in AnimalController.BreedsWithSpecie((ESPECE)animalFormSpeciesCB.SelectedItem))
                {
                    animalFormPossibleBreed.Items.Add(r);
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
                Text = "Nom de l'espèce",
                ForeColor = Color.Gray,
                Location = new Point(window.Width * 24 / 50, window.Height * 6 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                MaxLength = 128
            };
            window.Controls.Add(specieFormNameBox);
            specieFormNameBox.BringToFront();   
            specieFormNameBox.GotFocus += new EventHandler(GetFocus);
            specieFormNameBox.LostFocus += new EventHandler(LostFocus);

            specieFormValidate = new UIButton(UIColor.ORANGE, "Ajouter", window.Width * 8 / 100)
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Height = window.Height / 25,
                Location = new Point(window.Width * 24 / 50, window.Height * 7 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
            };
            window.Controls.Add(specieFormValidate);
            specieFormValidate.BringToFront();
            specieFormValidate.Click += new EventHandler(SubmitSpecieForm);

            specieFormAllSpecieLB = new ListBox()
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Location = new Point(window.Width * 35 / 50, window.Height * 6 / 20),
                Size = new Size(window.Width * 10 / 50, window.Height * 3 / 20),
            };
            window.Controls.Add(specieFormAllSpecieLB);
            specieFormAllSpecieLB.BringToFront();
            specieFormAllSpecieLB.SelectedIndexChanged += new EventHandler(SelectedSpeciesForUpdate);

            specieFormResearchBar = new TextBox()
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Text = "Recherche",
                ForeColor = Color.Gray,
                Location = new Point(window.Width * 35 / 50, window.Height * 5 / 20),
                Size = new Size(window.Width * 10 / 50, window.Height * 3 / 20),
            };
            window.Controls.Add(specieFormResearchBar);
            specieFormResearchBar.GotFocus += new EventHandler(ResearchGotFocus);
            specieFormResearchBar.TextChanged += new EventHandler(ResearchByName);

            specieFormUpdate = new UIButton(UIColor.ORANGE, "Modifier", window.Width * 8 / 100)
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Height = window.Height / 25,
                Location = new Point(window.Width * 35 / 50, window.Height * 9 / 20),
                Size = new Size(window.Width * 4 / 50, window.Height * 5 / 100),
            };
            specieFormUpdate.Click += new EventHandler(UpdateSpecie);

            specieFormDelete = new UIButton(UIColor.ORANGE, "Supprimer", window.Width * 8 / 100)
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Height = window.Height / 25,
                Location = new Point(window.Width * 41 / 50, window.Height * 9 / 20),
                Size = new Size(window.Width * 4 / 50, window.Height * 5 / 100),
            };
            specieFormDelete.Click += new EventHandler(DeleteSpecie);
        }

        private void DeleteSpecie(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Toutes les races liées à cette espèce et tout les animaux liés à ces races seront supprimé et IRRÉCUPÉRABLE.", "Demande de confirmation", MessageBoxButtons.OKCancel);
            if(result == DialogResult.OK)
            {
                if (AnimalController.DeleteSpecie((ESPECE)specieFormAllSpecieLB.SelectedItem))
                {
                    AddDataInCBandB();
                    MessageBox.Show("L'espèce à bien été supprimée.", "Confirmation de suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("L'espèce n'a pas pu être supprimée.", "Problème de suppression", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Méthode répondant à l'évenement de la sélection d'un objet dans la liste des espèces.
        /// Permet d'afficher les boutons de modification et de suppression d'une espèce.
        /// </summary>
        /// <param name="sender">Liste contenant les espèce</param>
        /// <param name="e">Sélection d'un objet</param>
        private void SelectedSpeciesForUpdate(object sender, EventArgs e)
        {
            if(specieFormAllSpecieLB.SelectedItem != null && specieFormAllSpecieLB.SelectedItem != " ")
            {
                window.Controls.Add(specieFormUpdate);
                window.Controls.Add(specieFormDelete);
                specieFormDelete.BringToFront();
                specieFormUpdate.BringToFront();
            } else
            {
                window.Controls.Remove(specieFormUpdate);
                window.Controls.Remove(specieFormDelete);
            }
            window.Refresh();
        }

        /// <summary>
        /// Méthode répondant à l'évenement du clic sur le bouton du formulaire d'ajout d'espèce.
        /// Permet d'jouter une éspèce à la base si le formulaire est bien remplis.
        /// </summary>
        /// <param name="sender">Bouron d'ajout du formulaire</param>
        /// <param name="e">Le clic</param>
        private void SubmitSpecieForm(object sender, EventArgs e)
        {
            if(specieFormNameBox.Text.Trim() != null && specieFormNameBox.Text != "Nom de l'espèce")
            {
                if (AnimalController.AddSpecie(NormalizeName(specieFormNameBox.Text)))
                {
                    MessageBox.Show("L'espèce a été ajoutée à la base de données avec succès", "Comfirmation d'ajout à la base de donnée", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } else
                {
                    MessageBox.Show("Ce nom d'espèce existe déjà", "Problème d'insertion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            AddDataInCBandB();
        }

        /// <summary>
        /// Méthode permettant de changer d'interface.
        /// </summary>
        /// <param name="sender">Bouton de modification d'une espèce</param>
        /// <param name="e">Le clic</param>
        private void UpdateSpecie(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceUpdateRelatedToAnimals(window, user, specieFormAllSpecieLB.SelectedItem, this));
        }
        #endregion

        // Région contenant tous tous les objets liés à l'ajout et la modification d'une race.
        #region BREED FORM
        /// <summary>
        /// Méthode permettant de générer les éléments du formulaire lié à l'ajout et la modification d'une race.
        /// </summary>
        private void FormBreed()
        {
            breedFormSpeciesCB = new ComboBox
            {
                Location = new Point(window.Width * 24 / 50, window.Height * 11 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                ForeColor = Color.Black,
                Text = "Espèce"
            };
            window.Controls.Add(breedFormSpeciesCB);
            breedFormSpeciesCB.BringToFront();
            breedFormSpeciesCB.SelectedValueChanged += new EventHandler(BreedFormSelectedSpecies);

            breedFormNameBox = new TextBox
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Text = "Nom de la race",
                ForeColor = Color.Gray,
                Location = new Point(window.Width * 24 / 50, window.Height * 12 / 20),
                Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100),
                MaxLength = 128
            };
            window.Controls.Add(breedFormNameBox);
            breedFormNameBox.GotFocus += new EventHandler(GetFocus);
            breedFormNameBox.LostFocus += new EventHandler(LostFocus);

            breedFormValidate = new UIButton(UIColor.ORANGE, "Ajouter", window.Width * 15 / 100)
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Height = window.Height / 25,
                Location = new Point(window.Width * 24 / 50, window.Height * 13 / 20)
            };
            window.Controls.Add(breedFormValidate);
            breedFormValidate.Click += new EventHandler(SubmitBreedForm);

            breedFormResearchBar = new TextBox()
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Text = "Recherche",
                ForeColor = Color.Gray,
                Location = new Point(window.Width * 35 / 50, window.Height * 11 / 20),
                Size = new Size(window.Width * 10 / 50, window.Height * 3 / 20),
            };
            window.Controls.Add(breedFormResearchBar);
            breedFormResearchBar.GotFocus += new EventHandler(ResearchGotFocus);
            breedFormResearchBar.TextChanged += new EventHandler(ResearchByName);

            breedFromBreedLB = new ListBox()
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Location = new Point(window.Width * 35 / 50, window.Height * 12 / 20),
                Size = new Size(window.Width * 10 / 50, window.Height * 3 / 20),
            };
            window.Controls.Add(breedFromBreedLB);
            breedFromBreedLB.BringToFront();
            breedFromBreedLB.SelectedIndexChanged += new EventHandler(SelectedBreedForUpdate);

            breedFormUpdate = new UIButton(UIColor.ORANGE, "Modifier", window.Width * 8 / 100)
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Height = window.Height / 25,
                Location = new Point(window.Width * 35 / 50, window.Height * 15 / 20),
                Size = new Size(window.Width * 4 / 50, window.Height * 5 / 100),
            };
            breedFormUpdate.Click += new EventHandler(UpdateBreed);

            breedFormDelete = new UIButton(UIColor.ORANGE, "Supprimer", window.Width * 8 / 100)
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Height = window.Height / 25,
                Location = new Point(window.Width * 41 / 50, window.Height * 15 / 20),
                Size = new Size(window.Width * 4 / 50, window.Height * 5 / 100),
            };
            breedFormDelete.Click += new EventHandler(DeleteBreed);
        }

        /// <summary>
        /// Méthode répondant à l'évenement du clic sur le bouton supprimer du formulaire lié aux races.
        /// Méthode permettant de supprimer une race.
        /// </summary>
        /// <param name="sender">Bouton de suppréssion</param>
        /// <param name="e">Le clic</param>
        private void DeleteBreed(object sender, EventArgs e)
        {
            // Demande de confirmation
            var result = MessageBox.Show("Tous les animaux liés à cette race seront supprimés et IRRÉCUPÉRABLE en plus de la race sélectionnée.", "Demande de confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if(result == DialogResult.OK)
            {
                if (AnimalController.DeleteBreed(breedFormSelectedBreed)){
                    AddToBreedCB();
                    MessageBox.Show("La race a bien été supprimée.", "Validation de suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }else
                {
                    MessageBox.Show("La race n'a pas pu être supprimée.", "Problème de suppression", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Méthoe répondant à l'évenement du clic sur le bouton de modification d'une race.
        /// Permet changer d'interface et de lancer interface de modification liées aux informations des animaux.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateBreed(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceUpdateRelatedToAnimals(window, user, breedFormSelectedBreed, this));
        }

        /// <summary>
        /// Méthode répondant à l'évenement d'un objet sélectionné dans la liste qui contient les races.
        /// Permet de faire apparaitre le bouton des modification et suppression d'éléments
        /// </summary>
        /// <param name="sender">Liste des races</param>
        /// <param name="e">Sélection un objet dans la liste</param>
        private void SelectedBreedForUpdate(object sender, EventArgs e)
        {
            breedFormSelectedBreed = (breedFromBreedLB.SelectedItem == " ") ? null : (RACE)breedFromBreedLB.SelectedItem;
            if (breedFormSelectedBreed != null)
            {
                window.Controls.Add(breedFormUpdate);
                window.Controls.Add(breedFormDelete);
            } else
            {
                window.Controls.Remove(breedFormUpdate);
                window.Controls.Remove(breedFormDelete);
            }
            window.Refresh();
        }

        /// <summary>
        /// Méthode répondant à l'évènement de click sur un objet de la liste des espèces du formulaire de race
        /// </summary>
        /// <param name="sender">Liste qui reçoit le clic</param>
        /// <param name="e">Le clic</param>
        private void BreedFormSelectedSpecies(object sender, EventArgs e)
        {
            breedFormSelectedSpecie = breedFormSpeciesCB.SelectedItem == " " ? null : (ESPECE)breedFormSpeciesCB.SelectedItem;
            UpdateBreedsList();
        }

        /// <summary>
        /// Liste de toutes les races présente dans la base.
        /// Si une espèce est sélectionnée par l'utilisateur, les races affichées seront celles qui sont liées à l'espèce.
        /// Si aucune espèce est choisie alors toutes les races seront affichées.
        /// </summary>
        private void UpdateBreedsList()
        {
            breedFromBreedLB.Items.Clear();
            breedFromBreedLB.Items.Add(" ");
            if(breedFormSelectedSpecie != null)
            {
                // Rempli la liste avec un trie par espèce
                foreach (RACE breed in AnimalController.BreedsWithSpecie(breedFormSelectedSpecie))
                {
                    breedFromBreedLB.Items.Add(breed);
                }
            } else
            {
                // Remplie la liste avec toutes les races 
                foreach (RACE breed in AnimalController.AllBreeds())
                {
                    breedFromBreedLB.Items.Add(breed);
                }
            }
        }

        /// <summary>
        /// Méthode répondant au clic du bouton du formulaire d'espèce.
        /// Permet d'ajouter une race à la base de donnée.
        /// </summary>
        /// <param name="sender">Le bouton du formulaire</param>
        /// <param name="e">Le clic</param>
        private void SubmitBreedForm(object sender, EventArgs e)
        {
            if (breedFormSelectedSpecie != null && breedFormNameBox.Text.Length != 0)
            {
                if(AnimalController.AddBreed(breedFormSelectedSpecie, NormalizeName(breedFormNameBox.Text)))
                {
                    MessageBox.Show("La race à bien été ajoutée à la base avec succés", "Comfirmation d'ajout à la base de donnée", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }else
                {
                    MessageBox.Show("La race existe déjà", "Problème d'insertion à la base de donnée", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            AddToBreedCB();
        }
        #endregion

        // Région liée au evènement de FOCUS d'objet notament les TextBox
        #region focus event
        /// <summary>
        /// Méthode qui répond à l'évenement une TextBox qui perd le focus de l'utilisateur.
        /// </summary>
        /// <param name="sender">La TextBox qui perd le focus</param>
        /// <param name="e">Perte du focus</param>
        private void LostFocus(object sender, EventArgs e)
        {
            if (sender.Equals(animalFormWeightBox) && animalFormWeightBox.Text.Length == 0)
            {
                animalFormWeightBox.Text = "Poids";
                animalFormWeightBox.ForeColor = Color.Gray;
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
            if (sender.Equals(animalFormWeightBox) && animalFormWeightBox.Text == "Poids")
            {
                animalFormWeightBox.Text = "";
                animalFormWeightBox.ForeColor = Color.Black;
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

        /// <summary>
        /// Méthode répondant à l'évenement d'une barre de recherche qui gagne le focus de l'utilisateur.
        /// Permet de vider le text si c'est la première fois que l'utilisateur clic sur la barre.
        /// </summary>
        /// <param name="sender">La barre de recherche</param>
        /// <param name="e">La prise de focus</param>
        private void ResearchGotFocus(object sender, EventArgs e)
        {
            if (sender.Equals(specieFormResearchBar) && specieFormResearchBar.Text == "Recherche")
            {
                specieFormResearchBar.Text = "";
                specieFormResearchBar.ForeColor = Color.Black;
            }
            if (sender.Equals(breedFormResearchBar) && breedFormResearchBar.Text == "Recherche")
            {
                breedFormResearchBar.Text = "";
                breedFormResearchBar.ForeColor = Color.Black;
            }
        }
        #endregion

        /// <summary>
        /// Méthode permettant de rechercher soit une espèce soit une race suivant la TextBox utilisée
        /// </summary>
        /// <param name="sender">La barre de recherche utilisé par l'utilisateur</param>
        /// <param name="e">Changement du text d'une TextBox de recherche</param>
        private void ResearchByName(object sender, EventArgs e)
        {
            if (sender.Equals(specieFormResearchBar))
            {
                specieFormAllSpecieLB.Items.Clear();
                specieFormAllSpecieLB.Items.Add(" ");
                foreach (ESPECE s in AnimalController.ResearchSpeciesByName(specieFormResearchBar.Text))
                {
                    specieFormAllSpecieLB.Items.Add(s);
                }
            }
            if (sender.Equals(breedFormResearchBar))
            {
                breedFromBreedLB.Items.Clear();
                breedFromBreedLB.Items.Add(" ");
                foreach (RACE b in AnimalController.ResearchBreedByName(breedFormSelectedSpecie, breedFormResearchBar.Text))
                {
                    breedFromBreedLB.Items.Add(b);
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
