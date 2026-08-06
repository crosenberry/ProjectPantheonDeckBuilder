using System.Linq;

namespace Pantheon.Core.Combat
{
    public static class CombatResolver
    {
        public static void PlayCard(Player player, Card card, Enemy target)
        {
            if (!player.Hand.Contains(card))
            {
                throw new System.InvalidOperationException($"Card '{card.Name}' is not in hand.");
            }

            player.SpendEnergy(card.EnergyCost);
            player.DiscardFromHand(card);

            var context = new CardEffectContext(player, target);
            foreach (var effect in card.Effects)
            {
                effect.Apply(context);
            }

            if (card.Tags.Contains(CardTag.Shot))
            {
                player.RecordShotPlayed();
            }
        }

        public static void EnemyAttack(Enemy enemy, Player target)
        {
            target.TakeDamage(CombatMath.ApplyDamageModifiers(enemy.AttackDamage, enemy.Strength, enemy.Drained, target.Exposed));
        }
    }
}
