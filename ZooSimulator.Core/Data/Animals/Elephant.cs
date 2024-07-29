namespace ZooSimulator.Core.Data.Animals
{
    public class Elephant : ZooAnimal
    {
        public override void UpdateStatus()
        {
            // Assumption: Elephant becomes healthy again when it's health is above 70 and when it previously coudln't walk
            if (Health < 70)
            {
                if (Status == AnimalStatus.Healthy)
                {
                    Status = AnimalStatus.CannotWalk;
                }
                else if (Status == AnimalStatus.CannotWalk)
                {
                    Status = AnimalStatus.Dead;
                }
            }
            else
            {
                Status = AnimalStatus.Healthy;
            }
        }
    }
}
