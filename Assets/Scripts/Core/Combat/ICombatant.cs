namespace Pantheon.Core.Combat
{
    public interface ICombatant
    {
        int Strength { get; }
        int Exposed { get; }
        int Drained { get; }

        void TakeDamage(int amount);
        void GainStrength(int amount);
        void ApplyExposed(int amount);
        void ApplyDrained(int amount);
    }
}
