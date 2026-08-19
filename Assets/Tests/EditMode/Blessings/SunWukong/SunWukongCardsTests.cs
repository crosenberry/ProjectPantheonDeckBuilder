using System.Linq;
using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Blessings.SunWukong
{
    public class SunWukongCardsTests
    {
        private static void Play(Player player, Card card, Enemy enemy)
        {
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);
            CombatResolver.PlayCard(player, card, enemy);
        }

        [Test]
        public void ApeFist_DealsFiveDamage()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.SunWukong.SunWukongCards.ApeFist();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(37));
        }

        [Test]
        public void CloudStep_GrantsFiveBlock()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.SunWukong.SunWukongCards.CloudStep();

            Play(player, card, enemy);

            Assert.That(player.CurrentBlock, Is.EqualTo(5));
        }

        [Test]
        public void RuyiStrike_DealsDamageAndChangesForm()
        {
            var player = new Player(startingEnergy: 2);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.SunWukong.SunWukongCards.RuyiStrike();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(33));
            Assert.That(player.Form, Is.EqualTo(Form.Beast));
        }

        [Test]
        public void BeastAwakening_ChangesToBeastFormAndGrantsBlock()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.SunWukong.SunWukongCards.BeastAwakening();

            Play(player, card, enemy);

            Assert.That(player.Form, Is.EqualTo(Form.Beast));
            Assert.That(player.CurrentBlock, Is.EqualTo(3));
        }

        [Test]
        public void PrimalRoar_GrantsOneStrength()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.SunWukong.SunWukongCards.PrimalRoar();

            Play(player, card, enemy);

            Assert.That(player.Strength, Is.EqualTo(1));
        }

        [Test]
        public void RecklessApe_DealsDamageAndCostsPlayerHP()
        {
            var player = new Player(startingEnergy: 1, startingHP: 70, new SystemRandom());
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.SunWukong.SunWukongCards.RecklessApe();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(37));
            Assert.That(player.CurrentHP, Is.EqualTo(69));
        }

        [Test]
        public void Rampage_DealsEightDamage()
        {
            var player = new Player(startingEnergy: 2);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.SunWukong.SunWukongCards.Rampage();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(34));
        }

        [Test]
        public void HavocInHeaven_DealsDamageThreeTimes()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.SunWukong.SunWukongCards.HavocInHeaven();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(18));
        }

        [Test]
        public void HavocInHeaven_PlayerInBeastForm_StaysAtThreeHits()
        {
            var player = new Player(startingEnergy: 3);
            player.ChangeForm(Form.Beast);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.SunWukong.SunWukongCards.HavocInHeaven();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(18));
        }

        [Test]
        public void ImmortalAscension_ChangesToImmortalFormAndGrantsStrength()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.SunWukong.SunWukongCards.ImmortalAscension();

            Play(player, card, enemy);

            // Card text is "Change to Immortal Form. Gain 2 Strength." in that
            // order, so the Strength gain lands after Immortal is already active
            // and picks up its own +1 bonus (2 + 1 = 3) - same effect-list-ordering
            // interaction as Syncretism's Twin Storms, just the reverse direction.
            Assert.That(player.Form, Is.EqualTo(Form.Immortal));
            Assert.That(player.Strength, Is.EqualTo(3));
        }

        [Test]
        public void SacredPeach_HealsFourHP()
        {
            var player = new Player(startingEnergy: 1, startingHP: 70, new SystemRandom());
            player.LoseHP(10);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.SunWukong.SunWukongCards.SacredPeach();

            Play(player, card, enemy);

            Assert.That(player.CurrentHP, Is.EqualTo(64));
        }

        [Test]
        public void CelestialWard_GrantsSixBlock()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.SunWukong.SunWukongCards.CelestialWard();

            Play(player, card, enemy);

            Assert.That(player.CurrentBlock, Is.EqualTo(6));
        }

        [Test]
        public void PeachOfLongevity_HealsAndGrantsStrength()
        {
            var player = new Player(startingEnergy: 2, startingHP: 70, new SystemRandom());
            player.LoseHP(20);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.SunWukong.SunWukongCards.PeachOfLongevity();

            Play(player, card, enemy);

            Assert.That(player.CurrentHP, Is.EqualTo(60));
            Assert.That(player.Strength, Is.EqualTo(1));
        }

        [Test]
        public void AscensionOfTheSage_IsTaggedExhaust()
        {
            var card = Core.Blessings.SunWukong.SunWukongCards.AscensionOfTheSage();

            Assert.That(card.Tags.Contains(CardTag.Exhaust), Is.True);
        }

        [Test]
        public void AscensionOfTheSage_ChangesToImmortalFormAndGrantsStrength()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.SunWukong.SunWukongCards.AscensionOfTheSage();

            Play(player, card, enemy);

            // Same Form-then-Strength ordering as Immortal Ascension: 5 + 1 = 6.
            Assert.That(player.Form, Is.EqualTo(Form.Immortal));
            Assert.That(player.Strength, Is.EqualTo(6));
        }

        [Test]
        public void ShiftingStance_ChangesFormToBeast()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.SunWukong.SunWukongCards.ShiftingStance();

            Play(player, card, enemy);

            Assert.That(player.Form, Is.EqualTo(Form.Beast));
        }

        [Test]
        public void FickleStrike_DealsDamageAndChangesForm()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.SunWukong.SunWukongCards.FickleStrike();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(37));
            Assert.That(player.Form, Is.EqualTo(Form.Beast));
        }

        [Test]
        public void AdaptiveGuard_ChangedFormThisTurn_GrantsBonusBlock()
        {
            var player = new Player(startingEnergy: 2);
            var enemy = new Enemy(maxHP: 42);
            var changeCard = Core.Blessings.SunWukong.SunWukongCards.ShiftingStance();
            var guardCard = Core.Blessings.SunWukong.SunWukongCards.AdaptiveGuard();
            player.AddToDrawPile(new[] { changeCard, guardCard });
            player.StartTurn(cardsToDraw: 2);
            CombatResolver.PlayCard(player, changeCard, enemy);

            CombatResolver.PlayCard(player, guardCard, enemy);

            Assert.That(player.CurrentBlock, Is.EqualTo(8));
        }

        [Test]
        public void AdaptiveGuard_DidNotChangeFormThisTurn_GrantsOnlyBaseBlock()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.SunWukong.SunWukongCards.AdaptiveGuard();

            Play(player, card, enemy);

            Assert.That(player.CurrentBlock, Is.EqualTo(5));
        }

        [Test]
        public void WhirlingTransformation_DealsDamageAndChangesForm()
        {
            var player = new Player(startingEnergy: 2);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.SunWukong.SunWukongCards.WhirlingTransformation();

            Play(player, card, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(32));
            Assert.That(player.Form, Is.EqualTo(Form.Beast));
        }

        [Test]
        public void SeventyTwoChanges_ChangesFormAndDrawsTwoCards()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = Core.Blessings.SunWukong.SunWukongCards.SeventyTwoChanges();
            var extraA = Core.Blessings.SunWukong.SunWukongCards.ApeFist();
            var extraB = Core.Blessings.SunWukong.SunWukongCards.CloudStep();
            player.AddToDrawPile(new[] { card, extraA, extraB });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, card, enemy);

            Assert.That(player.Form, Is.EqualTo(Form.Beast));
            Assert.That(player.Hand.Contains(extraA), Is.True);
            Assert.That(player.Hand.Contains(extraB), Is.True);
        }

        [Test]
        public void StarterDeck_ReturnsTenCards()
        {
            var deck = Core.Blessings.SunWukong.SunWukongCards.StarterDeck().ToList();

            Assert.That(deck.Count, Is.EqualTo(10));
        }

        [Test]
        public void StarterDeck_HasCorrectComposition()
        {
            var deck = Core.Blessings.SunWukong.SunWukongCards.StarterDeck().ToList();

            Assert.That(deck.Count(c => c.Name == "Ape Fist"), Is.EqualTo(5));
            Assert.That(deck.Count(c => c.Name == "Cloud Step"), Is.EqualTo(4));
            Assert.That(deck.Count(c => c.Name == "Ruyi Strike"), Is.EqualTo(1));
        }

        [Test]
        public void StarterDeck_ReturnsDistinctCardInstances()
        {
            var deck = Core.Blessings.SunWukong.SunWukongCards.StarterDeck().ToList();

            var distinctCount = deck.Distinct().Count();

            Assert.That(distinctCount, Is.EqualTo(10));
        }
    }
}
