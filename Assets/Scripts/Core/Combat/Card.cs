using System.Collections.Generic;

namespace Pantheon.Core.Combat
{
    public class Card
    {
        public string Name { get; }
        public int EnergyCost { get; }
        public CardType Type { get; }
        public IReadOnlyList<CardEffect> Effects { get; }

        public Card(string name, int energyCost, CardType type, IReadOnlyList<CardEffect> effects)
        {
            Name = name;
            EnergyCost = energyCost;
            Type = type;
            Effects = effects;
        }

        public static Card Attack(string name, int energyCost, int damage)
        {
            return new Card(name, energyCost, CardType.Attack, new CardEffect[] { new DealDamageEffect(damage) });
        }

        public static Card Skill(string name, int energyCost, int block)
        {
            return new Card(name, energyCost, CardType.Skill, new CardEffect[] { new GainBlockEffect(block) });
        }
    }
}
