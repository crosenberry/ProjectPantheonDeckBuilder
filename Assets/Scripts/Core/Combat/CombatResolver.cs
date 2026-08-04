namespace Pantheon.Core.Combat
{
    public static class CombatResolver
    {
        public static void PlayCard(Player player, Card card, Enemy target)
        {
            player.SpendEnergy(card.EnergyCost);
            target.TakeDamage(card.DamageAmount);
        }
    }
}
