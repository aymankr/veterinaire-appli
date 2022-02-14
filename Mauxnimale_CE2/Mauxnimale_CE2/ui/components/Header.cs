using System;
using System.Drawing;
using System.Windows.Forms;
using Mauxnimale_CE2.UI.Components;

namespace Mauxnimale_CE2
{
    class Header
    {
        MainWindow window;
        Label date;
        UITitleTextBox title;
        DateTimePicker dateTimePicker;

        public Header(MainWindow form)
        {
            this.window = form;
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
            date.Location = new System.Drawing.Point(window.Width - 100, 5);
            window.Controls.Add(date);
        }

        public void generate_Title(String t)
        {
            title = new UITitleTextBox(new Point(this.window.Width / 5, window.Height*25/1000), t, window.Height*75/1000);
            window.Controls.Add(title);
        }
    }
}
