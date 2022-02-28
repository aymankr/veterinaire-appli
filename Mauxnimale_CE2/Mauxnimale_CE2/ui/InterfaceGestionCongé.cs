using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.UI.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceGestionCongé : AInterface
    {
        MainWindow window;

        Header header;
        Footer footer;

        UIRoundButton back;
        UIButton confirm, remove;
        Label daysLeft;
        ListBox workers;
        List<string> workersList = new List<string>();
        TextBox days;

        //valeur modifié grace a des requetes
        string daysString = "";

        public InterfaceGestionCongé(MainWindow forme)
        {
            this.window = forme;
            header = new Header(window);
            footer = new Footer(window);
        }

        public override void load()
        {
            header.load("Mauxnimale - Gestion Congé");
            footer.load();
            generateButton();
            generateLabel();
            generateBox();
            generateListBox();
        }

        //cette fonction dépend de la variable string "daysString" par conséquent, il faudra actualiser la page à chaque fois que cette variable est actualisé
        public void generateBox()
        {
            days = new TextBox();
            days.Size = new System.Drawing.Size(daysLeft.Width / 3, daysLeft.Height);
            days.Font = new System.Drawing.Font("Poppins", window.Height * 3 / 100);
            days.ForeColor = Color.Black;

            days.Text = daysString;

            days.Location = new Point(daysLeft.Location.X + daysLeft.Size.Width, daysLeft.Location.Y);
            window.Controls.Add(days);
        }

        //cette fonction dépend de la variable List<string> "workersList" par conséquent, il faudra actualiser la page à chaque fois que cette variable est actualisé
        public void generateListBox()
        {
            workers = new ListBox();
            workers.Size = new System.Drawing.Size(window.Width / 3, window.Height / 15);
            workers.Font = new System.Drawing.Font("Poppins", window.Height * 2 / 100);
            workers.Location = new Point(window.Width / 2, window.Height * 3 / 15);
            workers.ForeColor = Color.Gray;
            workers.Items.Add("Sélectionnez un salarié");
            foreach(string worker in workersList)
            {
                workers.Items.Add(worker);
            }
            window.Controls.Add(workers);
        }

        public void generateButton()
        {
            confirm = new UIButton(UIColor.ORANGE, "Confirmer", Math.Min(window.Width / 4, window.Height / 3));
            confirm.Location = new System.Drawing.Point(window.Width * 5 / 8, window.Height * 65 / 100);
            window.Controls.Add(confirm);

            remove = new UIButton(UIColor.ORANGE, "Supprimer", Math.Min(window.Width / 4, window.Height / 3));
            remove.Location = new System.Drawing.Point(window.Width / 8, window.Height * 65 / 100);
            window.Controls.Add(remove);

            back = new UIRoundButton(window.Width / 20);
            back.Location = new System.Drawing.Point(window.Width * 9 / 10, window.Height / 10);
            window.Controls.Add(back);
        }

        public void generateLabel()
        {
            daysLeft = new Label();
            daysLeft.Text = "Congé restant";
            daysLeft.Font = new System.Drawing.Font("Poppins", Math.Min(window.Width * 5 / 100, window.Height * 3 / 100));
            daysLeft.ForeColor = UIColor.LIGHTBLUE;
            daysLeft.Size = new System.Drawing.Size(window.Width * 3 / 10, window.Height * 1 / 10);
            daysLeft.Location = new Point(window.Width / 8, window.Height * 3 / 10);
            window.Controls.Add(daysLeft);
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
