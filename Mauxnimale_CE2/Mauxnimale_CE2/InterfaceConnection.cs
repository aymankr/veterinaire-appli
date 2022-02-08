using System;
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
        DateTimePicker dateTimePicker = new DateTimePicker();
        Button connection;

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
            generate_Button();
            header.load("Connection");
            footer.load();

        }

        public void generate_Button()
        {
            connection = new Button();
            connection.Click += new EventHandler(this.connection_click);
            connection.AutoSize = true;
            connection.Location = new System.Drawing.Point((this.form.Width / 2) - (connection.Width/2), (this.form.Width / 4));
            connection.BackColor = System.Drawing.Color.FromArgb(255,156,3);  
            connection.ForeColor = System.Drawing.Color.White;
            connection.Text = "Connection";
            connection.TabStop = false; 
            connection.FlatStyle = FlatStyle.Flat;  
            connection.FlatAppearance.BorderSize = 0;
            connection.Font = new System.Drawing.Font("Roboto", 20, System.Drawing.FontStyle.Bold);
            form.Controls.Add(connection);
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
