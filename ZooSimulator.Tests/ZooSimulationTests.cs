using Moq;
using ZooSimulator.Core;
using ZooSimulator.Core.Constants;
using ZooSimulator.Core.Data;
using ZooSimulator.Core.Data.Animals;
using ZooSimulator.Core.Repositories;
using ZooSimulator.Core.Utilities;

namespace ZooSimulator.Tests
{
    [TestClass]
    public class ZooSimulationTests
    {
        private IZooSimulation _zooSimulation;
        private Mock<IRandomNumberProvider> _randomNumberProvider;

        public ZooSimulationTests()
        {
            _randomNumberProvider = new Mock<IRandomNumberProvider>();
            _randomNumberProvider.Setup(r => r.GetRandomNumber(RandomNumberProviderConstants.HealthUpdateMinValue, RandomNumberProviderConstants.HealthUpdateMaxValue)).Returns(20);
            _randomNumberProvider.SetupSequence(r => r.GetRandomNumber(RandomNumberProviderConstants.FeedUpdateMinValue, RandomNumberProviderConstants.FeedUpdateMaxValue)).Returns(10).Returns(15).Returns(20);
            _zooSimulation = new ZooSimulation(new DefaultZooRepository(), _randomNumberProvider.Object);
        }


        [TestMethod]
        public void Zoo_Is_Populated_With_5_Of_EachAnimal_With_Default_Repository()
        {
            var animals = _zooSimulation.GetAllZooAnimals();

            Assert.IsTrue(animals.OfType<Monkey>().Count() == 5);
            Assert.IsTrue(animals.OfType<Giraffe>().Count() == 5);
            Assert.IsTrue(animals.OfType<Elephant>().Count() == 5);
            Assert.IsTrue(animals.All(a => a.Health == 100));
        }

        [TestMethod]
        public void Zoo_Handles_Null_Repository()
        {
            var zooRepository = new Mock<IZooRepository>();
            zooRepository.Setup(r => r.GetAllAnimals()).Returns(() => null);
            _zooSimulation = new ZooSimulation(zooRepository.Object, _randomNumberProvider.Object);

            Assert.ThrowsException<ArgumentException>(() => _zooSimulation.UpdateAnimalHealth());
        }

        [TestMethod]
        public void Zoo_Handles_Empty_Repository()
        {
            var zooRepository = new Mock<IZooRepository>();
            zooRepository.Setup(r => r.GetAllAnimals()).Returns(() => new List<IZooAnimal>());
            _zooSimulation = new ZooSimulation(zooRepository.Object, _randomNumberProvider.Object);

            Assert.ThrowsException<ArgumentException>(() => _zooSimulation.UpdateAnimalHealth());
        }

        [TestMethod]
        public void Health_Each_Animal_Decreased_On_Update()
        {
            _zooSimulation.UpdateAnimalHealth();

            var animals = _zooSimulation.GetAllZooAnimals();

            Assert.IsTrue(animals.All(a => a.Health == 80));
        }

        [TestMethod]
        public void Health_Each_Animal_Decreased_By_Different_Amount()
        {
            _randomNumberProvider.SetupSequence(r => r.GetRandomNumber(RandomNumberProviderConstants.HealthUpdateMinValue, RandomNumberProviderConstants.HealthUpdateMaxValue)).Returns(10).Returns(15);

            _zooSimulation.UpdateAnimalHealth();

            var animals = _zooSimulation.GetAllZooAnimals();

            Assert.IsTrue(animals.ElementAt(0).Health == 90);
            Assert.IsTrue(animals.ElementAt(1).Health == 85);
        }

        [TestMethod]
        public void Health_Each_Animal_Increased_After_Feeding()
        {
            _zooSimulation.UpdateAnimalHealth();
            _zooSimulation.FeedAnimals();

            var animals = _zooSimulation.GetAllZooAnimals();

            Assert.IsTrue(animals.OfType<Monkey>().All(a => a.Health == 88));
            Assert.IsTrue(animals.OfType<Giraffe>().All(a => a.Health == 92));
            Assert.IsTrue(animals.OfType<Elephant>().All(a => a.Health == 96));
        }
    }
}