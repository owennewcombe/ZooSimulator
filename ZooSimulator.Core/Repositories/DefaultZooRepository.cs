using ZooSimulator.Core.Data;
using ZooSimulator.Core.Data.Animals;

namespace ZooSimulator.Core.Repositories
{
    public class DefaultZooRepository : IZooRepository
    {
        private readonly List<IZooAnimal> _allAnimals = [];
        private const int NumberOfEachAnimal = 5;

        public List<IZooAnimal> GetAllAnimals()
        {
            for (int i = 0; i < NumberOfEachAnimal; i++)
            {
                _allAnimals.Add(new Monkey());
                _allAnimals.Add(new Giraffe());
                _allAnimals.Add(new Elephant());
            }

            return _allAnimals;
        }
    }
}
