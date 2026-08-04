namespace Pantheon.Core
{
    public class SystemRandom : IRandom
    {
        private readonly System.Random _random;

        public SystemRandom()
        {
            _random = new System.Random();
        }

        public SystemRandom(int seed)
        {
            _random = new System.Random(seed);
        }

        public int Next(int maxExclusive)
        {
            return _random.Next(maxExclusive);
        }
    }
}
