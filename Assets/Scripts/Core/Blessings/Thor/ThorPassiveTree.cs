using Pantheon.Core.Progression;

namespace Pantheon.Core.Blessings.Thor
{
    public static class ThorPassiveTree
    {
        public static PassiveTree Create()
        {
            var trunk = new PassiveTreeBranch("Trunk", new[]
            {
                new PassiveTreeNode("Ironbound Vigor", 40),
                new PassiveTreeNode("Hearth of Asgard", 50),
                new PassiveTreeNode("Tempered Grip", 60),
                new PassiveTreeNode("Stormsight", 70),
                new PassiveTreeNode("Well-Forged Start", 80)
            });

            var bulwark = new PassiveTreeBranch("Bulwark", new[]
            {
                new PassiveTreeNode("Steadfast Stance", 60),
                new PassiveTreeNode("Runed Resolve", 90, isChoicePair: true),
                new PassiveTreeNode("Immovable Wall", 110),
                new PassiveTreeNode("Wrath of the Aesir", 200)
            });

            var chain = new PassiveTreeBranch("Chain", new[]
            {
                new PassiveTreeNode("Live Current's Grace", 60),
                new PassiveTreeNode("Arcing Focus", 90, isChoicePair: true),
                new PassiveTreeNode("Overcharged Reflexes", 110),
                new PassiveTreeNode("Endless Current", 200)
            });

            var unrelenting = new PassiveTreeBranch("Unrelenting", new[]
            {
                new PassiveTreeNode("Battle Scars", 60),
                new PassiveTreeNode("Berserker's Will", 90, isChoicePair: true),
                new PassiveTreeNode("Undying Resolve", 110),
                new PassiveTreeNode("Ragnarok's Defiance", 200)
            });

            return new PassiveTree(trunk, new[] { bulwark, chain, unrelenting });
        }
    }
}
