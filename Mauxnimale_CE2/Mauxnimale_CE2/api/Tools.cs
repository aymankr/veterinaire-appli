using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2.api
{
    public static class Tools
    {
        private static PT4_S4P2C_E2Entities1 database = new PT4_S4P2C_E2Entities1();

        public static PT4_S4P2C_E2Entities1 getDatabase()
        {
            return database;
        }
    }
}
