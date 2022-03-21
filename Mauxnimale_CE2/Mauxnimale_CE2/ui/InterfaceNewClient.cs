using System;
using System.Drawing;
using System.Windows.Forms;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.api.controllers;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceNewClient : AInterface
    {
        // Fenêtre principale
        MainWindow window;

        // Mise en forme du haut et du bas de la fenêtre
        readonly Header header;
        readonly Footer footer;
        // Les différents éléments de la fenêtre
        TextBox nameBox, surnameBox, numberBox;
        UIButton validate, backButton;

        public InterfaceNewClient(MainWindow window, SALARIE s)
        {
            this.window = window;
            header = new Header(window);
            footer = new Footer(window);
            user = s;
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
            nameBox.Text = "Nom";
            nameBox.ForeColor = Color.Gray;
            nameBox.BackColor = Color.White;
            nameBox.Location = new Point(window.Width * 20 / 50, window.Height * 8 / 20);
            nameBox.Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100);
            nameBox.MaxLength = 128;
            nameBox.TabIndex = 0;
            window.Controls.Add(nameBox);
            nameBox.GotFocus += new EventHandler(GetFocus);
            nameBox.LostFocus += new EventHandler(LostFocus);

            surnameBox = new TextBox();
            surnameBox.Font = new Font("Poppins", window.Height * 1 / 100);
            surnameBox.Text = "Prénom";
            surnameBox.ForeColor = Color.Gray;
            surnameBox.BackColor = Color.White;
            surnameBox.Location = new Point(window.Width * 20 / 50, window.Height * 9 / 20);
            surnameBox.Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100);
            surnameBox.MaxLength = 128;
            surnameBox.TabIndex = 1;
            window.Controls.Add(surnameBox);
            surnameBox.GotFocus += new EventHandler(GetFocus);
            surnameBox.LostFocus += new EventHandler(LostFocus);

            numberBox = new TextBox();
            numberBox.Font = new Font("Poppins", window.Height * 1 / 100);
            numberBox.Text = "Téléphone";
            numberBox.ForeColor = Color.Gray;
            numberBox.BackColor = Color.White;
            numberBox.Location = new Point(window.Width * 20 / 50, window.Height * 10 / 20);
            numberBox.Size = new Size(window.Width * 15 / 100, window.Height * 5 / 100);
            numberBox.MaxLength = 10;
            numberBox.TabIndex = 2;
            window.Controls.Add(numberBox);
            numberBox.GotFocus += new EventHandler(GetFocus);
            numberBox.LostFocus += new EventHandler(LostFocus);
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
            validate.Click += new EventHandler(Validate);

            backButton = new UIButton(UIColor.ORANGE, "Retour", window.Width * 15 / 100);
            backButton.Height = window.Height / 30;
            backButton.Location = new Point(window.Width * 20 / 50, window.Height * 12 / 20);
            window.Controls.Add(backButton);
            backButton.Click += new EventHandler(BackPage);
        }

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
        private void Validate(object sender, EventArgs e)
        {
            if (surnameBox.Text.Length != 0 && nameBox.Text.Length != 0 && numberBox.Text.Length != 0 && numberBox.Text.Length == 10)
            {
                ClientController.AddClient(nameBox.Text.ToUpper(), NormalizeSurname(), numberBox.Text);
                MessageBox.Show("Le client " + nameBox.Text + " " + surnameBox.Text + " à bien été ajouté à la base avec le numéro de téléphone " + numberBox.Text,
                    "Validation d'ajout",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                surnameBox.Text = "";
                numberBox.Text = "";
                nameBox.Text = "";
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

        /// <summary>
        /// Méthode permettant de normaliser le prénom du nouveau client.
        /// C'est à dire première lettre en majuscule et le reste en minuscule.
        /// </summary>
        /// <returns>Le prénom normalisé</returns>
        private string NormalizeSurname()
        {
            char[] surnameLetter = surnameBox.Text.ToCharArray();
            string surnameWithCapital = "";
            string letter = "";
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

        /// <summary>
        /// Méthode répondant à l'évènement de la perte de focus des TextBox.
        /// Si elle est vide on replace le text initial en gris.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LostFocus(object sender, EventArgs e)
        {

            if (sender.Equals(numberBox) && numberBox.Text.Length == 0)
            {
                numberBox.Text = "Téléphone";
                numberBox.ForeColor = Color.Gray;
            }
            if (sender.Equals(surnameBox) && surnameBox.Text.Length == 0)
            {
                surnameBox.Text = "Prénom";
                surnameBox.ForeColor = Color.Gray;
            }
            if (sender.Equals(nameBox) && nameBox.Text.Length == 0)
            {
                nameBox.Text = "Nom";
                nameBox.ForeColor = Color.Gray;
            }
        }

        /// <summary>
        /// Méthode répondant à l'évènement du gain de focus des TextBox.
        /// On enlève le text initial et on place la couleur de la police d'écriture à noire.
        /// </summary>
        /// <param name="sender">TextBox qui reçoit le focus</param>
        /// <param name="e">Le focus</param>
        private void GetFocus(object sender, EventArgs e)
        {
            if (sender.Equals(numberBox) && numberBox.Text == "Téléphone")
            {
                numberBox.Text = "";
                numberBox.ForeColor = Color.Black;
            }
            if (sender.Equals(surnameBox) && surnameBox.Text == "Prénom")
            {
                surnameBox.Text = "";
                surnameBox.ForeColor = Color.Black;
            }
            if (sender.Equals(nameBox) && nameBox.Text == "Nom")
            {
                nameBox.Text = "";
                nameBox.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Méthode permettant de redimensionner la fenêtre et ses éléments.
        /// </summary>
        public override void updateSize()
        {
            window.Controls.Clear();
            this.load();
        }


    }
}
