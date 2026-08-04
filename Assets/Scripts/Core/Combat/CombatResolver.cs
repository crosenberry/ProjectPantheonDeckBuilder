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
            player.GainBlock(card.BlockAmount);
            target.TakeDamage(card.DamageAmount);
        }

        public static void EnemyAttack(Enemy enemy, Player target)
        {
            target.TakeDamage(enemy.AttackDamage);
        }
    }
}
