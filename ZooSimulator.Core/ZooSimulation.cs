using ZooSimulator.Core.Constants;
using ZooSimulator.Core.Data;
using ZooSimulator.Core.Repositories;
using ZooSimulator.Core.Utilities;

namespace ZooSimulator.Core
{
    public class ZooSimulation : IZooSimulation
    {
        private readonly List<IZooAnimal> _allAnimals = [];
        private readonly IRandomNumberProvider _randomNumberProvider;
        public ZooSimulation(IZooRepository zooRepository, IRandomNumberProvider randomNumberProvider)
        {
            _randomNumberProvider = randomNumberProvider;
            _allAnimals = zooRepository.GetAllAnimals();
        }

        public List<IZooAnimal> GetAllZooAnimals()
        {
            return _allAnimals;
        }

        public void UpdateAnimalHealth()
        {
            if (_allAnimals == null || !_allAnimals.Any())
            {
                throw new ArgumentException("Repository contained no animals");
            }

            foreach (var animal in _allAnimals)
            {
                var randomDecrease = _randomNumberProvider.GetRandomNumber(RandomNumberProviderConstants.HealthUpdateMinValue, RandomNumberProviderConstants.HealthUpdateMaxValue);

                animal.ChangeHealthByPercentage(randomDecrease);
                animal.UpdateStatus();
            }
        }

        public void FeedAnimals()
        {
            var animalTypes = _allAnimals.GroupBy(x => x.GetType());

            foreach (var animalType in animalTypes)
            {
                var randomIncrease = _randomNumberProvider.GetRandomNumber(RandomNumberProviderConstants.FeedUpdateMinValue, RandomNumberProviderConstants.FeedUpdateMaxValue);

                foreach (var animal in animalType)
                {
                    animal.ChangeHealthByPercentage(randomIncrease, true);
                }
            }
        }
    }
}
