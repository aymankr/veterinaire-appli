using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2
{
    class InterfaceInscription : AInterface
    {
        MainWindow form;

        Header header;
        Footer footer;

        TextBox name, firstName, pswd, confirmPswd;
        Button inscription;
        Label alert;
        //Lister ici les différents éléments qui seront utilisés dans l'interface

        #region eventHandler
        private void nameEnter(object sender, EventArgs e)
        {
            if (name.Text == "Votre prénom")
            {
                name.Text = "";
                name.ForeColor = Color.Black;
            }
        }

        private void nameLeave(object sender, EventArgs e)
        {
            if (name.Text.Length == 0)
            {
                name.Text = "Votre prénom";
                name.ForeColor = Color.Gray;
            }
        }
        private void firstNameEnter(object sender, EventArgs e)
        {
            if (firstName.Text == "Votre nom")
            {
                firstName.Text = "";
                firstName.ForeColor = Color.Black;
            }
        }

        private void firstNameLeave(object sender, EventArgs e)
        {
            if (firstName.Text.Length == 0)
            {
                firstName.Text = "Votre nom";
                firstName.ForeColor = Color.Gray;
            }
        }
        private void pswdEnter(object sender, EventArgs e)
        {
            if (pswd.Text == "Votre mot de passe")
            {
                pswd.Text = "";
                pswd.ForeColor = Color.Black;
            }
        }

        private void pswdLeave(object sender, EventArgs e)
        {
            if (pswd.Text.Length == 0)
            {
                pswd.Text = "Votre mot de passe";
                pswd.ForeColor = Color.Gray;
            }
        }
        private void confirmPswdEnter(object sender, EventArgs e)
        {
            if (confirmPswd.Text == "Confirmation du mot de passe")
            {
                confirmPswd.Text = "";
                confirmPswd.ForeColor = Color.Black;
            }
        }

        private void confirmPswdLeave(object sender, EventArgs e)
        {
            if (confirmPswd.Text.Length == 0)
            {
                confirmPswd.Text = "Confirmation du mot de passe";
                confirmPswd.ForeColor = Color.Gray;
            }
        }
        #endregion 

        public InterfaceInscription(MainWindow forme, SALARIE s)
        {
            this.form = forme;
            header = new Header(forme);
            footer = new Footer(forme);
            salarie = s;
        }


        public override void load()
        {
            header.load("Inscription");
            footer.load();
            generateBox();
            generateLabel();
            generateButton();
        }

        public void generateLabel()
        {
            alert = new Label();
            alert.Text = "";
            alert.Font = new System.Drawing.Font("Poppins", form.Height * 3 / 100);
            alert.ForeColor = Color.Red;
            alert.Size = new System.Drawing.Size(form.Width / 2, form.Height*6/100);
            alert.Location = new Point(form.Width / 4, form.Height*62/100);
            form.Controls.Add(alert);
        }

        public void generateButton()
        {
            inscription = new Button();
            inscription.Text = "Inscription";
            inscription.Click += new EventHandler(inscription_click);
            inscription.Location = new Point(form.Width / (5/2), form.Height * 72 / 100);
            form.Controls.Add(inscription);
        }

        public void generateBox()
        {
            name = new TextBox();
            name.LostFocus += new EventHandler(nameLeave);
            name.GotFocus += new EventHandler(nameEnter);
            name.Location = new Point(form.Width / 4, form.Height * 22 / 100);
            setBox(name, "Votre prénom");

            firstName = new TextBox();
            firstName.LostFocus += new EventHandler(firstNameLeave);
            firstName.GotFocus += new EventHandler(firstNameEnter);
            firstName.Location = new Point(form.Width / 4, form.Height * 32 / 100);
            setBox(firstName, "Votre nom");

            pswd = new TextBox();
            pswd.LostFocus += new EventHandler(pswdLeave);
            pswd.GotFocus += new EventHandler(pswdEnter);
            pswd.Location = new Point(form.Width / 4, form.Height * 42 / 100);
            setBox(pswd, "Votre mot de passe");

            confirmPswd = new TextBox();
            confirmPswd.LostFocus += new EventHandler(confirmPswdLeave);
            confirmPswd.GotFocus += new EventHandler(confirmPswdEnter);
            confirmPswd.Location = new Point(form.Width / 4, form.Height * 52 / 100);
            setBox(confirmPswd, "Confirmation du mot de passe");
        }

        public void setBox(TextBox box, String text)
        {
            box.Size = new System.Drawing.Size(form.Width/2, form.Height * 5 / 100);
            box.Font = new System.Drawing.Font("Poppins", form.Height * 3/ 100);
            box.ForeColor = Color.Gray;
            box.Text = text;
            form.Controls.Add(box);
        }
        public void inscription_click(object sender, EventArgs e)
        {
            if (validEntry())
            {
                //Partie BD : Inscrire l'utilisateur en vérifiant qu'il n'existe pas déja
                form.Controls.Clear();
                //form.changerClasse(new Interface...());
            }
        }

        public Boolean validEntry()
        {
            string pattern = "^[A-Z]{1}[A-Za-z]{2,}$";
            if(name.Text == "Votre prénom" || firstName.Text == "Votre prénom" || pswd.Text == "Votre mot de passe" || confirmPswd.Text == "Confirmation du mot de passe")
            {
                alert.Text = "Il faut remplir tous les champs";
                return false;
            }
            if (!Regex.IsMatch(name.Text, pattern))
            {
                alert.Text = "Veuillez renseignez un prénom valide";
                return false;
            }
            if (!Regex.IsMatch(firstName.Text, pattern))
            {
                alert.Text = "Veuillez renseignez un nom valide";
                return false;
            }
            if (pswd.Text.Length < 6)
            {
                alert.Text = "Veuillez choisir un mot de passe plus long";
                return false;
            }
            if(pswd.Text != confirmPswd.Text)
            {
                alert.Text = "Les mots de passe renseignés ne sont pas identiques";
                return false;
            }
            return true;
        }

        public override void updateSize()
        {
            throw new NotImplementedException();
        }
    }
}
