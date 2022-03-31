using System;
using System.Collections.Generic;
using System.Linq;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2.api.controllers
{
    public static class ProductController
    {
        /// <summary>
        /// Ajouter un produit
        /// </summary>
        /// <param name="type"></param>
        /// <param name="quantity"></param>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="image"></param>
        public static void addProduct(TYPE_PRODUIT type, int quantity, string name, decimal price, DateTime date)
        {
            PRODUIT p = new PRODUIT();
            p.TYPE_PRODUIT = type;
            p.QUANTITEENSTOCK = quantity;
            p.PRIXDACHAT = price;
            p.DATEPEREMPTION = date;
            p.NOMPRODUIT = name;
            DbContext.get().PRODUIT.Add(p);
            DbContext.get().SaveChanges();
        }

        /// <summary>
        /// Récupérer la liste des produits selon le type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Paramétrer la quantité du produit, et la date d'expiration s'il y a lieu
        /// </summary>
        /// <param name="p"></param>
        /// <param name="quantity"></param>
        public static void setProductQuantity(PRODUIT p, int quantity)
        {
            p.QUANTITEENSTOCK += quantity;
            if (p.TYPE_PRODUIT.NOMTYPE.Equals("Nourriture"))
            {
                DateTime date = DateTime.Now; // expire date
                date = date.AddMonths(3);
                p.DATEPEREMPTION = date;
            }
            DbContext.get().SaveChanges();
        }

        /// <summary>
        /// Récupérer tous les produits
        /// </summary>
        /// <returns></returns>
        public static ICollection<PRODUIT> getProducts()
        {
            return (from p in DbContext.get().PRODUIT
                    select p).ToList();
        }

        /// <summary>
        /// Récupérer tous les types de produit
        /// </summary>
        /// <returns></returns>
        public static ICollection<TYPE_PRODUIT> getTypes()
        {
            return (from t in DbContext.get().TYPE_PRODUIT
                                      select t).ToList();
        }

        /// <summary>
        /// Retourne vrai si le produit est expiré, sinon faux
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool isExpiredProduct(PRODUIT p)
        {
            int result = DateTime.Compare((DateTime)p.DATEPEREMPTION, DateTime.Now);
            return result < 0;
        }

        /// <summary>
        /// Supprimer un produit
        /// </summary>
        /// <param name="p"></param>
        public static void removeProduct(PRODUIT p)
        {
            p.PRODUITLIES.Clear();
            p.PRODUITVENDU.Clear();
            DbContext.get().PRODUIT.Remove(p);
            DbContext.get().SaveChanges();
        }

        internal static List<PRODUIT> getByName(string name)
        {
            var products = (from p in DbContext.get().PRODUIT
                                      where p.NOMPRODUIT.StartsWith(name)
                                      select p).ToList();
            return products.ToList();
        }

        /// <summary>
        /// Permet de récupérer le top trois des produit trié par leur stock
        /// </summary>
        /// <returns>le tableau contenant les 3 types ayant le plus de produit</returns>
        public static TYPE_PRODUIT[] getTypeProductOrderByStock()
        {
            
            TYPE_PRODUIT[] types = DbContext.get().TYPE_PRODUIT.OrderBy(type => type.IDTYPE).ToArray();
            int[] nbProducts = new int[types.Length];
            int i = 0;
            foreach(TYPE_PRODUIT typeProduct in types)
            {
                typeProduct.PRODUIT.ToList().ForEach(p => nbProducts[i] += p.QUANTITEENSTOCK);
                i++;
            }
            TYPE_PRODUIT[] topTreeTypes = new TYPE_PRODUIT[3];
            i = 0;
            while (topTreeTypes[2] == null)
            {
                int max = 0;
                int pos = 0;
                int maxPos = 0;
                foreach(int typeProduct in nbProducts)
                {
                    if(typeProduct > max)
                    {
                        max = typeProduct;
                        topTreeTypes[i] = types[pos];
                        Console.WriteLine(types[pos + 1]);
                        maxPos = pos;
                    } else
                    {
                        pos++;
                    }
                }
                nbProducts[maxPos] = 0;
                i++;
            }
            return topTreeTypes;
        }
    }
}
