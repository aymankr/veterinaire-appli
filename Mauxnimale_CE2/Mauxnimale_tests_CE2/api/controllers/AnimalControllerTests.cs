using System;
using System.Linq;
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
            dbContext.ANIMAL.Remove(updated);
            dbContext.SaveChanges();
        }

        [TestMethod()]
        public void RemoveAnimalTest()
        {
            // Création d'un animal de test
            ANIMAL test = new ANIMAL();
            test.RACE = dbContext.RACE.Find(1);
            test.IDRACE = test.RACE.IDRACE;
            test.CLIENT = dbContext.CLIENT.Find(9);
            test.IDCLIENT = test.CLIENT.IDCLIENT;
            test.NOM = "Juvia";
            test.ANNEENAISSANCE = "2020";
            test.TAILLE = 74;
            test.POIDS = 50;
            test.ESTMALE = false;

            dbContext.ANIMAL.Add(test);
            dbContext.SaveChanges();

            // Suppression de l'animal de test
            AnimalController.RemoveAnimal(test);

            // Vérification de la suppression
            Assert.IsNull(dbContext.ANIMAL.Where(a => a.NOM.Equals("Juvia") && a.IDCLIENT.Equals(test.IDCLIENT)).FirstOrDefault());

            dbContext.ANIMAL.Add(test);

            // Ajoutons des dépendances à cet animal
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
            testPrescription.RENDEZ_VOUS = testAppointment;
            testPrescription.IDRDV = testAppointment.IDRDV;

            test.LIEN_MALADIE.Add(testDeases);
            test.RENDEZ_VOUS.Add(testAppointment);
            test.ORDONNANCE.Add(testPrescription);

            dbContext.LIEN_MALADIE.Add(testDeases);
            dbContext.RENDEZ_VOUS.Add(testAppointment);
            dbContext.ORDONNANCE.Add(testPrescription);
            dbContext.SaveChanges();

            // Supprimons le tout
            AnimalController.RemoveAnimal(test);

            // Vérifions qu'elles sont bien supprimées
            Assert.IsNull(dbContext.ANIMAL.Where(a => a.NOM.Equals("Juvia") && a.IDCLIENT.Equals(test.IDCLIENT)).FirstOrDefault());
            Assert.IsNull(dbContext.LIEN_MALADIE.Where(d => d.IDANIMAL.Equals(test.IDANIMAL) && d.IDMALADIE.Equals(testDeases.IDMALADIE)).FirstOrDefault());
            Assert.IsNull(dbContext.RENDEZ_VOUS.Where(a => a.IDCLIENT.Equals(testAppointment.IDCLIENT) &&
                                                           a.IDJOURNEE.Equals(testAppointment.IDJOURNEE) &&
                                                           a.HEUREDEBUT.Equals(testAppointment.HEUREDEBUT) &&
                                                           a.HEUREFIN.Equals(testAppointment.HEUREFIN)).FirstOrDefault());
            Assert.IsNull(dbContext.ORDONNANCE.Where(p => p.IDANIMAL.Equals(test.IDANIMAL)).FirstOrDefault());
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
            // Création d'une nouvelle espèce de test
            ESPECE test = new ESPECE();
            test.NOMESPECE = "test";

            dbContext.ESPECE.Add(test);
            dbContext.SaveChanges();

            // Modification de l'espèce (son nom)
            string newName = "update";
            Console.WriteLine(AnimalController.UpdateSpecie(test, newName));

            // Vérification de l'intégrité des données
            // Avec l'objet précédemment créé
            Assert.AreEqual(test.NOMESPECE, newName);

            // Avec la ligne dans la bd
            Assert.AreEqual(dbContext.ESPECE.Where(s => s.NOMESPECE.Equals(newName)).FirstOrDefault().NOMESPECE, newName);

            // Suppression de la race de test
            dbContext.ESPECE.Remove(test);
            dbContext.SaveChanges();
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
        public void AddBreedTest()
        {
            // Ajout de la race (sabs animaux liés pour le moment)
            ESPECE breedSpecie = dbContext.ESPECE.Find(1);
            AnimalController.AddBreed(breedSpecie, "test");

            // Vérification de l'existence de la race et de l'intégrité des données
            RACE added = dbContext.RACE.Where(b => b.NOMRACE.Equals("test") && b.IDESPECE.Equals(breedSpecie.IDESPECE)).FirstOrDefault();
            Assert.IsNotNull(added);
            Assert.AreEqual(added.NOMRACE, "test");
            Assert.AreEqual(added.IDESPECE, breedSpecie.IDESPECE);

            // Suppression de la race de test
            dbContext.RACE.Remove(added);
            dbContext.SaveChanges();
        }

        [TestMethod()]
        public void UpdateBreedTest()
        {
            // Ajout d'une race de test
            RACE breedTest = new RACE();
            breedTest.NOMRACE = "test";
            breedTest.ESPECE = dbContext.ESPECE.Find(1);
            breedTest.IDESPECE = breedTest.ESPECE.IDESPECE;

            dbContext.RACE.Add(breedTest);
            dbContext.SaveChanges();

            // Modification des infos
            ESPECE newSpecie = dbContext.ESPECE.Find(2);
            string newName = "updating";

            AnimalController.UpdateBreed(breedTest, newSpecie, newName);

            // Vérification de la modification
            // Avec l'objet précédemment créé
            Assert.AreEqual(breedTest.NOMRACE, newName);
            Assert.AreEqual(breedTest.ESPECE, newSpecie);

            // Avec la ligne dans la bd
            RACE updated = dbContext.RACE.Where(r => r.NOMRACE.Equals(newName) && r.IDESPECE.Equals(newSpecie.IDESPECE)).FirstOrDefault();
            Assert.AreEqual(updated.NOMRACE, newName);
            Assert.AreEqual(updated.ESPECE, newSpecie);

            // Suppression de la race de test
            dbContext.RACE.Remove(updated);
            dbContext.SaveChanges();
        }

        [TestMethod()]
        public void DeleteBreedTest()
        {
            // Ajout d'une race de test
            ESPECE breedSpecie = dbContext.ESPECE.Find(1);

            RACE breedTest = new RACE();
            breedTest.NOMRACE = "test";
            breedTest.ESPECE = breedSpecie;
            breedTest.IDESPECE = breedTest.ESPECE.IDESPECE;

            dbContext.RACE.Add(breedTest);
            dbContext.SaveChanges();

            // Suppression de la race sans animaux
            AnimalController.DeleteBreed(breedTest);

            // Vérification de la suppression
            Assert.IsNull(dbContext.RACE.Where(b => b.NOMRACE.Equals("test") && b.IDESPECE.Equals(breedSpecie.IDESPECE)).FirstOrDefault());

            // Ajoutons des animaux à cet race
            dbContext.RACE.Add(breedTest);

            ANIMAL test1 = new ANIMAL();
            test1.RACE = breedTest;
            test1.IDRACE = breedTest.IDRACE;
            test1.CLIENT = dbContext.CLIENT.Find(9);
            test1.IDCLIENT = test1.CLIENT.IDCLIENT;
            test1.NOM = "Juvia";
            test1.ANNEENAISSANCE = "2020";
            test1.TAILLE = 74;
            test1.POIDS = 50;
            test1.ESTMALE = false;

            ANIMAL test2 = new ANIMAL();
            test2.RACE = breedTest;
            test2.IDRACE = breedTest.IDRACE;
            test2.CLIENT = dbContext.CLIENT.Find(2);
            test2.IDCLIENT = test2.CLIENT.IDCLIENT;
            test2.NOM = "Grey";
            test2.ANNEENAISSANCE = "2020";
            test2.TAILLE = 89;
            test2.POIDS = 10;
            test2.ESTMALE = true;

            dbContext.ANIMAL.Add(test1);
            dbContext.ANIMAL.Add(test2);
            dbContext.SaveChanges();

            // Vérifions que tout est supprimé
            AnimalController.DeleteBreed(breedTest);
            Assert.IsNull(dbContext.RACE.Where(b => b.NOMRACE.Equals("test") && b.IDESPECE.Equals(breedSpecie.IDESPECE)).FirstOrDefault());
            Assert.IsNull(dbContext.ANIMAL.Where(a => a.NOM.Equals("Juvia")).FirstOrDefault());
            Assert.IsNull(dbContext.ANIMAL.Where(a => a.NOM.Equals("Grey")).FirstOrDefault());
        }
    }
}