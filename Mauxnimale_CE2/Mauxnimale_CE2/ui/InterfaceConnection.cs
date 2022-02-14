using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mauxnimale_CE2
{
    class InterfaceConnection : AInterface
    {
        Footer footer;
        DateTimePicker dateTimePicker = new DateTimePicker();
        Button connection;

        Label date;
        MainWindow window;
        //Lister ici les différents éléments qui seront utilisés dans l'interface

        public InterfaceConnection(MainWindow window)
        {
            this.window = window;
            this.footer = new Footer(window);
        }

        public override void load()
        {
            generate_Button();
            generate_Labels();
            footer.load();
        }

        public void generate_Button()
        {
            connection = new Button();
            connection.Click += new EventHandler(this.connection_click);
            /*connection.Size = new System.Drawing.Size(110, 42);*/
            connection.Location = new System.Drawing.Point((this.window.Width / 2) - (connection.Width/2), (this.window.Width / 4));
            connection.Text = "Connection";
            connection.Font = new System.Drawing.Font("Minion Pro", 20);
            window.Controls.Add(connection);
        }

        public void generate_Labels()
        {
            date = new Label();
            dateTimePicker = new DateTimePicker();
            date.Text = dateTimePicker.Value.ToString("yyyy-MM-dd");
            date.Location = new System.Drawing.Point(this.window.Width - 100, 5);
        }

        public void connection_click(object sender, EventArgs e)
        {
            foreach (Control item in window.Controls)
            {
                window.Controls.Remove(item);
            }
        }


    }
}
