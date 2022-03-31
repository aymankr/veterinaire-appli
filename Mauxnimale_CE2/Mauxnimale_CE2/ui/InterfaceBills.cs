using System;
using System.Drawing;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceBills : AInterface
    {
        Header _header;
        Footer _footer;

        public InterfaceBills(MainWindow window, SALARIE user) : base(window, user)
        {
            _header = new Header(window);
            _footer = new Footer(window, user);
        }

        public override void load()
        {
            _header.load("Plannimaux - Gestion des factures");
            _footer.load();
        }
    }
}
