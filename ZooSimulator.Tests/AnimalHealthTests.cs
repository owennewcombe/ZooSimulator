using Moq;
using ZooSimulator.Core.Repositories;
using ZooSimulator.Core.Utilities;
using ZooSimulator.Core;
using ZooSimulator.Core.Data;
using ZooSimulator.Core.Data.Animals;
using ZooSimulator.Core.Constants;

namespace ZooSimulator.Tests
{
    [TestClass]
    public class AnimalHealthTests
    {
        private IZooSimulation _zooSimulation;
        private Mock<IRandomNumberProvider> _randomNumberProvider;
        private Mock<IZooRepository> _zooRepository;

        public AnimalHealthTests()
        {
            _randomNumberProvider = new Mock<IRandomNumberProvider>();
            _zooRepository = new Mock<IZooRepository>();

            _randomNumberProvider.Setup(r => r.GetRandomNumber(RandomNumberProviderConstants.HealthUpdateMinValue, RandomNumberProviderConstants.HealthUpdateMaxValue)).Returns(20);
            _randomNumberProvider.SetupSequence(r => r.GetRandomNumber(RandomNumberProviderConstants.FeedUpdateMinValue, RandomNumberProviderConstants.FeedUpdateMaxValue)).Returns(10).Returns(15).Returns(20);
            _zooRepository.Setup(r => r.GetAllAnimals()).Returns(ZooAnimals());

            _zooSimulation = new ZooSimulation(_zooRepository.Object, _randomNumberProvider.Object);
        }

        [TestMethod]
        public void Monkey_Pronounced_Dead_When_Health_Below_30()
        {
            _zooSimulation.UpdateAnimalHealth();

            var animals = _zooSimulation.GetAllZooAnimals();

            Assert.IsTrue(animals.ElementAt(0).Status == AnimalStatus.Dead);
        }

        [TestMethod]
        public void Giraffe_Pronounced_Dead_When_Health_Below_50()
        {
            _zooSimulation.UpdateAnimalHealth();

            var animals = _zooSimulation.GetAllZooAnimals();

            Assert.IsTrue(animals.ElementAt(3).Status == AnimalStatus.Dead);
        }

        [TestMethod]
        public void Elephant_Cannot_Walk_When_Health_Below_70()
        {
            _zooSimulation.UpdateAnimalHealth();

            var animals = _zooSimulation.GetAllZooAnimals();

            Assert.IsTrue(animals.ElementAt(4).Status == AnimalStatus.CannotWalk);
        }

        [TestMethod]
        public void Elephant_Pronounced_Dead_When_Health_Below_70_And_Cannot_Walk()
        {
            _zooSimulation.UpdateAnimalHealth();

            var animals = _zooSimulation.GetAllZooAnimals();

            Assert.IsTrue(animals.ElementAt(5).Status == AnimalStatus.Dead);
        }

        [TestMethod]
        public void Dead_Animals_Stay_Dead_And_Health_Not_Updated()
        {
            _zooSimulation.UpdateAnimalHealth();

            var animals = _zooSimulation.GetAllZooAnimals();

            Assert.IsTrue(animals.ElementAt(2).Status == AnimalStatus.Dead && animals.ElementAt(2).Health == 28);
        }

        [TestMethod]
        public void Animal_Health_Cannot_Be_Increased_Over_100()
        {
            _zooSimulation.FeedAnimals();

            var animals = _zooSimulation.GetAllZooAnimals();

            Assert.IsTrue(animals.ElementAt(1).Health == 100);
        }

        private List<IZooAnimal> ZooAnimals()
        {
            return new List<IZooAnimal>
            {
                new Monkey { Health = 32, Status = AnimalStatus.Healthy },
                new Monkey { Health = 98, Status = AnimalStatus.Healthy },
                new Monkey { Health = 28, Status = AnimalStatus.Dead },
                new Giraffe { Health = 55, Status = AnimalStatus.Healthy },
                new Elephant { Health = 72, Status = AnimalStatus.Healthy },
                new Elephant { Health = 68, Status = AnimalStatus.CannotWalk }
            };
        }
    }
}
