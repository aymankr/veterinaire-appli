using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceChangeID : AInterface
    {
        MainWindow window;

        Header header;
        Footer footer;

        UIRoundButton back, home;
        UIButton confirm;
        TextBox oldID, newID, confirmID;
        Label lOldID, lNewID, lConfirmID;

        public InterfaceChangeID(MainWindow forme, SALARIE s)
        {
            user = s;
            this.window = forme;
            header = new Header(window);
            footer = new Footer(window, s);
            user = s;
        }

        public void newIDLeave(object sender, EventArgs e)
        {
            if (newID.Text.Length == 0)
            {
                newID.Text = "Texte à codé";//mettre le prénom de l'utilisateur connecté ici
            }
        }

        public void oldIDLeave(object sender, EventArgs e)
        {
            if (oldID.Text.Length == 0)
            {
                oldID.Text = "Texte à codé";//mettre l'email de l'utilisateur connecté ici
            }
        }

        public void confirmIDLeave(object sender, EventArgs e)
        {
            if (confirmID.Text.Length == 0)
            {
                confirmID.Text = "Texte à codé";//mettre le numéro de l'utilisateur connecté ici
            }
        }
        public void confirmIDClick(object sender, EventArgs e)
        {
            //effectuer les changements sur la base de données
        }

        public void homeClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceHome(window, user));
        }

        public void backClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceAccountManagement(window, user));
        }

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
            setBox(oldID, "Texte a codé");//mettre le nom de l'utilisateur connecté ici

            newID = new TextBox();
            newID.LostFocus += new EventHandler(newIDLeave);
            newID.Location = new Point(window.Width / 8, window.Height * 37 / 100);
            setBox(newID, "Texte a codé");//mettre le prénom de l'utilisateur connecté ici

            confirmID = new TextBox();
            confirmID.LostFocus += new EventHandler(confirmIDLeave);
            confirmID.Location = new Point(window.Width / 8, window.Height * 52 / 100);
            setBox(confirmID, "Texte a codé");//mettre l'email de l'utilisateur connecté ici
        }

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

        public void setLabel(Label l, string s)
        {
            l.Font = new System.Drawing.Font("Poppins", window.Height * 3 / 100);
            l.ForeColor = UIColor.LIGHTBLUE;
            l.Text = s;
            l.Size = new System.Drawing.Size(window.Width / 3, window.Height * 6 / 100);
            window.Controls.Add(l);
        }

        public void setBox(TextBox box, String text)
        {
            box.Size = new System.Drawing.Size(window.Width / 2, window.Height * 5 / 100);
            box.Font = new System.Drawing.Font("Poppins", window.Height * 3 / 100);
            box.Text = text;
            window.Controls.Add(box);
        }

        public override void updateSize()
        {
            window.Controls.Clear();
            this.load();
        }
    }
}
