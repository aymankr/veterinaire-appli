using System;
using System.Drawing;
using System.Windows.Forms;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api.controllers;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2.ui.components
{
    public class StockProduct : SplitContainer
    {
        private MainWindow _window;
        private SALARIE _user;

        public PRODUIT Product;

        private Label _productDescription;
        private NumericUpDown _quantityChooser;
        private UIButton _updateButton, _removeButton, _validateStockButton;

        private Action<StockProduct> _updateViewFunction;

        public StockProduct(PRODUIT product, Size size, Point location, Action<StockProduct> updateViewFunction, MainWindow window, SALARIE user)
        {
            _window = window;
            _user = user;
            Product = product;
            _updateViewFunction = updateViewFunction;

            Size = size;
            Location = location;
            SplitterDistance = Width / 4;

            generateDescription();
            generateStockQuantityChooser();
            generateButtons();
        }

        #region Génération des composants

        private void generateDescription()
        {
            _productDescription = new Label();
            _productDescription.Size = new Size(Panel1.Width / 4 * 3, Panel1.Height);
            _productDescription.Text = Product.NOMPRODUIT + " : " + Product.PRIXDEVENTECLIENT;

            Panel1.Controls.Add(_productDescription);
        }

        private void generateStockQuantityChooser()
        {
            // Taille & position
            _quantityChooser = new NumericUpDown();
            _quantityChooser.Size = new Size(Panel1.Width / 4, Panel1.Height);
            _quantityChooser.Location = new Point(Panel1.Width / 4 * 3 + 5, 0);

            // Valeurs
            _quantityChooser.Minimum = 0;
            _quantityChooser.Maximum = 9999;
            _quantityChooser.Value = Product.QUANTITEENSTOCK;

            // Evenements
            _quantityChooser.ValueChanged += onQuantityChanged;

            Panel1.Controls.Add(_quantityChooser);
        }

        private void generateButtons()
        {
            _updateButton = new UIButton(UIColor.LIGHTBLUE, "Modifier", Panel2.Width / 4);
            _updateButton.Height = Panel2.Height / 2;
            _updateButton.Click += onUpdateButtonClick;

            _validateStockButton = new UIButton(UIColor.LIGHTBLUE, "Valider le stock", Panel2.Width / 4);
            _validateStockButton.Height = Panel2.Height / 2;
            _validateStockButton.Location = new Point(Panel2.Width / 3, 0);
            _validateStockButton.Enabled = false;
            _validateStockButton.Click += onValideStockButtonClick;

            _removeButton = new UIButton(UIColor.ORANGE, "Supprimer", Panel2.Width / 4);
            _removeButton.Height = Panel2.Height / 2;
            _removeButton.Location = new Point(Panel2.Width / 3 * 2, 0);
            _removeButton.Click += onRemoveButtonClick;

            Panel2.Controls.Add(_updateButton);
            Panel2.Controls.Add(_validateStockButton); 
            Panel2.Controls.Add(_removeButton);
        }

        #endregion

        #region Gestion des événements

        /// <summary>
        /// Authorise à changer la quantité en stock si la valeur est différente de celle de base.
        /// </summary>
        private void onQuantityChanged(object sender, EventArgs eventArgs)
        {
            if (_quantityChooser.Value == Product.QUANTITEENSTOCK && _validateStockButton.Enabled)
                _validateStockButton.Enabled = false;
            else if (_quantityChooser.Value != Product.QUANTITEENSTOCK && !_validateStockButton.Enabled)
                _validateStockButton.Enabled = true;
        }

        /// <summary>
        /// Demande la confirmation et supprime le produit de la bd.
        /// </summary>
        private void onRemoveButtonClick(object sender, EventArgs eventArgs)
        {
            string message = "ATTENTION: Cette action ne met pas le stock du produit à 0 mais le supprime définitvement.\n" +
                             "Etes-vous sûr de vouloir supprimer ce produit ?";
            DialogResult confirmed = MessageBox.Show(message, "Demande de confirmation", MessageBoxButtons.YesNo);
            if (confirmed == DialogResult.Yes)
            {
                ProductController.removeProduct(Product);
                MessageBox.Show("Le produit a été supprimé avec succès.", "Information", MessageBoxButtons.OK);
                _updateViewFunction(this);
            }

        }

        /// <summary>
        /// Remplace la fenêtre actuelle par celle de modification d'un produit.
        /// </summary>
        private void onUpdateButtonClick(object sender, EventArgs eventArgs)
        {
            _window.Controls.Clear();
            _window.switchInterface(new InterfaceUpdateProduct(_window, _user, Product));
        }

        /// <summary>
        /// Demande de confirmer le nouveau stock.
        /// </summary>
        private void onValideStockButtonClick(object sender, EventArgs eventArgs)
        {
            string message = "Ancienne quantité en stock : " + Product.QUANTITEENSTOCK + "\n" +
                             "Nouvelle quantité en stock : " + _quantityChooser.Value + "\n\n" +
                             "Confirmez-vous le changement de quantité de ce produit en stock ?";
            DialogResult confirmed = MessageBox.Show(message, "Demande de confirmation", MessageBoxButtons.YesNo);
            if (confirmed == DialogResult.Yes)
            {
                ProductController.setProductQuantity(Product, (int)_quantityChooser.Value);
                generateDescription();
            }
        }

        #endregion
    }
}
