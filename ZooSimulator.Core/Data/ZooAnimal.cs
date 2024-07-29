namespace ZooSimulator.Core.Data
{
    public abstract class ZooAnimal : IZooAnimal
    {
        public ZooAnimal()
        {
            Health = 100;
        }

        public float Health { get; set; }

        public AnimalStatus Status { get; set; }

        public void ChangeHealthByPercentage(float percentageAmount, bool increase = false)
        {
            if (Status == AnimalStatus.Dead)
            {
                // Assumption: We don't update the health of dead animals?

                return;
            }

            float percentageChange = Health * (percentageAmount / 100);

            if (percentageAmount == 0)
            {
                return;
            }
            else
            {
                UpdateAnimalHealth(increase, percentageChange);
            }
        }

        private void UpdateAnimalHealth(bool increase, float percentageChange)
        {
            if (increase)
            {
                Health += percentageChange;

                if (Health > 100)
                {
                    Health = 100;
                }
            }
            else
            {
                // Assumption: Health can't be below 0?

                if (Health < 0)
                {
                    Health = 0;
                }

                Health -= percentageChange;
            }
        }

        public abstract void UpdateStatus();
    }
}
