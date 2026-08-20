using Pantheon.Core.Progression;

namespace Pantheon.Core.Blessings.SunWukong
{
    public static class SunWukongPassiveTree
    {
        public static PassiveTree Create()
        {
            var trunk = new PassiveTreeBranch("Trunk", new[]
            {
                new PassiveTreeNode("Sage's Vigor", 40),
                new PassiveTreeNode("Peachwood Fortune", 50),
                new PassiveTreeNode("Iron-Skinned Ape", 60),
                new PassiveTreeNode("Heavenly Sight", 70),
                new PassiveTreeNode("Auspicious Start", 80)
            });

            var beastRush = new PassiveTreeBranch("Beast Rush", new[]
            {
                new PassiveTreeNode("Primal Reflex", 60),
                new PassiveTreeNode("Feral Awakening", 90, isChoicePair: true),
                new PassiveTreeNode("Unshakable Rage", 110),
                new PassiveTreeNode("Great Sage's Rampage", 200)
            });

            var immortalAscension = new PassiveTreeBranch("Immortal Ascension", new[]
            {
                new PassiveTreeNode("Heavenly Reflex", 60),
                new PassiveTreeNode("Celestial Favor", 90, isChoicePair: true),
                new PassiveTreeNode("Boundless Patience", 110),
                new PassiveTreeNode("Ascended Sage's Blessing", 200)
            });

            var seventyTwoChanges = new PassiveTreeBranch("72 Changes", new[]
            {
                new PassiveTreeNode("Fluid Reflex", 60),
                new PassiveTreeNode("Endless Metamorphosis", 90, isChoicePair: true),
                new PassiveTreeNode("Master of the Myriad Forms", 110),
                new PassiveTreeNode("The Sage's Infinite Self", 200)
            });

            return new PassiveTree(trunk, new[] { beastRush, immortalAscension, seventyTwoChanges });
        }
    }
}
