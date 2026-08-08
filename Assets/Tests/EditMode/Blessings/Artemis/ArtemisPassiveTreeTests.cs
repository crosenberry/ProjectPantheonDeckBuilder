using System.Linq;
using NUnit.Framework;
using Pantheon.Core.Progression;

namespace Pantheon.Core.Tests.Blessings.Artemis
{
    public class ArtemisPassiveTreeTests
    {
        [Test]
        public void Create_TrunkHasFiveNodes()
        {
            var tree = Core.Blessings.Artemis.ArtemisPassiveTree.Create();

            Assert.That(tree.Trunk.Nodes.Count, Is.EqualTo(5));
        }

        [Test]
        public void Create_HasThreeBranchesOfFourNodesEach()
        {
            var tree = Core.Blessings.Artemis.ArtemisPassiveTree.Create();

            Assert.That(tree.Branches.Count, Is.EqualTo(3));
            Assert.That(tree.Branches.All(b => b.Nodes.Count == 4), Is.True);
        }

        [Test]
        public void Create_TotalNodeCountIsSeventeen()
        {
            var tree = Core.Blessings.Artemis.ArtemisPassiveTree.Create();

            var total = tree.Trunk.Nodes.Count + tree.Branches.Sum(b => b.Nodes.Count);

            Assert.That(total, Is.EqualTo(17));
        }

        [Test]
        public void Create_BranchNamesMatchArchetypePaths()
        {
            var tree = Core.Blessings.Artemis.ArtemisPassiveTree.Create();

            var branchNames = tree.Branches.Select(b => b.Name).ToList();

            Assert.That(branchNames, Does.Contain("Barrage"));
            Assert.That(branchNames, Does.Contain("Full Draw"));
            Assert.That(branchNames, Does.Contain("Huntress"));
        }

        [Test]
        public void Create_EachBranchHasExactlyOneChoicePairNode()
        {
            var tree = Core.Blessings.Artemis.ArtemisPassiveTree.Create();

            foreach (var branch in tree.Branches)
            {
                Assert.That(branch.Nodes.Count(n => n.IsChoicePair), Is.EqualTo(1));
            }
        }

        [Test]
        public void Create_EachBranchsMythicCapstoneIsTheLastNode()
        {
            var tree = Core.Blessings.Artemis.ArtemisPassiveTree.Create();
            var expectedCapstones = new[] { "Hail of a Thousand Arrows", "The Unbroken Nock", "One Perfect Shot" };

            var actualCapstones = tree.Branches.Select(b => b.Nodes[b.Nodes.Count - 1].Name).ToList();

            Assert.That(actualCapstones, Is.EquivalentTo(expectedCapstones));
        }

        [Test]
        public void Create_TrunkNodesAreNotChoicePairs()
        {
            var tree = Core.Blessings.Artemis.ArtemisPassiveTree.Create();

            Assert.That(tree.Trunk.Nodes.Any(n => n.IsChoicePair), Is.False);
        }
    }
}
