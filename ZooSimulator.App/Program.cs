using ZooSimulator.Core.Data;
using ZooSimulator.Core.Scheduler;
using ZooSimulator.Presentation;

class Program
{
    private static ZooSimulationScheduler scheduler = new ZooSimulationScheduler();
    private static int numberOfDays = 0;

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Zoo Simulator Console App! \n");

        Console.WriteLine("Type f to feed the animals \n");

        Console.WriteLine("Initial animal health \n");

        Console.WriteLine(ZooSimulationTableFormatter.DisplayZooSimulationTable(scheduler.GetSimulation().GetAllZooAnimals(), numberOfDays));

        scheduler.StartTimer(ZooUpdateCallback);

        while (true)
        {
            ConsoleKeyInfo result = Console.ReadKey();
            if ((result.KeyChar == 'f'))
            {
                scheduler.GetSimulation().FeedAnimals();

                Console.WriteLine("\nAnimals have been fed, updated health:");

                Console.WriteLine(ZooSimulationTableFormatter.DisplayZooSimulationTable(scheduler.GetSimulation().GetAllZooAnimals(), numberOfDays));
            }
        }
    }

    static void ZooUpdateCallback(List<IZooAnimal> animals, int numberOfDaysPassed)
    {
        numberOfDays = numberOfDaysPassed;
        Console.WriteLine(ZooSimulationTableFormatter.DisplayZooSimulationTable(animals, numberOfDaysPassed));
    }
}

