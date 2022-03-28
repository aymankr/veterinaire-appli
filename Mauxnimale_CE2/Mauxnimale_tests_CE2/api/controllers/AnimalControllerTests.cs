using Mauxnimale_CE2.api.controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mauxnimale_CE2.api.entities;
using System.Linq;

namespace Mauxnimale_CE2.api.controllers.Tests
{
    [TestClass()]
    public class AnimalControllerTests
    {
        private PT4_S4P2C_E2Entities dbContext = DbContext.get();

        [TestMethod()]
        public void IsAlreadyRegisteredTest()
        {
            // Création d'un animal de test
            ANIMAL testAnimal = new ANIMAL();
            testAnimal.TAILLE = 100;
            testAnimal.POIDS = 100;
            testAnimal.NOM = "Test";
            testAnimal.ESTMALE = true;
            testAnimal.ANNEENAISSANCE = "2019";
            testAnimal.RACE = dbContext.RACE.Find(1);
            testAnimal.IDRACE = testAnimal.RACE.IDRACE;
            testAnimal.CLIENT = dbContext.CLIENT.Find(1);
            testAnimal.IDCLIENT = testAnimal.CLIENT.IDCLIENT;

            // Ajout de l'animal de test à la base
            dbContext.ANIMAL.Add(testAnimal);
            dbContext.SaveChanges();

            // Il doit normalement exister
            Assert.IsTrue(AnimalController.IsAlreadyRegistered(testAnimal));

            // Suppression de l'animal de test
            dbContext.ANIMAL.Remove(testAnimal);
            dbContext.SaveChanges();

            // Il ne doit plus exister
            Assert.IsFalse(AnimalController.IsAlreadyRegistered(testAnimal));
        }

        [TestMethod()]
        public void RegisterAnimalTest()
        {
            // Création d'un animal de test
            ANIMAL test = new ANIMAL();
            test.RACE = dbContext.RACE.Find(1);
            test.IDRACE = test.RACE.IDRACE;
            test.CLIENT = dbContext.CLIENT.Find(3);
            test.IDCLIENT = test.CLIENT.IDCLIENT;
            test.NOM = "Juvia";
            test.ANNEENAISSANCE = "2020";
            test.TAILLE = 74;
            test.POIDS = 50;
            test.ESTMALE = false;

            // Ajout de cet animal dans la base
            dbContext.ANIMAL.Add(test);
            dbContext.SaveChanges();

            // Création d'un animal déjà existant
            ANIMAL existing = new ANIMAL();
            existing.RACE = dbContext.RACE.Find(2);
            existing.IDRACE = existing.RACE.IDRACE;
            existing.CLIENT = dbContext.CLIENT.Find(3);
            existing.IDCLIENT = existing.CLIENT.IDCLIENT;
            existing.NOM = "Juvia";
            existing.ANNEENAISSANCE = "2009";
            existing.TAILLE = 90;
            existing.POIDS = 50;
            existing.ESTMALE = false;

            bool result = AnimalController.RegisterAnimal(existing.RACE, existing.CLIENT, existing.NOM, existing.ANNEENAISSANCE,
                                                          existing.TAILLE, existing.POIDS, existing.ESTMALE);
            Assert.IsFalse(result);

            // Création d'un animal inexistant
            CLIENT owner = dbContext.CLIENT.Find(2);
            result = AnimalController.RegisterAnimal(dbContext.RACE.Find(4), owner, "Karli", "2020", 10, 10, true);
            Assert.IsTrue(result);

            // Vérification de son existence
            ANIMAL created = dbContext.ANIMAL.Where(a => a.NOM.Equals("Karli") && a.CLIENT.IDCLIENT == owner.IDCLIENT).ToArray()[0];
            Assert.IsNotNull(created);
            Assert.AreEqual(created.CLIENT, owner);
            Assert.AreEqual(created.NOM, "Karli");
            Assert.AreEqual(created.POIDS, 10);
            Assert.AreEqual(created.TAILLE, 10);
            Assert.AreEqual(created.ESTMALE, true);
            Assert.AreEqual(created.ANNEENAISSANCE, "2020");

            // Suppression des données de tests
            dbContext.ANIMAL.Remove(test);
            dbContext.ANIMAL.Remove(created);
            dbContext.SaveChanges();
        }

        [TestMethod()]
        public void UpdateAnimalTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveAnimalTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddSpecieTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateSpecieTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void BreedsWithSpecieTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddBreedTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteBreedTest()
        {
            Assert.Fail();
        }

    }
}