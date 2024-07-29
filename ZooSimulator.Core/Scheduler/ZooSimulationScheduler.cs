using System.Timers;
using ZooSimulator.Core.Data;
using ZooSimulator.Core.Repositories;
using ZooSimulator.Core.Utilities;
using Timer = System.Timers.Timer;

namespace ZooSimulator.Core.Scheduler
{
    public class ZooSimulationScheduler
    {
        private readonly ZooSimulation _simulation;
        private int numberOfDaysPassed = 0;
        public ZooSimulationScheduler()
        {
            _simulation = new ZooSimulation(new DefaultZooRepository(), new RandomNumberProvider());
        }

        public void StartTimer(Action<List<IZooAnimal>, int> action)
        {
            Timer timer = new Timer(20000);
            timer.Elapsed += (sender, e) => UpdateZoo(sender, e, action);
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        public ZooSimulation GetSimulation()
        {
            return _simulation;
        }

        private void UpdateZoo(Object? source, ElapsedEventArgs e, Action<List<IZooAnimal>, int> action)
        {
            _simulation.UpdateAnimalHealth();
            numberOfDaysPassed++;
            action(_simulation.GetAllZooAnimals(), numberOfDaysPassed);
        }
    }
}
