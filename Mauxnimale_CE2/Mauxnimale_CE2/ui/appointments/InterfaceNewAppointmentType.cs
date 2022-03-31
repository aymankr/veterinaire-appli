using Mauxnimale_CE2.api.controllers;
using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mauxnimale_CE2.ui.appointments
{
    internal class InterfaceNewAppointmentType : AInterface
    {
        Header header;
        Footer footer;

        TextBox typeName;
        Label typeLabel;
        UIButton confirmNewType;
        UIRoundButton back;

        public InterfaceNewAppointmentType(MainWindow window, SALARIE user) : base(window, user)
        {
            header = new Header(window);
            footer = new Footer(window, user);
        }

        public override void load()
        {
            footer.load();
            header.load("Création d'un nouveau type de RDV");
            GenerateLabel();
            GenerateBoxes();
            GenerateButtons();
        }

        public void GenerateLabel()
        {
            typeLabel = new Label
            {
                Text = "Nom du nouveau Type",
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = UIColor.DARKBLUE,
                Size = new Size(window.Width * 2 / 10, window.Height * 1 / 10),
                Location = new Point(window.Width * 400 / 1000, window.Height * 25 / 100)
            };
            window.Controls.Add(typeLabel);
        }

        public void GenerateButtons()
        {
            confirmNewType = new UIButton(UIColor.ORANGE, "Ajouter le type", 190)
            {
                Font = UIFont.BigButtonFont,
                Enabled = false
            };
            confirmNewType.Location = new Point((this.window.Width / 2) - (confirmNewType.Width / 2) - 25, 2 * (this.window.Height / 3));
            window.Controls.Add(confirmNewType);
            confirmNewType.Click += new EventHandler(confirmClick);


            back = new UIRoundButton(window.Width / 20, "<");
            back.Location = new Point(window.Width * 9 / 10, window.Height / 10);
            window.Controls.Add(back);
            back.Click += new EventHandler(backClick);

        }

        private void backClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceAppointmentManagment(window, user));
        }

        public void GenerateBoxes()
        {
            typeName = new TextBox();
            typeName.Size = new Size(window.Width / 2, window.Height * 5 / 100);
            typeName.Font = new Font("Poppins", window.Height * 3 / 100);
            typeName.ForeColor = Color.Gray;
            typeName.Text = "Entrez le type ici";
            window.Controls.Add(typeName);
            typeName.LostFocus += new EventHandler(typeLeave);
            typeName.GotFocus += new EventHandler(typeEnter);
            typeName.TextChanged += new EventHandler(typeEnter);
            typeName.Location = new Point(window.Width / 4, window.Height * 35 / 100);
        }

        private void typeEnter(object sender, EventArgs e)
        {
            if (typeName.Text == "Entrez le type ici")
            {
                typeName.Text = "";
                typeName.ForeColor = Color.Black;
                confirmNewType.Enabled = false;
            }
            else
            {
                confirmNewType.Enabled = true;
            }
        }

        private void typeLeave(object sender, EventArgs e)
        {
            if (typeName.Text.Length == 0)
            {
                typeName.Text = "Entrez le type ici";
                typeName.ForeColor = Color.Gray;
                confirmNewType.Enabled = false;
            }
        }

        private void confirmClick(object sender, EventArgs e)
        {
            foreach(TYPE_RDV type in AppointmentController.GetAllRDVType())
            {
                if(typeName.Text == type.NOMTYPE)
                {
                    var result = MessageBox.Show("Ce type RDV existe déjà", "Erreur RDV already in DataBase", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if(result == DialogResult.OK)
                    {
                        window.Controls.Clear();
                        window.switchInterface(new InterfaceNewAppointmentType(window, user));
                    }
                }
            }
            //AppointmentController.AddTypeRDV(typeName.Text);
            window.Controls.Clear();
            window.switchInterface(new InterfaceAppointmentCreation(window, user));
        }
    }
}
