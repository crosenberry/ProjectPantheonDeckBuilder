namespace Pantheon.Core.Combat
{
    public static class CombatMath
    {
        public static int ApplyDamageModifiers(int baseAmount, int attackerStrength, int attackerDrained, int targetExposed)
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
