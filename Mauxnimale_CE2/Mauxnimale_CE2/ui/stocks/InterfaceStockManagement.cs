﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api.controllers;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2.ui.stocks
{
    public class InterfaceStockManagement : AInterface
    {
        private Header _header;
        private Footer _footer;
        private UIRoundButton _back;

        private List<StockProduct> _products;
        private Panel _productsContainer;
        private ProductTypesComboBox _productsType;
        private TextBox _productsNameFilter;

        private UIButton _newProductButton, _typeButton;


        /// <summary>
        /// Constructeur de l'interface
        /// </summary>
        /// <param name="window"></param>
        /// <param name="user"></param>
        public InterfaceStockManagement(MainWindow window, SALARIE user) : base(window, user)
        {
            _header = new Header(window);
            _footer = new Footer(window, user);
            _products = new List<StockProduct>();
        }


        #region Génération des composants de l'interface

        private void generateProductContainer()
        {
            // Taille & position
            _productsContainer = new Panel();
            _productsContainer.Size = new Size(window.Width - 200, window.Height / 2);
            _productsContainer.Location = new Point(window.Width / 2 - _productsContainer.Width / 2, _back.Bottom + _back.Height + 5);

            // Styles
            _productsContainer.AutoScroll = true;
            _productsContainer.BorderStyle = BorderStyle.FixedSingle;

            // Données
            addAllProducts();

            window.Controls.Add(_productsContainer);
        }

        private void generateProductsType()
        {
            // Taille & position
            _productsType = new ProductTypesComboBox();
            _productsType.Size = new Size(_productsContainer.Width, window.Height / 20);
            _productsType.Location = new Point(_productsContainer.Left, _productsContainer.Top - (10 + _productsType.Height));

            // Evenements
            _productsType.SelectedIndexChanged += onTypeSelected;

            window.Controls.Add(_productsType);
        }

        private void generateProductsNameFilter()
        {
            // Taille & position
            _productsNameFilter = new TextBox();
            _productsNameFilter.Size = new Size(_productsContainer.Width, window.Height / 20);
            _productsNameFilter.Location = new Point(_productsContainer.Left, _productsType.Top - (10 + _productsNameFilter.Height));

            // Evenement
            _productsNameFilter.TextChanged += onProductNameFilterType;

            window.Controls.Add(_productsNameFilter);
        }

        private void generateBackButton()
        {
            _back = new UIRoundButton(window.Width / 20, "<");
            _back.Location = new Point(window.Width * 9 / 10, window.Height / 10);
            _back.Click += new EventHandler(backClick);
            window.Controls.Add(_back);
        }

        private void generateNewProductButton()
        {
            _newProductButton = new UIButton(UIColor.ORANGE, "Ajouter un nouveau produit", window.Width / 8);
            _newProductButton.Height = window.Height / 20;
            _newProductButton.Location = new Point(window.Width / 2 - _newProductButton.Width - 20, _productsContainer.Bottom + 15);
            _newProductButton.Click += onNewProductButtonClick;
            window.Controls.Add(_newProductButton);
        }

        private void generateTypeButton()
        {
            _typeButton = new UIButton(UIColor.ORANGE, "Gérer les types de produit", window.Width / 8);
            _typeButton.Height = _newProductButton.Height;
            _typeButton.Location = new Point(window.Width / 2 + 20, _newProductButton.Top);
            _typeButton.Click += onTypeButtonClick;
            window.Controls.Add(_typeButton);
        }

        #endregion

        #region Gestion des événements

        /// <summary>
        /// Retourne au menu principal.
        /// </summary>
        private void backClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceHome(window, user));
        }

        /// <summary>
        /// Remplace l'interface actuelle par celle de l'ajout d'un produit.
        /// </summary>
        private void onNewProductButtonClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceNewProduct(window, user));
        }

        /// <summary>
        /// Remplace l'interface actuelle par celle de gestion des types de produit.
        /// </summary>
        private void onTypeButtonClick(object sender, EventArgs eventArgs)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceProductsTypeManagement(window, user));
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

        #endregion

        #region Gestion des tries

        /// <summary>
        /// Trie la liste des produits par rapprot au nom ou partie du nom entrée dans le text box & le type sélectionné.
        /// </summary>
        private void filterProducts()
        {
            if (_productsNameFilter.Text.Length == 0)
            {
                if ((int)_productsType.SelectedValue != -1)
                {
                    TYPE_PRODUIT currentType = ProductController.getTypeById((int)_productsType.SelectedValue);
                    addProductsFromType(currentType);
                }
                else
                    addAllProducts();
            }
            else
            {
                if ((int)_productsType.SelectedValue != -1)
                {
                    TYPE_PRODUIT currentType = ProductController.getTypeById((int)_productsType.SelectedValue);
                    addProductsByNameAndType(_productsNameFilter.Text, currentType);
                }
                else
                {
                    addProductsByName(_productsNameFilter.Text);
                }
            }
        }

        #endregion

        #region Gestion des produits

        /// <summary>
        /// Ajoute tous les produits de la bd à la liste.
        /// </summary>
        private void addAllProducts()
        {
            _productsContainer.Controls.Clear();
            _products.Clear();

            int i = 0;
            ProductController.getProducts(false).ForEach(product =>
            {
                StockProduct stockProduct = new StockProduct(product, 
                                                             new Size(_productsContainer.Width, _productsContainer.Height / 4),
                                                             new Point(0, i * (_productsContainer.Height / 4 + 10)),
                                                             removeProductFromView,
                                                             window, user);
                stockProduct.Font = new Font("Poppins", window.Height * 2    / 100);
                _products.Add(stockProduct);
                _productsContainer.Controls.Add(stockProduct);
                i++;
            });
        }

        /// <summary>
        /// Ajoute tous les produits de la bd ayant le type donné à la liste.
        /// </summary>
        private void addProductsFromType(TYPE_PRODUIT type)
        {
            _productsContainer.Controls.Clear();
            _products.Clear();

            int i = 0;
            ProductController.getProductsFromType(type).ForEach(product =>
            {
                StockProduct stockProduct = new StockProduct(product,
                                                             new Size(_productsContainer.Width, _productsContainer.Height / 4),
                                                             new Point(0, i * (_productsContainer.Height / 4 + 10)),
                                                             removeProductFromView,
                                                             window, user);
                _products.Add(stockProduct);
                _productsContainer.Controls.Add(stockProduct);
                i++;
            });
        }

        /// <summary>
        /// Ajoute tous les produits de la bd ayant un nom qui contient la chaîne donné à la liste.
        /// </summary>
        private void addProductsByName(string name)
        {
            _productsContainer.Controls.Clear();
            _products.Clear();

            int i = 0;
            ProductController.getProductsByName(name, false).ForEach(product =>
            {
                StockProduct stockProduct = new StockProduct(product,
                                                             new Size(_productsContainer.Width, _productsContainer.Height / 4),
                                                             new Point(0, i * (_productsContainer.Height / 4 + 10)),
                                                             removeProductFromView,
                                                             window, user);
                _products.Add(stockProduct);
                _productsContainer.Controls.Add(stockProduct);
                i++;
            });
        }

        /// <summary>
        /// Ajoute tous les produits de la bd ayant un nom qui contient la chaîne donné et le type donné à la liste.
        /// </summary>
        private void addProductsByNameAndType(string name, TYPE_PRODUIT type)
        {
            _productsContainer.Controls.Clear();
            _products.Clear();

            int i = 0;
            ProductController.getProductsByNameAndType(name, type, false).ForEach(product =>
            {
                StockProduct stockProduct = new StockProduct(product,
                                                             new Size(_productsContainer.Width, _productsContainer.Height / 4),
                                                             new Point(0, i * (_productsContainer.Height / 4 + 10)),
                                                             removeProductFromView,
                                                             window, user);
                _products.Add(stockProduct);
                _productsContainer.Controls.Add(stockProduct);
                i++;
            });
        }

        /// <summary>
        /// Supprime le produit donné de la vue.
        /// </summary>
        /// <param name="stockProduct">Le produit à supprimer</param>
        private void removeProductFromView(StockProduct stockProduct)
        {
            _products.Remove(stockProduct);
            _productsContainer.Controls.Remove(stockProduct);
        }

        #endregion

        public override void load()
        {
            _header.load("Mauxnimal - Gestion des stocks");
            _footer.load();

            generateBackButton();
            generateProductContainer();
            generateProductsType();
            generateProductsNameFilter();
            generateNewProductButton();
            generateTypeButton();
        }
    }
}
