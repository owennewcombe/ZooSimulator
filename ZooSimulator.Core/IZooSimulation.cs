using ZooSimulator.Core.Data;

namespace ZooSimulator.Core
{
    public interface IZooSimulation
    {
        void UpdateAnimalHealth();

        void FeedAnimals();

        List<IZooAnimal> GetAllZooAnimals();
    }
}
