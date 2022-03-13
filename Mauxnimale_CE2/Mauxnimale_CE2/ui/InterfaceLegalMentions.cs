using System;
using System.Drawing;
using System.Windows.Forms;
using Mauxnimale_CE2.ui.components;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceLegalMentions : AInterface
    {
        MainWindow window;

        Header header;
        Footer footer;
        Label text;
        UIRoundButton back;
        //Lister ici les différents éléments qui seront utilisés dans l'interface

        public InterfaceLegalMentions(MainWindow forme)
        {
            this.window = forme;
            header = new Header(window);
            footer = new Footer(window);
        }

        public void generateLabel()
        {
            text = new Label();
            text.Text = "MENTIONS LÉGALES \n \n L’application Plannimaux® est éditée par l’équipe projet S3A - E1, enregistrée au registre de l’IUT Informatique de Bordeaux(33). \n \n Hébergeur des Services: OVH –  2 rue Kellermann – 59100 ROUBAIX – France ";
            text.TextAlign = ContentAlignment.MiddleLeft;
            text.Font = new System.Drawing.Font("Poppins", window.Height * 2 / 100);
            text.ForeColor = Color.Black;
            text.Size = new System.Drawing.Size(window.Width / 2, window.Height / 2);
            text.Location = new Point(window.Width / 4, window.Height / 4);
            window.Controls.Add(text);
        }
        public override void load()
        {
            header.load("Plannimaux");
            footer.load();
            generateLabel();
            generateButton();
        }

        public void generateButton()
        {

            back = new UIRoundButton(window.Width / 20);
            back.Location = new System.Drawing.Point(window.Width * 9 / 10, window.Height / 10);
            back.Click += new EventHandler(backClick);
            window.Controls.Add(back);
        }

        public void backClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceHome(window, salarie));
        }

        public override void updateSize()
        {
            window.Controls.Clear();
            this.load();
        }
    }
}
