using System;
using System.Drawing;
using System.Windows.Forms;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api.controllers;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2.ui
{
    public class InterfaceNewProductType : AInterface
    {
        private Header _header;
        private Footer _footer;

        private UIRoundButton _backButton, _homeButton;

        private TextBox _name;
        private UIButton _newButton;

        public InterfaceNewProductType(MainWindow window, SALARIE user) : base(window, user)
        {
            _header = new Header(window);
            _footer = new Footer(window, user);

            _backButton = new UIRoundButton(window.Width / 20, "<");
            _backButton.Location = new Point(window.Width * 9 / 10, window.Height / 10);
            _backButton.Click += backClick;
            window.Controls.Add(_backButton);

            _homeButton = new UIRoundButton(window.Width / 20, "«");
            _homeButton.Location = new Point(window.Width * 8 / 10, window.Height / 10);
            _homeButton.Click += homeClick;
            window.Controls.Add(_homeButton);
        }

        #region Génération des composants de l'interface

        private void generateNameBox()
        {
            _name = new TextBox();
            _name.GotFocus += nameEnter;
            _name.LostFocus += nameLeave;
            _name.Size = new Size(window.Width / 2, window.Height * 5 / 100);
            _name.Location = new Point(window.Width / 2 - _name.Width / 2, window.Height * 30 / 100);
            _name.Font = new Font("Poppins", window.Height * 3 / 100);
            _name.ForeColor = Color.Gray;
            _name.Text = "Nom du type";
            _name.TextChanged += nameChanged;
            window.Controls.Add(_name);
        }

        private void generateButtons()
        {
            _newButton = new UIButton(UIColor.ORANGE, "Ajouter", window.Width / 4);
            _newButton.Location = new Point(window.Width / 2 - _newButton.Width / 2, _name.Bottom + 50);
            _newButton.Enabled = false;
            _newButton.Click += onNewClick;
            window.Controls.Add(_newButton);
        }

        #endregion

        #region Gestion des événements

        private void nameEnter(object sender, EventArgs e)
        {
            if (_name.Text == "Nom du type")
            {
                _name.Text = "";
                _name.ForeColor = Color.Black;
            }
        }

        private void nameLeave(object sender, EventArgs e)
        {
            if (_name.Text.Length == 0)
            {
                _name.Text = "Nom du type";
                _name.ForeColor = Color.Gray;
            }
        }

        private void nameChanged(object sender, EventArgs eventArgs)
        {
            if (_name.Text.Length > 0 && !_name.Text.Length.Equals("Nom du type") && !_newButton.Enabled)
                _newButton.Enabled = true;
            else if ((_name.Text.Length == 0 || _name.Text.Equals("Nom du type")) && _newButton.Enabled)
                _newButton.Enabled = false;
        }

        /// <summary>
        /// Remplace l'interface actuelle par celle de gestion du stock.
        /// </summary>
        public void backClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceProductsTypeManagement(window, user));
        }

        public void homeClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceHome(window, user));
        }

        private void onNewClick(object sender, EventArgs eventArgs)
        {
            // Vérification de la non existence de ce type
            if (ProductController.getTypeByName(_name.Text) != null)
            {
                MessageBox.Show("Le type de produit portant le nom : " + _name.Text + " existe déjà.", "Informations non valides", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string message = "Confirmez-vous l'ajout du nouveau type : " + _name.Text + " ?";
            DialogResult confirmed = MessageBox.Show(message, "Demande de confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmed == DialogResult.Yes)
            {
                ProductController.addType(_name.Text);
                MessageBox.Show("Le type : " + _name.Text + " a été ajouté avec succès.", "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        public override void load()
        {
            _header.load("Mauxnimale - Ajout d'un nouveau type de produit");
            _footer.load();

            generateNameBox();
            generateButtons();
        }
    }
}
