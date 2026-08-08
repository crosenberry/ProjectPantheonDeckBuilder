using NUnit.Framework;
using Pantheon.Core.Progression;

namespace Pantheon.Core.Tests.Progression
{
    public class PassiveTreeBranchTests
    {
        private static PassiveTreeBranch CreateBranch()
        {
            return new PassiveTreeBranch("Test Branch", new[]
            {
                new PassiveTreeNode("First", 60),
                new PassiveTreeNode("Second", 90, isChoicePair: true),
                new PassiveTreeNode("Third", 110)
            });
        }

        [Test]
        public void NextNode_NothingPurchasedYet_ReturnsFirstNode()
        {
            var branch = CreateBranch();

            Assert.That(branch.NextNode, Is.EqualTo(branch.Nodes[0]));
        }

        [Test]
        public void NextNode_AllNodesPurchased_ReturnsNull()
        {
            var branch = CreateBranch();
            branch.MarkNextPurchased();
            branch.MarkNextPurchased();
            branch.MarkNextPurchased();

            Assert.That(branch.NextNode, Is.Null);
        }

        [Test]
        public void MarkNextPurchased_IncreasesPurchasedCount()
        {
            var branch = CreateBranch();

            branch.MarkNextPurchased();

            Assert.That(branch.PurchasedCount, Is.EqualTo(1));
        }

        [Test]
        public void MarkNextPurchased_AdvancesNextNode()
        {
            var branch = CreateBranch();

            branch.MarkNextPurchased();

            Assert.That(branch.NextNode, Is.EqualTo(branch.Nodes[1]));
        }

        [Test]
        public void IsPurchased_NodeWithinPurchasedCount_ReturnsTrue()
        {
            var branch = CreateBranch();
            branch.MarkNextPurchased();

            Assert.That(branch.IsPurchased(branch.Nodes[0]), Is.True);
        }

        [Test]
        public void IsPurchased_NodeBeyondPurchasedCount_ReturnsFalse()
        {
            var branch = CreateBranch();
            branch.MarkNextPurchased();

            Assert.That(branch.IsPurchased(branch.Nodes[1]), Is.False);
        }

        [Test]
        public void GetActiveChoice_NoChoiceSetYet_DefaultsToOptionA()
        {
            var branch = CreateBranch();

            Assert.That(branch.GetActiveChoice(branch.Nodes[1]), Is.EqualTo(ChoiceOption.A));
        }

        [Test]
        public void SetActiveChoice_ChoicePairNode_UpdatesActiveChoice()
        {
            var branch = CreateBranch();

            branch.SetActiveChoice(branch.Nodes[1], ChoiceOption.B);

            Assert.That(branch.GetActiveChoice(branch.Nodes[1]), Is.EqualTo(ChoiceOption.B));
        }

        [Test]
        public void SetActiveChoice_NonChoicePairNode_Throws()
        {
            var branch = CreateBranch();

            Assert.Throws<System.InvalidOperationException>(() => branch.SetActiveChoice(branch.Nodes[0], ChoiceOption.B));
        }
    }
}
