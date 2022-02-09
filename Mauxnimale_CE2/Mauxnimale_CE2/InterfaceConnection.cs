﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mauxnimale_CE2
{
    class InterfaceConnection : InterfaceAbs
    {


        Footer footer;
        Header header;

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
            connection = new Button();
            connection.Click += new EventHandler(this.connection_click);
            connection.BackColor = System.Drawing.Color.FromArgb(255,156,3);  
            connection.ForeColor = System.Drawing.Color.White;
            connection.Text = "Connection";
            connection.TabStop = false; 
            connection.FlatStyle = FlatStyle.Flat;  
            connection.FlatAppearance.BorderSize = 0;
            connection.Font = new System.Drawing.Font("Roboto", 20, System.Drawing.FontStyle.Bold);
            connection.AutoSize = true;
            connection.Location = new System.Drawing.Point((this.form.Width / 2) - (connection.Width / 2), 3 * (this.form.Height / 4));
            form.Controls.Add(connection);
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
            //form.changerClasse(new Interface...());
        }


    }
}
