using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using Mauxnimale_CE2.ui.clients;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api.controllers;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2.ui.stocks
{
    internal class InterfaceSell : AInterface
    {
        private Header _header;
        private Footer _footer;
        private UIRoundButton _back;

        private Label _productsLabel;
        private CheckedListBox _products;
        private ProductTypesComboBox _productsType;
        private TextBox _productsNameFilter;

        private Label _clientsLabel;
        private ClientsListBox _clients;
        private TextBox _clientsNameFilter;
        CLIENT _currentClient;

        private Label _sellVisualLabel;
        private Panel _sellVisual;
        private List<SellProduct> _productsAdded;
        private Label _total;

        private UIButton _stockButton, _clientsButton, _sellButton;

        public InterfaceSell(MainWindow window, SALARIE user) : base(window, user)
        {
            _header = new Header(window);
            _footer = new Footer(window, user);

            _productsAdded = new List<SellProduct>();
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
            ProductController.getProducts(true).ForEach(product => _products.Items.Add(product));

            // Evénements
            _products.ItemCheck += onProductCheckedChange;

            window.Controls.Add(_products);
        }

        private void generateProductsType()
        {
            // Taille & position
            _productsType = new ProductTypesComboBox();
            _productsType.Size = new Size(window.Width / 4, window.Height / 20);
            _productsType.Location = new Point(_products.Left, _products.Top - (10 + _productsType.Height));

            // Evenements
            _productsType.SelectedIndexChanged += onTypeSelected;

            window.Controls.Add(_productsType);
        }

        private void generateProductsNameFilter()
        {
            // Taille & position
            _productsNameFilter = new TextBox();
            _productsNameFilter.Size = new Size(window.Width / 4, window.Height / 20);
            _productsNameFilter.Location = new Point(_products.Left, _productsType.Top - (10 + _productsNameFilter.Height));

            // Evenement
            _productsNameFilter.TextChanged += onProductNameFilterType;

            window.Controls.Add(_productsNameFilter);
        }

        private void generateClientsList()
        {
            // Taille & position
            _clients = new ClientsListBox();
            _clients.Size = new Size(window.Width / 4, window.Height / 2);
            _clients.Location = new Point(_products.Right + window.Width / 20, window.Height / 4);

            // Evénements
            _clients.SelectedValueChanged += onClientChoosed;

            window.Controls.Add(_clients);
        }

        private void generateClientsNameFilter()
        {
            // Taille & position
            _clientsNameFilter = new TextBox();
            _clientsNameFilter.Size = new Size(window.Width / 4, window.Height / 20);
            _clientsNameFilter.Location = new Point(_clients.Left, _clients.Top - (10 + _clientsNameFilter.Height));

            // Evenement
            _clientsNameFilter.TextChanged += onClientNameFilterType;

            window.Controls.Add(_clientsNameFilter);
        }

        private void generateSellVisual()
        {
            // Taille & position
            _sellVisual = new Panel();
            _sellVisual.AutoScroll = true;
            _sellVisual.Size = new Size(window.Width / 4, window.Height / 2);
            _sellVisual.Location = new Point(_clients.Right + window.Width / 20, window.Height / 4);
            _sellVisual.BorderStyle = BorderStyle.FixedSingle;

            window.Controls.Add(_sellVisual);
        }

        private void generateSellTotal()
        {
            // Total du prix de vente
            _total = new Label();
            _total.Text = "Total : 0 €";
            _total.Font = new Font("Poppins", window.Height / 70);
            _total.Size = TextRenderer.MeasureText(_total.Text, _total.Font);
            _total.Location = new Point(_sellVisual.Width - _total.Width - 2, _sellVisual.Height - _total.Height - 2);

            _sellVisual.Controls.Add(_total);
        }

        private void generateButtons()
        {
            _stockButton = new UIButton(UIColor.ORANGE, "Gestion du stock", window.Width / 5);
            _stockButton.Height = window.Height / 20;
            _stockButton.Location = new Point(_products.Left + _products.Width / 2 - _stockButton.Width / 2, _products.Bottom + 10);
            _stockButton.Click += onStockButtonClick;
            window.Controls.Add(_stockButton);

            _clientsButton = new UIButton(UIColor.ORANGE, "Gestion des clients", window.Width / 5);
            _clientsButton.Height = window.Height / 20;
            _clientsButton.Location = new Point(_clients.Left + _clients.Width / 2 - _clientsButton.Width / 2, _clients.Bottom + 10);
            _clientsButton.Click += onClientsButtonClick;
            window.Controls.Add(_clientsButton);

            _sellButton = new UIButton(UIColor.ORANGE, "Réaliser la vente", window.Width / 5);
            _sellButton.Height = window.Height / 20;
            _sellButton.Location = new Point(_sellVisual.Left + _sellVisual.Width / 2 - _sellButton.Width / 2, _sellVisual.Bottom + 10);
            _sellButton.Click += onSellButtonClick;
            _sellButton.Enabled = false;
            window.Controls.Add(_sellButton);

            _back = new UIRoundButton(window.Width / 20, "<");
            _back.Location = new Point(window.Width * 9 / 10, window.Height / 10);
            _back.Click += new EventHandler(backClick);
            window.Controls.Add(_back);
        }

        private void generateLabels()
        {
            _productsLabel = new Label();
            _productsLabel.Size = new Size(window.Width / 4, window.Height / 20);
            _productsLabel.Location = new Point(_products.Left, _productsNameFilter.Top - (10 + _productsLabel.Height));
            _productsLabel.Text = "Produits";
            _productsLabel.Font = new Font("Poppins", window.Height * 3 / 100);
            _productsLabel.ForeColor = Color.Gray;
            window.Controls.Add(_productsLabel);

            _clientsLabel = new Label();
            _clientsLabel.Size = new Size(window.Width / 4, window.Height / 20);
            _clientsLabel.Location = new Point(_clients.Left, _clientsNameFilter.Top - (10 + _clientsLabel.Height));
            _clientsLabel.Text = "Clients";
            _clientsLabel.Font = new Font("Poppins", window.Height * 3 / 100);
            _clientsLabel.ForeColor = Color.Gray;
            window.Controls.Add(_clientsLabel);

            _sellVisualLabel = new Label();
            _sellVisualLabel.Size = new Size(window.Width / 4, window.Height / 20);
            _sellVisualLabel.Location = new Point(_sellVisual.Left, _sellVisual.Top - (10 + _sellVisualLabel.Height));
            _sellVisualLabel.Text = "Vente en cours";
            _sellVisualLabel.Font = new Font("Poppins", window.Height * 3 / 100);
            _sellVisualLabel.ForeColor = Color.Gray;
            window.Controls.Add(_sellVisualLabel);
        }

        #endregion

        #region Gestion des événements

        /// <summary>
        /// Retourne au menu principal.
        /// </summary>
        public void backClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceHome(window, user));
        }

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

        /// <summary>
        /// Demande la confirmation de la vente et la réalise si l'utilisateur confirme.
        /// </summary>
        private void onSellButtonClick(object sender, EventArgs eventArgs)
        {
            if (_productsAdded.Count == 0)
            {
                MessageBox.Show("Veuillez ajouter au moins un produit à la vente en cours.", "Conditions de ventes non valides", MessageBoxButtons.OK);
                return;
            }
            if (_clients.SelectedValue == null)
            {
                MessageBox.Show("Veuillez choisir un client à qui vendre.", "Conditions de ventes non valides", MessageBoxButtons.OK);
                return;
            }

            string productsToSellStr = "";
            _productsAdded.ForEach(p => productsToSellStr += p.Product.ToString() + " x" + p.QuantityToSell.ToString() + "\n");
            string message = "Résumé de la vente:\n\n" +
                             productsToSellStr + "\n" +
                             _total.Text + "\n" +
                             "Confirmez vous la réalisation de la vente ?";

            DialogResult confirmed = MessageBox.Show(message, "Demande de confirmation", MessageBoxButtons.YesNo);
            if (confirmed == DialogResult.Yes)
            {
                // Enlève les produits vendus du stock
                _productsAdded.ForEach(p => ProductController.setProductQuantity(p.Product, p.Product.QUANTITEENSTOCK - p.QuantityToSell));
                _products.Items.Clear();
                ProductController.getProducts(true).ForEach(product => _products.Items.Add(product));

                // Réinitialiser la fenêtre
                window.Controls.Clear();
                window.switchInterface(new InterfaceSell(window, user));
            }
        }

        /// <summary>
        /// Trie la liste des produits par rapport au type choisi.
        /// </summary>
        private void onTypeSelected(object sender, EventArgs eventArgs)
        {
            filterProducts();
        }

        
        /// <summary>
        /// Filtre les produits par au nom entré.
        /// </summary>
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

        /// <summary>
        /// Ajoute ou retire le produit de la vente en cours et met à jour le visuel.
        /// </summary>
        private void onProductCheckedChange(object sender, ItemCheckEventArgs eventArgs) => window.BeginInvoke((MethodInvoker)(() => updateVisual()));

        /// <summary>
        /// Met à jour le total de la vente avec la nouvelle quantité.
        /// </summary>
        private void onProductQuantityChanged(object sender, EventArgs eventArgs) => updateTotal();

        /// <summary>
        /// Authorise l'utilisateur à réaliser la vente si au moins un produit a été ajouté.
        /// </summary>
        private void onClientChoosed(object sender, EventArgs eventArgs)
        {
            if (_productsAdded.Count > 0 && !_sellButton.Enabled)
                _sellButton.Enabled = true;

            _currentClient = ClientController.GetClientFromID((int)_clients.SelectedValue);
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
                    ProductController.getProducts(true).ForEach(product => _products.Items.Add(product));
                }
            }
            else
            {
                if ((int)_productsType.SelectedValue != -1)
                {
                    TYPE_PRODUIT currentType = ProductController.getTypeById((int)_productsType.SelectedValue);
                    ProductController.getProductsByNameAndType(_productsNameFilter.Text, currentType, true).ForEach(product => _products.Items.Add(product));
                }
                else
                {
                    ProductController.getProductsByName(_productsNameFilter.Text, true).ForEach(product => _products.Items.Add(product));
                }
            }
        }

        #endregion

        #region Gestion du visuel de la vente en cours

        /// <summary>
        /// Met à jour la vue de la vente en cours.
        /// </summary>
        private void updateVisual()
        {
            decimal totalPrice = 0;

            // Mise à jour de la liste des produits
            for (int i = 0; i < _products.Items.Count; i++)
            {
                PRODUIT product = (PRODUIT)_products.Items[i];

                if (!isProductAdded(product) && _products.GetItemChecked(i))
                {
                    addNewProduct(product);
                    totalPrice += product.PRIXDEVENTECLIENT;
                }
                else if (isProductAdded(product))
                {
                    if (!_products.GetItemChecked(i))
                        removeAddedProduct(product);
                    else
                        totalPrice += product.PRIXDEVENTECLIENT;
                }
            }
            updateTotal();

            if (_productsAdded.Count > 0 && _currentClient != null)
                _sellButton.Enabled = true;
            else
            {
                if (_sellButton.Enabled)
                    _sellButton.Enabled = false;            
            }

            _sellVisual.Refresh();
        }

        /// <summary>
        /// Détermine si un produit est ajotué dans la vente en cours.
        /// </summary>
        /// <param name="product">Le produit en question</param>
        /// <returns>true si le produit est dans la vente en cours, false sinon.</returns>
        private bool isProductAdded(PRODUIT product)
        {
            foreach (SellProduct sellProduct in _productsAdded)
            {
                if (sellProduct.Product.IDPRODUIT == product.IDPRODUIT)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Ajoute un nouveau produit à la vente en cours.
        /// </summary>
        /// <param name="product">Le produit à ajouter</param>
        private void addNewProduct(PRODUIT product)
        {
            // Taille & position
            int yLocation = _productsAdded.Count == 0 ? 15 : _productsAdded[_productsAdded.Count - 1].Bottom;
            SellProduct sellProduct = new SellProduct(product,
                                                      new Size(_sellVisual.Width - 20, _sellVisual.Height / 15),
                                                      new Point(10, yLocation));
            // Evénements
            sellProduct.QuantityChooser.ValueChanged += onProductQuantityChanged;

            _productsAdded.Add(sellProduct);
            _sellVisual.Controls.Add(sellProduct);
        }

        /// <summary>
        /// Supprime un produit ajouté à la vente de la vente en cours.
        /// </summary>
        /// <param name="product">Le produit à supprimer</param>
        private void removeAddedProduct(PRODUIT product)
        {
            for (int i = 1; i < _sellVisual.Controls.Count; i++)
            {
                SellProduct sellProduct = (SellProduct)_sellVisual.Controls[i];
                if (sellProduct.Product.IDPRODUIT == product.IDPRODUIT)
                {
                    _sellVisual.Controls.Remove(_sellVisual.Controls[i]);
                    _productsAdded.Remove(sellProduct);
                }
            }

            for (int i = 1; i < _sellVisual.Controls.Count; i++)
            {
                _sellVisual.Controls[i].Location = new Point(10, _sellVisual.Height / 15 * (i - 1) + 15);
            }
        }

        /// <summary>
        /// Calcule le prix total de vente et l'affiche en fonction des produits ajoutés et de la quantité choisi.
        /// </summary>
        private void updateTotal()
        {
            decimal totalPrice = 0;

            // Calcul tu prix total
            _productsAdded.ForEach(p => totalPrice += p.Product.PRIXDEVENTECLIENT * p.QuantityToSell);

            _total.Text = "Total : " + totalPrice + " €";
            _total.Size = TextRenderer.MeasureText(_total.Text, _total.Font);
            _total.Location = new Point(_sellVisual.Width - _total.Width - 2, _sellVisual.Height - _total.Height - 2);
        }

        #endregion

        public override void load()
        {
            _header.load("Plannimaux - Réaliser une vente");
            _footer.load();

            generateProductsList();
            generateProductsType();
            generateProductsNameFilter();

            generateClientsList();
            generateClientsNameFilter();

            generateSellVisual();
            generateSellTotal();

            generateButtons();
            generateLabels();
        }
    }
}
