using System;
using System.Drawing;
using System.Windows.Forms;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.api.controllers;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceUpdateClient : AInterface
    {
        // Mise en forme du haut et du bas de la fenêtre
        private readonly Header header;
        private readonly Footer footer;
        // Les différents éléments de la fenêtre
        private TextBox nameBox, surnameBox, numberBox;
        private UIButton validate;
        private UIRoundButton backButton;

        private CLIENT selectedClient;

        public InterfaceUpdateClient(MainWindow window, SALARIE user, CLIENT client) : base(window, user)
        {
            header = new Header(window);
            footer = new Footer(window, base.user);
            selectedClient = client;
        }

        /// <summary>
        /// Méthode permettant de générer tous les objets de la page
        /// </summary>
        public override void load()
        {
            header.load("Mauxnimale - Page de gestion des clients");
            footer.load();
            // On appel toutes les méthodes qui génére les éléments
            GenerateButton();
            GenerateTextBox();
        }

        /// <summary>
        /// Méthode permettant de générer les TextBox de l'ihm
        /// </summary>
        private void GenerateTextBox()
        {
            nameBox = new TextBox();
            nameBox.Font = new Font("Poppins", window.Height * 1 / 100);
            nameBox.Text = selectedClient.NOMCLIENT;
            nameBox.ForeColor = Color.Black;
            nameBox.BackColor = Color.White;
            nameBox.Location = new Point(window.Width * 20 / 50, window.Height * 8 / 20);
            nameBox.Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100);
            nameBox.MaxLength = 128;
            nameBox.TabIndex = 0;
            window.Controls.Add(nameBox);

            surnameBox = new TextBox();
            surnameBox.Font = new Font("Poppins", window.Height * 1 / 100);
            surnameBox.Text = selectedClient.PRENOMCLIENT;
            surnameBox.ForeColor = Color.Black;
            surnameBox.BackColor = Color.White;
            surnameBox.Location = new Point(window.Width * 20 / 50, window.Height * 9 / 20);
            surnameBox.Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100);
            surnameBox.MaxLength = 128;
            surnameBox.TabIndex = 1;
            window.Controls.Add(surnameBox);

            numberBox = new TextBox();
            numberBox.Font = new Font("Poppins", window.Height * 1 / 100);
            numberBox.Text = selectedClient.TELCLIENT;
            numberBox.ForeColor = Color.Black;
            numberBox.BackColor = Color.White;
            numberBox.Location = new Point(window.Width * 20 / 50, window.Height * 10 / 20);
            numberBox.Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100);
            numberBox.MaxLength = 10;
            numberBox.TabIndex = 2;
            window.Controls.Add(numberBox);
            numberBox.KeyPress += new KeyPressEventHandler(KeyPress);
        }

        /// <summary>
        /// Methodes qui génére tous les bouttons
        /// </summary>
        private void GenerateButton()
        {
            validate = new UIButton(UIColor.ORANGE, "Valider", window.Width * 15 / 100);
            validate.Height = window.Height / 30;
            validate.Location = new Point(window.Width * 20 / 50, window.Height * 11 / 20);
            window.Controls.Add(validate);
            validate.Click += new EventHandler(ModifyInformation);

            backButton = new UIRoundButton(window.Width / 20, "<");
            backButton.Location = new Point(window.Width * 9 / 10, window.Height / 10);
            window.Controls.Add(backButton);
            backButton.Click += new EventHandler(BackPage);
        }
        /// <summary>
        /// Méthode répondant à l'évènement du clic sur le bouton valider
        /// </summary>
        /// <param name="sender">Le bouton</param>
        /// <param name="e">Le clic</param>
        private void BackPage(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceClient(window, user));
        }

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

        /// <summary>
        /// Méthode répondant à l'évènement du clic sur le boutton valider.
        /// Permet d'ajouter un client à la base de données si tous les champs sont remplis.
        /// </summary>
        /// <param name="sender">Boutton valider</param>
        /// <param name="e">Le clic</param>
        private void ModifyInformation(object sender, EventArgs e)
        {
            if (surnameBox.Text != selectedClient.PRENOMCLIENT || nameBox.Text != selectedClient.NOMCLIENT || numberBox.Text != selectedClient.TELCLIENT)
            {
                var result = MessageBox.Show("Etes-vous certain de vos modification ?",
                                "Validation des modifications",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (surnameBox.Text.Length != 0 && nameBox.Text.Length != 0 && numberBox.Text.Length != 0 && numberBox.Text.Length == 10)
                    {
                        ClientController.UpdateClient(selectedClient, nameBox.Text.ToUpper(), NormalizeSurname(), numberBox.Text);
                        MessageBox.Show("Les modifications ont été effectuées avec succès.",
                                    "Confirmation de modification",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    }
                    else if (nameBox.Text.Length == 0)
                    {
                        MessageBox.Show("Le nom ne peut pas être vide.",
                                          "Erreur nom",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Error);
                    }
                    else if (surnameBox.Text.Length == 0)
                    {
                        MessageBox.Show("Le prénom ne peut pas être vide.",
                                          "Erreur prénom",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Error);
                    }
                    else if (numberBox.Text.Length != 10)
                    {
                        MessageBox.Show("Le numéro de téléphone n'est pas valide.",
                                          "Erreur numéro de téléphone",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Error);
                    }
                }
            } else
            {
                MessageBox.Show("Aucune modification n'a été effectué",
                                "Pas de modification",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Méthode permettant de normaliser le prénom du nouveau client.
        /// C'est à dire première lettre en majuscule et le reste en minuscule.
        /// </summary>
        /// <returns>Le prénom normalisé</returns>
        private string NormalizeSurname()
        {
            char[] surnameLetter = surnameBox.Text.ToCharArray();
            string surnameWithCapital = "";
            string letter;
            bool firstLetter = true;
            foreach (char c in surnameLetter)
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
                surnameWithCapital += letter;
            }
            return surnameWithCapital;
        }
    }
}
