using Mauxnimale_CE2.api.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mauxnimale_CE2.api.controllers
{
    public static class InvoiceController
    {
        public static void addInvoice(CLIENT customer, DateTime date, decimal price, ICollection<PRODUITVENDU> products)
        {
            FACTURE_PRODUIT invoice = new FACTURE_PRODUIT();
            invoice.CLIENT = customer;
            invoice.DATE = date;
            invoice.MONTANT = price;
            invoice.PRODUITVENDU = products;
            DbContext.get().FACTURE_PRODUIT.Add(invoice);
            DbContext.get().SaveChanges();
        }
    }
}
