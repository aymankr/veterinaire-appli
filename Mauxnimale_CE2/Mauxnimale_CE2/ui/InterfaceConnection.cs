using Mauxnimale_CE2.ui;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.ui.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mauxnimale_CE2
{
    class InterfaceConnection : AInterface
    {

        UIButton connectionButton;

        Footer footer;
        Header header;

        TextBox login;
        TextBox password;

        MainWindow form;
        //Lister ici les différents éléments qui seront utilisés dans l'interface

        public InterfaceConnection(MainWindow form)
        {
            this.form = form;
            footer = new Footer(form);
            header = new Header(form);
        }

        public override void load()
        {
            header.load("Connection");
            footer.load();
            generate_Button();
            generate_TextBox();
        }

        public void generate_Button()
        {
            connectionButton = new UIButton(UIColor.ORANGE, "Connection", 190);
            connectionButton.Font = UIFont.BigButtonFont;
            connectionButton.Location = new Point((this.form.Width / 2) - (connectionButton.Width / 2) - 25, 2 * (this.form.Height / 3));
            form.Controls.Add(connectionButton);
            connectionButton.Click += new EventHandler(connection_click);
        }


        public void setBox(TextBox box, String text)
        {
            box.Size = new Size(form.Width / 2, form.Height * 5 / 100);
            box.Font = new Font("Poppins", form.Height * 3 / 100);
            box.ForeColor = Color.Gray;
            box.Text = text;
            form.Controls.Add(box);
        }

        public void generate_TextBox()
        {
            login = new TextBox();
            login.LostFocus += new EventHandler(loginLeave);
            login.GotFocus += new EventHandler(loginEnter);
            login.Location = new Point(form.Width / 4, form.Height * 35 / 100);
            setBox(login, "login");

            password = new TextBox();   
            password.LostFocus += new EventHandler(passwordLeave);
            password.GotFocus += new EventHandler(passwordEnter);
            password.Location = new Point(form.Width / 4, form.Height * 45 / 100);
            password.PasswordChar = '•';
            setBox(password, "password");

        }

        public void connection_click(object sender, EventArgs e)
        {
            if (login.Text != null && password.Text != null && Connection.GetSALARIE(login.Text, password.Text) != null)
            {
                form.Controls.Clear();
                form.switchInterface(new InterfaceHome(form, Connection.GetSALARIE(login.Text, password.Text)));
            } else
            {
                MessageBox.Show("identifiant ou mot de passe incorrecte");
            }           
        }

        private void loginEnter(object sender, EventArgs e)
        {
            if (login.Text == "login")
            {
                login.Text = "";
                login.ForeColor = Color.Black;
            }
        }

        private void loginLeave(object sender, EventArgs e)
        {
            if (login.Text.Length == 0)
            {
                login.Text = "login";
                login.ForeColor = Color.Gray;
            }
        }
       
        private void passwordEnter(object sender, EventArgs e)
        {
            if (password.Text == "password")
            {
                password.Text = "";
                password.ForeColor = Color.Black;
            }
        }

        private void passwordLeave(object sender, EventArgs e)
        {
            if (password.Text.Length == 0)
            {
                password.Text = "password";
                password.ForeColor = Color.Gray;
            }
        }

        public override void updateSize()
        {
            throw new NotImplementedException();
        }
    }
}
