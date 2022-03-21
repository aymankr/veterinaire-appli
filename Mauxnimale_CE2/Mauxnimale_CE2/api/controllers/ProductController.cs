using Mauxnimale_CE2.api.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mauxnimale_CE2.api.controllers
{
    public static class ProductController
    {
        public static void addProduct(TYPE_PRODUIT type, int quantity, string name, decimal price, byte[] image)
        {
            PRODUIT p = new PRODUIT();
            p.TYPE_PRODUIT = type;
            p.QUANTITEENSTOCK = quantity;
            p.PRIXDACHAT = price;
            p.IMAGEPRODUIT = image;
            p.NOMPRODUIT = name;
            DbContext.get().PRODUIT.Add(p);
            DbContext.get().SaveChanges();
        }

        public static ICollection<PRODUIT> getProductsFromType(TYPE_PRODUIT type)
        {
            List<PRODUIT> products = (from p in DbContext.get().PRODUIT
                                where p.IDTYPE == type.IDTYPE
                           select p).ToList();
            if (products.Count != 0)
            {
                return products;
            }
            return null;
        }

        public static void setProductQuantity(PRODUIT p, int quantity)
        {
            p.QUANTITEENSTOCK += quantity;
            if (p.TYPE_PRODUIT.NOMTYPE.Equals("Nourriture"))
            {
                DateTime date = DateTime.Now; // expire date
                date = date.AddMonths(3);
                p.DATEPEREMPTION = date;
            }
        }

        public static ICollection<PRODUIT> getProducts()
        {
            return (from p in DbContext.get().PRODUIT
                    select p).ToList();
        }

        public static ICollection<TYPE_PRODUIT> getTypes()
        {
            return (from t in DbContext.get().TYPE_PRODUIT
                                      select t).ToList();
        }

        public static bool isExpiredProduct(PRODUIT p)
        {
            int result = DateTime.Compare((DateTime)p.DATEPEREMPTION, DateTime.Now);
            return result < 0;
        }

        public static void removeProduct(PRODUIT p)
        {
            p.PRODUITLIES.Clear();
            p.PRODUITVENDU.Clear();
            DbContext.get().PRODUIT.Remove(p);
            DbContext.get().SaveChanges();
        }
    }
}
