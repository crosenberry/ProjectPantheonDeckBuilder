using Pantheon.Core.Progression;

namespace Pantheon.Core.Blessings.Anubis
{
    public static class AnubisPassiveTree
    {
        public static PassiveTree Create()
        {
            var trunk = new PassiveTreeBranch("Trunk", new[]
            {
                new PassiveTreeNode("Embalmed Resilience", 40),
                new PassiveTreeNode("Tomb's Wealth", 50),
                new PassiveTreeNode("Bound Jackal", 60),
                new PassiveTreeNode("Anubis's Sight", 70),
                new PassiveTreeNode("Rite of Passage", 80)
            });

            var balanceKeeper = new PassiveTreeBranch("Balance-keeper", new[]
            {
                new PassiveTreeNode("Steady Scale", 60),
                new PassiveTreeNode("Ma'at's Whisper", 90, isChoicePair: true),
                new PassiveTreeNode("Centered Focus", 110),
                new PassiveTreeNode("Perfect Balance", 200)
            });

            var reaper = new PassiveTreeBranch("Reaper", new[]
            {
                new PassiveTreeNode("Bound to Chaos", 60),
                new PassiveTreeNode("Apep's Favor", 90, isChoicePair: true),
                new PassiveTreeNode("Devourer's Patience", 110),
                new PassiveTreeNode("Ammit's Feast", 200)
            });

            var ascendant = new PassiveTreeBranch("Ascendant", new[]
            {
                new PassiveTreeNode("Blessed by Ma'at", 60),
                new PassiveTreeNode("Osiris's Grace", 90, isChoicePair: true),
                new PassiveTreeNode("Eternal Vigil", 110),
                new PassiveTreeNode("Osiris's Dominion", 200)
            });

            return new PassiveTree(trunk, new[] { balanceKeeper, reaper, ascendant });
        }
    }
}
