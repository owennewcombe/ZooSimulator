namespace ZooSimulator.Core.Utilities
{
    public class RandomNumberProvider : IRandomNumberProvider
    {
        public int GetRandomNumber(int min, int max)
        {
            Random random = new Random();

            return random.Next(min, max);
        }
    }
}
