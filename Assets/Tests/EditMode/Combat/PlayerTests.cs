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
        public void GainStorm_IncreasesStorm()
        {
            var player = new Player(startingEnergy: 3);

            player.GainStorm(2);

            Assert.That(player.Storm, Is.EqualTo(2));
        }

        [Test]
        public void StartTurn_DoesNotResetStorm()
        {
            var player = new Player(startingEnergy: 3);
            player.GainStorm(2);

            player.StartTurn(cardsToDraw: 0);

            Assert.That(player.Storm, Is.EqualTo(2));
        }

        [Test]
        public void ConsumeStorm_ResetsStormToZero()
        {
            var player = new Player(startingEnergy: 3);
            player.GainStorm(3);

            player.ConsumeStorm();

            Assert.That(player.Storm, Is.EqualTo(0));
        }

        [Test]
        public void ConsumeStorm_ReturnsAmountConsumed()
        {
            var player = new Player(startingEnergy: 3);
            player.GainStorm(3);

            var consumed = player.ConsumeStorm();

            Assert.That(consumed, Is.EqualTo(3));
        }

        [Test]
        public void ConsumeStormWithMaxAmount_AvailableExceedsMax_ConsumesOnlyMax()
        {
            var player = new Player(startingEnergy: 3);
            player.GainStorm(5);

            var consumed = player.ConsumeStorm(2);

            Assert.That(consumed, Is.EqualTo(2));
            Assert.That(player.Storm, Is.EqualTo(3));
        }

        [Test]
        public void ConsumeStormWithMaxAmount_AvailableBelowMax_ConsumesAllAvailable()
        {
            var player = new Player(startingEnergy: 3);
            player.GainStorm(1);

            var consumed = player.ConsumeStorm(2);

            Assert.That(consumed, Is.EqualTo(1));
            Assert.That(player.Storm, Is.EqualTo(0));
        }

        [Test]
        public void ConsumeStormWithMaxAmount_NoStorm_ReturnsZero()
        {
            var player = new Player(startingEnergy: 3);

            var consumed = player.ConsumeStorm(2);

            Assert.That(consumed, Is.EqualTo(0));
        }

        [Test]
        public void AdjustScale_PositiveAmount_IncreasesScale()
        {
            var player = new Player(startingEnergy: 3);

            player.AdjustScale(2);

            Assert.That(player.Scale, Is.EqualTo(2));
        }

        [Test]
        public void AdjustScale_NegativeAmount_DecreasesScale()
        {
            var player = new Player(startingEnergy: 3);

            player.AdjustScale(-2);

            Assert.That(player.Scale, Is.EqualTo(-2));
        }

        [Test]
        public void AdjustScale_ExceedsUpperBound_ClampsAtFive()
        {
            var player = new Player(startingEnergy: 3);
            player.AdjustScale(4);

            player.AdjustScale(4);

            Assert.That(player.Scale, Is.EqualTo(5));
        }

        [Test]
        public void AdjustScale_ExceedsLowerBound_ClampsAtNegativeFive()
        {
            var player = new Player(startingEnergy: 3);
            player.AdjustScale(-4);

            player.AdjustScale(-4);

            Assert.That(player.Scale, Is.EqualTo(-5));
        }

        [Test]
        public void SetScale_SetsScaleToGivenValue()
        {
            var player = new Player(startingEnergy: 3);
            player.AdjustScale(3);

            player.SetScale(0);

            Assert.That(player.Scale, Is.EqualTo(0));
        }

        [Test]
        public void SetScale_ExceedsUpperBound_ClampsAtFive()
        {
            var player = new Player(startingEnergy: 3);

            player.SetScale(9);

            Assert.That(player.Scale, Is.EqualTo(5));
        }

        [Test]
        public void StartTurn_DoesNotResetScale()
        {
            var player = new Player(startingEnergy: 3);
            player.AdjustScale(3);

            player.StartTurn(cardsToDraw: 0);

            Assert.That(player.Scale, Is.EqualTo(3));
        }

        [Test]
        public void LoseHP_ReducesCurrentHPDirectly()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());

            player.LoseHP(5);

            Assert.That(player.CurrentHP, Is.EqualTo(65));
        }

        [Test]
        public void LoseHP_IgnoresBlock()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            player.GainBlock(10);

            player.LoseHP(5);

            Assert.That(player.CurrentHP, Is.EqualTo(65));
            Assert.That(player.CurrentBlock, Is.EqualTo(10));
        }

        [Test]
        public void LoseHP_AmountExceedsCurrentHP_ClampsAtZero()
        {
            var player = new Player(startingEnergy: 3, startingHP: 10, new SystemRandom());

            player.LoseHP(999);

            Assert.That(player.CurrentHP, Is.EqualTo(0));
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
        public void PrepareForCombat_ClearsAllPilesAndFillsDrawPile()
        {
            var player = new Player(startingEnergy: 3);
            var oldCards = Enumerable.Range(1, 4).Select(i => Card.Attack($"Old {i}", energyCost: 1, damage: 1)).ToList();
            player.AddToDrawPile(oldCards);
            player.StartTurn(cardsToDraw: 2);
            player.ExhaustFromHand(player.Hand[0]);
            player.EndTurn();
            var newDeck = Enumerable.Range(1, 3).Select(i => Card.Attack($"New {i}", energyCost: 1, damage: 1)).ToList();

            player.PrepareForCombat(newDeck);

            Assert.That(player.Hand, Is.Empty);
            Assert.That(player.DiscardPile, Is.Empty);
            Assert.That(player.ExhaustPile, Is.Empty);
            Assert.That(player.DrawPile, Is.EquivalentTo(newDeck));
        }

        [Test]
        public void PrepareForCombat_ClearsTriggers()
        {
            var player = new Player(startingEnergy: 3);
            player.AddTrigger(new TriggeredEffect(TriggerEvent.TurnStarted, new GainVolleyEffect(1)));

            player.PrepareForCombat(Enumerable.Empty<Card>());

            Assert.That(player.Triggers, Is.Empty);
        }

        [Test]
        public void PrepareForCombat_ResetsCombatScopedStats()
        {
            var player = new Player(startingEnergy: 3);
            player.GainStrength(2);
            player.ApplyExposed(2);
            player.ApplyDrained(2);
            player.ApplySundered(2);
            player.GainBlock(5);
            player.GainVolley(3);
            player.GainStorm(3);
            player.AdjustScale(3);

            player.PrepareForCombat(Enumerable.Empty<Card>());

            Assert.That(player.Strength, Is.EqualTo(0));
            Assert.That(player.Exposed, Is.EqualTo(0));
            Assert.That(player.Drained, Is.EqualTo(0));
            Assert.That(player.Sundered, Is.EqualTo(0));
            Assert.That(player.CurrentBlock, Is.EqualTo(0));
            Assert.That(player.Volley, Is.EqualTo(0));
            Assert.That(player.Storm, Is.EqualTo(0));
            Assert.That(player.Scale, Is.EqualTo(0));
        }

        [Test]
        public void PrepareForCombat_DoesNotChangeHP()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            player.TakeDamage(25);

            player.PrepareForCombat(Enumerable.Empty<Card>());

            Assert.That(player.CurrentHP, Is.EqualTo(45));
        }

        [Test]
        public void StartTurn_StrengthDoesNotDecay()
        {
            var player = new Player(startingEnergy: 3);
            player.GainStrength(3);

            player.StartTurn(cardsToDraw: 0);

            Assert.That(player.Strength, Is.EqualTo(3));
        }

        [Test]
        public void Form_DefaultsToMortal()
        {
            var player = new Player(startingEnergy: 3);

            Assert.That(player.Form, Is.EqualTo(Form.Mortal));
        }

        [Test]
        public void ChangeForm_CyclesMortalToBeastToImmortalToMortal()
        {
            var player = new Player(startingEnergy: 3);

            player.ChangeForm();
            Assert.That(player.Form, Is.EqualTo(Form.Beast));

            player.ChangeForm();
            Assert.That(player.Form, Is.EqualTo(Form.Immortal));

            player.ChangeForm();
            Assert.That(player.Form, Is.EqualTo(Form.Mortal));
        }

        [Test]
        public void ChangeForm_Cycle_SetsChangedFormThisTurn()
        {
            var player = new Player(startingEnergy: 3);

            player.ChangeForm();

            Assert.That(player.ChangedFormThisTurn, Is.True);
        }

        [Test]
        public void ChangeFormToSpecificForm_SetsFormDirectly()
        {
            var player = new Player(startingEnergy: 3);

            player.ChangeForm(Form.Immortal);

            Assert.That(player.Form, Is.EqualTo(Form.Immortal));
        }

        [Test]
        public void ChangeFormToSpecificForm_DifferentFromCurrent_SetsChangedFormThisTurn()
        {
            var player = new Player(startingEnergy: 3);

            player.ChangeForm(Form.Beast);

            Assert.That(player.ChangedFormThisTurn, Is.True);
        }

        [Test]
        public void ChangeFormToSpecificForm_SameAsCurrent_DoesNotSetChangedFormThisTurn()
        {
            var player = new Player(startingEnergy: 3);

            player.ChangeForm(Form.Mortal);

            Assert.That(player.ChangedFormThisTurn, Is.False);
        }

        [Test]
        public void ChangedFormThisTurn_DefaultsToFalse()
        {
            var player = new Player(startingEnergy: 3);

            Assert.That(player.ChangedFormThisTurn, Is.False);
        }

        [Test]
        public void StartTurn_ResetsChangedFormThisTurnToFalse()
        {
            var player = new Player(startingEnergy: 3);
            player.ChangeForm();

            player.StartTurn(cardsToDraw: 0);

            Assert.That(player.ChangedFormThisTurn, Is.False);
        }

        [Test]
        public void StartTurn_DoesNotResetForm()
        {
            var player = new Player(startingEnergy: 3);
            player.ChangeForm();

            player.StartTurn(cardsToDraw: 0);

            Assert.That(player.Form, Is.EqualTo(Form.Beast));
        }

        [Test]
        public void PrepareForCombat_ResetsFormToMortal()
        {
            var player = new Player(startingEnergy: 3);
            player.ChangeForm(Form.Immortal);

            player.PrepareForCombat(Enumerable.Empty<Card>());

            Assert.That(player.Form, Is.EqualTo(Form.Mortal));
        }

        [Test]
        public void PrepareForCombat_ResetsChangedFormThisTurnToFalse()
        {
            var player = new Player(startingEnergy: 3);
            player.ChangeForm();

            player.PrepareForCombat(Enumerable.Empty<Card>());

            Assert.That(player.ChangedFormThisTurn, Is.False);
        }
    }
}
