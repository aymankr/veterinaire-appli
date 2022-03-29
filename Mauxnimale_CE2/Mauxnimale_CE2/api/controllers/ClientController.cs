using System;
using System.Collections.Generic;
using System.Linq;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2.api.controllers
{
    public static class ClientController
    {
        /// <summary>
        /// Ajouter un client dans la bd, avec ses informations
        /// </summary>
        /// <param name="name">Le nom du client</param>
        /// <param name="surname">Le prénom du client</param>
        /// <param name="phoneNumber">Le téléphone du client</param>
       public static bool RegisterClient(string name, string surname, string phoneNumber)
        {
            try
            {
                CLIENT c = new CLIENT
                {
                    NOMCLIENT = name,
                    PRENOMCLIENT = surname,
                    TELCLIENT = phoneNumber
                };
                DbContext.get().CLIENT.Add(c);
                DbContext.get().SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Récupérer un client selon ses noms
        /// </summary>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <returns></returns>
        public static CLIENT GetClientFromNameAndSurname(string name, string surname)
        {
            var client = from c in DbContext.get().CLIENT
                              where c.NOMCLIENT == name
                              where c.PRENOMCLIENT == surname
                         select c;
            return client.First();
        }

        /// <summary>
        /// Méthode permettant de récupérer un client depuis son id
        /// </summary>
        /// <param name="id">Id du client à récupérer</param>
        /// <returns>Le client qui possède cette id</returns>
        public static CLIENT GetClientFromID(int id)
        {
            var client = from c in DbContext.get().CLIENT
                         where c.IDCLIENT == id
                         select c;
            return client.First();
        }

        /// <summary>
        /// Permet de rechercher des clients depuis leur nom
        /// </summary>
        /// <param name="name">Nom recherché</param>
        /// <returns>La liste des client ont leur nom commence par ce nom</returns>
        public static List<CLIENT> ResearhByName(string name)
        {
            var client = from c in DbContext.get().CLIENT
                         where c.NOMCLIENT.StartsWith(name)
                         select c;
            return client.ToList();
        }

        /// <summary>
        /// Permet de rechercher des clients depuis leur prénom
        /// </summary>
        /// <param name="name">Nom recherché</param>
        /// <returns>La liste des client ou leur prénom commence par ce prénom</returns>
        public static List<CLIENT> ResearhBySurname(string surname)
        {
            var client = from c in DbContext.get().CLIENT
                         where c.PRENOMCLIENT.StartsWith(surname)
                         select c;
            return client.ToList();
        }

        /// <summary>
        /// Récupérer tous les clients, triés par nom
        /// </summary>
        /// <returns></returns>
        public static List<CLIENT> AllClient()
        {
            return DbContext.get().CLIENT.OrderBy(customer => customer.NOMCLIENT).ToList();
        }

        /// <summary>
        /// Supprimer un client
        /// </summary>
        /// <param name="c"></param>
        public static void DeleteClient(CLIENT c)
        {
            if (c.RENDEZ_VOUS.Any())
            {
                List<RENDEZ_VOUS> appointments = c.RENDEZ_VOUS.ToList();
                foreach (RENDEZ_VOUS appointment in appointments)
                {
                    AppointmentController.deleteAppointment(appointment);
                }
            }
            if (c.FACTURE_PRODUIT.Any())
            {
                var bills = from b in DbContext.get().FACTURE_PRODUIT
                                   where b.CLIENT == c
                                   select b;
                foreach (FACTURE_PRODUIT bill in bills.ToList())
                {
                    DbContext.get().FACTURE_PRODUIT.Remove(bill);
                }
            }
            if (c.ANIMAL.Any())
            {
                List<ANIMAL> animals = c.ANIMAL.ToList();
                foreach(ANIMAL a in animals)
                {
                    AnimalController.RemoveAnimal(a);
                }
            }
                    
            DbContext.get().CLIENT.Remove(c);
            DbContext.get().SaveChanges();
        }

        public static void UpdateClient(CLIENT c, string newName, string newSurname, string newPhoneNumber) { 
            c.NOMCLIENT = newName;
            c.PRENOMCLIENT = newSurname;
            c.TELCLIENT = newPhoneNumber;
            DbContext.get().SaveChanges();
        }


        /// <summary>
        /// Ajouter l'animal d'un client
        /// </summary>
        /// <param name="c">Propriètaire</param>
        /// <param name="a">Animal</param>
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
        /// Récupérer les animaux du client, trié par nom.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static List<ANIMAL> ListOfAnimal(CLIENT c)
        {
            return c.ANIMAL.OrderBy(animal => animal.NOM).ToList();
        }

        public static List<ANIMAL> ListAnimalByName(CLIENT c, string name)
        {

            var animaux = from a in DbContext.get().ANIMAL
                         where a.IDCLIENT == c.IDCLIENT
                         where a.NOM.StartsWith(name)
                         select a;
            return animaux.ToList();
        }
    }
}
