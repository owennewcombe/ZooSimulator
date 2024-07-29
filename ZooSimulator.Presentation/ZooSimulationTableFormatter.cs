using System.Text;
using ZooSimulator.Core.Data;

namespace ZooSimulator.Presentation
{
    public static class ZooSimulationTableFormatter
    {
        public static string DisplayZooSimulationTable(List<IZooAnimal> animals, int numberOfDaysPassed)
        {
            var stringBuilder = new StringBuilder();

            var animalsGrouped = animals.GroupBy(a => a.GetType());

            int groupCount = 0;
            int animalCount = 0;

            stringBuilder.AppendLine($"\nDays passed {numberOfDaysPassed} \n");

            foreach (var animalGroup in animalsGrouped)
            {
                groupCount++;
                stringBuilder.AppendLine($"Animal group {groupCount} \n");

                foreach (var animal in animalGroup)
                {
                    animalCount++;
                    stringBuilder.AppendLine($"Name: {animal.GetType().Name} {animalCount} Health: {animal.Health.ToString("N2")} Status: {animal.Status}");
                }

                animalCount = 0;
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
    }
}
