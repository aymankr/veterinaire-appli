using System;
using System.Drawing;
using System.Windows.Forms;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.api.controllers;

namespace Mauxnimale_CE2.ui.clients
{
    internal class InterfaceClient : AInterface
    {
        private readonly Header header;
        private readonly Footer footer;

        private ListBox listClient, listInfoClient, listAnimal, listInfoAnimal;
        private TextBox researchBar;
        private UIButton updateClient, deleteClient, newClient, 
                 byName, bySurname,
                 updateAnimal, deleteAnimal, newAnimal;

        private UIRoundButton backButton;

        private CLIENT selectedClient;
        private ANIMAL selectedAnimal;

        /// <summary>
        /// Constructeur de l'interface
        /// </summary>
        /// <param name="window"></param>
        /// <param name="user"></param>
        public InterfaceClient(MainWindow window, SALARIE user) : base(window, user)
        {
            header = new Header(window);
            footer = new Footer(window, base.user);
        }

        /// <summary>
        /// Permet de générer les éléments de la fenêtre
        /// </summary>
        public override void load()
        {
            // On génere les éléments de décoration de de la fenêtre
            header.load("Mauxnimale - Page de gestion des clients");
            footer.load();
            // On génere les éléments avec lequels l'utilisateur peut interagir 
            GenerateLists();
            GenerateTextBox();
            GenerateButtons();
            // On remplis la liste des clients avec des données de la base
            AddListOfClient();
        }

        /// <summary>
        /// Permet de générer toutes les ComboBox de l'interface.s
        /// </summary>
        public void GenerateLists()
        {
            listClient = new ListBox
            {
                Text = "",
                Font = new Font("Poppins", window.Height / 100),
                ForeColor = Color.Black,
                BackColor = Color.White,
                Location = new Point(window.Width * 20 / 1000, window.Height * 2 / 14)
            };
            ;
            listClient.Size = new Size(window.Width * 40 / 100, window.Height * 50 / 100);
            window.Controls.Add(listClient);
            listClient.SelectedIndexChanged += new EventHandler(ClientSelection);

            listInfoClient = new ListBox
            {
                Text = "",
                Font = new Font("Poppins", window.Height * 1 / 100),
                ForeColor = Color.Black,
                BackColor = Color.White,
                Location = new Point(window.Width * 20 / 1000, window.Height * 9 / 14),
                Size = new Size(window.Width * 40 / 100, window.Height * 16 / 100)
            };
            window.Controls.Add(listInfoClient);

            listAnimal = new ListBox
            {
                Text = "",
                Font = new Font("Poppins", window.Height * 1 / 100),
                ForeColor = Color.Black,
                BackColor = Color.White,
                Location = new Point(window.Width * 520 / 1000, window.Height * 2 / 14),
                Size = new Size(window.Width * 37 / 100, window.Height * 50 / 100)
            };
            window.Controls.Add(listAnimal);
            listAnimal.SelectedIndexChanged += new EventHandler(AnimalSelection);

            listInfoAnimal = new ListBox
            {
                Text = "",
                Font = new Font("Poppins", window.Height * 1 / 100),
                ForeColor = Color.Black,
                BackColor = Color.White,
                Location = new Point(window.Width * 520 / 1000, window.Height * 9 / 14),
                Size = new Size(window.Width * 37 / 100, window.Height * 16 / 100)
            };
            window.Controls.Add(listInfoAnimal);
        }

        /// <summary>
        /// Permet de générer toutes les TextBox de l'interface
        /// </summary>
        public void GenerateTextBox()
        {
            researchBar = new TextBox
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                ForeColor = Color.Black,
                BackColor = Color.White,
                Location = new Point(window.Width * 20 / 1000, window.Height / 10),
                Size = new Size(window.Width * 22 / 100, window.Height * 5 / 100)
            };
            window.Controls.Add(researchBar);
            researchBar.TextChanged += new EventHandler(Research);
        }

        /// <summary>
        /// Permet de générer tous les boutons de l'interface.
        /// </summary>
        public void GenerateButtons()
        {
            newClient = new UIButton(UIColor.ORANGE, "Nouveau", window.Width / 8)
            {
                Height = window.Height / 25,
                Location = new Point(window.Width * 20 / 1000, window.Height * 12 / 15)
            };
            newClient.Click += new EventHandler(OpenNewClientInterface);
            window.Controls.Add(newClient);

            deleteClient = new UIButton(UIColor.ORANGE, "Supprimer", window.Width / 8)
            {
                Height = window.Height / 25,
                Location = new Point(window.Width * 295 / 1000, window.Height * 12 / 15)
            };
            deleteClient.Click += new EventHandler(DeleteClient);

            updateClient = new UIButton(UIColor.ORANGE, "Modifier", window.Width / 8)
            {
                Height = window.Height / 25,
                Location = new Point(window.Width * 157 / 1000, window.Height * 12 / 15)
            };
            updateClient.Click += new EventHandler(OpenUpdateClientInterface);

            newAnimal = new UIButton(UIColor.ORANGE, "Nouveau", window.Width / 10)
            {
                Height = window.Height / 25,
                Location = new Point(window.Width * 520 / 1000, window.Height * 12 / 15)
            };
            newAnimal.Click += new EventHandler(OpenNewAnimalInterface);
            window.Controls.Add(newAnimal);

            deleteAnimal = new UIButton(UIColor.ORANGE, "Supprimer", window.Width / 10)
            {
                Height = window.Height / 25,
                Location = new Point(window.Width * 789 / 1000, window.Height * 12 / 15)
            };
            deleteAnimal.Click += new EventHandler(DeleteAnimal);

            updateAnimal = new UIButton(UIColor.ORANGE, "Modifier", window.Width / 10)
            {
                Height = window.Height / 25,
                Location = new Point(window.Width * 655 / 1000, window.Height * 12 / 15)
            };
            updateAnimal.Click += new EventHandler(OpenUpdateAnimalInterface);

            byName = new UIButton(UIColor.ORANGE, "Par nom", window.Width / 12)
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Location = new Point(window.Width * 245 / 1000, window.Height / 11)
            };
            window.Controls.Add(byName);
            byName.Click += new EventHandler(ChangeMode);

            bySurname = new UIButton(UIColor.LIGHTBLUE, "Par prénom", window.Width / 12)
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Location = new Point(window.Width * 335 / 1000, window.Height / 11)
            };
            window.Controls.Add(bySurname);
            bySurname.Click += new EventHandler(ChangeMode);

            backButton = new UIRoundButton(window.Width / 20, "<")
            {
                Location = new Point(window.Width * 9 / 10, window.Height / 10)
            };
            window.Controls.Add(backButton);
            backButton.Click += new EventHandler(ReturnHomePage);
        }

        #region Delete something
        /// <summary>
        /// Permet de supprimer un client.
        /// </summary>
        /// <param name="sender">Bouton supprimer client</param>
        /// <param name="e">clics</param>
        private void DeleteClient(object sender, EventArgs e)
        {
            string message = "Êtes vous sur de vouloirs supprimer le client suivant : \n" + selectedClient + "\nSa suppression sera IRRÉVERSIBLE";
            var result = MessageBox.Show(message, "Demande de confirmation de Suppression", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if(result == DialogResult.OK)
            {
                ClientController.DeleteClient(selectedClient);
                selectedClient = null;
                AddListOfClient();
                ClientInfo();
            }
        }

        /// <summary>
        /// Permet de supprimer un animal.
        /// </summary>
        /// <param name="sender">Bouton supprimer animal</param>
        /// <param name="e">clic</param>
        private void DeleteAnimal(object sender, EventArgs e)
        {
            AnimalController.RemoveAnimal(selectedAnimal);
            selectedAnimal = null;
            AddListOfAnimal();
            AnimalInfo();
        }
        #endregion

        #region Open other interface

        /// <summary>
        /// Permet de retourner sur l'interface principale.
        /// </summary>
        /// <param name="sender">Bouton retour</param>
        /// <param name="e">clic</param>
        private void ReturnHomePage(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceHome(window, user));
        }

        /// <summary>
        /// Permet de changer d'interface vers l'interface nouvel animal.
        /// </summary>
        /// <param name="sender">Bouton nouvel animal</param>
        /// <param name="e">clic</param>
        private void OpenNewAnimalInterface(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceNewsRelatedToAnimals(window, user, this));
        }

        /// <summary>
        /// Permet de changer d'interface vers l'interface de modification client.s
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenUpdateClientInterface(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceUpdateClient(window, user, selectedClient));
        }

        /// <summary>
        /// Permet de changer d'interface vers l'interface de modification d'un animal
        /// </summary>
        /// <param name="sender">Nouveau client</param>
        /// <param name="e">clic</param>
        private void OpenNewClientInterface(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceNewClient(window, user, this));
        }

        /// <summary>
        /// Permet de changer d'interface vers l'interface de modification d'un animal
        /// </summary>
        /// <param name="sender">Bouton modifier animal</param>
        /// <param name="e">clic</param>
        private void OpenUpdateAnimalInterface(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceUpdateRelatedToAnimals(window, user, selectedAnimal, this));
        }
        #endregion

        /// <summary>
        /// Permet de changer le mode de recherche de client.
        /// Soit on recherche par le nom soit par le prénom.
        /// </summary>
        /// <param name="sender">Le bouton par nom ou le bouton par prénom</param>
        /// <param name="e">Le clic</param>
        private void ChangeMode(object sender, EventArgs e)
        {
            if (sender.Equals(byName))
            {
                if (byName.BackColor != UIColor.ORANGE)
                {
                    byName.BackColor = UIColor.ORANGE;
                    bySurname.BackColor = UIColor.LIGHTBLUE;
                } 
            }
            else if (sender.Equals(bySurname))
            {
                if(bySurname.BackColor != UIColor.ORANGE)
                {
                    bySurname.BackColor = UIColor.ORANGE;
                    byName.BackColor = UIColor.LIGHTBLUE;
                }
            }
            window.Refresh();
        }

        #region Add in principal list
        private void AddListOfClient()
        {
            listClient.Items.Clear();
            listClient.Items.Add(" ");
            foreach (CLIENT client in ClientController.AllClient())
            {
                listClient.Items.Add(client);
            }
        }

        private void AddListOfAnimal()
        {
            listAnimal.Items.Clear();
            listAnimal.Items.Add(" ");
            if (selectedClient != null)
            {
                foreach (ANIMAL animal in ClientController.ListOfAnimal(selectedClient))
                {
                    listAnimal.Items.Add(animal);
                }
            }
        }
        #endregion

        #region Selected items
        /// <summary>
        /// Permet d'ajouter ou d'enlever les boutons de modification et de suppression d'un client.
        /// </summary>
        /// <param name="sender">La liste contenant les clients</param>
        /// <param name="e">Sélection d'un client</param>
        public void ClientSelection(object sender, EventArgs e)
        {
            if (listClient.SelectedItem != null && listClient.SelectedItem != " ")
            {
                window.Controls.Add(deleteClient);
                window.Controls.Add(updateClient);
                selectedClient = (CLIENT)listClient.SelectedItem;
                selectedAnimal = null;
                AddListOfAnimal();
            }
            else
            {
                if (window.Controls.Contains(deleteClient) && window.Controls.Contains(updateClient))
                {
                    window.Controls.Remove(deleteClient);
                    window.Controls.Remove(updateClient);
                    selectedClient = null;
                    listAnimal.SelectedItem = " ";
                }
            }
            ClientInfo();
            AnimalInfo();
            AddListOfAnimal();
            window.Refresh();
        }

        /// <summary>
        /// Permet de d'afficher ou d'enlever les boutons liés à la modification et à la suppression d'un animal
        /// </summary>
        /// <param name="sender">La liste contenant les animaux</param>
        /// <param name="e">Sélection d'un animal</param>
        private void AnimalSelection(object sender, EventArgs e)
        {
            if (listAnimal.SelectedItem != null && listAnimal.SelectedItem != " ")
            {
                window.Controls.Add(deleteAnimal);
                window.Controls.Add(updateAnimal);
                selectedAnimal = (ANIMAL)listAnimal.SelectedItem;
            }
            else
            {
                if (window.Controls.Contains(deleteAnimal) && window.Controls.Contains(updateAnimal))
                {
                    window.Controls.Remove(deleteAnimal);
                    window.Controls.Remove(updateAnimal);
                    selectedAnimal = null;
                }
            }
            AnimalInfo();
            window.Refresh();
        }
        #endregion

        /// <summary>
        /// Permet d'afficher les informations liées au client sélectionné. 
        /// </summary>
        public void ClientInfo()
        {
            listInfoClient.Items.Clear();
            if (selectedClient != null)
            {
                listInfoClient.Items.Add("Nom : " + selectedClient.NOMCLIENT);
                listInfoClient.Items.Add("Prénom : " + selectedClient.PRENOMCLIENT);
                listInfoClient.Items.Add("Téléphone client :  " + selectedClient.TELCLIENT);
            }
        }

        /// <summary>
        /// Permet d'afficher les informations liées à l'animal sélectionné.
        /// </summary>
        public void AnimalInfo()
        {
            listInfoAnimal.Items.Clear();
            if(selectedAnimal != null)
            {
                listInfoAnimal.Items.Add("Nom : " + selectedAnimal.NOM);
                listInfoAnimal.Items.Add("Race : " + selectedAnimal.RACE);
                listInfoAnimal.Items.Add("Espèce : " + selectedAnimal.RACE.ESPECE);
                listInfoAnimal.Items.Add("Taille : " + selectedAnimal.TAILLE);
                listInfoAnimal.Items.Add("Poids : " + selectedAnimal.POIDS);
                string genre = (selectedAnimal.ESTMALE == true) ? "Male" : "Femelle";
                listInfoAnimal.Items.Add("Genre : " + genre);
            }
        }

        /// <summary>
        /// Permet de rechercher un client des que l'utilisateur utilise la barre de recherche.
        /// Permet selon le mode choisi, soit de rechercher par nom soit par prénom
        /// </summary>
        /// <param name="sender">La barre de recherche ce client</param>
        /// <param name="e">Changement du text</param>
        public void Research(object sender, EventArgs e)
        {
            if (researchBar.Text.Length != 0 && byName.BackColor == UIColor.ORANGE)
            {
                ResultResearhByName();
            } else if (researchBar.Text.Length != 0 && bySurname.BackColor == UIColor.ORANGE) {
                ResultResearhBySurname();
            } else 
            {
                AddListOfClient();
            }
                    
        }

        /// <summary>
        /// Permet de recherche un client par son prénom
        /// </summary>
        private void ResultResearhBySurname()
        {
            listClient.Items.Clear();
            listClient.Items.Add(" ");
            foreach (CLIENT client in ClientController.ResearhBySurname(researchBar.Text.ToString()))
            {
                listClient.Items.Add(client);
            }
        }

        /// <summary>
        /// Permet de rechercher un client par son prénom
        /// </summary>
        private void ResultResearhByName()
        {
            listClient.Items.Clear();
            listClient.Items.Add(" ");
            foreach (CLIENT client in ClientController.ResearhByName(researchBar.Text.ToString()))
            {
                listClient.Items.Add(client);
            }
        }
    }
}
