using Mauxnimale_CE2.api.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mauxnimale_CE2.api.controllers
{
    public static class SaleController
    {
        /// <summary>
        /// Ajout de produits vendus à un client
        /// </summary>
        /// <param name="productsWithQuantities"></param>
        /// <param name="customer"></param>
        public static void addSoldProducts(Dictionary<PRODUIT, int> productsWithQuantities, CLIENT customer)
        {
            decimal price = 0;
            FACTURE_PRODUIT invoice = InvoiceController.addInvoice(customer, DateTime.Now);
            List<PRODUITVENDU> soldProducts = new List<PRODUITVENDU>();

            foreach (var item in productsWithQuantities)
            {
                price += item.Key.PRIXDEVENTECLIENT * item.Value;
                PRODUITVENDU soldProduct = new PRODUITVENDU();
                soldProduct.PRODUIT = item.Key;
                soldProduct.QUANTITE = item.Value;
                soldProduct.FACTURE_PRODUIT = invoice;
                soldProducts.Add(soldProduct);
            }
            DbContext.get().PRODUITVENDU.AddRange(soldProducts);
            InvoiceController.editInvoice(invoice, soldProducts, price);
        }
    }
}
