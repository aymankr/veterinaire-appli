using Mauxnimale_CE2.api.entities;
using System.Collections.Generic;

namespace Mauxnimale_CE2.api.controllers
{
    public static class PrescriptionController
    {
        /// <summary>
        /// Créer une ordonnance avec ses informations
        /// </summary>
        /// <param name="animal"></param>
        /// <param name="productsWithQuantities"></param>
        public static void addAPrescription(ANIMAL animal, Dictionary<PRODUIT, int> productsWithQuantities)
        {
            ORDONNANCE prescription = new ORDONNANCE();
            prescription.ANIMAL = animal;

            foreach(var item in productsWithQuantities)
            {
                PRODUITLIES linkedProduct = new PRODUITLIES();
                linkedProduct.PRODUIT = item.Key; // product
                linkedProduct.QUANTITEPRODUITS = item.Value; // quantity
                linkedProduct.ORDONNANCE = prescription;
                DbContext.get().PRODUITLIES.Add(linkedProduct);
                //DbContext.get().SaveChanges();
            }
            DbContext.get().ORDONNANCE.Add(prescription);
            DbContext.get().SaveChanges();
        }
    }
}
