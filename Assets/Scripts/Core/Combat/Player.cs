namespace Pantheon.Core.Combat
{
    public class Player
    {
        public int CurrentEnergy { get; private set; }

        public Player(int startingEnergy)
        {
            CurrentEnergy = startingEnergy;
        }

        public void SpendEnergy(int amount)
        {
            if (amount > CurrentEnergy)
            {
                throw new System.InvalidOperationException(
                    $"Cannot spend {amount} energy with only {CurrentEnergy} available.");
            }

            CurrentEnergy -= amount;
        }
    }
}
