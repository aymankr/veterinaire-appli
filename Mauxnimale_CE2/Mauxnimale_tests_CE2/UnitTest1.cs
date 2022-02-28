using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Mauxnimale_CE2.api;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_tests_CE2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void addAppointmentTest()
        {
            CLIENT costumer = new CLIENT("name1", "name2", "0909090909");
            Tools.getDatabase().CLIENT.Add(costumer);
            Tools.getDatabase().SaveChanges();

            ANIMAL animal = new ANIMAL(1, costumer.IDCLIENT, "pouki", "2020", 10, 20, false);

            costumer.ANIMAL.Add(animal);
            Tools.getDatabase().ANIMAL.Add(animal);
            Tools.getDatabase().SaveChanges();

            RENDEZ_VOUS newAppointment = new RENDEZ_VOUS();
            ORDONNANCE prescription = new ORDONNANCE(animal, newAppointment);
            JOURNEE day = new JOURNEE(DateTime.Now);
            newAppointment = new RENDEZ_VOUS(costumer, day, TimeSpan.Parse("12-00-00"), TimeSpan.Parse("13-00-00"), "vaccin", animal, prescription);

            Tools.getDatabase().RENDEZ_VOUS.Add(newAppointment);
            Tools.getDatabase().SaveChanges();

            RENDEZ_VOUS testAppointment = Tools.getDatabase().RENDEZ_VOUS.Find(newAppointment);
            Assert.IsTrue(testAppointment != null);
            Assert.Equals(testAppointment.JOURNEE, day);
            Assert.Equals(testAppointment.HEUREDEBUT, TimeSpan.Parse("12-00-00"));
            Assert.Equals(testAppointment.HEUREFIN, TimeSpan.Parse("13-00-00"));
            Assert.Equals(testAppointment.RAISON, "vaccin");
            Assert.Equals(testAppointment.ANIMAL, animal);
            Assert.Equals(testAppointment.ORDONNANCE, prescription);
        }
    }
}
