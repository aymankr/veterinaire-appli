using System;
using System.Drawing;
using System.Windows.Forms;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.api.controllers;
namespace Mauxnimale_CE2.ui
{
    internal class InterfaceNewCaresAndDiseases : AInterface
    {
        Header header;
        Footer footer;

        #region DiseaseForm
        Label nameDiseaseForm;
        TextBox diseaseName;
        ComboBox allPossibleCares;
        ListBox allSelectedCares;
        UIButton validateDiseaseForm;
        #endregion

        public InterfaceNewCaresAndDiseases(MainWindow window, SALARIE user) : base(window, user)
        {
            header = new Header(window);
            footer = new Footer(window, base.user);
        }


        public override void load()
        {
            header.load("Ajout d'une maladie ou d'un soin");
            footer.load();

            GenerateDiseaseForm();
            GenerateCaresForm();
        }

        private void GenerateDiseaseForm()
        {
            nameDiseaseForm = new Label()
            {
                Text = "Formulaire ajout d'une maladie",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = UIColor.DARKBLUE,
                Size = new Size(window.Width * 3 / 10, window.Height * 1 / 10),
                Location = new Point(window.Width * 5 / 15, window.Height  * 2/ 10)
            };

            window.Controls.Add(nameDiseaseForm);

            diseaseName = new TextBox()
            {
                Text = "Nom",
                Font = new Font("Poppins", window.Height * 2 / 100), 
                ForeColor = Color.Gray,
                Size = new Size(window.Width * 3 / 10, window.Height * 1 / 10),
                Location = new Point(window.Width * 5 / 15, window.Height * 3/ 10)
            };
            diseaseName.GotFocus += new EventHandler(GotFocus);
            window.Controls.Add(diseaseName);

            allPossibleCares = new ComboBox()
            {
                Text = "Soins",
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = Color.Gray,
                Size = new Size(window.Width * 3 / 10, window.Height * 1 / 10),
                Location = new Point(window.Width * 5 / 15, window.Height * 4/ 10)
            };
            allPossibleCares.SelectedValueChanged += new EventHandler(SelectCare);
            window.Controls.Add(allPossibleCares);

            allSelectedCares = new ListBox()
            {
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = Color.Gray,
                Size = new Size(window.Width * 3 / 10, window.Height * 1 / 10),
                Location = new Point(window.Width * 5 / 15, window.Height * 4 / 10)
            };
            allSelectedCares.SelectedValueChanged += new EventHandler(SelectCare);
            window.Controls.Add(allSelectedCares);

            validateDiseaseForm = new UIButton(UIColor.ORANGE, "Ajouter", window.Width * 15 / 100)
            {
                Font = new Font("Poppins", window.Height * 1 / 100),
                Height = window.Height / 25,
                Location = new Point(window.Width * 7 / 50, window.Height * 12 / 20),
                TabIndex = 8
            };
            validateDiseaseForm.Click += new EventHandler(SubmitDiseaseForm);
            window.Controls.Add(validateDiseaseForm);
        }

        private void SubmitDiseaseForm(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SelectCare(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void GotFocus(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void GenerateCaresForm()
        {
            throw new NotImplementedException();
        }
    }
}
