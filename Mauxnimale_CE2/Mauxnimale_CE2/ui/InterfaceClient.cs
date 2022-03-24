using System;
using System.Drawing;
using System.Windows.Forms;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.api.controllers;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceClient : AInterface
    {
        MainWindow window;

        Header header;
        Footer footer;

        ListBox listClient, listInfoClient, listAnimal, listInfoAnimal;
        TextBox researchBar;
        UIButton updateClient, deleteClient, newClient, 
                 byName, bySurname,
                 updateAnimal, deleteAnimal, newAnimal;

        UIRoundButton backButton;

        CLIENT selectedClient;
        ANIMAL selectedAnimal;

        public InterfaceClient(MainWindow window, SALARIE s)
        {
            this.window = window;
            user = s;
            header = new Header(window);
            footer = new Footer(window, user);
        }

        public override void load()
        {
            header.load("Mauxnimale - Page de gestion des clients");
            footer.load();

            GenerateLists();
            GenerateTextBox();
            GenerateButtons();

            AddListOfClient();
        }

        public override void updateSize()
        {
            if (window.WindowState != FormWindowState.Minimized)
            {
                window.Controls.Clear();
                this.load();
            }
        }

        public void GenerateLists()
        {
           listClient = new ListBox();
            listClient.Text = "";
            listClient.Font = new Font("Poppins", window.Height / 100);
            listClient.ForeColor = Color.Black;
            listClient.BackColor = Color.White;
            listClient.Location = new Point(window.Width * 20 / 1000, window.Height * 2/ 14); ;
            listClient.Size = new Size(window.Width * 40 / 100, window.Height * 50 / 100);
            window.Controls.Add(listClient);
            listClient.SelectedIndexChanged += new EventHandler(ClientSelection);

            listInfoClient = new ListBox();
            listInfoClient.Text = "";
            listInfoClient.Font = new Font("Poppins", window.Height * 1 / 100);
            listInfoClient.ForeColor = Color.Black;
            listInfoClient.BackColor = Color.White;
            listInfoClient.Location = new Point(window.Width * 20 / 1000, window.Height * 9 / 14);
            listInfoClient.Size = new Size(window.Width * 40 / 100, window.Height * 16 / 100);
            window.Controls.Add(listInfoClient);

            listAnimal = new ListBox();
            listAnimal.Text = "";
            listAnimal.Font = new Font("Poppins", window.Height * 1 / 100);
            listAnimal.ForeColor = Color.Black;
            listAnimal.BackColor = Color.White;
            listAnimal.Location = new Point(window.Width * 520 / 1000, window.Height * 2 / 14);
            listAnimal.Size = new Size(window.Width * 37 / 100, window.Height * 50 / 100);
            window.Controls.Add(listAnimal);
            listAnimal.SelectedIndexChanged += new EventHandler(AnimalSelection);

            listInfoAnimal = new ListBox();
            listInfoAnimal.Text = "";
            listInfoAnimal.Font = new Font("Poppins", window.Height * 1 / 100);
            listInfoAnimal.ForeColor = Color.Black;
            listInfoAnimal.BackColor = Color.White;
            listInfoAnimal.Location = new Point(window.Width * 520 / 1000, window.Height * 9 / 14);
            listInfoAnimal.Size = new Size(window.Width * 37 / 100, window.Height * 16 / 100);
            window.Controls.Add(listInfoAnimal);
        }

        public void GenerateTextBox()
        {
            researchBar = new TextBox();
            researchBar.Font = new Font("Poppins", window.Height * 1 / 100);
            researchBar.ForeColor = Color.Black;
            researchBar.BackColor = Color.White;
            researchBar.Location = new Point(window.Width * 20 / 1000, window.Height / 10);
            researchBar.Size = new Size(window.Width * 22 / 100, window.Height * 5 / 100);
            window.Controls.Add(researchBar);
            researchBar.TextChanged += new EventHandler(Research);
        }

        public void GenerateButtons()
        {
            newClient = new UIButton(UIColor.ORANGE, "Nouveau", window.Width / 8);
            newClient.Height = window.Height / 25;
            newClient.Location = new Point(window.Width * 20 / 1000, window.Height * 12 / 15);
            newClient.Click += new EventHandler(OpenNewClientInterface);
            window.Controls.Add(newClient);

            deleteClient = new UIButton(UIColor.ORANGE, "Supprimer", window.Width / 8);
            deleteClient.Height = window.Height / 25;
            deleteClient.Location = new Point(window.Width * 295 / 1000, window.Height * 12 / 15);
            deleteClient.Click += new EventHandler(DeleteClient);

            updateClient = new UIButton(UIColor.ORANGE, "Modifier", window.Width / 8);
            updateClient.Height = window.Height / 25;
            updateClient.Location = new Point(window.Width * 157 / 1000, window.Height * 12 / 15);
            updateClient.Click += new EventHandler(OpenUpdateClientInterface);

            newAnimal = new UIButton(UIColor.ORANGE, "Nouveau", window.Width / 10);
            newAnimal.Height = window.Height / 25;
            newAnimal.Location = new Point(window.Width * 520 / 1000, window.Height * 12 / 15);
            newAnimal.Click += new EventHandler(OpenNewAnimalInterface);
            window.Controls.Add(newAnimal);

            deleteAnimal = new UIButton(UIColor.ORANGE, "Supprimer", window.Width / 10);
            deleteAnimal.Height = window.Height / 25;
            deleteAnimal.Location = new Point(window.Width * 789 / 1000, window.Height * 12 / 15);
            deleteAnimal.Click += new EventHandler(DeleteAnimal);

            updateAnimal = new UIButton(UIColor.ORANGE, "Modifier", window.Width / 10);
            updateAnimal.Height = window.Height / 25;
            updateAnimal.Location = new Point(window.Width * 655 / 1000, window.Height * 12 / 15);
            updateAnimal.Click += new EventHandler(OpenUpdateAnimalInterface);

            byName = new UIButton(UIColor.ORANGE, "Par nom", window.Width / 12);
            byName.Font = new Font("Poppins", window.Height * 1 / 100);
            byName.Location = new Point(window.Width * 245 / 1000, window.Height / 11);
            window.Controls.Add(byName);
            byName.Click += new EventHandler(ChangeMode);

            bySurname = new UIButton(UIColor.LIGHTBLUE, "Par prénom", window.Width / 12);
            bySurname.Font = new Font("Poppins", window.Height * 1 / 100);
            bySurname.Location = new Point(window.Width * 335 / 1000, window.Height / 11);
            window.Controls.Add(bySurname);
            bySurname.Click += new EventHandler(ChangeMode);

            backButton = new UIRoundButton(window.Width / 20, "<");
            backButton.Location = new Point(window.Width * 9 / 10, window.Height / 10);
            window.Controls.Add(backButton);
            backButton.Click += new EventHandler(ReturnHomePage);
        }

        private void OpenNewAnimalInterface(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceNewsRelatedToAnimals(window, user));
        }

        private void ReturnHomePage(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceHome(window, user));
        }

        #region Delete something
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
        private void DeleteAnimal(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Open other interface
        private void OpenUpdateClientInterface(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceUpdateClient(window, user, selectedClient));
        }

        private void OpenNewClientInterface(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceNewClient(window, user));
        }

        private void OpenUpdateAnimalInterface(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion

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
            }
        }

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
        private void ResultResearhBySurname()
        {
            listClient.Items.Clear();
            listClient.Items.Add(" ");
            foreach (CLIENT client in ClientController.ResearhBySurname(researchBar.Text.ToString()))
            {
                listClient.Items.Add(client);
            }
        }

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
