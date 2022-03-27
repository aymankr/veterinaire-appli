using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mauxnimale_CE2.api.entities;

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
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveAnimalTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AllSpeciesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void BreedsWithSpecieTest()
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