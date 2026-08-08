namespace Pantheon.Core.Progression
{
    public class PassiveTreeNode
    {
        public string Name { get; }
        public int Cost { get; }
        public bool IsChoicePair { get; }

        public PassiveTreeNode(string name, int cost, bool isChoicePair = false)
        {
            Name = name;
            Cost = cost;
            IsChoicePair = isChoicePair;
        }
    }
}
