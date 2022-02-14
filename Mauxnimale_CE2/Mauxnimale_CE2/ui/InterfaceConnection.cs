using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.UI.Components;
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


        Footer footer;
        Header header;

        UIButton button;

        Button connection;
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
            Console.WriteLine(this.form.Height +": Y\n"+this.form.Width+": X");   
        }

        public void generate_Button()
        {
            button = new UIButton(UIColor.ORANGE,"Bouton",160);  
            button.Location = new Point(form.Width/2,form.Height/2);


            connection = new Button();
            connection.Click += new EventHandler(this.connection_click);
            connection.BackColor = Color.FromArgb(255,156,3);  
            connection.ForeColor = Color.White;
            connection.Text = "Connection";
            connection.TabStop = false; 
            connection.FlatStyle = FlatStyle.Flat;  
            connection.FlatAppearance.BorderSize = 0;
            connection.Font = new Font("Roboto", 20, FontStyle.Bold);
            connection.AutoSize = true;
            connection.Location = new Point((this.form.Width / 2) - (connection.Width / 2), 3 * (this.form.Height / 4));
            form.Controls.Add(connection);
            form.Controls.Add(button);
        }

        public void generate_TextBox()
        {
            login = new TextBox();
            login.Text = "login";
        }

        public void connection_click(object sender, EventArgs e)
        {
            foreach (Control item in form.Controls)
            {
                form.Controls.Remove(item);
            }
        }


    }
}
