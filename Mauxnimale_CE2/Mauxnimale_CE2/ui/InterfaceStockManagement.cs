using Mauxnimale_CE2.api.controllers;
using Mauxnimale_CE2.api.entities;
﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceStockManagement : AInterface
    {
        Header header;
        Footer footer;
        ComboBox type;
        PRODUIT choosed;
        UIRoundButton back;
        UIButton newProduct, changeProduct, deleteProduct;
        List<TYPE_PRODUIT> productType = new List<TYPE_PRODUIT>();
        ListBox products;
        List<PRODUIT> productsList = new List<PRODUIT>();

        /// <summary>
        /// Constructeur de l'interface
        /// </summary>
        /// <param name="window"></param>
        /// <param name="user"></param>
        public InterfaceStockManagement(MainWindow window, SALARIE user) : base(window, user)
        {
            header = new Header(this.window);
            footer = new Footer(this.window, user);
        }

        /// <summary>
        /// Fonction permettant de générer l'interface
        /// </summary>
        public override void load()
        {
            header.load("Plannimaux - Gestion des stocks");
            footer.load();
            generateBox();
            generateButton();
        }

        public void generateBox()
        {
            type = new ComboBox();
            foreach (TYPE_PRODUIT typeProduct in ProductController.getTypes())
            {
                type.Items.Add(typeProduct);
            }
            type.Size = new System.Drawing.Size(window.Width / 2, window.Height / 7);
            type.Font = new System.Drawing.Font("Poppins", window.Height / 40);
            type.Location = new Point(window.Width / 4, window.Height * 3 / 15);
            type.ForeColor = Color.Gray;
            type.SelectedIndexChanged += new EventHandler(typeSelectedChange);
            window.Controls.Add(type);


            products = new ListBox();
            products.Size = new System.Drawing.Size(window.Width / 2, window.Height / 4);
            products.Font = new System.Drawing.Font("Poppins", window.Height / 60);
            products.Location = new Point(window.Width / 4, window.Height * 5 / 15);
            products.ForeColor = Color.Gray;
            products.SelectedIndexChanged += new EventHandler(productSelectedChange);

            window.Controls.Add(products);
        }

        /// <summary>
        /// Génère les bouttons
        /// </summary>
        public void generateButton()
        {
            newProduct = new UIButton(UIColor.ORANGE, "Nouveau Produit", Math.Min(window.Width / 4, window.Height / 3));
            newProduct.Location = new System.Drawing.Point(window.Width * 5 / 8, window.Height * 65 / 100);
            newProduct.Click += new EventHandler(newProductClick);
            window.Controls.Add(newProduct);

            deleteProduct = new UIButton(UIColor.ORANGE, "Supprimer Produit", Math.Min(window.Width / 4, window.Height / 3));
            deleteProduct.Location = new System.Drawing.Point(window.Width * 3 / 8, window.Height * 65 / 100);
            deleteProduct.Click += new EventHandler(deleteProductClick);
            window.Controls.Add(deleteProduct);

            changeProduct = new UIButton(UIColor.ORANGE, "Changer Produit", Math.Min(window.Width / 4, window.Height / 3));
            changeProduct.Location = new System.Drawing.Point(window.Width / 8, window.Height * 65 / 100);
            changeProduct.Click += new EventHandler(changeProductClick);
            window.Controls.Add(changeProduct);

            back = new UIRoundButton(window.Width / 20, "<");
            back.Location = new System.Drawing.Point(window.Width * 9 / 10, window.Height / 10);
            back.Click += new EventHandler(backClick);
            window.Controls.Add(back);
        }


        public void backClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceHome(window, user));
        }

        public void newProductClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceStockNewProduct(window, user));
        }

        public void deleteProductClick(object sender, EventArgs e)
        {
            if(choosed != null)
            {
                ProductController.removeProduct(choosed);
                choosed = null;
                products.Items.Clear();
                foreach (PRODUIT product in ProductController.getProductsFromType((TYPE_PRODUIT)type.Items[type.SelectedIndex]))
                {
                    products.Items.Add(product);
                }
            }
        }

        public void changeProductClick(object sender, EventArgs e)
        {
            if(choosed != null)
            {
                window.Controls.Clear();
                window.switchInterface(new InterfaceChangeStock(window, user, choosed));
            }
        }

        public void typeSelectedChange(object sender, EventArgs e)
        {
            products.Items.Clear();
            if(ProductController.getProductsFromType((TYPE_PRODUIT)type.Items[type.SelectedIndex])!= null)
            {
                foreach (PRODUIT product in ProductController.getProductsFromType((TYPE_PRODUIT)type.Items[type.SelectedIndex]))
                {
                    products.Items.Add(product);
                }
            }
        }
        
        public void productSelectedChange(object sender, EventArgs e)
        {
            if(products.SelectedIndex != -1)
            {
                choosed = (PRODUIT)products.Items[products.SelectedIndex];
            }
        }
    }
}
