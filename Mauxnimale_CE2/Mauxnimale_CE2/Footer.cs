using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mauxnimale_CE2
{
    class Footer
    {
        PaintEventArgs e;
        MainWindow form;

        Label admin;
        Label mentions;
        Rectangle up;
        Rectangle down;

        public Footer(MainWindow form)
        {
            this.form = form;
        }

        public void load()
        {
            generate_Labels();

            form.Paint += new PaintEventHandler(draw_Rectangles);
        }

        public void generate_Labels()
        {
            admin = new Label();
            admin.AutoSize = true;  
            admin.Text = "Contact admin";
            admin.Location = new Point(100, form.Height - 100);
            admin.BackColor = Color.Transparent;
            admin.Font = new Font("Roboto", 15, FontStyle.Bold);
            admin.ForeColor = Color.White;

            mentions = new Label();
            mentions.AutoSize = true;
            mentions.Text = "Mentions légales";
            mentions.Location = new Point(form.Width - 150 - mentions.Width, form.Height - 100);
            mentions.BackColor = Color.Transparent;
            mentions.Font = new Font("Roboto", 15, FontStyle.Bold);
            mentions.ForeColor = Color.White;
            

            form.Controls.Add(admin);
            form.Controls.Add(mentions);
        }

        public void draw_Rectangles(object sender, PaintEventArgs e)
        {
            SolidBrush b = new SolidBrush(Color.FromArgb(144,222,223));
            up = new Rectangle();
            up.Height = 181;
            up.Width = form.Width;
            up.X = 0;
            up.Y = form.Height - 181;

            e.Graphics.FillRectangle(b, up);

            b = new SolidBrush(Color.FromArgb(33, 188, 190));
            down = new Rectangle();
            down.Height = 123;
            down.Width = form.Width;
            down.X = 0;
            down.Y = form.Height - 123;

            e.Graphics.FillRectangle(b, down);
        }

    }
}
