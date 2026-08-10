using NUnit.Framework;
using Pantheon.Core.Meta;

namespace Pantheon.Core.Tests.Meta
{
    public class EssenceWalletTests
    {
        [Test]
        public void Gain_IncreasesBalance()
        {
            var wallet = new EssenceWallet();

            wallet.Gain(10);

            Assert.That(wallet.Balance, Is.EqualTo(10));
        }

        [Test]
        public void Spend_SufficientBalance_DecreasesBalance()
        {
            var wallet = new EssenceWallet();
            wallet.Gain(10);

            wallet.Spend(4);

            Assert.That(wallet.Balance, Is.EqualTo(6));
        }

        [Test]
        public void Spend_MoreThanBalance_Throws()
        {
            var wallet = new EssenceWallet();
            wallet.Gain(3);

            Assert.Throws<System.InvalidOperationException>(() => wallet.Spend(4));
        }
    }
}
