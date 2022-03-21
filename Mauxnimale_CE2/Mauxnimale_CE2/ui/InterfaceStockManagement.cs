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
    internal class InterfaceStockManagement : AInterface
    {
        MainWindow window;

        Header header;
        Footer footer;
        ComboBox type;
        PRODUIT choisi;
        UIRoundButton back;
        UIButton newProduct, changeProduct, deleteProduct;
        List<TYPE_PRODUIT> productType = new List<TYPE_PRODUIT>();
        ListBox products;
        List<PRODUIT> productsList = new List<PRODUIT>();

        public InterfaceStockManagement(MainWindow forme, SALARIE s)
        {
            user = s;
            this.window = forme;
            header = new Header(window);
            footer = new Footer(window, s);
        }

        public void generateBox()
        {
            type = new ComboBox();
            //génerer liste des types avec une fonction API requete sql tous ca
            foreach(TYPE_PRODUIT typeProduct in productType)
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

            updateProducts();
            window.Controls.Add(products);
        }

        public override void load()
        {
            header.load("Plannimaux - Gestion des stocks");
            footer.load();
            generateBox();
            generateButton();
        }

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
            //window.switchInterface(new InterfaceHome(window, user));
        }

        public void deleteProductClick(object sender, EventArgs e)
        {
            //if(choisi != null)
            //{
                //deleteProduct
            //}
        }

        public void changeProductClick(object sender, EventArgs e)
        {
            //if(choisi != null)
            //{
                window.Controls.Clear();
                window.switchInterface(new InterfaceChangeStock(window, user));
            //}
        }

        public void typeSelectedChange(object sender, EventArgs e)
        {
            //mettre a jour la liste des produits (productsList) a affichés en fonction du type choisi (type.items[type.SelectedIndex])
            updateProducts();
        }
        
        public void productSelectedChange(object sender, EventArgs e)
        {
            //choisi = Le produit qui faut chopper a partir du string (product.items[product.SelectedIndex])
            //si jamais ca vous casse les couilles de chopper le produit a partir d'un string appelez moi je vous dirai comment modifiez la listBox pour que ca soit plus simple
        }

        public void updateProducts()
        {
            foreach (PRODUIT product in productsList)
            {
                //ajouter chaque produit dans la liste
                //products.Items.Add(le nom du produit + " -  quantité : " + la quantité du produit);
                products.Items.Add(product.IDPRODUIT + ". " + product.NOMPRODUIT + " - Quantité : " + product.QUANTITEENSTOCK);
            }
        }

        public override void updateSize()
        {
            window.Controls.Clear();
            this.load();
        }
    }
}
