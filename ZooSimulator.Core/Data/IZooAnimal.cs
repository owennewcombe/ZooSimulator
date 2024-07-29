namespace ZooSimulator.Core.Data
{
    public interface IZooAnimal
    {
        void ChangeHealthByPercentage(float percentageAmount, bool increase = false);

        abstract void UpdateStatus();

        float Health { get; }

        AnimalStatus Status { get; }
    }
}
