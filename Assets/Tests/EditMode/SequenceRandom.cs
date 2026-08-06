using Pantheon.Core;

namespace Pantheon.Core.Tests
{
    // Deterministic IRandom double that returns a fixed sequence of values, one
    // per call, regardless of maxExclusive. Used to prove a value changes across
    // successive rolls (e.g. Enemy re-choosing its intent after acting), which a
    // constant-value fake like FixedValueRandom can't demonstrate.
    public class SequenceRandom : IRandom
    {
        private readonly int[] _values;
        private int _index;

        public SequenceRandom(params int[] values)
        {
            _values = values;
        }

        public int Next(int maxExclusive)
        {
            var value = _values[_index];
            _index = System.Math.Min(_index + 1, _values.Length - 1);
            return value;
        }
    }
}
