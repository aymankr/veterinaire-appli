using System;
using System.Collections.Generic;
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

        public void load(Form1 form)
        {
            generate_Label(form);
        }

        public void generate_Label(Form1 form)
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

    }
}
