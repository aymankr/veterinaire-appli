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
        public static List<PRODUIT> getProductsFromType(TYPE_PRODUIT type)
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
        public static List<PRODUIT> getProducts()
        {
            return DbContext.get().PRODUIT.ToList();
        }

        /// <summary>
        /// Récupère les produits dont le nom contient la chaine donnée en paramètre.
        /// </summary>
        /// <param name="name">Le nom ou partie du nom du produit</param>
        /// <returns>les produits dont le nom contient la chaine donnée en paramètre</returns>
        public static List<PRODUIT> getProductsByName(string name)
        {
            return DbContext.get().PRODUIT.Where(p => p.NOMPRODUIT.Contains(name)).ToList();
        }

        /// <summary>
        /// Récupère les produits dont le nom contient la chaine donnée en paramètre et correspondant au type donné.
        /// </summary>
        /// <param name="name">Le nom ou partie du nom du produit</param>
        /// <param name="type">Le type des produits</param>
        /// <returns>les produits dont le nom contient la chaine donnée en paramètre et le dont le type correspond à celui donné</returns>
        public static List<PRODUIT> getProductsByNameAndType(string name, TYPE_PRODUIT type)
        {
            return DbContext.get().PRODUIT.Where(p => p.NOMPRODUIT.Contains(name) && p.IDTYPE.Equals(type.IDTYPE)).ToList();
        }

        /// <summary>
        /// Récupère le produit avec l'id donné s'il existe.
        /// </summary>
        /// <param name="id">l'id du produit</param>
        /// <returns>le produit avec l'id donné ou null s'il n'existe pas</returns>
        public static PRODUIT getProductById(int id)
        {
            return DbContext.get().PRODUIT.Find(id);
        }

        /// <summary>
        /// Récupérer tous les types de produit
        /// </summary>
        /// <returns></returns>
        public static List<TYPE_PRODUIT> getTypes()
        {
            return DbContext.get().TYPE_PRODUIT.ToList();
        }

        /// <summary>
        /// Retrouve un produit par son ID.
        /// </summary>
        /// <param name="id">l'id du type à chercher</param>
        /// <returns>Le type du produit avec l'id correspondant ou null si non trouvé</returns>
        public static TYPE_PRODUIT getTypeById(int id)
        {
            return DbContext.get().TYPE_PRODUIT.Find(id);
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
    }
}
