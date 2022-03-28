using System;
using System.Drawing;
using System.Windows.Forms;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api.controllers;
using Mauxnimale_CE2.api.controllers.utils;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2.ui
{
    class InterfaceFirstConnection : AInterface
    {
        Header header;
        Footer footer;

        TextBox lastName, firstName, pswd, confirmPswd, email, phoneNumber, address;
        UIButton inscriptionButton;

        #region Input events handling
        private void lastNameEnter(object sender, EventArgs e)
        {
            if (lastName.Text == "Nom")
            {
                lastName.Text = "";
                lastName.ForeColor = Color.Black;
            }
        }

        private void lastNameLeave(object sender, EventArgs e)
        {
            if (lastName.Text.Length == 0)
            {
                lastName.Text = "Nom";
                lastName.ForeColor = Color.Gray;
            }
        }
        private void firstNameEnter(object sender, EventArgs e)
        {
            if (firstName.Text == "Prénom")
            {
                firstName.Text = "";
                firstName.ForeColor = Color.Black;
            }
        }

        private void firstNameLeave(object sender, EventArgs e)
        {
            if (firstName.Text.Length == 0)
            {
                firstName.Text = "Prénom";
                firstName.ForeColor = Color.Gray;
            }
        }
        private void emailEnter(object sender, EventArgs e)
        {
            if (email.Text == "Email")
            {
                email.Text = "";
                email.ForeColor = Color.Black;
            }
        }

        private void emailLeave(object sender, EventArgs e)
        {
            if (email.Text.Length == 0)
            {
                email.Text = "Email";
                email.ForeColor = Color.Gray;
            }
        }
        private void phoneNumberEnter(object sender, EventArgs e)
        {
            if (phoneNumber.Text == "N° de téléphone")
            {
                phoneNumber.Text = "";
                phoneNumber.ForeColor = Color.Black;
            }
        }

        private void phoneNumberLeave(object sender, EventArgs e)
        {
            if (phoneNumber.Text.Length == 0)
            {
                phoneNumber.Text = "N° de téléphone";
                phoneNumber.ForeColor = Color.Gray;
            }
        }
        private void addressEnter(object sender, EventArgs e)
        {
            if (address.Text == "Adresse")
            {
                address.Text = "";
                address.ForeColor = Color.Black;
            }
        }

        private void addressLeave(object sender, EventArgs e)
        {
            if (address.Text.Length == 0)
            {
                address.Text = "Adresse";
                address.ForeColor = Color.Gray;
            }
        }
        private void pswdEnter(object sender, EventArgs e)
        {
            if (pswd.Text == "Mot de passe")
            {
                pswd.Text = "";
                pswd.ForeColor = Color.Black;
                pswd.PasswordChar = '•';
            }
        }

        private void pswdLeave(object sender, EventArgs e)
        {
            if (pswd.Text.Length == 0)
            {
                pswd.Text = "Mot de passe";
                pswd.ForeColor = Color.Gray;
                pswd.PasswordChar = (char)0;
            }
        }
        private void confirmPswdEnter(object sender, EventArgs e)
        {
            if (confirmPswd.Text == "Confirmation du mot de passe")
            {
                confirmPswd.Text = "";
                confirmPswd.ForeColor = Color.Black;
                confirmPswd.PasswordChar = '•';
            }
        }

        private void confirmPswdLeave(object sender, EventArgs e)
        {
            if (confirmPswd.Text.Length == 0)
            {
                confirmPswd.Text = "Confirmation du mot de passe";
                confirmPswd.ForeColor = Color.Gray;
                confirmPswd.PasswordChar = (char)0;
            }
        }
        #endregion 

        public InterfaceFirstConnection(MainWindow window, SALARIE user) : base(window, user)
        {
            header = new Header(window);
            footer = new Footer(window, user);
        }


        public override void load()
        {
            header.load("Inscription");
            footer.load();
            generateTextBoxes();
            generateButton();
        }

        public void generateButton()
        {
            inscriptionButton = new UIButton(UIColor.ORANGE, "Inscription", 200);
            inscriptionButton.Location = new Point(window.Width / 2 - inscriptionButton.Width / 2, window.Height * 72 / 100);
            inscriptionButton.Click += new EventHandler(onInscriptionButtonClick);
            window.Controls.Add(inscriptionButton);
        }

        public void generateTextBoxes()
        {
            lastName = new TextBox();
            lastName.LostFocus += new EventHandler(lastNameLeave);
            lastName.GotFocus += new EventHandler(lastNameEnter);
            lastName.Size = new Size(window.Width / 4, lastName.Height);
            lastName.Location = new Point(window.Width / 2 - lastName.Width - 10, window.Height * 12 / 100);
            setBox(lastName, "Nom");

            firstName = new TextBox();
            firstName.LostFocus += new EventHandler(firstNameLeave);
            firstName.GotFocus += new EventHandler(firstNameEnter);
            firstName.Size = new Size(window.Width / 4, lastName.Height);
            firstName.Location = new Point(window.Width / 2 + 10, window.Height * 12 / 100);
            setBox(firstName, "Prénom");

            email = new TextBox();
            email.LostFocus += new EventHandler(emailLeave);
            email.GotFocus += new EventHandler(emailEnter);
            email.Size = new Size(window.Width / 2 + 20, window.Height * 5 / 100);
            email.Location = new Point(window.Width / 4 - 10, window.Height * 22 / 100);
            setBox(email, "Email");

            phoneNumber = new TextBox();
            phoneNumber.LostFocus += new EventHandler(phoneNumberLeave);
            phoneNumber.GotFocus += new EventHandler(phoneNumberEnter);
            phoneNumber.Size = new Size(window.Width / 2 + 20, window.Height * 5 / 100);
            phoneNumber.Location = new Point(window.Width / 4 - 10, window.Height * 32 / 100);
            setBox(phoneNumber, "N° de téléphone");

            address = new TextBox();
            address.LostFocus += new EventHandler(addressLeave);
            address.GotFocus += new EventHandler(addressEnter);
            address.Size = new Size(window.Width / 2 + 20, window.Height * 5 / 100);
            address.Location = new Point(window.Width / 4 - 10, window.Height * 42 / 100);
            setBox(address, "Adresse");

            pswd = new TextBox();
            pswd.LostFocus += new EventHandler(pswdLeave);
            pswd.GotFocus += new EventHandler(pswdEnter);
            pswd.Size = new Size(window.Width / 2 + 20, window.Height * 5 / 100);
            pswd.Location = new Point(window.Width / 4 - 10, window.Height * 52 / 100);
            pswd.PasswordChar = (char)0;
            setBox(pswd, "Mot de passe");

            confirmPswd = new TextBox();
            confirmPswd.LostFocus += new EventHandler(confirmPswdLeave);
            confirmPswd.GotFocus += new EventHandler(confirmPswdEnter);
            confirmPswd.Size = new Size(window.Width / 2 + 20, window.Height * 5 / 100);
            confirmPswd.Location = new Point(window.Width / 4 - 10, window.Height * 62 / 100);
            confirmPswd.PasswordChar = (char)0;
            setBox(confirmPswd, "Confirmation du mot de passe");
        }

        public void setBox(TextBox textBox, string text)
        {
            textBox.Font = new System.Drawing.Font("Poppins", window.Height * 3/ 100);
            textBox.ForeColor = Color.Gray;
            textBox.Text = text;
            window.Controls.Add(textBox);
        }
        public void onInscriptionButtonClick(object sender, EventArgs e)
        {
            if (validEntry())
            {
                if (UserController.updateInfos(user, firstName.Text, lastName.Text, null, pswd.Text, email.Text, phoneNumber.Text))
                {
                    UserController.setFirstConnectionDone(user);
                    window.Controls.Clear();
                    window.switchInterface(new InterfaceHome(window, user));
                }
            }
        }

        public bool validEntry()
        {
            if(lastName.Text == "Nom" || lastName.Text.Length == 0 ||
               firstName.Text == "Prénom" || firstName.Text.Length == 0 ||
               email.Text == "Email" || email.Text.Length == 0 ||
               phoneNumber.Text == "N° de téléphone" || phoneNumber.Text.Length == 0 ||
               address.Text == "Adresse" || address.Text.Length == 0 ||
               pswd.Text == "Mot de passe" || pswd.Text.Length == 0 ||
               confirmPswd.Text == "Confirmation du mot de passe" || confirmPswd.Text.Length == 0)
            {
                string errorMessage = "Veuillez remplir tous les champs.";
                MessageBox.Show(window, errorMessage, "Entrées non valides", MessageBoxButtons.OK);
                return false;
            }
            if (!InputVerification.noSpecialCharacters(firstName.Text) || !InputVerification.noNumber(firstName.Text))
            {
                string errorMessage = "Veuillez renseignez un prénom valide.\nLes caractères spéciaux et les chiffres ne sont pas autorisés.";
                MessageBox.Show(window, errorMessage, "Entrées non valides", MessageBoxButtons.OK);
                return false;
            }
            if (!InputVerification.noSpecialCharacters(lastName.Text) || !InputVerification.noNumber(lastName.Text))
            {
                string errorMessage = "Veuillez renseignez un nom valide.\nLes caractères spéciaux et les chiffres ne sont pas autorisés.";
                MessageBox.Show(window, errorMessage, "Entrées non valides", MessageBoxButtons.OK);
                return false;
            }
            if (!InputVerification.isEmail(email.Text))
            {
                string errorMessage = "Veuillez renseignez une adresse email valide.";
                MessageBox.Show(window, errorMessage, "Entrées non valides", MessageBoxButtons.OK);
                return false;
            }
            if (!InputVerification.isPhoneNumber(phoneNumber.Text))
            {
                string errorMessage = "Veuillez renseignez un numéro de téléphone valide.";
                MessageBox.Show(window, errorMessage, "Entrées non valides", MessageBoxButtons.OK);
                return false;
            }
            if (!InputVerification.noSpecialCharacters(address.Text))
            {
                string errorMessage = "Veuillez renseignez une adresse valide.\nLes caractères spéciaux ne sont pas autorisés.";
                MessageBox.Show(window, errorMessage, "Entrées non valides", MessageBoxButtons.OK);
                return false;
            }
            if (pswd.Text.Length < 6 || InputVerification.noSpecialCharacters(pswd.Text) || InputVerification.noNumber(pswd.Text))
            {
                string errorMessage = "Le mot de passe doit comprendre au moins 6 caractères, dont au moins 1 caractère spécial et un chiffre.";
                MessageBox.Show(window, errorMessage, "Entrées non valides", MessageBoxButtons.OK);
                return false;
            }
            if (pswd.Text != confirmPswd.Text)
            {
                string errorMessage = "Les mots de passe renseignés ne sont pas identiques.";
                MessageBox.Show(window, errorMessage, "Entrées non valides", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }
    }
}
