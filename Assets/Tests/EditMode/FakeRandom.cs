using Pantheon.Core;

namespace Pantheon.Core.Tests
{
    // Deterministic IRandom double for tests. Always swaps with index 0 in a
    // Fisher-Yates shuffle, producing a fixed, repeatable permutation - tests
    // that use it assert on which cards ended up where, not on shuffle quality.
    public class FakeRandom : IRandom
    {
        public int Next(int maxExclusive)
        {
            return 0;
        }
    }
}
