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
       public static void addClient(string name, string surname, string phoneNumber)
        {
            CLIENT c = new CLIENT();
            c.NOMCLIENT = surname;
            c.PRENOMCLIENT = name;
            c.TELCLIENT = phoneNumber;
            DbContext.get().CLIENT.Add(c);
            DbContext.get().SaveChanges();
        }
    }
}
