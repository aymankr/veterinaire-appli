using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2.api.controllers
{
    internal static class ClientController
    {
        /// <summary>
        /// Ajouter un client dans la bd, avec ses informations
        /// </summary>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="phoneNumber"></param>
       public static void AddClient(string name, string surname, string phoneNumber)
        {
            CLIENT c = new CLIENT();
            c.NOMCLIENT = surname;
            c.PRENOMCLIENT = name;
            c.TELCLIENT = phoneNumber;
            DbContext.get().CLIENT.Add(c);
            DbContext.get().SaveChanges();
        }

        /// <summary>
        /// Récupérer un client selon ses noms
        /// </summary>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <returns></returns>
        public static CLIENT GetClient(string name, string surname)
        {
            var client = from c in DbContext.get().CLIENT
                              where c.NOMCLIENT == surname
                              where c.PRENOMCLIENT == name
                              select c;
            return client.First();
        }

        /// <summary>
        /// Récupérer tous les clients
        /// </summary>
        /// <returns></returns>
        public static List<CLIENT> AllClient()
        {
            return DbContext.get().CLIENT.ToList();
        }

        /// <summary>
        /// Supprimer un client
        /// </summary>
        /// <param name="c"></param>
        public static void DeleteClient(CLIENT c)
        {
            DbContext.get().CLIENT.Remove(c);
            DbContext.get().SaveChanges();
        }

        /// <summary>
        /// Ajouter l'animal d'un client
        /// </summary>
        /// <param name="c"></param>
        /// <param name="a"></param>
        public static void AddAnimal(CLIENT c, ANIMAL a)
        {
            c.ANIMAL.Add(a);
        }

        /// <summary>
        /// Supprimer l'animal d'un client
        /// </summary>
        /// <param name="c"></param>
        /// <param name="a"></param>
        public static void RemoveAnimal(CLIENT c, ANIMAL a)
        {
            c.ANIMAL.Remove(a);
        }

        /// <summary>
        /// Récupérer les animaux du client
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static List<ANIMAL> ListOfAnimal(CLIENT c)
        {
            return c.ANIMAL.ToList();
        }
    }
}
