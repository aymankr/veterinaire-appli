using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mauxnimale_CE2
{
    class InterfaceInscription : InterfaceAbs
    {
        Form1 form;
        //Title t;
        //Footer f;
        TextBox name, firstName, mdp, confirmMdp;
        Button b;

        public InterfaceInscription(Form1 forme)
        {
            form = forme;
        }

        public override void load()
        {
            generate_TextBox();
            //t = new Title(form);
            //t.load();
            //f = new Footer(form);
            //f.load();
        }

        protected void firstName_SetText()
        {
            firstName.Text = "Votre prénom";
            firstName.ForeColor = Color.Gray;
        }

        private void firstName_Enter(object sender, EventArgs e)
        {
            if (firstName.ForeColor == Color.Black)
                return;
            firstName.Text = "";
            firstName.ForeColor = Color.Black;
        }
        private void firstName_Leave(object sender, EventArgs e)
        {
            if (firstName.Text.Trim() == "")
                firstName_SetText();
        }

        public void generate_TextBox()
        {
            configure_TextBox(firstName);
            firstName_SetText();
            firstName.Enter += new EventHandler(firstName_Enter);
            firstName.Leave += new EventHandler(firstName_Leave);
        }

        public void configure_TextBox(TextBox t)
        {
            t = new TextBox();
        }

        public void generate_Buttons()
        {

        }

        public void button_click(object sender, EventArgs e)
        {
            foreach (Control item in form.Controls)
            {
                form.Controls.Remove(item);
            }
            //form.changerClasse(new Interface...());
        }
    }
}
