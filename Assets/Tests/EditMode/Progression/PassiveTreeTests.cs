using NUnit.Framework;
using Pantheon.Core.Progression;

namespace Pantheon.Core.Tests.Progression
{
    public class PassiveTreeTests
    {
        private static PassiveTree CreateTree()
        {
            var trunk = new PassiveTreeBranch("Trunk", new[] { new PassiveTreeNode("Trunk Node", 40) });
            var branch = new PassiveTreeBranch("Branch", new[] { new PassiveTreeNode("Branch Node", 60) });
            return new PassiveTree(trunk, new[] { branch });
        }

        [Test]
        public void GainMythos_IncreasesMythos()
        {
            var tree = CreateTree();

            tree.GainMythos(50);

            Assert.That(tree.Mythos, Is.EqualTo(50));
        }

        [Test]
        public void CanPurchaseNext_EnoughMythos_ReturnsTrue()
        {
            var tree = CreateTree();
            tree.GainMythos(40);

            Assert.That(tree.CanPurchaseNext(tree.Trunk), Is.True);
        }

        [Test]
        public void CanPurchaseNext_InsufficientMythos_ReturnsFalse()
        {
            var tree = CreateTree();
            tree.GainMythos(10);

            Assert.That(tree.CanPurchaseNext(tree.Trunk), Is.False);
        }

        [Test]
        public void PurchaseNext_SpendsMythos()
        {
            var tree = CreateTree();
            tree.GainMythos(40);

            tree.PurchaseNext(tree.Trunk);

            Assert.That(tree.Mythos, Is.EqualTo(0));
        }

        [Test]
        public void PurchaseNext_MarksNodePurchasedOnBranch()
        {
            var tree = CreateTree();
            tree.GainMythos(40);

            tree.PurchaseNext(tree.Trunk);

            Assert.That(tree.Trunk.PurchasedCount, Is.EqualTo(1));
        }

        [Test]
        public void PurchaseNext_InsufficientMythos_ThrowsAndDoesNotChangeState()
        {
            var tree = CreateTree();
            tree.GainMythos(10);

            Assert.Throws<System.InvalidOperationException>(() => tree.PurchaseNext(tree.Trunk));
            Assert.That(tree.Mythos, Is.EqualTo(10));
            Assert.That(tree.Trunk.PurchasedCount, Is.EqualTo(0));
        }
    }
}
