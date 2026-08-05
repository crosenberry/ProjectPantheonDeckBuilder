using System.Collections.Generic;

namespace Pantheon.Core.Combat
{
    public class Card
    {
        public string Name { get; }
        public int EnergyCost { get; }
        public CardType Type { get; }
        public IReadOnlyList<CardEffect> Effects { get; }
        public IReadOnlyList<CardTag> Tags { get; }

        public Card(string name, int energyCost, CardType type, IReadOnlyList<CardEffect> effects, IReadOnlyList<CardTag> tags = null)
        {
            Name = name;
            EnergyCost = energyCost;
            Type = type;
            Effects = effects;
            Tags = tags ?? System.Array.Empty<CardTag>();
        }

        public static Card Attack(string name, int energyCost, int damage, IReadOnlyList<CardTag> tags = null)
        {
            return new Card(name, energyCost, CardType.Attack, new CardEffect[] { new DealDamageEffect(damage) }, tags);
        }

        public static Card Skill(string name, int energyCost, int block, IReadOnlyList<CardTag> tags = null)
        {
            return new Card(name, energyCost, CardType.Skill, new CardEffect[] { new GainBlockEffect(block) }, tags);
        }
    }
}
