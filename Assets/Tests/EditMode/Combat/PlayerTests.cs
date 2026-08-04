using System;
using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat
{
    public class PlayerTests
    {
        [Test]
        public void SpendEnergy_SufficientEnergy_ReducesCurrentEnergy()
        {
            var player = new Player(startingEnergy: 3);

            player.SpendEnergy(1);

            Assert.That(player.CurrentEnergy, Is.EqualTo(2));
        }

        [Test]
        public void SpendEnergy_InsufficientEnergy_ThrowsAndEnergyUnchanged()
        {
            var player = new Player(startingEnergy: 1);

            Assert.Throws<InvalidOperationException>(() => player.SpendEnergy(2));
            Assert.That(player.CurrentEnergy, Is.EqualTo(1));
        }
    }
}
