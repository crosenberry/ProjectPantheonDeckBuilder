using System.Collections.Generic;

namespace Pantheon.Core.Progression
{
    public class PassiveTreeBranch
    {
        private readonly Dictionary<PassiveTreeNode, ChoiceOption> _activeChoices = new Dictionary<PassiveTreeNode, ChoiceOption>();

        public string Name { get; }
        public IReadOnlyList<PassiveTreeNode> Nodes { get; }
        public int PurchasedCount { get; private set; }
        public PassiveTreeNode NextNode => PurchasedCount < Nodes.Count ? Nodes[PurchasedCount] : null;

        public PassiveTreeBranch(string name, IReadOnlyList<PassiveTreeNode> nodes)
        {
            Name = name;
            Nodes = nodes;
        }

        public bool IsPurchased(PassiveTreeNode node)
        {
            for (var i = 0; i < Nodes.Count; i++)
            {
                if (Nodes[i] == node)
                {
                    return i < PurchasedCount;
                }
            }

            return false;
        }

        public void MarkNextPurchased()
        {
            PurchasedCount++;
        }

        public ChoiceOption GetActiveChoice(PassiveTreeNode node)
        {
            return _activeChoices.TryGetValue(node, out var choice) ? choice : ChoiceOption.A;
        }

        public void SetActiveChoice(PassiveTreeNode node, ChoiceOption choice)
        {
            if (!node.IsChoicePair)
            {
                throw new System.InvalidOperationException($"'{node.Name}' is not a choice-pair node.");
            }

            _activeChoices[node] = choice;
        }
    }
}
