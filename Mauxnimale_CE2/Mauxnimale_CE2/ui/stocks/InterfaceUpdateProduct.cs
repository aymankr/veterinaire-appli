﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api.controllers;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2.ui.stocks
{
    public class InterfaceUpdateProduct : AInterface
    {
        private PRODUIT _product;

        private Header _header;
        private Footer _footer;

        private ComboBox _typeList;
        private TYPE_PRODUIT _choosedType;
        private UIRoundButton _backButton, _homeButton;
        private UIButton _addProductButton, _resetType, _resetQuantity, _resetSellPrice, _typeManagementButton;

        private NumericUpDown _quantity, _sellPrice;
        private Label _lQuantity, _lResellCost, _lName, _lType;
        private TextBox _name;

        public InterfaceUpdateProduct(MainWindow window, SALARIE user, PRODUIT product) : base(window, user)
        {
            _header = new Header(window);
            _footer = new Footer(window, user);
            _product = product;
            _choosedType = null;
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
            _addProductButton = new UIButton(UIColor.ORANGE, "Modifier les informations", Math.Min(window.Width / 4, window.Height / 3));
            _addProductButton.Location = new Point(window.Width * 2 / 5, window.Height * 75 / 100);
            _addProductButton.Click += onModifyButtonClick;
            window.Controls.Add(_addProductButton);

            _backButton = new UIRoundButton(window.Width / 20, "<");
            _backButton.Location = new Point(window.Width * 9 / 10, window.Height / 10);
            _backButton.Click += backClick;
            window.Controls.Add(_backButton);

            _homeButton = new UIRoundButton(window.Width / 20, "«");
            _homeButton.Location = new Point(window.Width * 8 / 10, window.Height / 10);
            _homeButton.Click += homeClick;
            window.Controls.Add(_homeButton);

            _typeManagementButton = new UIButton(UIColor.ORANGE, "Gérer les types", window.Width / 5);
            _typeManagementButton.Height = _typeList.Height;
            _typeManagementButton.Location = new Point(_typeList.Right + 25, _typeList.Top - _typeManagementButton.Height / 2);
            _typeManagementButton.Click += onTypeManagementButtonClick;
            window.Controls.Add(_typeManagementButton);

            _resetType = new UIButton(UIColor.LIGHTBLUE, "Réinitialiser", window.Width / 5);
            _resetType.Height = _typeList.Height;
            _resetType.Location = new Point(_typeList.Right + 25, _typeManagementButton.Bottom + 5);
            _resetType.Click += onResetTypeClick;
            window.Controls.Add(_resetType);


            _resetSellPrice = new UIButton(UIColor.LIGHTBLUE, "Réinitialiser", window.Width / 5);
            _resetSellPrice.Height = _sellPrice.Height;
            _resetSellPrice.Location = new Point(_sellPrice.Right + 25, _sellPrice.Top);
            _resetSellPrice.Click += onResetSellPriceClick;
            window.Controls.Add(_resetSellPrice);

            _resetQuantity = new UIButton(UIColor.LIGHTBLUE, "Réinitialiser", window.Width / 5);
            _resetQuantity.Height = _quantity.Height;
            _resetQuantity.Location = new Point(_quantity.Right + 25, _quantity.Top);
            _resetQuantity.Click += onResetQuantityClick;
            window.Controls.Add(_resetQuantity);
        }

        public void setLabel(Label l, string s)
        {
            l.Font = new Font("Poppins", window.Height * 2 / 100);
            l.ForeColor = UIColor.LIGHTBLUE;
            l.Text = s;
            l.Size = new Size(window.Width / 3, window.Height * 6 / 100);
            window.Controls.Add(l);
        }

        private void generateTypeList()
        {
            _typeList = new ComboBox();
            foreach (TYPE_PRODUIT typeProduct in ProductController.getTypes())
            {
                _typeList.Items.Add(typeProduct);
            }
            _typeList.Size = new Size(window.Width / 2, window.Height / 7);
            _typeList.DropDownStyle = ComboBoxStyle.DropDownList;
            _typeList.Font = new Font("Poppins", window.Height / 40);
            _typeList.Location = new Point(window.Width / 4, window.Height * 5 / 15);
            _typeList.ForeColor = Color.Gray;
            _typeList.SelectedIndexChanged += new EventHandler(typeSelectedChange);
            selectProductType();
            window.Controls.Add(_typeList);
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
            _name.Text = _product.NOMPRODUIT;
            window.Controls.Add(_name);
        }

        public void generateNumUpDown()
        {
            _sellPrice = new NumericUpDown();
            _sellPrice.Size = new Size(window.Width / 2, window.Height * 5 / 100);
            _sellPrice.Location = new Point(window.Width / 4, window.Height * 52 / 100);
            _sellPrice.Font = new Font("Poppins", window.Height * 3 / 100);
            _sellPrice.ForeColor = Color.Black;
            _sellPrice.Minimum = 0;
            _sellPrice.Maximum = 100000;
            _sellPrice.Value = _product.PRIXDEVENTECLIENT;
            window.Controls.Add(_sellPrice);

            _quantity = new NumericUpDown();
            _quantity.Size = new Size(window.Width / 2, _quantity.Height);
            _quantity.Location = new Point(window.Width / 4, window.Height * 42 / 100);
            _quantity.Font = new Font("Poppins", window.Height * 3 / 100);
            _quantity.ForeColor = Color.Black;
            _quantity.Minimum = 0;
            _quantity.Maximum = 100000;
            _quantity.Value = _product.QUANTITEENSTOCK;
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

        public void homeClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceHome(window, user));
        }

        public void typeSelectedChange(object sender, EventArgs e)
        {
            _choosedType = (TYPE_PRODUIT)_typeList.Items[_typeList.SelectedIndex];
        }

        private void nameEnter(object sender, EventArgs e)
        {
            if (_name.Text == _product.NOMPRODUIT)
            {
                _name.Text = "";
                _name.ForeColor = Color.Black;
            }
        }

        private void nameLeave(object sender, EventArgs e)
        {
            if (_name.Text.Length == 0)
            {
                _name.Text = _product.NOMPRODUIT;
                _name.ForeColor = Color.Gray;
            }
        }

        /// <summary>
        /// Sélectionne la valeur par défaut (avant modification) pour le type du produit.
        /// </summary>
        private void onResetTypeClick(object sender, EventArgs eventArgs) => selectProductType();

        /// <summary>
        /// Sélectionne la valeur par défaut (avant modification) pour la quantité en stock.
        /// </summary>
        private void onResetQuantityClick(object sender, EventArgs eventArgs) => _quantity.Value = _product.QUANTITEENSTOCK;

        /// <summary>
        /// Sélectionne la valeur par défaut (avant modification) pour la quantité en stock.
        /// </summary>
        private void onResetSellPriceClick(object sender, EventArgs eventArgs) => _sellPrice.Value = _product.PRIXDEVENTECLIENT;


        private void onModifyButtonClick(object sender, EventArgs eventArgs)
        {
            // Vérifier qu'au moins une information a été modifiée
            if (_name.Text == _product.NOMPRODUIT && _choosedType == _product.TYPE_PRODUIT && 
                _quantity.Value == _product.QUANTITEENSTOCK && _sellPrice.Value == _product.PRIXDEVENTECLIENT)
            {
                MessageBox.Show("Aucune information n'a été faite.", "Modification invalides", MessageBoxButtons.OK);
                return;
            }
            // Vérifier que les informations sont valides
            if (_sellPrice.Value <= 0)
            {
                MessageBox.Show("Veuillez entrez un prix supérieur à 0 pour le produit.", "Informations non valides", MessageBoxButtons.OK);
                return;
            }
            if (_name.Text.Equals("Nom") || _name.Text.Length == 0)
            {
                MessageBox.Show("Veuillez entrer un nom pour votre produit.", "Informations non valides", MessageBoxButtons.OK);
                return;
            }

            string message = "Récapitulation des modifications :\n\n" +
                             (_name.Text == _product.NOMPRODUIT ? "" : "Ancien nom : " + _product.NOMPRODUIT + "     Nouveau nom : " + _name.Text + "\n") +
                             (_choosedType == _product.TYPE_PRODUIT ? "" : "Ancien type : " + _product.TYPE_PRODUIT.NOMTYPE + "     Nouveau type : " + _choosedType.NOMTYPE + "\n") +
                             (_quantity.Value == _product.QUANTITEENSTOCK ? "" :
                                "Ancienne quantité en stock : " + _product.QUANTITEENSTOCK + "     Nouvelle quantité : " + _quantity.Value + "\n") +
                            (_sellPrice.Value == _product.PRIXDEVENTECLIENT ? "" :
                                "Ancien prix de vente : " + _product.PRIXDEVENTECLIENT + "     Nouveau prix de vente : " + _sellPrice.Value + "\n\n") +
                            "Confirmez-vous ces modifications ?";
            DialogResult confirmed = MessageBox.Show(message, "Demande de confirmation", MessageBoxButtons.YesNo);
            if (confirmed == DialogResult.Yes)
            {
                ProductController.updateProductInfos(_product, _name.Text, _choosedType, _sellPrice.Value, (int)_quantity.Value);
                MessageBox.Show("Les modifications ont bien été appliquées.", "Informations", MessageBoxButtons.OK);
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

        #region Gestion des valeurs par défauts

        private void selectProductType()
        {
            for (int i = 0; i < _typeList.Items.Count; i++)
            {
                TYPE_PRODUIT type = (TYPE_PRODUIT)_typeList.Items[i];
                if (type.IDTYPE == _product.TYPE_PRODUIT.IDTYPE)
                {
                    _typeList.SelectedIndex = i;
                    break;
                }
            }
        }

        #endregion

        public override void load()
        {
            _header.load("Mauxnimal - Modifier un produit");
            _footer.load();
            generateTextBoxes();
            generateTypeList();
            generateNumUpDown();
            generateButtons();
            generateLabels();
        }
    }
}
