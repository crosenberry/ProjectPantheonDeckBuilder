using System;
using System.Linq;
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

        [Test]
        public void StartTurn_RefillsEnergyToMax()
        {
            var player = new Player(startingEnergy: 3);
            player.SpendEnergy(3);

            player.StartTurn(cardsToDraw: 0);

            Assert.That(player.CurrentEnergy, Is.EqualTo(player.MaxEnergy));
        }

        [Test]
        public void StartTurn_DrawsRequestedNumberOfCardsFromDrawPile()
        {
            var player = new Player(startingEnergy: 3);
            var deck = Enumerable.Range(1, 5).Select(i => Card.Attack($"Card {i}", energyCost: 1, damage: 1));
            player.AddToDrawPile(deck);

            player.StartTurn(cardsToDraw: 5);

            Assert.That(player.Hand.Count, Is.EqualTo(5));
            Assert.That(player.DrawPile.Count, Is.EqualTo(0));
        }

        [Test]
        public void StartTurn_DrawPileHasFewerCardsThanRequested_DrawsOnlyWhatsAvailable()
        {
            var player = new Player(startingEnergy: 3);
            var deck = Enumerable.Range(1, 2).Select(i => Card.Attack($"Card {i}", energyCost: 1, damage: 1));
            player.AddToDrawPile(deck);

            player.StartTurn(cardsToDraw: 5);

            Assert.That(player.Hand.Count, Is.EqualTo(2));
            Assert.That(player.DrawPile.Count, Is.EqualTo(0));
        }

        [Test]
        public void EndTurn_MovesHandToDiscardPileAndClearsHand()
        {
            var player = new Player(startingEnergy: 3);
            var deck = Enumerable.Range(1, 3).Select(i => Card.Attack($"Card {i}", energyCost: 1, damage: 1));
            player.AddToDrawPile(deck);
            player.StartTurn(cardsToDraw: 3);

            player.EndTurn();

            Assert.That(player.Hand.Count, Is.EqualTo(0));
            Assert.That(player.DiscardPile.Count, Is.EqualTo(3));
        }

        [Test]
        public void StartTurn_DrawPileEmptyWithCardsInDiscard_ShufflesDiscardIntoDrawPileAndDraws()
        {
            var player = new Player(startingEnergy: 3, new FakeRandom());
            var deck = Enumerable.Range(1, 3).Select(i => Card.Attack($"Card {i}", energyCost: 1, damage: 1));
            player.AddToDrawPile(deck);
            player.StartTurn(cardsToDraw: 3);
            player.EndTurn();

            player.StartTurn(cardsToDraw: 3);

            Assert.That(player.Hand.Count, Is.EqualTo(3));
            Assert.That(player.DrawPile.Count, Is.EqualTo(0));
            Assert.That(player.DiscardPile.Count, Is.EqualTo(0));
        }

        [Test]
        public void StartTurn_DrawPileRunsOutMidDraw_ShufflesDiscardAndDrawsRemainder()
        {
            var player = new Player(startingEnergy: 3, new FakeRandom());
            var deck = Enumerable.Range(1, 5).Select(i => Card.Attack($"Card {i}", energyCost: 1, damage: 1)).ToList();
            player.AddToDrawPile(deck);
            player.StartTurn(cardsToDraw: 3);
            player.EndTurn();

            player.StartTurn(cardsToDraw: 5);

            Assert.That(player.Hand.Count, Is.EqualTo(5));
            Assert.That(player.DrawPile.Count, Is.EqualTo(0));
            Assert.That(player.DiscardPile.Count, Is.EqualTo(0));
        }

        [Test]
        public void StartTurn_TotalCardsAcrossBothPilesLessThanRequested_DrawsAllAvailableCards()
        {
            var player = new Player(startingEnergy: 3, new FakeRandom());
            var deck = Enumerable.Range(1, 3).Select(i => Card.Attack($"Card {i}", energyCost: 1, damage: 1));
            player.AddToDrawPile(deck);
            player.StartTurn(cardsToDraw: 1);
            player.EndTurn();

            player.StartTurn(cardsToDraw: 5);

            Assert.That(player.Hand.Count, Is.EqualTo(3));
            Assert.That(player.DrawPile.Count, Is.EqualTo(0));
            Assert.That(player.DiscardPile.Count, Is.EqualTo(0));
        }

        [Test]
        public void GainBlock_IncreasesCurrentBlock()
        {
            var player = new Player(startingEnergy: 3);

            player.GainBlock(5);

            Assert.That(player.CurrentBlock, Is.EqualTo(5));
        }

        [Test]
        public void TakeDamage_DamageLessThanBlock_ReducesBlockOnlyNoHPLoss()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            player.GainBlock(10);

            player.TakeDamage(6);

            Assert.That(player.CurrentBlock, Is.EqualTo(4));
            Assert.That(player.CurrentHP, Is.EqualTo(70));
        }

        [Test]
        public void TakeDamage_DamageExceedsBlock_ExcessReducesHP()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            player.GainBlock(5);

            player.TakeDamage(8);

            Assert.That(player.CurrentBlock, Is.EqualTo(0));
            Assert.That(player.CurrentHP, Is.EqualTo(67));
        }

        [Test]
        public void TakeDamage_NoBlock_ReducesHPDirectly()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());

            player.TakeDamage(8);

            Assert.That(player.CurrentHP, Is.EqualTo(62));
        }

        [Test]
        public void TakeDamage_AmountExceedsCurrentHP_ClampsAtZero()
        {
            var player = new Player(startingEnergy: 3, startingHP: 10, new SystemRandom());

            player.TakeDamage(999);

            Assert.That(player.CurrentHP, Is.EqualTo(0));
        }

        [Test]
        public void StartTurn_ResetsBlockToZero()
        {
            var player = new Player(startingEnergy: 3);
            player.GainBlock(8);

            player.StartTurn(cardsToDraw: 0);

            Assert.That(player.CurrentBlock, Is.EqualTo(0));
        }

        [Test]
        public void DrawCards_AddsRequestedCardsToHandFromDrawPile()
        {
            var player = new Player(startingEnergy: 3);
            var deck = Enumerable.Range(1, 3).Select(i => Card.Attack($"Card {i}", energyCost: 1, damage: 1));
            player.AddToDrawPile(deck);

            player.DrawCards(2);

            Assert.That(player.Hand.Count, Is.EqualTo(2));
            Assert.That(player.DrawPile.Count, Is.EqualTo(1));
        }

        [Test]
        public void RecordShotPlayed_IncreasesShotsPlayedThisTurn()
        {
            var player = new Player(startingEnergy: 3);

            player.RecordShotPlayed();

            Assert.That(player.ShotsPlayedThisTurn, Is.EqualTo(1));
        }

        [Test]
        public void StartTurn_ResetsShotsPlayedThisTurnToZero()
        {
            var player = new Player(startingEnergy: 3);
            player.RecordShotPlayed();

            player.StartTurn(cardsToDraw: 0);

            Assert.That(player.ShotsPlayedThisTurn, Is.EqualTo(0));
        }

        [Test]
        public void ReduceShotCostThisTurn_IncreasesShotCostReductionThisTurn()
        {
            var player = new Player(startingEnergy: 3);

            player.ReduceShotCostThisTurn(1);

            Assert.That(player.ShotCostReductionThisTurn, Is.EqualTo(1));
        }

        [Test]
        public void StartTurn_ResetsShotCostReductionThisTurnToZero()
        {
            var player = new Player(startingEnergy: 3);
            player.ReduceShotCostThisTurn(1);

            player.StartTurn(cardsToDraw: 0);

            Assert.That(player.ShotCostReductionThisTurn, Is.EqualTo(0));
        }

        [Test]
        public void ExhaustFromHand_RemovesFromHandAddsToExhaustPile()
        {
            var player = new Player(startingEnergy: 3);
            var card = Card.Attack("Practiced Hand", energyCost: 1, damage: 0);
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);

            player.ExhaustFromHand(card);

            Assert.That(player.Hand.Contains(card), Is.False);
            Assert.That(player.ExhaustPile.Contains(card), Is.True);
        }

        [Test]
        public void ExhaustFromHand_DoesNotAddToDiscardPile()
        {
            var player = new Player(startingEnergy: 3);
            var card = Card.Attack("Practiced Hand", energyCost: 1, damage: 0);
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);

            player.ExhaustFromHand(card);

            Assert.That(player.DiscardPile.Contains(card), Is.False);
        }

        [Test]
        public void AddTrigger_AddsToTriggersList()
        {
            var player = new Player(startingEnergy: 3);
            var trigger = new TriggeredEffect(TriggerEvent.TurnStarted, new GainVolleyEffect(1));

            player.AddTrigger(trigger);

            Assert.That(player.Triggers.Contains(trigger), Is.True);
        }

        [Test]
        public void GainStrength_IncreasesStrength()
        {
            var player = new Player(startingEnergy: 3);

            player.GainStrength(2);

            Assert.That(player.Strength, Is.EqualTo(2));
        }

        [Test]
        public void ApplyExposed_IncreasesExposed()
        {
            var player = new Player(startingEnergy: 3);

            player.ApplyExposed(2);

            Assert.That(player.Exposed, Is.EqualTo(2));
        }

        [Test]
        public void ApplyDrained_IncreasesDrained()
        {
            var player = new Player(startingEnergy: 3);

            player.ApplyDrained(2);

            Assert.That(player.Drained, Is.EqualTo(2));
        }

        [Test]
        public void ApplySundered_IncreasesSundered()
        {
            var player = new Player(startingEnergy: 3);

            player.ApplySundered(2);

            Assert.That(player.Sundered, Is.EqualTo(2));
        }

        [Test]
        public void StartTurn_ExposedAboveZero_DecrementsByOne()
        {
            var player = new Player(startingEnergy: 3);
            player.ApplyExposed(2);

            player.StartTurn(cardsToDraw: 0);

            Assert.That(player.Exposed, Is.EqualTo(1));
        }

        [Test]
        public void StartTurn_ExposedAtZero_StaysAtZero()
        {
            var player = new Player(startingEnergy: 3);

            player.StartTurn(cardsToDraw: 0);

            Assert.That(player.Exposed, Is.EqualTo(0));
        }

        [Test]
        public void StartTurn_DrainedAboveZero_DecrementsByOne()
        {
            var player = new Player(startingEnergy: 3);
            player.ApplyDrained(2);

            player.StartTurn(cardsToDraw: 0);

            Assert.That(player.Drained, Is.EqualTo(1));
        }

        [Test]
        public void StartTurn_SunderedAboveZero_DecrementsByOne()
        {
            var player = new Player(startingEnergy: 3);
            player.ApplySundered(2);

            player.StartTurn(cardsToDraw: 0);

            Assert.That(player.Sundered, Is.EqualTo(1));
        }

        [Test]
        public void GainVolley_IncreasesVolley()
        {
            var player = new Player(startingEnergy: 3);

            player.GainVolley(2);

            Assert.That(player.Volley, Is.EqualTo(2));
        }

        [Test]
        public void StartTurn_ResetsVolleyToZero()
        {
            var player = new Player(startingEnergy: 3);
            player.GainVolley(2);

            player.StartTurn(cardsToDraw: 0);

            Assert.That(player.Volley, Is.EqualTo(0));
        }

        [Test]
        public void ConsumeVolley_ResetsVolleyToZero()
        {
            var player = new Player(startingEnergy: 3);
            player.GainVolley(3);

            player.ConsumeVolley();

            Assert.That(player.Volley, Is.EqualTo(0));
        }

        [Test]
        public void ConsumeVolley_ReturnsAmountConsumed()
        {
            var player = new Player(startingEnergy: 3);
            player.GainVolley(3);

            var consumed = player.ConsumeVolley();

            Assert.That(consumed, Is.EqualTo(3));
        }

        [Test]
        public void Heal_BelowMaxHP_IncreasesCurrentHP()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            player.TakeDamage(20);

            player.Heal(10);

            Assert.That(player.CurrentHP, Is.EqualTo(60));
        }

        [Test]
        public void Heal_AmountWouldExceedMaxHP_ClampsAtMaxHP()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            player.TakeDamage(5);

            player.Heal(999);

            Assert.That(player.CurrentHP, Is.EqualTo(70));
        }

        [Test]
        public void StartTurn_StrengthDoesNotDecay()
        {
            var player = new Player(startingEnergy: 3);
            player.GainStrength(3);

            player.StartTurn(cardsToDraw: 0);

            Assert.That(player.Strength, Is.EqualTo(3));
        }
    }
}
