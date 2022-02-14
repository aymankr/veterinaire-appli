using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mauxnimale_CE2
{
    //Ceci est une classe exemple, toutes les interfaces devront au moins comporter ce qui est dans cette classe
    class ExempleInterface : AInterface
    {
        MainWindow form;
        //Lister ici les différents éléments qui seront utilisés dans l'interface

        public ExempleInterface(MainWindow forme)
        {
            this.form = forme;
        }

        public override void load()
        {
            //Créer et charger les boutons (avec form.Controls.Add(l'élement))
        }

        public void button_click(object sender, EventArgs e)
        {
            form.Controls.Clear();
            //form.changerClasse(new Interface...());
        }
    }
}
