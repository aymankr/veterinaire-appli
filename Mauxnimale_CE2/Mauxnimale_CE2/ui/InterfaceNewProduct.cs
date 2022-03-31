using Mauxnimale_CE2.api.controllers;
using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.ui.components;
using System;
using System.Drawing;
using System.Windows.Forms;
using Mauxnimale_CE2.ui.components.componentsTools;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceNewProduct : AInterface
    {
        private Header _header;
        private Footer _footer;

        private ComboBox _type;
        private TYPE_PRODUIT _choosedType;
        private UIRoundButton _backButton, _homeButton;
        private UIButton _addProductButton, _typeManagementButton;

        private NumericUpDown _quantity, _sellCost;
        private Label _lQuantity, _lResellCost, _lName, _lType;
        private TextBox _name;

        public InterfaceNewProduct(MainWindow window, SALARIE user) : base(window, user)
        {
            _header = new Header(window);
            _footer = new Footer(window, user);
        }

        #region Génération des composants de l'interface

        public void generateLabels()
        {
            _lQuantity = new Label();
            _lQuantity.Location = new Point(window.Width / 20, window.Height * 42 / 100);
            setLabel(_lQuantity, "Quantité");

            _lResellCost = new Label();
            _lResellCost.Location = new Point(window.Width / 20, window.Height * 52 / 100);
            setLabel(_lResellCost, "Prix de vente");

            _lName = new Label();
            _lName.Location = new Point(window.Width / 20, window.Height * 24 / 100);
            setLabel(_lName, "Nom");

            _lType = new Label();
            _lType.Location = new Point(window.Width / 20, window.Height / 3);
            setLabel(_lType, "Type");
        }

        public void generateButtons()
        {
            _addProductButton = new UIButton(UIColor.ORANGE, "Nouveau Produit", Math.Min(window.Width / 3, window.Height / 2));
            _addProductButton.Location = new Point(window.Width * 2 / 5, window.Height * 75 / 100);
            _addProductButton.Click += new EventHandler(newProductClick);
            window.Controls.Add(_addProductButton);

            _backButton = new UIRoundButton(window.Width / 20, "<");
            _backButton.Location = new Point(window.Width * 9 / 10, window.Height / 10);
            _backButton.Click += new EventHandler(backClick);
            window.Controls.Add(_backButton);

            _homeButton = new UIRoundButton(window.Width / 20, "«");
            _homeButton.Location = new Point(window.Width * 8 / 10, window.Height / 10);
            _homeButton.Click += new EventHandler(homeClick);
            window.Controls.Add(_homeButton);

            _typeManagementButton = new UIButton(UIColor.ORANGE, "Gérer les types", window.Width / 5);
            _typeManagementButton.Height = _type.Height;
            _typeManagementButton.Location = new Point(_type.Right + 10, _type.Top);
            _typeManagementButton.Click += onTypeManagementButtonClick;
            window.Controls.Add(_typeManagementButton);
        }

        public void setLabel(Label l, string s)
        {
            l.Font = new Font("Poppins", window.Height * 2 / 100);
            l.ForeColor = UIColor.LIGHTBLUE;
            l.Text = s;
            l.Size = new Size(window.Width / 3, window.Height * 6 / 100);
            window.Controls.Add(l);
        }

        private void generateBox()
        {
            _type = new ComboBox();
            foreach (TYPE_PRODUIT typeProduct in ProductController.getTypes())
            {
                _type.Items.Add(typeProduct);
            }
            _type.SelectedIndex = 0;
            _type.Size = new Size(window.Width / 2, window.Height / 7);
            _type.DropDownStyle = ComboBoxStyle.DropDownList;
            _type.Font = new Font("Poppins", window.Height / 40);
            _type.Location = new Point(window.Width / 4, window.Height * 5 / 15);
            _type.ForeColor = Color.Gray;
            _type.SelectedIndexChanged += new EventHandler(typeSelectedChange);
            window.Controls.Add(_type);
        }

        public void generateTextBoxes()
        {
            _name = new TextBox();
            _name.LostFocus += new EventHandler(nameLeave);
            _name.GotFocus += new EventHandler(nameEnter);
            _name.Size = new Size(window.Width / 2, window.Height * 5 / 100);
            _name.Location = new Point(window.Width / 4, window.Height * 24 / 100);
            _name.Font = new Font("Poppins", window.Height * 3 / 100);
            _name.ForeColor = Color.Gray;
            _name.Text = "Nom";
            window.Controls.Add(_name);
        }

        public void generateNumUpDown()
        {
            _sellCost = new NumericUpDown();
            _sellCost.Size = new Size(window.Width / 2, window.Height * 5 / 100);
            _sellCost.Location = new Point(window.Width / 4, window.Height * 52 / 100);
            _sellCost.Font = new Font("Poppins", window.Height * 3 / 100);
            _sellCost.ForeColor = Color.Black;
            _sellCost.Minimum = 0;
            _sellCost.Maximum = 100000;
            window.Controls.Add(_sellCost);

            _quantity = new NumericUpDown();
            _quantity.Size = new Size(window.Width / 2, _quantity.Height);
            _quantity.Location = new Point(window.Width / 4, window.Height * 42 / 100);
            _quantity.Font = new Font("Poppins", window.Height * 3 / 100);
            _quantity.ForeColor = Color.Black;
            _quantity.Minimum = 0;
            _quantity.Maximum = 100000;
            window.Controls.Add(_quantity);
        }

        public void setBox(TextBox textBox, string text)
        {
            textBox.Font = new Font("Poppins", window.Height * 3 / 100);
            textBox.ForeColor = Color.Gray;
            textBox.Text = text;
            window.Controls.Add(textBox);
        }

        #endregion

        #region Gestion des événements

        /// <summary>
        /// Remplace l'interface actuelle par celle de gestion du stock.
        /// </summary>
        public void backClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceStockManagement(window, user));
        }

        /// <summary>
        /// Vérifie si les entrées sont valides, demande confirmation et ajoute le produit à la base en fonction.
        /// </summary>
        public void newProductClick(object sender, EventArgs e)
        {
            // Vérification des entrées
            if (_sellCost.Value <= 0)
            {
                MessageBox.Show("Veuillez entrez un prix supérieur à 0 pour le produit.", "Informations non valides", MessageBoxButtons.OK);
                return;
            }
            if (_name.Text.Equals("Nom") || _name.Text.Length == 0)
            {
                MessageBox.Show("Veuillez entrer un nom pour votre produit.", "Informations non valides", MessageBoxButtons.OK);
                return;
            }

            // Demander la confirmation de l'ajout
            string message = "Récapitulation des informations du produit à ajouter :\n\n" +
                             "Nom : " + _name.Text + "\n" +
                             "Type : " + _choosedType.NOMTYPE + "\n" +
                             "Prix de vente : " + _sellCost.Value + " €\n" +
                             "Quantité initiale en stock : " + _quantity.Value + "\n\n" +
                             "Confirmez-vous l'ajout de ce nouveau produit ?";
            DialogResult confirmed = MessageBox.Show(message, "Demande de confirmation", MessageBoxButtons.YesNo);
            if (confirmed == DialogResult.Yes)
            {
                // Vérification de non existence dans la base de données
                if (ProductController.producAlreadyExists(_name.Text))
                {
                    message = "Un produit avec le nom \"" + _name.Text + "\" existe déjà. Voulez-vous l'ajouter quand même ?";
                    confirmed = MessageBox.Show(message, "Conflit détecté", MessageBoxButtons.YesNo);
                    if (confirmed == DialogResult.Yes)
                        ProductController.addProduct(_choosedType, (int)_quantity.Value, _name.Text, _sellCost.Value, DateTime.Today);
                    else
                        return;
                }
                else
                    ProductController.addProduct(_choosedType, (int)_quantity.Value, _name.Text, _sellCost.Value, DateTime.Today);

                MessageBox.Show("Le produit a été ajouté avec succès.", "Informations", MessageBoxButtons.OK);

                window.Controls.Clear();
                window.switchInterface(new InterfaceNewProduct(window, user));
            }
        }

        public void homeClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceHome(window, user));
        }
        private void lastNameEnter(object sender, EventArgs e)
        {
            if (_quantity.Text == "Nom")
            {
                _quantity.Text = "";
                _quantity.ForeColor = Color.Black;
            }
        }

        public void typeSelectedChange(object sender, EventArgs e)
        {
            _choosedType = (TYPE_PRODUIT)_type.Items[_type.SelectedIndex];
        }


        private void lastNameLeave(object sender, EventArgs e)
        {
            if (_quantity.Text.Length == 0)
            {
                _quantity.Text = "Nom";
                _quantity.ForeColor = Color.Gray;
            }
        }

        private void nameEnter(object sender, EventArgs e)
        {
            if (_name.Text == "Nom")
            {
                _name.Text = "";
                _name.ForeColor = Color.Black;
            }
        }

        private void nameLeave(object sender, EventArgs e)
        {
            if (_name.Text.Length == 0)
            {
                _name.Text = "Nom";
                _name.ForeColor = Color.Gray;
            }
        }

        /// <summary>
        /// Remplace l'interface actuelle par celle de gestion des types de produit.
        /// </summary>
        private void onTypeManagementButtonClick(object sender, EventArgs eventArgs)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceProductsTypeManagement(window, user));
        }

        #endregion

        public override void load()
        {
            _header.load("Plannimaux - Nouveau produit");
            _footer.load();
            generateTextBoxes();
            generateBox();
            generateNumUpDown();
            generateButtons();
            generateLabels();
        }
    }
}
