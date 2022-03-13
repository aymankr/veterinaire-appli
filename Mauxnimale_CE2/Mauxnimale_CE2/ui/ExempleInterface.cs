using System;
using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.ui.components;

namespace Mauxnimale_CE2.ui
{
    //Ceci est une classe exemple, toutes les interfaces devront au moins comporter ce qui est dans cette classe
    class ExempleInterface : AInterface
    {
        MainWindow window;

        Header header;
        Footer footer;
        //Lister ici les différents éléments qui seront utilisés dans l'interface

        public ExempleInterface(MainWindow forme, SALARIE s)
        {
            salarie = s;
            this.window = forme;
            header = new Header(window);
            footer = new Footer(window);
        }

        public override void load()
        {
            //Créer et charger les boutons (avec form.Controls.Add(l'élement))
        }

        public void button_click(object sender, EventArgs e)
        {
            window.Controls.Clear();
            //form.changerClasse(new Interface...());
        }

        public override void updateSize()
        {
            window.Controls.Clear();
            this.load();
        }
    }
}
