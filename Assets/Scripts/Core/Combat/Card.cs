namespace Pantheon.Core.Combat
{
    public class Card
    {
        public string Name { get; }
        public int EnergyCost { get; }
        public int DamageAmount { get; }
        public int BlockAmount { get; }

        public Card(string name, int energyCost, int damageAmount, int blockAmount = 0)
        {
            Name = name;
            EnergyCost = energyCost;
            DamageAmount = damageAmount;
            BlockAmount = blockAmount;
        }
    }
}
