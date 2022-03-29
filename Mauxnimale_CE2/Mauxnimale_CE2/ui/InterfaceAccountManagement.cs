﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceAccountManagement : AInterface
    {
        Header header;
        Footer footer;

        UIRoundButton back;
        UIButton confirm, passwordPage, idPage, logOut;
        TextBox name, prénom, email, phone, adresse;
        Label lName, lPrénom, lEmail, lPhone, lAdresse;

        public InterfaceAccountManagement(MainWindow window, SALARIE user) : base(window, user)
        {
            header = new Header(window);
            footer = new Footer(window, user);
        }

        public override void load()
        {
            header.load("Plannimaux - Gestion du compte");
            footer.load();
            generateButton();
            generateTextBox();
            generateLabel();
        }


        #region eventHandler

        public void confirmClick(object sender, EventArgs e)
        {
            UserController.updateInfos(user, prénom.Text, name.Text, null, null, email.Text, phone.Text, adresse.Text);
        }

        public void logOutClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceConnection(window,null));
        }

        public void idPageClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceChangeID(window, user));
        }

        public void passwordPageClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceChangePassword(window, user));
        }

        public void backClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceHome(window, user));
        }

        public void nameLeave(object sender, EventArgs e)
        {
            if (name.Text.Length == 0)
            {
                name.Text = user.NOM;
            }
        }
        public void adresseLeave(object sender, EventArgs e)
        {
            if (adresse.Text.Length == 0)
            {
                adresse.Text = user.ADRESSE;
            }
        }

        public void prénomLeave(object sender, EventArgs e)
        {
            if (prénom.Text.Length == 0)
            {
                prénom.Text = user.PRENOM;
            }
        }

        public void emailLeave(object sender, EventArgs e)
        {
            if (email.Text.Length == 0)
            {
                email.Text = user.EMAIL;
            }
        }

        public void phoneLeave(object sender, EventArgs e)
        {
            if (phone.Text.Length == 0)
            {
                phone.Text = user.TEL;
            }
        }
        #endregion

        public void generateLabel()
        {
            lName = new Label();
            lName.Location = new Point(window.Width / 20, window.Height * 17 / 100);
            setLabel(lName, "Nom");
            lPrénom = new Label();
            lPrénom.Location = new Point(window.Width / 20, window.Height * 32 / 100);
            setLabel(lPrénom, "Prénom");
            lEmail = new Label();
            lEmail.Location = new Point(window.Width / 20, window.Height * 47 / 100);
            setLabel(lEmail, "Email");
            lPhone = new Label();
            lPhone.Location = new Point(window.Width / 20, window.Height * 62 / 100);
            setLabel(lPhone, "N° de téléphone");
            lAdresse = new Label();
            lAdresse.Location = new Point(window.Width / 20, window.Height * 77 / 100);
            setLabel(lAdresse, "Adresse");
        }

        public void setLabel(Label l, string s)
        {
            l.Font = new System.Drawing.Font("Poppins", window.Height * 3 / 100);
            l.ForeColor = UIColor.LIGHTBLUE;
            l.Text = s;
            l.Size = new System.Drawing.Size(window.Width / 3, window.Height * 6 / 100);
            window.Controls.Add(l);
        }

        public void generateTextBox()
        {
            name = new TextBox();
            name.LostFocus += new EventHandler(nameLeave);
            name.Location = new Point(window.Width / 8, window.Height * 22 / 100);
            setBox(name, user.NOM);

            prénom = new TextBox();
            prénom.LostFocus += new EventHandler(prénomLeave);
            prénom.Location = new Point(window.Width / 8, window.Height * 37 / 100);
            setBox(prénom, user.PRENOM);

            email = new TextBox();
            email.LostFocus += new EventHandler(emailLeave);
            email.Location = new Point(window.Width / 8, window.Height * 52 / 100);
            setBox(email, user.EMAIL);

            phone = new TextBox();
            phone.LostFocus += new EventHandler(phoneLeave);
            phone.Location = new Point(window.Width / 8, window.Height * 67 / 100);
            setBox(phone, user.TEL);

            adresse = new TextBox();
            adresse.LostFocus += new EventHandler(adresseLeave);
            adresse.Location = new Point(window.Width / 8, window.Height * 82 / 100);
            setBox(adresse, user.ADRESSE);
        }
        public void setBox(TextBox box, String text)
        {
            box.Size = new System.Drawing.Size(window.Width / 2, window.Height * 5 / 100);
            box.Font = new System.Drawing.Font("Poppins", window.Height * 3 / 100);
            box.Text = text;
            window.Controls.Add(box);
        }

        public void generateButton()
        {
            passwordPage = new UIButton(UIColor.ORANGE, "Changer de mot de passe", window.Width / 6);
            passwordPage.Location = new System.Drawing.Point(window.Width * 4 / 6, window.Height * 225 / 1000);
            window.Controls.Add(passwordPage);

            idPage = new UIButton(UIColor.ORANGE, "Changer d'identifiant", window.Width / 6);
            idPage.Location = new System.Drawing.Point(window.Width * 4/ 6, window.Height * 375 / 1000);
            window.Controls.Add(idPage);

            confirm = new UIButton(UIColor.ORANGE, "Confirmer changement", window.Width / 6);
            confirm.Location = new System.Drawing.Point(window.Width * 4 / 6, window.Height * 525 / 1000);
            window.Controls.Add(confirm);

            logOut = new UIButton(UIColor.ORANGE, "Déconnexion", window.Width / 6);
            logOut.Location = new System.Drawing.Point(window.Width * 4 / 6, window.Height * 675 / 1000);
            window.Controls.Add(logOut);

            back = new UIRoundButton(window.Width / 20, "<");
            back.Location = new System.Drawing.Point(window.Width * 9 / 10, window.Height / 10);
            window.Controls.Add(back);

            passwordPage.Click += new EventHandler(passwordPageClick);
            idPage.Click += new EventHandler(idPageClick);
            confirm.Click += new EventHandler(confirmClick);
            logOut.Click += new EventHandler(logOutClick);
            back.Click += new EventHandler(backClick);
        }
    }
}
