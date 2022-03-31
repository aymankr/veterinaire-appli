using Mauxnimale_CE2.api.entities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Mauxnimale_CE2.api.controllers
{
    public static class PrescriptionController
    {
        /// <summary>
        /// Créer une ordonnance avec ses informations
        /// </summary>
        /// <param name="animal"></param>
        /// <param name="productsWithQuantities"></param>
        public static void addAPrescription(ANIMAL animal, Dictionary<PRODUIT, int> productsWithQuantities, List<SOIN> cares, RENDEZ_VOUS rdv, string orders, string diagnostique)
        {
            ORDONNANCE prescription = new ORDONNANCE();
            prescription.ANIMAL = animal;
            prescription.RENDEZ_VOUS = rdv;
            prescription.DIAGNOSTIQUE = diagnostique;
            prescription.PRESCRIPTION = orders;

            foreach(var item in productsWithQuantities)
            {
                PRODUITLIES linkedProduct = new PRODUITLIES();
                linkedProduct.PRODUIT = item.Key; // product
                linkedProduct.QUANTITEPRODUITS = item.Value; // quantity
                linkedProduct.ORDONNANCE = prescription;
                DbContext.get().PRODUITLIES.Add(linkedProduct);
                prescription.PRODUITLIES.Add(linkedProduct);
                //DbContext.get().SaveChanges();
            }
            DbContext.get().ORDONNANCE.Add(prescription);

            foreach(SOIN care in cares)
            {
                LIEN_SOIN linkedCare = new LIEN_SOIN();
                linkedCare.ORDONNANCE = prescription;
                linkedCare.SOIN = care;
                DbContext.get().LIEN_SOIN.Add(linkedCare);
                prescription.LIEN_SOIN.Add(linkedCare);
            }
            DbContext.get().SaveChanges();
        }

        public static void modifPrescription(ORDONNANCE prescription, Dictionary<PRODUIT, int> productsWithQuantities, List<SOIN> cares, string orders, string diagnostique)
        {
            ORDONNANCE newPrescription = new ORDONNANCE()
            {
                ANIMAL = prescription.ANIMAL,
                RENDEZ_VOUS = prescription.RENDEZ_VOUS,
            };

            DeletePrescription(prescription);
            
            addAPrescription(newPrescription.ANIMAL, productsWithQuantities,cares, prescription.RENDEZ_VOUS, orders, diagnostique);

        }

        public static ORDONNANCE GetORDONNANCEFromRDVAndAnimal(RENDEZ_VOUS rdv, ANIMAL animal)
        {
            ORDONNANCE ordonnance = (from o in DbContext.get().ORDONNANCE
                             where o.IDANIMAL == animal.IDANIMAL && o.IDRDV == rdv.IDRDV
                             select o).FirstOrDefault();
            return ordonnance;
        }

        public static void DeletePrescription(ORDONNANCE prescription)
        {
            List<PRODUITLIES> prods = prescription.PRODUITLIES.ToList(); 
            List<LIEN_SOIN> soins = prescription.LIEN_SOIN.ToList(); 
            foreach(PRODUITLIES prod in prods)
            {
                DbContext.get().PRODUITLIES.Remove(prod);
            }
            foreach(LIEN_SOIN l in soins)
            {
                DbContext.get().LIEN_SOIN.Remove(l);
            }
            DbContext.get().ORDONNANCE.Remove(prescription);
            DbContext.get().SaveChanges();
        }
    }
}
