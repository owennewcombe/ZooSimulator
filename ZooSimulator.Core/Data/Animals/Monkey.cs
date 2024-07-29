namespace ZooSimulator.Core.Data.Animals
{
    public class Monkey : ZooAnimal
    {
        public override void UpdateStatus()
        {
            if (Health < 30)
            {
                Status = AnimalStatus.Dead;
            }
        }
    }
}
