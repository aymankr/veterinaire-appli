using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Mauxnimale_CE2
{
    class InterfaceInscription : InterfaceAbs
    {
        Form1 form;

        Header head;
        Footer foot;

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

        public InterfaceInscription(Form1 forme)
        {
            this.form = forme;
            head = new Header(forme);
            foot = new Footer(forme);
        }


        public override void load()
        {
            head.load("Inscription");
            foot.load();
            generateBox();
            generateLabel();
            generateButton();
        }

        public void generateLabel()
        {
            alert = new Label();
            alert.Text = "";
            alert.Font = new System.Drawing.Font("Poppins", 10);
            alert.ForeColor = Color.Red;
            alert.Size = new System.Drawing.Size(form.Width / 2, 20);
            alert.Location = new Point(form.Width / 4, 235);
            form.Controls.Add(alert);
        }

        public void generateButton()
        {
            inscription = new Button();
            inscription.Text = "Inscription";
            inscription.Click += new EventHandler(inscription_click);
            inscription.Location = new Point(form.Width / (5/2), 265);
            form.Controls.Add(inscription);
        }

        public void generateBox()
        {
            name = new TextBox();
            name.LostFocus += new EventHandler(nameLeave);
            name.GotFocus += new EventHandler(nameEnter);
            name.Location = new Point(form.Width / 4, 115);
            setBox(name, "Votre prénom");

            firstName = new TextBox();
            firstName.LostFocus += new EventHandler(firstNameLeave);
            firstName.GotFocus += new EventHandler(firstNameEnter);
            firstName.Location = new Point(form.Width / 4, 145);
            setBox(firstName, "Votre nom");

            pswd = new TextBox();
            pswd.LostFocus += new EventHandler(pswdLeave);
            pswd.GotFocus += new EventHandler(pswdEnter);
            pswd.Location = new Point(form.Width / 4, 185);
            setBox(pswd, "Votre mot de passe");

            confirmPswd = new TextBox();
            confirmPswd.LostFocus += new EventHandler(confirmPswdLeave);
            confirmPswd.GotFocus += new EventHandler(confirmPswdEnter);
            confirmPswd.Location = new Point(form.Width / 4, 215);
            setBox(confirmPswd, "Confirmation du mot de passe");
        }

        public void setBox(TextBox box, String text)
        {
            box.Size = new System.Drawing.Size(form.Width/2, 20);
            box.Font = new System.Drawing.Font("Poppins", 10);
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
    }
}
