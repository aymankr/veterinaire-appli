using Mauxnimale_CE2.api.entities;
using System;
using System.Collections.Generic;

namespace Mauxnimale_CE2.api.controllers
{
    public static class InvoiceController
    {
        public static FACTURE_PRODUIT addInvoice(CLIENT customer, DateTime date)
        {
            FACTURE_PRODUIT invoice = new FACTURE_PRODUIT();
            invoice.CLIENT = customer;
            invoice.DATE = date;
            DbContext.get().FACTURE_PRODUIT.Add(invoice);
            DbContext.get().SaveChanges();
            return invoice;
        }

        public static void editInvoice(FACTURE_PRODUIT invoice, ICollection<PRODUITVENDU> products, decimal totalPrice)
        {
            DbContext.get().FACTURE_PRODUIT.Find(invoice.IDFACTURE).PRODUITVENDU = products;
            DbContext.get().FACTURE_PRODUIT.Find(invoice.IDFACTURE).MONTANT = totalPrice;
            DbContext.get().SaveChanges();
        }
    }
}
