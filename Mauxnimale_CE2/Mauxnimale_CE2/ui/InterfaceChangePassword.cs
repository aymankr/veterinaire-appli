using System;
using System.Drawing;
using System.Windows.Forms;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceChangePassword : AInterface
    {
        Header header;
        Footer footer;

        UIRoundButton back, home;
        UIButton confirm;
        TextBox oldPassword, newPassword, confirmPassword;
        Label lOldPassword, lNewPassword, lConfirmPassword;

        public InterfaceChangePassword(MainWindow window, SALARIE user) : base(window, user)
        {
            header = new Header(window);
            footer = new Footer(window, user);
        }

        public void newIDLeave(object sender, EventArgs e)
        {
            if (newPassword.Text.Length == 0)
            {
                newPassword.Text = "Nouveau mot de passe";
            }
        }

        public void oldIDLeave(object sender, EventArgs e)
        {
            if (oldPassword.Text.Length == 0)
            {
                oldPassword.Text = "Ancien mot de passe";
            }
        }

        public void confirmIDLeave(object sender, EventArgs e)
        {
            if (confirmPassword.Text.Length == 0)
            {
                confirmPassword.Text = "Confirmez le mot de passe";
            }
        }
        public void confirmIDClick(object sender, EventArgs e)
        {
            if (oldPassword.Text.Length != 0 && newPassword.Text.Length != 0 && confirmPassword.Text.Length != 0 && confirmPassword.Text != "Confirmez le mot de passe" && newPassword.Text != "Nouveau mot de passe" && oldPassword.Text != "Ancien mot de passe")
            {
                UserController.updatePassword(user, oldPassword.Text, newPassword.Text);
                window.Controls.Clear();
                window.switchInterface(new InterfaceAccountManagement(window, user));
            }
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
            header.load("Plannimaux - Changement de mot de passe");
            footer.load();
            generateTextBox();
            generateLabel();
            generateButton();
        }

        public void generateTextBox()
        {
            oldPassword = new TextBox();
            oldPassword.LostFocus += new EventHandler(oldIDLeave);
            oldPassword.Location = new Point(window.Width / 8, window.Height * 22 / 100);
            setBox(oldPassword, "Ancien mot de passe");//mettre le nom de l'utilisateur connecté ici

            newPassword = new TextBox();
            newPassword.LostFocus += new EventHandler(newIDLeave);
            newPassword.Location = new Point(window.Width / 8, window.Height * 37 / 100);
            setBox(newPassword, "Nouveau mot de passe");//mettre le prénom de l'utilisateur connecté ici

            confirmPassword = new TextBox();
            confirmPassword.LostFocus += new EventHandler(confirmIDLeave);
            confirmPassword.Location = new Point(window.Width / 8, window.Height * 52 / 100);
            setBox(confirmPassword, "Confirmez le mot de passe");//mettre l'email de l'utilisateur connecté ici
        }

        public void generateLabel()
        {
            lOldPassword = new Label();
            lOldPassword.Location = new Point(window.Width / 20, window.Height * 17 / 100);
            setLabel(lOldPassword, "Ancien mot de passe");
            lNewPassword = new Label();
            lNewPassword.Location = new Point(window.Width / 20, window.Height * 32 / 100);
            setLabel(lNewPassword, "Nouveau mot de passe");
            lConfirmPassword = new Label();
            lConfirmPassword.Location = new Point(window.Width / 20, window.Height * 47 / 100);
            setLabel(lConfirmPassword, "Confirmez le mot de passe");
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
    }
}
