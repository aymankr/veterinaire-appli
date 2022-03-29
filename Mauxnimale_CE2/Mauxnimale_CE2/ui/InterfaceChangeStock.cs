using Mauxnimale_CE2.api.controllers;
using Mauxnimale_CE2.api.entities;
﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceChangeStock : AInterface
    {
        PRODUIT choosed;
        Header header;
        UIButton applyQuantity;
        Label productName;
        Footer footer;
        UIRoundButton back, home;
        NumericUpDown quantity;

        public InterfaceChangeStock(MainWindow window, SALAIRE user, PRODUIT product) : base(window, user)
        {
            header = new Header(window);
            footer = new Footer(window, s);
            choosed = product;
        }

        public override void load()
        {
            header.load("Plannimaux - Changer Produit");
            footer.load();
            generateButton();
            generateNumericUpDown();
            generateLabel();
        }

        private void generateNumericUpDown()
        {

            quantity = new NumericUpDown();
            quantity.Size = new System.Drawing.Size(window.Width / 13, window.Height / 10);
            quantity.Font = new System.Drawing.Font("Poppins", window.Height * 3 / 100);
            quantity.ForeColor = Color.Black;
            quantity.Minimum = -choosed.QUANTITEENSTOCK;

            quantity.Location = new Point(window.Width  * 5 / 13, window.Height / 2);
            window.Controls.Add(quantity);
        }
        private void generateButton()
        {
            back = new UIRoundButton(window.Width / 20, "<");
            back.Location = new System.Drawing.Point(window.Width * 9 / 10, window.Height / 10);
            window.Controls.Add(back);

            home = new UIRoundButton(window.Width / 20, "«");
            home.Location = new System.Drawing.Point(window.Width * 8 / 10, window.Height / 10);
            window.Controls.Add(home);

            applyQuantity = new UIButton(UIColor.ORANGE, "Appliquer modification", Math.Min(window.Width / 3, window.Height / 3));
            applyQuantity.Location = new System.Drawing.Point(window.Width / 3, window.Height * 65 / 100);
            applyQuantity.Click += new EventHandler(applyClick);
            window.Controls.Add(applyQuantity);

            home.Click += new EventHandler(homeClick);
            back.Click += new EventHandler(backClick);
        }

        private void generateLabel()
        {
            productName = new Label();
            productName.Text = choosed.NOMPRODUIT;
            productName.Font = new System.Drawing.Font("Poppins", Math.Min(window.Width * 10 / 100, window.Height * 6 / 100)/(int)Math.Sqrt(Math.Sqrt(choosed.NOMPRODUIT.Length)));
            productName.ForeColor = UIColor.LIGHTBLUE;
            productName.Size = new System.Drawing.Size(window.Width, window.Height * 1 / 10);
            productName.Location = new Point(window.Width / 3, window.Height * 3 / 10);
            window.Controls.Add(productName);
        }

        public void applyClick(object sender, EventArgs e)
        {
            if(quantity.Value ==- choosed.QUANTITEENSTOCK)
            {
                ProductController.removeProduct(choosed);
            }
            else
            {
                ProductController.setProductQuantity(choosed, (int)quantity.Value);
            }
            window.Controls.Clear();
            window.switchInterface(new InterfaceStockManagement(window, user));
        }

        public void homeClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceHome(window, user));
        }

        public void backClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceStockManagement(window, user));
        }
    }
}
