namespace ZooSimulator.Core.Data.Animals
{
    public class Giraffe : ZooAnimal
    {
        public override void UpdateStatus()
        {
            if (Health < 50)
            {
                Status = AnimalStatus.Dead;
            }
        }
    }
}
