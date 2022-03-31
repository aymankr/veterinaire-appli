using System;
using System.Drawing;
using System.Windows.Forms;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.api.controllers;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceChangeID : AInterface
    {
        Header header;
        Footer footer;

        UIRoundButton back, home;
        UIButton confirm;
        TextBox oldID, newID, confirmID;
        Label lOldID, lNewID, lConfirmID;

        /// <summary>
        /// Constructeur de l'interface
        /// </summary>
        /// <param name="window"></param>
        /// <param name="user"></param>
        public InterfaceChangeID(MainWindow window, SALARIE user) : base(window, user)
        {
            header = new Header(window);
            footer = new Footer(window, user);
        }

        /// <summary>
        /// Fonction permettant de générer l'interface
        /// </summary>
        public override void load()
        {
            header.load("Plannimaux - Changement d'identifiant");
            footer.load();
            generateTextBox();
            generateLabel();
            generateButton();
        }

        public void generateTextBox()
        {
            oldID = new TextBox();
            oldID.LostFocus += new EventHandler(oldIDLeave);
            oldID.Location = new Point(window.Width / 8, window.Height * 22 / 100);
            setBox(oldID, "Ancien identifiant");

            newID = new TextBox();
            newID.LostFocus += new EventHandler(newIDLeave);
            newID.Location = new Point(window.Width / 8, window.Height * 37 / 100);
            setBox(newID, "Nouveau identifiant");

            confirmID = new TextBox();
            confirmID.LostFocus += new EventHandler(confirmIDLeave);
            confirmID.Location = new Point(window.Width / 8, window.Height * 52 / 100);
            setBox(confirmID, "Confirmez l'identifiant");
        }

        /// <summary>
        /// Génère les Labels de l'interface
        /// </summary>
        public void generateLabel()
        {
            lOldID = new Label();
            lOldID.Location = new Point(window.Width / 20, window.Height * 17 / 100);
            setLabel(lOldID, "Ancien Identifiant");
            lNewID = new Label();
            lNewID.Location = new Point(window.Width / 20, window.Height * 32 / 100);
            setLabel(lNewID, "Nouveau Identifiant");
            lConfirmID = new Label();
            lConfirmID.Location = new Point(window.Width / 20, window.Height * 47 / 100);
            setLabel(lConfirmID, "Confirmer Identifiant");
        }

        /// <summary>
        /// Génère les bouttons
        /// </summary>
        private void generateButton()
        {
            confirm = new UIButton(UIColor.ORANGE, "Confirmer changement", window.Width / 5);
            confirm.Location = new System.Drawing.Point(window.Width * 2 / 5, window.Height * 7 / 10);
            window.Controls.Add(confirm);

            back = new UIRoundButton(window.Width / 20, "<");
            back.Location = new System.Drawing.Point(window.Width * 9 / 10, window.Height / 10);
            window.Controls.Add(back);

            home = new UIRoundButton(window.Width / 20, "«");
            home.Location = new System.Drawing.Point(window.Width * 8 / 10, window.Height / 10);
            window.Controls.Add(home);

            confirm.Click += new EventHandler(confirmIDClick);
            home.Click += new EventHandler(homeClick);
            back.Click += new EventHandler(backClick);
        }


        /// <summary>
        /// Sets les differents labels
        /// </summary>
        /// <param name="l"></param>
        /// <param name="s"></param>
        public void setLabel(Label l, string s)
        {
            l.Font = new System.Drawing.Font("Poppins", window.Height * 3 / 100);
            l.ForeColor = UIColor.LIGHTBLUE;
            l.Text = s;
            l.Size = new System.Drawing.Size(window.Width / 3, window.Height * 6 / 100);
            window.Controls.Add(l);
        }

        /// <summary>
        /// Sets les différentes boxs
        /// </summary>
        /// <param name="box"></param>
        /// <param name="text"></param>
        public void setBox(TextBox box, String text)
        {
            box.Size = new System.Drawing.Size(window.Width / 2, window.Height * 5 / 100);
            box.Font = new System.Drawing.Font("Poppins", window.Height * 3 / 100);
            box.Text = text;
            window.Controls.Add(box);
        }

        /// <summary>
        /// Met un placeholder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void newIDLeave(object sender, EventArgs e)
        {
            if (newID.Text.Length == 0)
            {
                newID.Text = "Nouveau identifiant";
            }
        }

        /// <summary>
        /// Met un placeholder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void oldIDLeave(object sender, EventArgs e)
        {
            if (oldID.Text.Length == 0)
            {
                oldID.Text = "Ancien identifiant";
            }
        }

        /// <summary>
        /// Met un placeholder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void confirmIDLeave(object sender, EventArgs e)
        {
            if (confirmID.Text.Length == 0)
            {
                confirmID.Text = "Confirmez l'identifiant";
            }
        }

        /// <summary>
        /// Confirme la modification de l'indentifant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void confirmIDClick(object sender, EventArgs e)
        {
            if (confirmID.Text.Length != 0 && oldID.Text.Length != 0 && newID.Text.Length != 0 && confirmID.Text != "Confirmez l'identifiant" && newID.Text != "Nouveau identifiant" && oldID.Text != "Ancien identifiant")
            {
                UserController.updateLogin(user, confirmID.Text);
                window.Controls.Clear();
                window.switchInterface(new InterfaceAccountManagement(window, user));
            }
        }

        /// <summary>
        /// Retourne vers l'interface de menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void homeClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceHome(window, user));
        }

        /// <summary>
        /// retourne vers l'interface de modification de l'utilisateur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void backClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceAccountManagement(window, user));
        }

    }
}
