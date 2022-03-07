using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
        using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.ui.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceGestionConsultation : AInterface
    {

        MainWindow window;

        Header header;
        Footer footer;

        UIButton modifConsult, createOrdonance, deleteConsult, createConsult;
        UIRoundButton back, backConsult,forwardConsult;

        ComboBox clientCombo, animalCombo, maladieCombo, soinCombo, dateCombo;
        Label clientLabel, animalLabel, maladieLabel, soinLabel, dateLabel, descriptionLabel;

        Label incEvent;
        TextBox events;

        public InterfaceGestionConsultation(MainWindow window)
        {
            this.window = window;
            header = new Header(window);
            footer = new Footer(window);

        }
        public override void load()
        {
            header.load("Mauxnimale - Gestion des Consultations");
            footer.load();
            generateButton();
            generateLabel();
            generateTextBox();
        }

        public void generateLabel()
        {
            incEvent = new Label();
            incEvent.Text = "Consultations à venir";
            incEvent.TextAlign = ContentAlignment.MiddleLeft;
            incEvent.Font = new System.Drawing.Font("Poppins", window.Height * 2 / 100);
            incEvent.ForeColor = Color.Black;
            incEvent.Size = new System.Drawing.Size(window.Width * 3 / 10, window.Height * 1 / 10);
            incEvent.Location = new Point(window.Width * 25 / 1000, window.Height / 10);
            window.Controls.Add(incEvent);
        }

        public void generateTextBox()
        {
            events = new TextBox();
            events.Text = "";
            events.Font = new System.Drawing.Font("Poppins", window.Height * 2 / 100);
            events.ForeColor = Color.Black;
            events.BackColor = Color.White;
            events.ReadOnly = true;
            events.Multiline = true;
            events.Size = new System.Drawing.Size(window.Width * 25 / 100, window.Height * 45 / 100);
            events.Location = new Point(window.Width * 25 / 1000, window.Height * 2 / 10);
            window.Controls.Add(events);
        }

        public void generateButton()
        {
            modifConsult = new UIButton(UIColor.DARKBLUE, "Modifier Consultation", window.Width *3  / 20);
            modifConsult.Location = new System.Drawing.Point(window.Width * 400 / 1000, window.Height * 14 / 20);
            window.Controls.Add(modifConsult);

            createOrdonance = new UIButton(UIColor.DARKBLUE, "Créer ordonance", window.Width*3 / 20);
            createOrdonance.Location = new System.Drawing.Point(window.Width * 6 / 10, window.Height * 14 / 20);
            window.Controls.Add(createOrdonance);

            deleteConsult = new UIButton(UIColor.DARKBLUE, "Supprimer Consultation", window.Width *3 / 20);
            deleteConsult.Location = new System.Drawing.Point(window.Width * 800 / 1000, window.Height * 14 / 20);
            window.Controls.Add(deleteConsult);

            createConsult = new UIButton(UIColor.DARKBLUE, "Créer Consultation", window.Width / 10);
            createConsult.Location = new System.Drawing.Point(((window.Width * 2 / 10 + window.Width / 20)/2) - createConsult.Width/2, window.Height * 15 / 20);
            window.Controls.Add(createConsult);

            back = new UIRoundButton(window.Width / 20, "<");
            back.Location = new Point(window.Width * 9 / 10, window.Height / 10);
            window.Controls.Add(back);
            back.Click += new EventHandler(backClick);

            backConsult = new UIRoundButton(window.Width / 40, "<");
            backConsult.Location = new Point(window.Width / 20, window.Height * 15 / 20);
            window.Controls.Add(backConsult);
            backConsult.Click += new EventHandler(backConsultClick);

            forwardConsult = new UIRoundButton(window.Width / 40, ">");
            forwardConsult.Location = new Point(window.Width * 2 / 10, window.Height * 15 / 20);
            forwardConsult.Text = ">";
            window.Controls.Add(forwardConsult);
            forwardConsult.Click += new EventHandler(forwardConsultClick);

            /*//* parti a finir lors de la création des fonctions API
            if(admin){*//*
            manageCongé = new UIButton(UIColor.DARKBLUE, "Gestion des congés", window.Width / 4);
            manageCongé.Location = new System.Drawing.Point(window.Width * 325 / 1000, window.Height * 625 / 1000);
            window.Controls.Add(manageCongé);

            stats = new UIButton(UIColor.DARKBLUE, "Statistiques", window.Width / 4);
            stats.Location = new System.Drawing.Point(window.Width * 6 / 10, window.Height * 625 / 1000);
            window.Controls.Add(stats);
            //}*/


            /*compte = new Button();
            compte.FlatAppearance.BorderSize = 0;
            compte.Size = new Size(window.Width / 10, window.Height / 10);
            compte.Location = new System.Drawing.Point(window.Width * 87 / 100, window.Height * 15 / 100);
            window.Controls.Add(compte);
            
            compte.Paint += new PaintEventHandler(comptePaint);
            compte.Click += new EventHandler(manageCompteClick);*/
            /*     stats.Click += new EventHandler(statsClick);*/


            modifConsult.Click += new EventHandler(modifConsultClick);
            createOrdonance.Click += new EventHandler(createOrdonanceClick);
            deleteConsult.Click += new EventHandler(deleteConsultClick);
            createConsult.Click += new EventHandler(createConsultClick);
        }


        #region eventHandler

        /* protected void comptePaint(object sender, PaintEventArgs pe)
         {
             Graphics g = pe.Graphics;
             g.DrawEllipse(new Pen(UIColor.ORANGE), 0, 0, compte.Size.Width, compte.Size.Height);
             g.FillEllipse(new SolidBrush(UIColor.ORANGE), 0, 0, compte.Size.Width, compte.Size.Height);
             g.DrawString("☺", new System.Drawing.Font("Roboto", (compte.Size.Width + compte.Size.Height) / 4), new SolidBrush(Color.White), compte.Width / 4, compte.Height / 200);

             GraphicsPath path = new GraphicsPath();
             path.AddEllipse(0, 0, compte.Size.Width, compte.Size.Height);
             compte.Region = new Region(path);


         }*/
        private void forwardConsultClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void backConsultClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void backClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceHome(window));
        }

        public void manageCompteClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            //form.changerClasse(new Interface...());
        }

        public void modifConsultClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            //form.changerClasse(new Interface...());
        }

        public void createOrdonanceClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            //form.changerClasse(new Interface...());
        }

        public void deleteConsultClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            //form.changerClasse(new Interface...());
        }

        public void createConsultClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            //form.changerClasse(new Interface...());
        }

        public void statsClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            //form.changerClasse(new Interface...());
        }
        #endregion

        public override void updateSize()
        {
            window.Controls.Clear();
            this.load();
        }
    }
}



