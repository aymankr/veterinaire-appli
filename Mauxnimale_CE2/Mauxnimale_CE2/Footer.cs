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
        Label admin;
        Label mentions;
        Rectangle up;
        Rectangle down;

        public void load(Form1 form)
        {
            generate_Labels(form);
        }

        public void generate_Labels(Form1 form)
        {
            admin = new Label();
            admin.Text = "Contact admin";
            admin.Location = new System.Drawing.Point(15, form.Height - 40);

            mentions = new Label();
            mentions.Text = "Mentions légales";
            mentions.Location = new System.Drawing.Point(form.Width - 100, form.Height - 40);

            form.Controls.Add(admin);
            form.Controls.Add(mentions);
        }

        public void generate_Rectangles(Form1 form)
        {
            Pen pen = new Pen();
            up = new Rectangle();
            up.Height = 181;
            up.Width = form.Width;
            up.X = 0;
            up.Y = form.Height - up.Height;
            


            down = new Rectangle();
        }

    }
}
