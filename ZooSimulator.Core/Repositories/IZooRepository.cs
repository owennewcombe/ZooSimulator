using ZooSimulator.Core.Data;

namespace ZooSimulator.Core.Repositories
{
    public interface IZooRepository
    {
        List<IZooAnimal> GetAllAnimals();
    }
}
