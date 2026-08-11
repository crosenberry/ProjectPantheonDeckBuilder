using System.Collections.Generic;

namespace Pantheon.Core.Run
{
    public static class Rift
    {
        public static IReadOnlyList<Mythology> RevealMythologyOptions(IReadOnlyList<Mythology> allMythologies, IRandom random, int revealCount)
        {
            var pool = new List<Mythology>(allMythologies);
            var revealed = new List<Mythology>();

            while (revealed.Count < revealCount)
            {
                var index = random.Next(pool.Count);
                revealed.Add(pool[index]);
                pool.RemoveAt(index);
            }

            return revealed;
        }
    }
}
