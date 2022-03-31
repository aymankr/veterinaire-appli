using System;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceStatsPage : AInterface
    {
        readonly Header header;
        readonly Footer footer;

        UIButton productPage, clientPage;
        UIRoundButton back;

        public InterfaceStatsPage(MainWindow window, SALARIE user) : base(window, user)
        {
            header = new Header(window);
            footer = new Footer(window, user);
        }

        public override void load()
        {
            header.load("Mauxnimale - Page Statistiques");
            footer.load();
            generateButton();
        }

        /// <summary>
        /// Permet de générer les boutons de l'interface.
        /// </summary>
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

        /// <summary>
        /// Permet de charger les éléments de la page de statistique des produits.
        /// </summary>
        /// <param name="sender">Bouton stats produit</param>
        /// <param name="e">clic</param>
        public void productPageClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceStatsProducts(window, user));
        }

        /// <summary>
        /// Permet de charger l'interface statistique client.
        /// </summary>
        /// <param name="sender">Bouton stats client</param>
        /// <param name="e"></param>
        public void clientPageClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceStatsClient(window, user));
        }

        public void backClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceHome(window, user));
        }
    }
}
