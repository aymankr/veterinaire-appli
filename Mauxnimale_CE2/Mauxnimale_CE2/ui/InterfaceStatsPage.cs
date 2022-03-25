using System;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceStatsPage : AInterface
    {
        Header header;
        Footer footer;

        UIButton productPage, clientPage;
        UIRoundButton back;

        public InterfaceStatsPage(MainWindow window, SALARIE user) : base(window, user)
        {
            this.window = forme;
            header = new Header(window);
            footer = new Footer(window, s);
            user = s;
        }

        public override void load()
        {
            header.load("Mauxnimale - Page Statistiques");
            footer.load();
            generateButton();
        }

        public void generateButton()
        {
            productPage = new UIButton(UIColor.DARKBLUE, "Statistiques produits", window.Width / 3);
            productPage.Location = new System.Drawing.Point(window.Width / 3, window.Height * 225 / 1000);
            window.Controls.Add(productPage);

            clientPage = new UIButton(UIColor.DARKBLUE, "Statistiques clients", window.Width / 3);
            clientPage.Location = new System.Drawing.Point(window.Width / 3, window.Height * 525 / 1000);
            window.Controls.Add(clientPage);

            back = new UIRoundButton(window.Width/20, "<");
            back.Location = new System.Drawing.Point(window.Width * 9 / 10, window.Height / 10);
            window.Controls.Add(back);

            productPage.Click += new EventHandler(productPageClick);
            clientPage.Click += new EventHandler(clientPageClick);
            back.Click += new EventHandler(backClick);
        }

        public void productPageClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            //form.changerClasse(new Interface...());
        }

        public void clientPageClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            //form.changerClasse(new Interface...());
        }

        public void backClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceHome(window, user));
        }
    }
}
