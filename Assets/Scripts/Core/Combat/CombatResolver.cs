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

            var block = card.BlockAmount;
            if (block > 0 && player.Sundered > 0)
            {
                block = (int)(block * 0.75);
            }

            player.GainBlock(block);
            target.TakeDamage(ApplyDamageModifiers(card.DamageAmount, player.Strength, player.Drained, target.Exposed));
        }

        public static void EnemyAttack(Enemy enemy, Player target)
        {
            target.TakeDamage(ApplyDamageModifiers(enemy.AttackDamage, enemy.Strength, enemy.Drained, target.Exposed));
        }

        private static int ApplyDamageModifiers(int baseAmount, int attackerStrength, int attackerDrained, int targetExposed)
        {
            if (baseAmount <= 0)
            {
                return baseAmount;
            }

            var damage = baseAmount + attackerStrength;

            if (attackerDrained > 0)
            {
                damage = (int)(damage * 0.75);
            }

            if (targetExposed > 0)
            {
                damage = (int)(damage * 1.5);
            }

            return damage;
        }
    }
}
