using System;
using System.Drawing;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceSalesManagement : AInterface
    {
        private Header _header;
        private Footer _footer;

        private UIButton _sellBtn, _billBtn;

        public InterfaceSalesManagement(MainWindow window, SALARIE user) : base(window, user)
        {
            _header = new Header(window);
            _footer = new Footer(window, user);

            generateButtons();
        }

        #region Génération des composants de l'interface

        /// <summary>
        /// Créer, positionne et ajoute la gestion des événements sur les bouttons de la page.
        /// </summary>
        private void generateButtons()
        {
            _sellBtn = new UIButton(UIColor.ORANGE, "Réaliser une vente", window.Width / 4);
            _sellBtn.Location = new Point(window.Width / 4, window.Height / 2 - _sellBtn.Height / 2);
            _sellBtn.Click += onSellButtonClick;

            _billBtn = new UIButton(UIColor.ORANGE, "Gestion des factures", window.Width / 4);
            _billBtn.Location = new Point(window.Width / 4 * 2, window.Height / 2 - _sellBtn.Height / 2);
            _billBtn.Click += onBillButtonClick;
        }

        #endregion

        #region Gestion des événements

        /// <summary>
        /// Remplace l'interface actuelle par celle de réalisation d'une vente.
        /// </summary>
        private void onSellButtonClick(object sender, EventArgs eventArgs)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceSell(window, user));
        }

        /// <summary>
        /// Remplace l'interface actuelle par celle de gestion des factures.
        /// </summary>
        private void onBillButtonClick(object sender, EventArgs eventArgs)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceBills(window, user));
        }

        #endregion

        public override void load()
        {
            _header.load("Plannimaux - Gestion des ventes");
            _footer.load();
            window.Controls.Add(_sellBtn);
            window.Controls.Add(_billBtn);
        }
    }
}
