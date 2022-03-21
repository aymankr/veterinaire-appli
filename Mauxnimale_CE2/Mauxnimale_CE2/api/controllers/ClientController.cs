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
       public static void AddClient(string name, string surname, string phoneNumber)
        {
            CLIENT c = new CLIENT();
            c.NOMCLIENT = surname;
            c.PRENOMCLIENT = name;
            c.TELCLIENT = phoneNumber;
            DbContext.get().CLIENT.Add(c);
            DbContext.get().SaveChanges();
        }

        public static CLIENT GetClient(string name, string surname)
        {
            var client = from c in DbContext.get().CLIENT
                              where c.NOMCLIENT == surname
                              where c.PRENOMCLIENT == name
                              select c;
            return client.First();
        }

        public static List<CLIENT> ResearhByName(string name)
        {
            var client = from c in DbContext.get().CLIENT
                         where c.NOMCLIENT.StartsWith(name)
                         select c;
            return client.ToList();
        }

        public static List<CLIENT> ResearhBySurname(string surname)
        {
            var client = from c in DbContext.get().CLIENT
                         where c.PRENOMCLIENT.StartsWith(surname)
                         select c;
            return client.ToList();
        }

        public static List<CLIENT> AllClient()
        {
            return DbContext.get().CLIENT.ToList();
        }

        public static void DeleteClient(CLIENT c)
        {
            DbContext.get().CLIENT.Remove(c);
            DbContext.get().SaveChanges();
        }

        public static void AddAnimal(CLIENT c, ANIMAL a)
        {
            c.ANIMAL.Add(a);
        }

        public static void RemoveAnimal(CLIENT c, ANIMAL a)
        {
            c.ANIMAL.Remove(a);
        }

        public static List<ANIMAL> ListOfAnimal(CLIENT c)
        {
            return c.ANIMAL.ToList();
        }
    }
}
