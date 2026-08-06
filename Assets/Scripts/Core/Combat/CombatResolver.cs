using System.Linq;

namespace Pantheon.Core.Combat
{
    public static class CombatResolver
    {
        public static void PlayCard(Player player, Card card, Enemy target)
        {
            PlayCard(player, card, target, new[] { target });
        }

        public static void PlayCard(Player player, Card card, Enemy target, System.Collections.Generic.IReadOnlyList<Enemy> allEnemies)
        {
            if (!player.Hand.Contains(card))
            {
                throw new System.InvalidOperationException($"Card '{card.Name}' is not in hand.");
            }

            var isShot = card.Tags.Contains(CardTag.Shot);
            var effectiveCost = isShot ? System.Math.Max(0, card.EnergyCost - player.ShotCostReductionThisTurn) : card.EnergyCost;

            player.SpendEnergy(effectiveCost);
            player.DiscardFromHand(card);

            var context = new CardEffectContext(player, target, allEnemies);
            foreach (var effect in card.Effects)
            {
                effect.Apply(context);
            }

            if (isShot)
            {
                player.RecordShotPlayed();
            }
        }

        public static void EnemyAttack(Enemy enemy, Player target)
        {
            target.TakeDamage(CombatMath.ApplyDamageModifiers(enemy.AttackDamage, enemy.Strength, enemy.Drained, target.Exposed));
        }

        public static void ExecuteEnemyIntent(Enemy enemy, Player target)
        {
            var move = enemy.CurrentIntent;

            if (move == null)
            {
                return;
            }

            switch (move.Intent)
            {
                case IntentType.Attack:
                    target.TakeDamage(CombatMath.ApplyDamageModifiers(move.Value, enemy.Strength, enemy.Drained, target.Exposed));
                    break;
                case IntentType.Block:
                    enemy.GainBlock(move.Value);
                    break;
                case IntentType.Debuff:
                    ApplyStatus(target, move.Status, move.Value);
                    break;
                case IntentType.Buff:
                    ApplyStatus(enemy, move.Status, move.Value);
                    break;
            }

            enemy.ChooseNextIntent();
        }

        private static void ApplyStatus(ICombatant combatant, StatusType status, int amount)
        {
            switch (status)
            {
                case StatusType.Strength:
                    combatant.GainStrength(amount);
                    break;
                case StatusType.Exposed:
                    combatant.ApplyExposed(amount);
                    break;
                case StatusType.Drained:
                    combatant.ApplyDrained(amount);
                    break;
            }
        }
    }
}
