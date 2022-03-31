using System;
using System.Collections.Generic;
using System.Linq;
using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.api.controllers.utils;

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
            p.NOMPRODUIT = InputVerification.capitalizeText(name);
            DbContext.get().PRODUIT.Add(p);
            DbContext.get().SaveChanges();
        }

        /// <summary>
        /// Détermine s'il existe un produit porte le même nom que celui donné dans la base.
        /// </summary>
        /// <param name="productName">Le nom du produit</param>
        /// <returns>true s'il existe un produit porte le même nom que celui donné dans la base, false si non.</returns>
        public static bool producAlreadyExists(string productName) => DbContext.get().PRODUIT.Where(p => p.NOMPRODUIT.ToLower().Equals(productName.ToLower())).Count() > 0;

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
            p.QUANTITEENSTOCK = quantity;
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
        /// <param name="sellable">true pour obtenir les produits qui ont un stock > 0</param>
        /// <returns>Tous les produits de la base de données</returns>
        public static List<PRODUIT> getProducts(bool sellable)
        {
            if (sellable)
                return DbContext.get().PRODUIT.ToList();
            else
                return DbContext.get().PRODUIT.Where(p => p.QUANTITEENSTOCK > 0).ToList();
        }

        /// <summary>
        /// Récupère les produits dont le nom contient la chaine donnée en paramètre.
        /// </summary>
        /// <param name="name">Le nom ou partie du nom du produit</param>
        /// <param name="sellable">true pour obtenir les produits qui ont un stock > 0</param>
        /// <returns>les produits dont le nom contient la chaine donnée en paramètre</returns>
        public static List<PRODUIT> getProductsByName(string name, bool sellable)
        {
            if (sellable)
                return DbContext.get().PRODUIT.Where(p => p.NOMPRODUIT.Contains(name) && p.QUANTITEENSTOCK > 0).ToList();
            else
                return DbContext.get().PRODUIT.Where(p => p.NOMPRODUIT.Contains(name)).ToList();
        }

        /// <summary>
        /// Récupère les produits dont le nom contient la chaine donnée en paramètre et correspondant au type donné.
        /// </summary>
        /// <param name="name">Le nom ou partie du nom du produit</param>
        /// <param name="type">Le type des produits</param>
        /// <param name="sellable">true pour obtenir les produits qui ont un stock > 0</param>
        /// <returns>les produits dont le nom contient la chaine donnée en paramètre et le dont le type correspond à celui donné</returns>
        public static List<PRODUIT> getProductsByNameAndType(string name, TYPE_PRODUIT type, bool sellable)
        {
            return DbContext.get().PRODUIT.Where(p => p.NOMPRODUIT.Contains(name) && p.IDTYPE.Equals(type.IDTYPE) && p.QUANTITEENSTOCK > 0).ToList();
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
        /// Récupère tous les types de produit qui contiennent la chaîne de caractère passé en paramètre.
        /// </summary>
        public static List<TYPE_PRODUIT> getTypesByName(string name) => DbContext.get().TYPE_PRODUIT.Where(type => type.NOMTYPE.Contains(name)).ToList();

        /// <summary>
        /// Récupère le type de produit qui est égal à la chaîne de caractère passé en paramètre ou null s'il n'existe pas.
        /// </summary>
        public static TYPE_PRODUIT getTypeByName(string name) => DbContext.get().TYPE_PRODUIT.Where(type => type.NOMTYPE.Equals(name)).FirstOrDefault();

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
        /// Supprime un produit
        /// </summary>
        /// <param name="product">produit à supprimer</param>
        public static void removeProduct(PRODUIT product)
        {
            product.PRODUITLIES.Clear();
            product.PRODUITVENDU.Clear();
            DbContext.get().PRODUIT.Remove(product);
            DbContext.get().SaveChanges();
        }

        /// <summary>
        /// Met à jour les informations du produit donné avec les paramètres donnés.
        /// </summary>
        /// <param name="productToUpdate">Le produit à mettre à jour.</param>
        /// <param name="newName">Le nouveau nom</param>
        /// <param name="newType">Le nouveau type</param>
        /// <param name="newPrice">Le nouveau prix</param>
        /// <param name="newStockQuantity">Nouvelle quantité en stock</param>
        public static void updateProductInfos(PRODUIT productToUpdate, string newName, TYPE_PRODUIT newType, decimal newPrice, int newStockQuantity)
        {
            productToUpdate.NOMPRODUIT = InputVerification.capitalizeText(newName);
            productToUpdate.TYPE_PRODUIT = newType;
            productToUpdate.IDTYPE = newType.IDTYPE;
            productToUpdate.PRIXDEVENTECLIENT = newPrice;
            productToUpdate.QUANTITEENSTOCK = newStockQuantity;
            DbContext.get().SaveChanges();
        }

        /// <summary>
        /// Met à jour le nom du type donné.
        /// </summary>
        /// <param name="type">Le type dont le nom est à changer.</param>
        /// <param name="newName">Le nouveau non à donner.</param>
        public static void updateType(TYPE_PRODUIT type, string newName)
        {
            type.NOMTYPE = newName;
            DbContext.get().SaveChanges();
        }

        public static void removeType(TYPE_PRODUIT type)
        {
            PT4_S4P2C_E2Entities dbContext = DbContext.get();
            dbContext.TYPE_PRODUIT.Remove(type);
            dbContext.SaveChanges();
        }

        public static void addType(string name)
        {
            TYPE_PRODUIT newType = new TYPE_PRODUIT();
            newType.NOMTYPE = name;
            PT4_S4P2C_E2Entities dbContext = DbContext.get();
            dbContext.TYPE_PRODUIT.Add(newType);
            dbContext.SaveChanges();
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
