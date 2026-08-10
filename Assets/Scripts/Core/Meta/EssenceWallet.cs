namespace Pantheon.Core.Meta
{
    public class EssenceWallet
    {
        public int Balance { get; private set; }

        public void Gain(int amount)
        {
            Balance += amount;
        }

        public void Spend(int amount)
        {
            if (amount > Balance)
            {
                throw new System.InvalidOperationException($"Cannot spend {amount} Essence with only {Balance} available.");
            }

            Balance -= amount;
        }
    }
}
