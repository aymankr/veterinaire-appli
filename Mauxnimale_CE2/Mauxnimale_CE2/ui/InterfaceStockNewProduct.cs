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
        List<TYPE_PRODUIT> productType = new List<TYPE_PRODUIT>();

        TextBox quantity, name, initialCost, resellCost, expirationDate;

        #region Input events handling
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
            //mettre a jour la liste des produits (productsList) a affichés en fonction du type choisi (type.items[type.SelectedIndex])
        }

        public void productSelectedChange(object sender, EventArgs e)
        {
            //choisi = Le produit qui faut chopper a partir du string (product.items[product.SelectedIndex])
            //si jamais ca vous casse les couilles de chopper le produit a partir d'un string appelez moi je vous dirai comment modifiez la listBox pour que ca soit plus simple
        }


        private void lastNameLeave(object sender, EventArgs e)
        {
            if (quantity.Text.Length == 0)
            {
                quantity.Text = "Nom";
                quantity.ForeColor = Color.Gray;
            }
        }

        private void firstNameEnter(object sender, EventArgs e)
        {
            if (name.Text == "Prénom")
            {
                name.Text = "";
                name.ForeColor = Color.Black;
            }
        }

        private void firstNameLeave(object sender, EventArgs e)
        {
            if (name.Text.Length == 0)
            {
                name.Text = "Prénom";
                name.ForeColor = Color.Gray;
            }
        }

        private void emailEnter(object sender, EventArgs e)
        {
            if (expirationDate.Text == "Email")
            {
                expirationDate.Text = "";
                expirationDate.ForeColor = Color.Black;
            }
        }

        private void emailLeave(object sender, EventArgs e)
        {
            if (expirationDate.Text.Length == 0)
            {
                expirationDate.Text = "Email";
                expirationDate.ForeColor = Color.Gray;
            }
        }

        private void pswdEnter(object sender, EventArgs e)
        {
            if (initialCost.Text == "Mot de passe")
            {
                initialCost.Text = "";
                initialCost.ForeColor = Color.Black;
                initialCost.PasswordChar = '•';
            }
        }

        private void pswdLeave(object sender, EventArgs e)
        {
            if (initialCost.Text.Length == 0)
            {
                initialCost.Text = "Mot de passe";
                initialCost.ForeColor = Color.Gray;
                initialCost.PasswordChar = (char)0;
            }
        }
        private void confirmPswdEnter(object sender, EventArgs e)
        {
            if (resellCost.Text == "Confirmation du mot de passe")
            {
                resellCost.Text = "";
                resellCost.ForeColor = Color.Black;
                resellCost.PasswordChar = '•';
            }
        }

        private void confirmPswdLeave(object sender, EventArgs e)
        {
            if (resellCost.Text.Length == 0)
            {
                resellCost.Text = "Confirmation du mot de passe";
                resellCost.ForeColor = Color.Gray;
                resellCost.PasswordChar = (char)0;
            }
        }
        #endregion 

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
            //génerer liste des types avec une fonction API requete sql tous ca
            foreach (TYPE_PRODUIT typeProduct in productType)
            {
                type.Items.Add(typeProduct);
            }
            type.Size = new System.Drawing.Size(window.Width / 2, window.Height / 7);
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
        }

        public void generateTextBoxes()
        {
            quantity = new TextBox();
            quantity.LostFocus += new EventHandler(lastNameLeave);
            quantity.GotFocus += new EventHandler(lastNameEnter);
            quantity.Size = new Size(window.Width / 4, quantity.Height);
            quantity.Location = new Point(window.Width / 2 - quantity.Width - 10, window.Height * 12 / 100);
            setBox(quantity, "Quantité");

            name = new TextBox();
            name.LostFocus += new EventHandler(firstNameLeave);
            name.GotFocus += new EventHandler(firstNameEnter);
            name.Size = new Size(window.Width / 4, quantity.Height);
            name.Location = new Point(window.Width / 2 + 10, window.Height * 12 / 100);
            setBox(name, "Nom");

            expirationDate = new TextBox();
            expirationDate.LostFocus += new EventHandler(emailLeave);
            expirationDate.GotFocus += new EventHandler(emailEnter);
            expirationDate.Size = new Size(window.Width / 2 + 20, window.Height * 5 / 100);
            expirationDate.Location = new Point(window.Width / 4 - 10, window.Height * 22 / 100);
            setBox(expirationDate, "Date D'expiration");

            initialCost = new TextBox();
            initialCost.LostFocus += new EventHandler(pswdLeave);
            initialCost.GotFocus += new EventHandler(pswdEnter);
            initialCost.Size = new Size(window.Width / 2 + 20, window.Height * 5 / 100);
            initialCost.Location = new Point(window.Width / 4 - 10, window.Height * 52 / 100);
            initialCost.PasswordChar = (char)0;
            setBox(initialCost, "Prix d'achat");

            resellCost = new TextBox();
            resellCost.LostFocus += new EventHandler(confirmPswdLeave);
            resellCost.GotFocus += new EventHandler(confirmPswdEnter);
            resellCost.Size = new Size(window.Width / 2 + 20, window.Height * 5 / 100);
            resellCost.Location = new Point(window.Width / 4 - 10, window.Height * 62 / 100);
            resellCost.PasswordChar = (char)0;
            setBox(resellCost, "Prix de reventes");
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
            //throw new NotImplementedException();
        }
    }
}
