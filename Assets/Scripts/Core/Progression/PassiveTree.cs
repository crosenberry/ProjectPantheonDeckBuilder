using System.Collections.Generic;

namespace Pantheon.Core.Progression
{
    public class PassiveTree
    {
        public int Mythos { get; private set; }
        public PassiveTreeBranch Trunk { get; }
        public IReadOnlyList<PassiveTreeBranch> Branches { get; }

        public PassiveTree(PassiveTreeBranch trunk, IReadOnlyList<PassiveTreeBranch> branches)
        {
            Trunk = trunk;
            Branches = branches;
        }

        public void GainMythos(int amount)
        {
            Mythos += amount;
        }

        public bool CanPurchaseNext(PassiveTreeBranch branch)
        {
            return branch.NextNode != null && Mythos >= branch.NextNode.Cost;
        }

        public void PurchaseNext(PassiveTreeBranch branch)
        {
            if (!CanPurchaseNext(branch))
            {
                throw new System.InvalidOperationException($"Cannot purchase the next node in '{branch.Name}'.");
            }

            Mythos -= branch.NextNode.Cost;
            branch.MarkNextPurchased();
        }
    }
}
