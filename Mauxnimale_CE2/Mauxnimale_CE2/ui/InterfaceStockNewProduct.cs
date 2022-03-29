using Mauxnimale_CE2.api.controllers;
using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceStockNewProduct : AInterface
    {
        MainWindow window;

        Header header;
        Footer footer;

        ComboBox type;
        TYPE_PRODUIT choosed;
        UIRoundButton back, home;
        UIButton addProduct;

        NumericUpDown quantity, initialCost, resellCost;
        Label lQuantity, lInitialCost, lResellCost, lExpirationDate, lName, lType;
        DateTimePicker expirationDate;
        TextBox name;

        #region Input events handling

        public void backClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceStockManagement(window, user));
        }

        public void newProductClick(object sender, EventArgs e)
        {
            if(quantity.Value > 0 && name.Text != "Nom" && choosed != null)
            {
                ProductController.addProduct(choosed, (int)quantity.Value, name.Text, resellCost.Value, expirationDate.Value);
                window.Controls.Clear();
                window.switchInterface(new InterfaceStockManagement(window, user));
            }
        }

        public void homeClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceHome(window, user));
        }
        private void lastNameEnter(object sender, EventArgs e)
        {
            if (quantity.Text == "Nom")
            {
                quantity.Text = "";
                quantity.ForeColor = Color.Black;
            }
        }

        public void typeSelectedChange(object sender, EventArgs e)
        {
            choosed = (TYPE_PRODUIT)type.Items[type.SelectedIndex];
        }


        private void lastNameLeave(object sender, EventArgs e)
        {
            if (quantity.Text.Length == 0)
            {
                quantity.Text = "Nom";
                quantity.ForeColor = Color.Gray;
            }
        }

        private void nameEnter(object sender, EventArgs e)
        {
            if (name.Text == "Nom")
            {
                name.Text = "";
                name.ForeColor = Color.Black;
            }
        }

        private void nameLeave(object sender, EventArgs e)
        {
            if (name.Text.Length == 0)
            {
                name.Text = "Nom";
                name.ForeColor = Color.Gray;
            }
        }
        #endregion

        public void generateLabel()
        {
            lQuantity = new Label();
            lQuantity.Location = new Point(window.Width / 20, window.Height * 42 / 100);
            setLabel(lQuantity, "Quantité");
            lInitialCost = new Label();
            lInitialCost.Location = new Point(window.Width / 20, window.Height * 52 / 100);
            setLabel(lInitialCost, "Prix d'achat");
            lResellCost = new Label();
            lResellCost.Location = new Point(window.Width / 20, window.Height * 62 / 100);
            setLabel(lResellCost, "Prix de revente");
            lExpirationDate = new Label();
            lExpirationDate.Location = new Point(window.Width / 20, window.Height * 22 / 100);
            setLabel(lExpirationDate, "Date d'expiration");
            lName = new Label();
            lName.Location = new Point(window.Width / 20, window.Height * 12 / 100);
            setLabel(lName, "Nom");
            lType = new Label();
            lType.Location = new Point(window.Width / 20, window.Height / 3);
            setLabel(lType, "Type");
        }

        public void generateButton()
        {
            addProduct = new UIButton(UIColor.ORANGE, "Nouveau Produit", Math.Min(window.Width / 4, window.Height / 3));
            addProduct.Location = new System.Drawing.Point(window.Width * 2 / 5, window.Height * 75 / 100);
            addProduct.Click += new EventHandler(newProductClick);
            window.Controls.Add(addProduct);

            back = new UIRoundButton(window.Width / 20, "<");
            back.Location = new System.Drawing.Point(window.Width * 9 / 10, window.Height / 10);
            back.Click += new EventHandler(backClick);
            window.Controls.Add(back);

            home = new UIRoundButton(window.Width / 20, "«");
            home.Location = new System.Drawing.Point(window.Width * 8 / 10, window.Height / 10);
            home.Click += new EventHandler(homeClick);
            window.Controls.Add(home);
        }

        public void setLabel(Label l, string s)
        {
            l.Font = new System.Drawing.Font("Poppins", window.Height * 2 / 100);
            l.ForeColor = UIColor.LIGHTBLUE;
            l.Text = s;
            l.Size = new System.Drawing.Size(window.Width / 3, window.Height * 6 / 100);
            window.Controls.Add(l);
        }

        public InterfaceStockNewProduct(MainWindow window, SALARIE user)
        {
            this.window = window;
            header = new Header(window);
            footer = new Footer(window, user);
            base.user = user;
        }

        private void generateBox()
        {
            type = new ComboBox();
            foreach (TYPE_PRODUIT typeProduct in ProductController.getTypes())
            {
                type.Items.Add(typeProduct);
            }
            type.Size = new System.Drawing.Size(window.Width / 2, window.Height / 7);
            type.DropDownStyle = ComboBoxStyle.DropDownList;
            type.Font = new System.Drawing.Font("Poppins", window.Height / 40);
            type.Location = new Point(window.Width / 4, window.Height * 5 / 15);
            type.ForeColor = Color.Gray;
            type.SelectedIndexChanged += new EventHandler(typeSelectedChange);
            window.Controls.Add(type);
        }

        public override void load()
        {
            header.load("Plannimaux - Nouveau produit");
            footer.load();
            generateTextBoxes();
            generateBox();
            generateNumUpDown();
            generateDatePicker();
            generateButton();
            generateLabel();
        }

        public void generateDatePicker()
        {
            expirationDate = new DateTimePicker();
            expirationDate.Format = DateTimePickerFormat.Custom;
            expirationDate.CustomFormat = "dd:MM";
            expirationDate.Size = new Size(window.Width / 2, window.Height * 5 / 100);
            expirationDate.Location = new Point(window.Width / 4, window.Height * 22 / 100);
            expirationDate.Font = new System.Drawing.Font("Poppins", window.Height * 3 / 100);
            expirationDate.ForeColor = Color.Black;
            window.Controls.Add(expirationDate);
        }

        public void generateTextBoxes()
        {
            name = new TextBox();
            name.LostFocus += new EventHandler(nameLeave);
            name.GotFocus += new EventHandler(nameEnter);
            name.Size = new Size(window.Width / 2, window.Height * 5 / 100);
            name.Location = new Point(window.Width / 4, window.Height * 12 / 100);
            name.Font = new System.Drawing.Font("Poppins", window.Height * 3 / 100);
            name.ForeColor = Color.Gray;
            name.Text = "Nom";
            window.Controls.Add(name);
        }

        public void generateNumUpDown()
        {
            initialCost = new NumericUpDown();
            initialCost.Size = new Size(window.Width / 2, window.Height * 5 / 100);
            initialCost.Location = new Point(window.Width / 4, window.Height * 52 / 100);
            initialCost.Font = new System.Drawing.Font("Poppins", window.Height * 3 / 100);
            initialCost.ForeColor = Color.Black;
            initialCost.Minimum = 0;
            initialCost.Maximum = 100000;
            window.Controls.Add(initialCost);

            resellCost = new NumericUpDown();
            resellCost.Size = new Size(window.Width / 2, window.Height * 5 / 100);
            resellCost.Location = new Point(window.Width / 4, window.Height * 62 / 100);
            resellCost.Font = new System.Drawing.Font("Poppins", window.Height * 3 / 100);
            resellCost.ForeColor = Color.Black;
            resellCost.Minimum = 0;
            resellCost.Maximum = 100000;
            window.Controls.Add(resellCost);

            quantity = new NumericUpDown();
            quantity.Size = new Size(window.Width / 2, quantity.Height);
            quantity.Location = new Point(window.Width / 4, window.Height * 42 / 100);
            quantity.Font = new System.Drawing.Font("Poppins", window.Height * 3 / 100);
            quantity.ForeColor = Color.Black;
            quantity.Minimum = 0;
            quantity.Maximum = 100000;
            window.Controls.Add(quantity);
        }

        public void setBox(TextBox textBox, string text)
        {
            textBox.Font = new System.Drawing.Font("Poppins", window.Height * 3 / 100);
            textBox.ForeColor = Color.Gray;
            textBox.Text = text;
            window.Controls.Add(textBox);
        }

        public override void updateSize()
        {
            window.Controls.Clear();
            this.load();
        }
    }
}
