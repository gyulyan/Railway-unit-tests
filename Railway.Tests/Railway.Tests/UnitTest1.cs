namespace Railway.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using static System.Collections.Specialized.BitVector32;
    using System.Threading;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("Ivan")]
        [TestCase("Pesho Meshow")]
        [TestCase("Guru123")]
        public void TestConstructorWithValidCredentials(string name)
        {
            RailwayStation railway = new RailwayStation(name);
            Assert.Pass(name, railway.Name);
            Assert.IsNotNull(railway.ArrivalTrains);
            Assert.IsNotNull(railway.DepartureTrains);

        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("     ")]
        [TestCase(null)]
        public void TestConstructorWithInValidCredentials(string name)
        {
            ArgumentException message = Assert.Throws<ArgumentException>(()
               =>
            { RailwayStation railway = new RailwayStation(name); });

            Assert.AreEqual("Name cannot be null or empty!", message.Message);

        }

        [TestCase("Train")]
        public void TestIfNewArrivalOnBoardWorksAsExpected(string name)
        {
            RailwayStation railway = new RailwayStation(name);
            railway.NewArrivalOnBoard(name);
            Assert.AreEqual(1, railway.ArrivalTrains.Count);
        }

        [Test]
        public void TestIfNewArrivalOnBoardWorksAsExpectedWitMoreThanOneEntity()
        {
            RailwayStation railway = new RailwayStation("Gosho");
            railway.NewArrivalOnBoard("Gosho");
            railway.NewArrivalOnBoard("Pehso");
            railway.NewArrivalOnBoard("Tosho");
            Assert.AreEqual(3, railway.ArrivalTrains.Count);
        }

        [Test]
        public void TestIfTrainHasArrivedWorksAsExpected()
        {
            RailwayStation railway = new RailwayStation("Gosho");
            railway.NewArrivalOnBoard("Tosho");

            string result = railway.TrainHasArrived("Tosho");

            Assert.AreEqual("Tosho is on the platform and will leave in 5 minutes.", result);
            Assert.AreEqual(1, railway.DepartureTrains.Count);
            Assert.AreEqual(0, railway.ArrivalTrains.Count);
        }

        [Test]
        public void TestIfTrainHasArrivedWorksAsExpectedIfConditionIsNotOk()
        {
            RailwayStation railway = new RailwayStation("Gosho");
            railway.NewArrivalOnBoard("Tosho");

            string result = railway.TrainHasArrived("Pesho");

            Assert.AreEqual("There are other trains to arrive before Pesho.", result);
            Assert.AreEqual(0, railway.DepartureTrains.Count);
            Assert.AreEqual(1, railway.ArrivalTrains.Count);
        }

        [Test]
        public void TestIfTrainHasLeftWorksAsExpected()
        {
            RailwayStation railway = new RailwayStation("Gosho");
            railway.NewArrivalOnBoard("Tosho");
            railway.TrainHasArrived("Tosho");


            bool result = railway.TrainHasLeft("Tosho");

            Assert.AreEqual(true, result);
            Assert.AreEqual(0, railway.DepartureTrains.Count);
            Assert.AreEqual(0, railway.ArrivalTrains.Count);

        }

        [Test]
        public void TestIfTrainHasLeftWorksAsExpectedIfNo()
        {
            RailwayStation railway = new RailwayStation("Gosho");
            railway.NewArrivalOnBoard("Tosho");
            railway.TrainHasArrived("Tosho");


            bool result = railway.TrainHasLeft("Gosho");

            Assert.AreEqual(false, result);
            Assert.AreEqual(1, railway.DepartureTrains.Count);
            Assert.AreEqual(0, railway.ArrivalTrains.Count);

        }
    }
}