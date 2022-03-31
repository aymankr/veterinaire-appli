using System;
using System.Collections.Generic;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2.api.controllers
{
    public static class InvoiceController
    {
        /// <summary>
        /// Créer une facture
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static FACTURE_PRODUIT addInvoice(CLIENT customer, DateTime date)
        {
            FACTURE_PRODUIT invoice = new FACTURE_PRODUIT();
            invoice.CLIENT = customer;
            invoice.DATE = date;
            DbContext.get().FACTURE_PRODUIT.Add(invoice);
            DbContext.get().SaveChanges();
            return invoice;
        }

        /// <summary>
        /// Editer une facture avec informations
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="products"></param>
        /// <param name="totalPrice"></param>
        public static void editInvoice(FACTURE_PRODUIT invoice, ICollection<PRODUITVENDU> products, decimal totalPrice)
        {
            DbContext.get().FACTURE_PRODUIT.Find(invoice.IDFACTURE).PRODUITVENDU = products;
            DbContext.get().FACTURE_PRODUIT.Find(invoice.IDFACTURE).MONTANT = totalPrice;
            DbContext.get().SaveChanges();
        }

        /// <summary>
        /// Permet de récupérer toutes les factures de la bases.
        /// </summary>
        /// <returns></returns>
        public static List<FACTURE_PRODUIT> allInvoices()
        {
            List<FACTURE_PRODUIT> allInvoices = new List<FACTURE_PRODUIT>();
            foreach (CLIENT c in DbContext.get().CLIENT)
            {
                foreach (FACTURE_PRODUIT invoice in c.FACTURE_PRODUIT)
                {
                    allInvoices.Add(invoice);
                }
            }
            return allInvoices;
        }
    }
}
