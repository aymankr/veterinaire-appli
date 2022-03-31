using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api.controllers;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceSell : AInterface
    {
        private Header _header;
        private Footer _footer;

        private Label _productsLabel;
        private CheckedListBox _products;
        private ProductTypesComboBox _productsType;
        private TextBox _productsNameFilter;

        private Label _clientsLabel;
        private ListBox _clients;
        private TextBox _clientsNameFilter;

        private Label _sellVisualLabel;
        private GroupBox _sellVisual;
        private Label _total;

        private UIButton _stockButton, _clientsButton, _sellButton;

        public InterfaceSell(MainWindow window, SALARIE user) : base(window, user)
        {
            _header = new Header(window);
            _footer = new Footer(window, user);

            generateProductsList();
            generateProductsType();
            generateProductsNameFilter();

            generateClientsList();
            generateClientsNameFilter();

            generateSellVisual();

            generateButtons();
            generateLabels();
        }

        #region Génération des composants de l'interface

        private void generateProductsList()
        {
            // Style
            _products = new CheckedListBox();
            _products.Size = new Size(window.Width / 4, window.Height / 2);
            _products.Location = new Point(window.Width / 20, window.Height / 4);
            _products.CheckOnClick = true;

            // Données
            ProductController.getProducts().ForEach(product => _products.Items.Add(product));

            // Evénements
            _products.ItemCheck += onProductCheckedChange;
        }

        private void generateProductsType()
        {
            // Taille & position
            _productsType = new ProductTypesComboBox();
            _productsType.Size = new Size(window.Width / 4, window.Height / 20);
            _productsType.Location = new Point(_products.Left, _products.Top - (10 + _productsType.Height));

            // Evenements
            _productsType.SelectedIndexChanged += onTypeSelected;
        }

        private void generateProductsNameFilter()
        {
            // Taille & position
            _productsNameFilter = new TextBox();
            _productsNameFilter.Size = new Size(window.Width / 4, window.Height / 20);
            _productsNameFilter.Location = new Point(_products.Left, _productsType.Top - (10 + _productsNameFilter.Height));

            // Evenement
            _productsNameFilter.TextChanged += onProductNameFilterType;
        }

        private void generateClientsList()
        {
            // Taille & position
            _clients = new ListBox();
            _clients.Size = new Size(window.Width / 4, window.Height / 2);
            _clients.Location = new Point(_products.Right + window.Width / 20, window.Height / 4);

            // Données
            ClientController.AllClient().ForEach(client => _clients.Items.Add(client));
        }

        private void generateClientsNameFilter()
        {
            // Taille & position
            _clientsNameFilter = new TextBox();
            _clientsNameFilter.Size = new Size(window.Width / 4, window.Height / 20);
            _clientsNameFilter.Location = new Point(_clients.Left, _clients.Top - (10 + _clientsNameFilter.Height));

            // Evenement
            _clientsNameFilter.TextChanged += onClientNameFilterType;
        }

        private void generateSellVisual()
        {
            // Taille & position
            _sellVisual = new GroupBox();
            _sellVisual.Size = new Size(window.Width / 4, window.Height / 2);
            _sellVisual.Location = new Point(_clients.Right + window.Width / 20, window.Height / 4);

            _total = new Label();
            _total.Size = new Size(window.Width / 4, _sellVisual.Height / 12);
            _total.Location = new Point(0, _sellVisual.Bottom - _total.Height);
            _total.Text = "Total : 0 €";
            

            _sellVisual.Controls.Add(_total);
        }

        private void generateButtons()
        {
            _stockButton = new UIButton(UIColor.ORANGE, "Gestion du stock", window.Width / 5);
            _stockButton.Location = new Point(_products.Left + _products.Width / 2 - _stockButton.Width / 2, _products.Bottom + 10);
            _stockButton.Click += onStockButtonClick;

            _clientsButton = new UIButton(UIColor.ORANGE, "Gestion des clients", window.Width / 5);
            _clientsButton.Location = new Point(_clients.Left + _clients.Width / 2 - _clientsButton.Width / 2, _clients.Bottom + 10);
            _clientsButton.Click += onClientsButtonClick;

            _sellButton = new UIButton(UIColor.ORANGE, "Réaliser la vente", window.Width / 5);
            _sellButton.Location = new Point(_sellVisual.Left + _sellVisual.Width / 2 - _sellButton.Width / 2, _sellVisual.Bottom + 10);
            _sellButton.Click += onSellButtonClick;
        }

        private void generateLabels()
        {
            _productsLabel = new Label();
            _productsLabel.Size = new Size(window.Width / 4, window.Height / 20);
            _productsLabel.Location = new Point(_products.Left, _productsNameFilter.Top - (10 + _productsLabel.Height));
            _productsLabel.Text = "Produits";
            _productsLabel.Font = new Font("Poppins", window.Height * 3 / 100);
            _productsLabel.ForeColor = Color.Gray;

            _clientsLabel = new Label();
            _clientsLabel.Size = new Size(window.Width / 4, window.Height / 20);
            _clientsLabel.Location = new Point(_clients.Left, _clientsNameFilter.Top - (10 + _clientsLabel.Height));
            _clientsLabel.Text = "Clients";
            _clientsLabel.Font = new Font("Poppins", window.Height * 3 / 100);
            _clientsLabel.ForeColor = Color.Gray;

            _sellVisualLabel = new Label();
            _sellVisualLabel.Size = new Size(window.Width / 4, window.Height / 20);
            _sellVisualLabel.Location = new Point(_sellVisual.Left, _sellVisual.Top - (10 + _sellVisualLabel.Height));
            _sellVisualLabel.Text = "Vente en cours";
            _sellVisualLabel.Font = new Font("Poppins", window.Height * 3 / 100);
            _sellVisualLabel.ForeColor = Color.Gray;
        }

        #endregion

        #region Gestion des événements

        /// <summary>
        /// Remplace l'interface actuelle par celle de gestion du stock de produits.
        /// </summary>
        private void onStockButtonClick(object sender, EventArgs eventArgs)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceStockManagement(window, user));
        }

        /// <summary>
        /// Remplace l'interface actuelle par celle de gestion des clients.
        /// </summary>
        private void onClientsButtonClick(object sender, EventArgs eventArgs)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceClient(window, user));
        }

        private void onSellButtonClick(object sender, EventArgs eventArgs)
        {
            
        }

        /// <summary>
        /// Trie la liste des produits par rapport au type choisi.
        /// </summary>
        private void onTypeSelected(object sender, EventArgs eventArgs)
        {
            filterProducts();
        }

        
        private void onProductNameFilterType(object sender, EventArgs eventArgs)
        {
            filterProducts();
        }

        /// <summary>
        /// Trie la liste des clients par rapprot au nom ou partie du nom entrée dans le text box.
        /// </summary>
        private void onClientNameFilterType(object sender, EventArgs eventArgs)
        {
            _clients.Items.Clear();

            if (_clientsNameFilter.Text.Length == 0)
            {
                ClientController.AllClient().ForEach(client => _clients.Items.Add(client));
            }
            else
            {
                ClientController.GetClientsByName(_clientsNameFilter.Text).ForEach(client => _clients.Items.Add(client));
            }
        }

        private void onProductCheckedChange(object sender, ItemCheckEventArgs eventArgs)
        {
            window.BeginInvoke((MethodInvoker)(
            () => updateVisual()));
        }

        #endregion

        #region Gestion des tries

        /// <summary>
        /// Trie la liste des produits par rapprot au nom ou partie du nom entrée dans le text box & le type sélectionné.
        /// </summary>
        private void filterProducts()
        {
            _products.Items.Clear();

            if (_productsNameFilter.Text.Length == 0)
            {
                if ((int)_productsType.SelectedValue != -1)
                {
                    TYPE_PRODUIT currentType = ProductController.getTypeById((int)_productsType.SelectedValue);
                    ProductController.getProductsFromType(currentType).ForEach(product => _products.Items.Add(product));
                }
                else
                {
                    ProductController.getProducts().ForEach(product => _products.Items.Add(product));
                }
            }
            else
            {
                if ((int)_productsType.SelectedValue != -1)
                {
                    TYPE_PRODUIT currentType = ProductController.getTypeById((int)_productsType.SelectedValue);
                    ProductController.getProductsByNameAndType(_productsNameFilter.Text, currentType).ForEach(product => _products.Items.Add(product));
                }
                else
                {
                    ProductController.getProductsByName(_productsNameFilter.Text).ForEach(product => _products.Items.Add(product));
                }
            }
        }

        #endregion

        #region Gestion du visuel de la vente en cours

        private void updateVisual()
        {
            for (int i = 0; i < _products.Items.Count; i++)
            {
                PRODUIT product = (PRODUIT)_products.Items[i];

                if (!isProductAdded(product) && _products.GetItemChecked(i))
                    addNewProduct(product);

                else if (isProductAdded(product) && !_products.GetItemChecked(i))
                    removeAddedProduct(product);
            }

            _sellVisual.Refresh();
        }

        private bool isProductAdded(PRODUIT product)
        {
            foreach (Control item in _sellVisual.Controls)
            {
                SellProduct sellProduct = (SellProduct)item;
                if (sellProduct.Product.IDPRODUIT == product.IDPRODUIT)
                    return true;
            }
            return false;
        }

        private void addNewProduct(PRODUIT product)
        {
            int yLocation = _sellVisual.Controls.Count == 0 ? 15 : _sellVisual.Controls[_sellVisual.Controls.Count - 1].Bottom;
            SellProduct sellProduct = new SellProduct(product,
                                                      new Size(_sellVisual.Width - 20, _sellVisual.Height / 15),
                                                      new Point(10, yLocation));
            _sellVisual.Controls.Add(sellProduct);
        }

        private void removeAddedProduct(PRODUIT product)
        {
            for (int i = 0; i < _sellVisual.Controls.Count; i++)
            {
                SellProduct sellProduct = (SellProduct)_sellVisual.Controls[i];
                if (sellProduct.Product.IDPRODUIT == product.IDPRODUIT)
                    _sellVisual.Controls.Remove(_sellVisual.Controls[i]);
            }

            for (int i = 0; i < _sellVisual.Controls.Count; i++)
            {
                _sellVisual.Controls[i].Location = new Point(10, _sellVisual.Height / 15 * i + 15);
            }
        }

        #endregion

        public override void load()
        {
            _header.load("Plannimaux - Réaliser une vente");
            _footer.load();

            window.Controls.Add(_products);
            window.Controls.Add(_productsType);
            window.Controls.Add(_productsNameFilter);
            window.Controls.Add(_productsLabel);
            window.Controls.Add(_stockButton);

            window.Controls.Add(_clients);
            window.Controls.Add(_clientsNameFilter);
            window.Controls.Add(_clientsLabel);
            window.Controls.Add(_clientsButton);

            window.Controls.Add(_sellVisual);
            window.Controls.Add(_sellVisualLabel);
            window.Controls.Add(_sellButton);
        }
    }
}
