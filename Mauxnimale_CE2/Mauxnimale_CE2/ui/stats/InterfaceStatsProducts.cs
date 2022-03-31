using System;
using System.Drawing;
using System.Windows.Forms;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.api.controllers;
using System.Collections.Generic;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceStatsProducts : AInterface
    {
        readonly Header header;
        readonly Footer footer;

        Label allProductLabel, secondProductLabel, firstProductLabel, thirdProductLabel;
        TextBox totalProduct, totalSecondProduct, totalFirstProduct, totalThirdProduct;

        UIRoundButton backButton;

        public InterfaceStatsProducts(MainWindow window, SALARIE user) : base(window, user)
        {
            header = new Header(window);
            footer = new Footer(window, user);
        }

        public override void load()
        {
            header.load("Statistique des produits");
            footer.load();

            CreateBackButton();
            GenerateStatsStockTotal();
            GenerateValueSellTotal();
        }

        /// <summary>
        /// Permet de générer les éléments qui vont contenir les données. 
        /// </summary>
        private void GenerateStatsStockTotal()
        {
            allProductLabel = new Label()
            {
                Text = "Nombre total de produits",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = UIColor.DARKBLUE,
                Size = new Size(window.Width * 4 / 10, window.Height * 1 / 10),
                Location = new Point(window.Width * 4 / 14, window.Height * 2 / 20),
            };
            window.Controls.Add(allProductLabel);

            totalProduct = new TextBox()
            {
                Font = new Font("Poppins", window.Height * 4 / 100),
                Location = new Point(window.Width * 8 / 20, window.Height * 4 / 20),
                Size = new Size(window.Width * 2 / 10, window.Height * 2 / 20),
                Enabled = false,
                TextAlign = HorizontalAlignment.Center,
                Text = CalculateStock()
            };
            totalProduct.BringToFront();
            window.Controls.Add(totalProduct);

            firstProductLabel = new Label()
            {
                Text = "Nombre total de ",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = UIColor.DARKBLUE,
                Location = new Point(window.Width / 15, window.Height * 5 / 20),
                Size = new Size(window.Width * 2 / 10, window.Height * 1 / 10)
            };
            window.Controls.Add(firstProductLabel);

            totalFirstProduct = new TextBox()
            {
                Font = new Font("Poppins", window.Height * 4 / 100),
                Location = new Point(window.Width / 10, window.Height * 7 / 20),
                Size = new Size(window.Width * 2 / 10, window.Height * 2 / 20),
                Enabled = false,
                TextAlign = HorizontalAlignment.Center,
            };
            totalFirstProduct.BringToFront();
            window.Controls.Add(totalFirstProduct);

            secondProductLabel = new Label()
            {
                Text = "Nombre total de ",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = UIColor.DARKBLUE,
                Location = new Point(window.Width * 5 / 15, window.Height * 5 / 20),
                Size = new Size(window.Width * 3 / 10, window.Height * 1 / 10)
            };
            window.Controls.Add(secondProductLabel);

            totalSecondProduct = new TextBox()
            {
                Font = new Font("Poppins", window.Height * 4 / 100),
                Location = new Point(window.Width * 4 / 10, window.Height * 7 / 20),
                Size = new Size(window.Width * 2 / 10, window.Height * 2 / 20),
                Enabled = false,
                TextAlign = HorizontalAlignment.Center,
            };
            totalFirstProduct.BringToFront();
            window.Controls.Add(totalSecondProduct);

            thirdProductLabel = new Label()
            {
                Text = "Nombre total de ",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Poppins", window.Height * 2 / 100),
                ForeColor = UIColor.DARKBLUE,
                Location = new Point(window.Width * 10 / 15, window.Height * 5 / 20),
                Size = new Size(window.Width * 3 / 10, window.Height * 1 / 10)
            };
            window.Controls.Add(thirdProductLabel);

            totalThirdProduct = new TextBox()
            {
                Font = new Font("Poppins", window.Height * 4 / 100),
                Location = new Point(window.Width * 7 / 10, window.Height * 7 / 20),
                Size = new Size(window.Width * 2 / 10, window.Height * 2 / 20),
                Enabled = false,
                TextAlign = HorizontalAlignment.Center,
            };
            totalFirstProduct.BringToFront();
            window.Controls.Add(totalThirdProduct);
        }

        /// <summary>
        /// Permet de générer les informations des trois premiers types de soins en fonction de leur stock
        /// </summary>
        public void GenerateValueSellTotal()
        {

            TYPE_PRODUIT[] types = ProductController.getTypeProductOrderByStock();
            firstProductLabel.Text += types[0];
            int stock = 0;
            foreach (PRODUIT product in ProductController.getProductsFromType(types[0]))
            {
                stock += product.QUANTITEENSTOCK;
            }
            totalFirstProduct.Text = stock.ToString();

            secondProductLabel.Text += types[1];
            stock = 0;
            foreach (PRODUIT product in ProductController.getProductsFromType(types[1]))
            {
                stock += product.QUANTITEENSTOCK;
            }
            totalSecondProduct.Text = stock.ToString();

            thirdProductLabel.Text += types[2];
            stock = 0;
            foreach (PRODUIT product in ProductController.getProductsFromType(types[2]))
            {
                stock += product.QUANTITEENSTOCK;
            }
            totalThirdProduct.Text = stock.ToString();
        }

        /// <summary>
        /// Permet d'ajouter les données liées au stock total.
        /// </summary>
        /// <returns>Le stock total en chaine de caractère</returns>
        private string CalculateStock()
        {
            int stock = 0;
            foreach (PRODUIT p in ProductController.getProducts(false))
            {
                stock += p.QUANTITEENSTOCK;
            }
            return stock.ToString();
        }

        #region Back button
        private void BackPage(object sender, EventArgs e)
        {
            // Permet de retourne sur l'interface qui avait lancée cette interface
            window.Controls.Clear();
            window.switchInterface(new InterfaceStatsPage(window, user));
        }

        private void CreateBackButton()
        {
            backButton = new UIRoundButton(window.Width / 20, "<")
            {
                Location = new Point(window.Width * 9 / 10, window.Height / 10)
            };
            window.Controls.Add(backButton);
            backButton.Click += new EventHandler(BackPage);
        }
        #endregion
    }
}
