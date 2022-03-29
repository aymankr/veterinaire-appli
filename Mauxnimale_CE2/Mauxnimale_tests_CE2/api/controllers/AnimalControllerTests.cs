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
            ANIMAL created = dbContext.ANIMAL.Where(a => a.NOM.Equals("Karli") && a.CLIENT.IDCLIENT == owner.IDCLIENT).FirstOrDefault();
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

            // Ajout de l'animal de test
            dbContext.ANIMAL.Add(test);
            dbContext.SaveChanges();

            // Update de toutes les infos
            string newName = "Marcel";
            RACE newBreed = dbContext.RACE.Find(2);
            CLIENT newClient = dbContext.CLIENT.Find(9);
            string newBirthYear = "2018";
            int newSize = 20, newWeight = 30;
            bool newGenre = true;

            AnimalController.UpdateAnimal(test, newBreed, newClient, newName, newBirthYear, newSize, newWeight, newGenre);

            // Vérification de l'intégrité des nouvelles informations
            // Avec l'objet déjà créé
            Assert.AreEqual(test.NOM, newName);
            Assert.AreEqual(test.RACE, newBreed);
            Assert.AreEqual(test.CLIENT, newClient);
            Assert.AreEqual(test.ANNEENAISSANCE, newBirthYear);
            Assert.AreEqual(test.TAILLE, newSize);
            Assert.AreEqual(test.POIDS, newWeight);
            Assert.AreEqual(test.ESTMALE, newGenre);

            // En récupérant de la bd
            ANIMAL updated = dbContext.ANIMAL.Where(a => a.NOM.Equals(newName) && a.IDCLIENT.Equals(newClient.IDCLIENT)).FirstOrDefault();
            Assert.AreEqual(updated.NOM, newName);
            Assert.AreEqual(updated.RACE, newBreed);
            Assert.AreEqual(updated.CLIENT, newClient);
            Assert.AreEqual(updated.ANNEENAISSANCE, newBirthYear);
            Assert.AreEqual(updated.TAILLE, newSize);
            Assert.AreEqual(updated.POIDS, newWeight);
            Assert.AreEqual(updated.ESTMALE, newGenre);

            // Suppression de l'animal de test
            dbContext.ANIMAL.Remove(test);
            dbContext.SaveChanges();
        }

        [TestMethod()]
        public void RemoveAnimalTest()
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

            dbContext.ANIMAL.Add(test);
            dbContext.SaveChanges();

            // Suppression de l'animal de test à l'aide de la méthode
            AnimalController.RemoveAnimal(test);

            // Vérifions qu'il a bien été supprimé
            Assert.AreEqual(dbContext.ANIMAL.Where(a => a.NOM.Equals("Juvia") && a.IDCLIENT.Equals(test.IDCLIENT)).Count(), 0);

            // Ajoutons des dépendances à cet animal et vérifions qu'elles sont bien supprimées

            // Maladie
            LIEN_MALADIE testDeases = new LIEN_MALADIE();
            testDeases.ANIMAL = test;
            testDeases.IDANIMAL = test.IDANIMAL;
            testDeases.MALADIE = dbContext.MALADIE.Find(1);
            testDeases.IDMALADIE = testDeases.MALADIE.IDMALADIE;

            // Rendez-vous
            RENDEZ_VOUS testAppointment = new RENDEZ_VOUS();
            testAppointment.ANIMAL.Add(test);
            testAppointment.CLIENT = test.CLIENT;
            testAppointment.IDCLIENT = test.CLIENT.IDCLIENT;
            testAppointment.HEUREDEBUT = new System.TimeSpan(11, 15, 0);
            testAppointment.HEUREFIN = new System.TimeSpan(11, 40, 0);
            testAppointment.RAISON = "Test";
            testAppointment.TYPE_RDV = dbContext.TYPE_RDV.Find(1);
            testAppointment.IDTYPE = testAppointment.TYPE_RDV.IDTYPE;
            testAppointment.JOURNEE = dbContext.JOURNEE.Find(50);
            testAppointment.IDJOURNEE = testAppointment.JOURNEE.IDJOURNEE;

            // Ordonnance
            ORDONNANCE testPrescription = new ORDONNANCE();
            testPrescription.ANIMAL = test;
            testPrescription.IDANIMAL = test.IDANIMAL;
        }

        [TestMethod()]
        public void AddSpecieTest()
        {
            // Création d'une nouvelle espèce de test
            Assert.IsTrue(AnimalController.AddSpecie("test"));

            // Vérification de sa présence dans la bd et de l'intégrité des informations
            ESPECE created = dbContext.ESPECE.Where(e => e.NOMESPECE.Equals("test")).FirstOrDefault();
            Assert.IsNotNull(created);
            Assert.AreEqual(created.NOMESPECE, "test");

            // Création de la même espèce (doit échouer)
            Assert.IsFalse(AnimalController.AddSpecie("test"));

            // Suppression de l'espèce de test
            dbContext.ESPECE.Remove(created);
            dbContext.SaveChanges();

            // Vérifions qu'un doublon n'a pas été créé
            ESPECE duplicate = dbContext.ESPECE.Where(e => e.NOMESPECE.Equals("test")).FirstOrDefault();
            Assert.IsNull(duplicate);
        }

        [TestMethod()]
        public void UpdateSpecieTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteSpecieTest()
        {
            // Création d'une nouvelle espèce de test
            ESPECE test = new ESPECE();
            test.NOMESPECE = "test";

            dbContext.ESPECE.Add(test);
            dbContext.SaveChanges();

            // Suprression de cette espèce sans race
            AnimalController.DeleteSpecie(test);

            // Vérificaiton de la suppression
            Assert.IsNull(dbContext.ESPECE.Where(e => e.NOMESPECE.Equals("test")).FirstOrDefault());

            // Création de races liées à cette espèce
            RACE breed1 = new RACE(), breed2 = new RACE();
            breed1.NOMRACE = "testBreed1";
            breed1.ESPECE = test;
            breed1.IDESPECE = test.IDESPECE;
            breed2.NOMRACE = "testBreed2";
            breed2.ESPECE = test;
            breed2.IDESPECE = test.IDESPECE;

            test.RACE.Add(breed1);
            test.RACE.Add(breed2);

            dbContext.ESPECE.Add(test);
            dbContext.RACE.Add(breed1);
            dbContext.RACE.Add(breed2);
            dbContext.SaveChanges();

            // Suprression de cette espèce avec les races liées
            AnimalController.DeleteSpecie(test);

            // Vérification des suppressions
            Assert.IsNull(dbContext.ESPECE.Where(e => e.NOMESPECE.Equals("test")).FirstOrDefault());
            Assert.IsNull(dbContext.RACE.Where(r => r.NOMRACE.Equals("breed1")).FirstOrDefault());
            Assert.IsNull(dbContext.RACE.Where(r => r.NOMRACE.Equals("breed2")).FirstOrDefault());
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