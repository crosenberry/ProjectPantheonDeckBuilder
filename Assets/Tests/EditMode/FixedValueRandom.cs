using Pantheon.Core;

namespace Pantheon.Core.Tests
{
    // Deterministic IRandom double that always returns a fixed value, regardless
    // of maxExclusive. Used to pin down which branch a weighted-roll consumer
    // takes (e.g. Enemy.ChooseNextIntent), unlike FakeRandom which is specific
    // to Fisher-Yates shuffle determinism.
    public class FixedValueRandom : IRandom
    {
        private readonly int _value;

        public FixedValueRandom(int value)
        {
            _value = value;
        }

        public int Next(int maxExclusive)
        {
            return _value;
        }
    }
}
