using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.ui.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceGestionCongé : AInterface
    {
        MainWindow window;

        Header header;
        Footer footer;

        UIRoundButton back;
        string selectedWorker;
        UIButton confirm, remove;
        Label daysLeft, h, d, m;
        ListBox workers;
        List<string> workersList = new List<string>();
        TextBox days, congés;
        NumericUpDown day, month, hour;

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
            generateNUpDown();
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

            congés = new TextBox();
            congés.Text = "";
            congés.Font = new System.Drawing.Font("Poppins", window.Height * 2 / 100);
            congés.ForeColor = Color.Black;
            congés.BackColor = Color.White;
            congés.ReadOnly = true;
            congés.Multiline = true;
            congés.Size = new System.Drawing.Size(window.Width * 25 / 100, window.Height * 20 / 100);
            congés.Location = new Point(window.Width * 6 / 100, window.Height * 4 / 10);
            window.Controls.Add(congés);
        }

        public void generateNUpDown()
        {

            day = new NumericUpDown();
            day.Size = new System.Drawing.Size(window.Width / 12, window.Height / 10);
            day.Font = new System.Drawing.Font("Poppins", window.Height * 3 / 100);
            day.ForeColor = Color.Black;
            day.Maximum = 31;
            day.Minimum = 1;

            day.Location = new Point(window.Width * 15 / 24, window.Height / 2);
            window.Controls.Add(day);

            month = new NumericUpDown();
            month.Size = new System.Drawing.Size(window.Width / 12, window.Height / 10);
            month.Font = new System.Drawing.Font("Poppins", window.Height * 3 / 100);
            month.ForeColor = Color.Black;
            month.Maximum = 12;
            month.Minimum = 1;
            month.Location = new Point(window.Width * 18 / 24, window.Height / 2);
            window.Controls.Add(month);

            hour = new NumericUpDown();
            hour.Size = new System.Drawing.Size(window.Width / 12, window.Height / 10);
            hour.Font = new System.Drawing.Font("Poppins", window.Height * 3 / 100);
            hour.ForeColor = Color.Black;
            hour.Maximum = 23;
            hour.Minimum = 0;
            hour.Location = new Point(window.Width * 12 / 24, window.Height / 2);
            window.Controls.Add(hour);
        }
        //cette fonction dépend de la variable List<string> "workersList" par conséquent, il faudra actualiser la page à chaque fois que cette variable est actualisé
        public void generateListBox()
        {
            workers = new ListBox();
            workers.Size = new System.Drawing.Size(window.Width / 3, window.Height / 15);
            workers.Font = new System.Drawing.Font("Poppins", window.Height / 80);
            workers.Location = new Point(window.Width / 2, window.Height * 3 / 15);
            workers.ForeColor = Color.Gray;
            workers.Items.Add("Sélectionnez un salarié");
            workers.SelectedIndexChanged += new EventHandler(workersSelectedIndexChanged);

            /*
             * Les 3 lignes servant d'exemples en dessous sont a remplacer lors de la mise en place des fonctions API
             
             */
            workers.Items.Add("exemple 1");
            workers.Items.Add("exemple 2");
            workers.Items.Add("vous mettrez les vrai salariés a la place");

            foreach (string worker in workersList)
            {
                workers.Items.Add(worker);
            }
            window.Controls.Add(workers);
        }

        public void generateButton()
        {
            confirm = new UIButton(UIColor.ORANGE, "Confirmer", Math.Min(window.Width / 4, window.Height / 3));
            confirm.Location = new System.Drawing.Point(window.Width * 5 / 8, window.Height * 65 / 100);
            confirm.Click += new EventHandler(confirmClick);
            window.Controls.Add(confirm);

            remove = new UIButton(UIColor.ORANGE, "Supprimer", Math.Min(window.Width / 4, window.Height / 3));
            remove.Location = new System.Drawing.Point(window.Width / 8, window.Height * 65 / 100);
            remove.Click += new EventHandler(removeClick);
            window.Controls.Add(remove);

            back = new UIRoundButton(window.Width / 20);
            back.Location = new System.Drawing.Point(window.Width * 9 / 10, window.Height / 10);
            back.Click += new EventHandler(backClick);
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

            h = new Label();
            m = new Label();
            d = new Label();
            h.Text = "h";
            d.Text = "d";
            m.Text = "m";
            h.Location = new Point(window.Width * 14 / 24, window.Height / 2);
            h.Size = new System.Drawing.Size(window.Width / 24, window.Height / 10);
            h.Font = new System.Drawing.Font("Poppins", window.Height * 2 / 100);

            d.Location = new Point(window.Width * 17 / 24, window.Height / 2);
            d.Size = new System.Drawing.Size(window.Width / 24, window.Height / 10);
            d.Font = new System.Drawing.Font("Poppins", window.Height * 2 / 100);

            m.Location = new Point(window.Width * 20 / 24, window.Height / 2);
            m.Size = new System.Drawing.Size(window.Width / 24, window.Height / 10);
            m.Font = new System.Drawing.Font("Poppins", window.Height * 2 / 100);
            window.Controls.Add(h);
            window.Controls.Add(d);
            window.Controls.Add(m);
        }

        public void backClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceHome(window));
        }

        public void confirmClick(object sender, EventArgs e)
        {
            //ajouter le rendez vous dans la BD, si day.Value, month.Value et hour.Value est correcte et que 
        }

        public void removeClick(object sender, EventArgs e)
        {
            //supprimez le rendez vous selectionnez
        }

        public override void updateSize()
        {
            window.Controls.Clear();
            this.load();
        }

        private void workersSelectedIndexChanged(object sender, System.EventArgs e)
        {
            if(workers.SelectedIndex != 0)
            {
                selectedWorker = workers.SelectedItem.ToString();
            }
        }
    }
}
