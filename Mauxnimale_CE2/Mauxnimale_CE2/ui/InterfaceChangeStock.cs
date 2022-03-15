using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.ui.components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceChangeStock : AInterface
    {
        MainWindow window;

        Header header;
        Footer footer;
        UIRoundButton back, home;
        //Lister ici les différents éléments qui seront utilisés dans l'interface

        public InterfaceChangeStock(MainWindow forme, SALARIE s)
        {
            user = s;
            this.window = forme;
            header = new Header(window);
            footer = new Footer(window, s);
        }

        public override void load()
        {
            header.load("Plannimaux - Changer Produit");
            footer.load();
            generateButton();
        }

        public override void updateSize()
        {
            window.Controls.Clear();
            this.load();
        }

        private void generateButton()
        {
            back = new UIRoundButton(window.Width / 20, "<");
            back.Location = new System.Drawing.Point(window.Width * 9 / 10, window.Height / 10);
            window.Controls.Add(back);

            home = new UIRoundButton(window.Width / 20, "«");
            home.Location = new System.Drawing.Point(window.Width * 8 / 10, window.Height / 10);
            window.Controls.Add(home);

            home.Click += new EventHandler(homeClick);
            back.Click += new EventHandler(backClick);
        }

        public void homeClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceHome(window, user));
        }

        public void backClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceStockManagement(window, user));
        }
    }
}
