using System;
using System.Drawing;
using System.Windows.Forms;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.api.controllers;

namespace Mauxnimale_CE2.ui
{
    class InterfaceConnection : AInterface
    {

        UIButton connectionButton;

        Header header;

        TextBox login;
        TextBox password;

        MainWindow window;
        //Lister ici les différents éléments qui seront utilisés dans l'interface

        public InterfaceConnection(MainWindow form)
        {
            this.window = form;
            header = new Header(form);
        }

        public override void load()
        {
            header.load("Connection");
            generate_Button();
            generate_TextBox();
        }

        public void generate_Button()
        {
            connectionButton = new UIButton(UIColor.ORANGE, "Connection", 190);
            connectionButton.Font = UIFont.BigButtonFont;
            connectionButton.Location = new Point((this.window.Width / 2) - (connectionButton.Width / 2) - 25, 2 * (this.window.Height / 3));
            window.Controls.Add(connectionButton);
            connectionButton.Click += new EventHandler(connection_click);
        }


        public void setBox(TextBox box, String text)
        {
            box.Size = new Size(window.Width / 2, window.Height * 5 / 100);
            box.Font = new Font("Poppins", window.Height * 3 / 100);
            box.ForeColor = Color.Gray;
            box.Text = text;
            window.Controls.Add(box);
        }

        public void generate_TextBox()
        {
            login = new TextBox();
            login.LostFocus += new EventHandler(loginLeave);
            login.GotFocus += new EventHandler(loginEnter);
            login.Location = new Point(window.Width / 4, window.Height * 35 / 100);
            setBox(login, "Identifiant");

            password = new TextBox();   
            password.LostFocus += new EventHandler(passwordLeave);
            password.GotFocus += new EventHandler(passwordEnter);
            password.Location = new Point(window.Width / 4, window.Height * 45 / 100);
            password.PasswordChar = (char)0;
            password.KeyPress += new KeyPressEventHandler(passwordKeyPressed);
            setBox(password, "Mot de passe");

        }

        public void connection_click(object sender, EventArgs e)
        {
            if (login.Text != null && password.Text != null)
            {
                SALARIE user = UserController.getConnection(login.Text, password.Text);
                if (user != null)
                {
                    window.Controls.Clear();
                    if (!user.PREMIERECONNEXION)
                        window.switchInterface(new InterfaceFirstConnection(window, user));
                    else
                        window.switchInterface(new InterfaceHome(window, user));
                    return;
                }
            }
            MessageBox.Show("Identifiant ou mot de passe incorrecte", "Entrées non valides", MessageBoxButtons.OK);          
        }

        private void loginEnter(object sender, EventArgs e)
        {
            if (login.Text == "Identifiant")
            {
                login.Text = "";
                login.ForeColor = Color.Black;
            }
        }

        private void passwordKeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (login.Text != null && password.Text != null)
                {
                    SALARIE user = UserController.getConnection(login.Text, password.Text);
                    if (user != null)
                    {
                        window.Controls.Clear();
                        if (!user.PREMIERECONNEXION)
                            window.switchInterface(new InterfaceFirstConnection(window, user));
                        else
                            window.switchInterface(new InterfaceHome(window, user));
                        return;
                    }
                }
                MessageBox.Show("Identifiant ou mot de passe incorrecte", "Entrées non valides", MessageBoxButtons.OK);
            }
        }

        private void loginLeave(object sender, EventArgs e)
        {
            if (login.Text.Length == 0)
            {
                login.Text = "Identifiant";
                login.ForeColor = Color.Gray;   
            }
        }
       
        private void passwordEnter(object sender, EventArgs e)
        {
            if (password.Text == "Mot de passe")
            {
                password.Text = "";
                password.ForeColor = Color.Black;
                password.PasswordChar = '•';
            }
        }

        private void passwordLeave(object sender, EventArgs e)
        {
            if (password.Text.Length == 0)
            {
                password.Text = "Mot de passe";
                password.ForeColor = Color.Gray;
                password.PasswordChar = (char)0;
            }
        }

        public override void updateSize()
        {
            window.Controls.Clear();
            this.load();
        }
    }
}
