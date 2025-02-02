﻿using Mauxnimale_CE2.api.entities;
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
        public static void AddAPrescription(ANIMAL animal, Dictionary<PRODUIT, int> productsWithQuantities, List<SOIN> cares, RENDEZ_VOUS rdv, string orders, string diagnostique)
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

        /// <summary>
        /// Supprime l'ancienne ordonnance et créé une nouvelle ordonnance avec les modification voulues
        /// </summary>
        /// <param name="prescription"></param>
        /// <param name="productsWithQuantities"></param>
        /// <param name="cares"></param>
        /// <param name="orders"></param>
        /// <param name="diagnostique"></param>
        public static void modifPrescription(ORDONNANCE prescription, Dictionary<PRODUIT, int> productsWithQuantities, List<SOIN> cares, string orders, string diagnostique)
        {
            ANIMAL animal = prescription.ANIMAL;
            RENDEZ_VOUS appointement = prescription.RENDEZ_VOUS;
            DeletePrescription(prescription);
            AddAPrescription(animal, productsWithQuantities, cares, appointement, orders, diagnostique);
        }

        /// <summary>
        /// Retourne une ordonnance grâce au rendez vous et à l'animal concrené
        /// </summary>
        /// <param name="rdv"></param>
        /// <param name="animal"></param>
        /// <returns></returns>
        public static ORDONNANCE GetORDONNANCEFromRDVAndAnimal(RENDEZ_VOUS rdv, ANIMAL animal)
        {
            ORDONNANCE ordonnance = (from o in DbContext.get().ORDONNANCE
                             where o.IDANIMAL == animal.IDANIMAL && o.IDRDV == rdv.IDRDV
                             select o).FirstOrDefault();
            return ordonnance;
        }

        /// <summary>
        /// supprime une Ordonnance de la base
        /// </summary>
        /// <param name="prescription"></param>
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
