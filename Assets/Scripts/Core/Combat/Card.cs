namespace Pantheon.Core.Combat
{
    public class Card
    {
        public string Name { get; }
        public int EnergyCost { get; }
        public int DamageAmount { get; }

        public Card(string name, int energyCost, int damageAmount)
        {
            Name = name;
            EnergyCost = energyCost;
            DamageAmount = damageAmount;
        }
    }
}
