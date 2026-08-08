using Pantheon.Core.Progression;

namespace Pantheon.Core.Blessings.Artemis
{
    public static class ArtemisPassiveTree
    {
        public static PassiveTree Create()
        {
            var trunk = new PassiveTreeBranch("Trunk", new[]
            {
                new PassiveTreeNode("Huntress's Vigor", 40),
                new PassiveTreeNode("Favor of the Hunt", 50),
                new PassiveTreeNode("Steady Quiver", 60),
                new PassiveTreeNode("Hunter's Instinct", 70),
                new PassiveTreeNode("Well-Aimed Start", 80)
            });

            var barrage = new PassiveTreeBranch("Barrage", new[]
            {
                new PassiveTreeNode("Nimble Draw", 60),
                new PassiveTreeNode("Momentum's Edge", 90, isChoicePair: true),
                new PassiveTreeNode("Overdraw Mastery", 110),
                new PassiveTreeNode("Hail of a Thousand Arrows", 200)
            });

            var fullDraw = new PassiveTreeBranch("Full Draw", new[]
            {
                new PassiveTreeNode("Practiced Patience", 60),
                new PassiveTreeNode("Momentum's Reserve", 90, isChoicePair: true),
                new PassiveTreeNode("Loaded Quiver", 110),
                new PassiveTreeNode("The Unbroken Nock", 200)
            });

            var huntress = new PassiveTreeBranch("Huntress", new[]
            {
                new PassiveTreeNode("Marked for the Hunt", 60),
                new PassiveTreeNode("Hunter's Reflex", 90, isChoicePair: true),
                new PassiveTreeNode("Silent Step", 110),
                new PassiveTreeNode("One Perfect Shot", 200)
            });

            return new PassiveTree(trunk, new[] { barrage, fullDraw, huntress });
        }
    }
}
