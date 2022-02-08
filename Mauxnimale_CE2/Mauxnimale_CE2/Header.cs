using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mauxnimale_CE2
{
    class Header
    {
        MainWindow form;
        Label date, title;
        DateTimePicker dateTimePicker;

        public Header(MainWindow form)
        {
            this.form = form;
        }

        public void load(String t)
        {
            generate_Labels();
            generate_Title(t);
        }

        public void generate_Labels()
        {
            date = new Label();
            dateTimePicker = new DateTimePicker();
            date.Text = dateTimePicker.Value.ToString("yyyy-MM-dd");
            date.Location = new System.Drawing.Point(form.Width - 100, 5);
            form.Controls.Add(date);
        }

        public void generate_Title(String t)
        {
            title = new Label();
            title.Text = t;
            title.Font = new System.Drawing.Font("Poppins", 30);
            title.Size = new System.Drawing.Size(this.form.Width, 60);
            title.ForeColor = System.Drawing.Color.CadetBlue;
            title.Location = new System.Drawing.Point(this.form.Width/5, 25);
            form.Controls.Add(title);
        }
    }
}
