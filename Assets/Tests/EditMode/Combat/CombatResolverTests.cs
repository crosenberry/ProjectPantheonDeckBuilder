using System;
using System.Linq;
using NUnit.Framework;
using Pantheon.Core.Combat;

namespace Pantheon.Core.Tests.Combat
{
    public class CombatResolverTests
    {
        [Test]
        public void PlayCard_SufficientEnergy_DealsCardDamageToTarget()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var quickShot = Card.Attack("Quick Shot", energyCost: 1, damage: 6);
            player.AddToDrawPile(new[] { quickShot });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, quickShot, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(36));
        }

        [Test]
        public void PlayCard_SufficientEnergy_SpendsEnergyEqualToCardCost()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var quickShot = Card.Attack("Quick Shot", energyCost: 1, damage: 6);
            player.AddToDrawPile(new[] { quickShot });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, quickShot, enemy);

            Assert.That(player.CurrentEnergy, Is.EqualTo(2));
        }

        [Test]
        public void PlayCard_InsufficientEnergy_ThrowsAndTargetHPUnchanged()
        {
            var player = new Player(startingEnergy: 0);
            var enemy = new Enemy(maxHP: 42);
            var quickShot = Card.Attack("Quick Shot", energyCost: 1, damage: 6);
            player.AddToDrawPile(new[] { quickShot });
            player.StartTurn(cardsToDraw: 1);

            Assert.Throws<InvalidOperationException>(() => CombatResolver.PlayCard(player, quickShot, enemy));
            Assert.That(enemy.CurrentHP, Is.EqualTo(42));
        }

        [Test]
        public void PlayCard_CardInHand_RemovesCardFromHand()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var quickShot = Card.Attack("Quick Shot", energyCost: 1, damage: 6);
            player.AddToDrawPile(new[] { quickShot });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, quickShot, enemy);

            Assert.That(player.Hand.Contains(quickShot), Is.False);
        }

        [Test]
        public void PlayCard_CardInHand_MovesCardToDiscardPile()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var quickShot = Card.Attack("Quick Shot", energyCost: 1, damage: 6);
            player.AddToDrawPile(new[] { quickShot });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, quickShot, enemy);

            Assert.That(player.DiscardPile.Contains(quickShot), Is.True);
        }

        [Test]
        public void PlayCard_CardNotInHand_ThrowsAndStateUnchanged()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var phantomCard = Card.Attack("Phantom Card", energyCost: 1, damage: 6);

            Assert.Throws<InvalidOperationException>(() => CombatResolver.PlayCard(player, phantomCard, enemy));
            Assert.That(enemy.CurrentHP, Is.EqualTo(42));
            Assert.That(player.CurrentEnergy, Is.EqualTo(3));
        }

        [Test]
        public void PlayCard_CardGrantsBlock_IncreasesPlayerBlock()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var sideStep = Card.Skill("Side Step", energyCost: 1, block: 5);
            player.AddToDrawPile(new[] { sideStep });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, sideStep, enemy);

            Assert.That(player.CurrentBlock, Is.EqualTo(5));
        }

        [Test]
        public void PlayCard_BlockOnlyCard_DoesNotDamageTarget()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var sideStep = Card.Skill("Side Step", energyCost: 1, block: 5);
            player.AddToDrawPile(new[] { sideStep });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, sideStep, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(42));
        }

        [Test]
        public void EnemyAttack_DealsAttackDamageToPlayer()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var enemy = new Enemy(maxHP: 42, attackDamage: 10);

            CombatResolver.EnemyAttack(enemy, player);

            Assert.That(player.CurrentHP, Is.EqualTo(60));
        }

        [Test]
        public void EnemyAttack_DamageMitigatedByPlayerBlock()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            player.GainBlock(5);
            var enemy = new Enemy(maxHP: 42, attackDamage: 10);

            CombatResolver.EnemyAttack(enemy, player);

            Assert.That(player.CurrentBlock, Is.EqualTo(0));
            Assert.That(player.CurrentHP, Is.EqualTo(65));
        }

        [Test]
        public void PlayCard_PlayerHasStrength_AddsFlatDamageToAttackCard()
        {
            var player = new Player(startingEnergy: 1);
            player.GainStrength(3);
            var enemy = new Enemy(maxHP: 42);
            var quickShot = Card.Attack("Quick Shot", energyCost: 1, damage: 6);
            player.AddToDrawPile(new[] { quickShot });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, quickShot, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(33));
        }

        [Test]
        public void PlayCard_PlayerDrained_ReducesAttackDamageByQuarterRoundedDown()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var strike = Card.Attack("Strike", energyCost: 1, damage: 8);
            player.AddToDrawPile(new[] { strike });
            player.StartTurn(cardsToDraw: 1);
            player.ApplyDrained(1);

            CombatResolver.PlayCard(player, strike, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(36));
        }

        [Test]
        public void PlayCard_TargetExposed_IncreasesDamageTakenByHalfRoundedDown()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            enemy.ApplyExposed(1);
            var strike = Card.Attack("Strike", energyCost: 1, damage: 8);
            player.AddToDrawPile(new[] { strike });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, strike, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(30));
        }

        [Test]
        public void PlayCard_PlayerHasStrength_DoesNotAffectBlockOnlyCard()
        {
            var player = new Player(startingEnergy: 1);
            player.GainStrength(3);
            var enemy = new Enemy(maxHP: 42);
            var sideStep = Card.Skill("Side Step", energyCost: 1, block: 5);
            player.AddToDrawPile(new[] { sideStep });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, sideStep, enemy);

            Assert.That(player.CurrentBlock, Is.EqualTo(5));
        }

        [Test]
        public void PlayCard_PlayerSundered_ReducesBlockGainedByQuarterRoundedDown()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var sideStep = Card.Skill("Side Step", energyCost: 1, block: 8);
            player.AddToDrawPile(new[] { sideStep });
            player.StartTurn(cardsToDraw: 1);
            player.ApplySundered(1);

            CombatResolver.PlayCard(player, sideStep, enemy);

            Assert.That(player.CurrentBlock, Is.EqualTo(6));
        }

        [Test]
        public void PlayCard_PlayerSundered_DoesNotAffectAttackDamage()
        {
            var player = new Player(startingEnergy: 1);
            player.ApplySundered(2);
            var enemy = new Enemy(maxHP: 42);
            var quickShot = Card.Attack("Quick Shot", energyCost: 1, damage: 6);
            player.AddToDrawPile(new[] { quickShot });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, quickShot, enemy);

            Assert.That(enemy.CurrentHP, Is.EqualTo(36));
        }

        [Test]
        public void EnemyAttack_EnemyHasStrength_AddsFlatDamage()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var enemy = new Enemy(maxHP: 42, attackDamage: 10);
            enemy.GainStrength(2);

            CombatResolver.EnemyAttack(enemy, player);

            Assert.That(player.CurrentHP, Is.EqualTo(58));
        }

        [Test]
        public void EnemyAttack_EnemyDrained_ReducesDamageByQuarterRoundedDown()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var enemy = new Enemy(maxHP: 42, attackDamage: 8);
            enemy.ApplyDrained(1);

            CombatResolver.EnemyAttack(enemy, player);

            Assert.That(player.CurrentHP, Is.EqualTo(64));
        }

        [Test]
        public void EnemyAttack_PlayerExposed_IncreasesDamageByHalfRoundedDown()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            player.ApplyExposed(1);
            var enemy = new Enemy(maxHP: 42, attackDamage: 8);

            CombatResolver.EnemyAttack(enemy, player);

            Assert.That(player.CurrentHP, Is.EqualTo(58));
        }

        [Test]
        public void PlayCard_ShotTaggedCard_IncrementsShotsPlayedThisTurn()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var quickShot = Card.Attack("Quick Shot", energyCost: 1, damage: 6, tags: new[] { CardTag.Shot });
            player.AddToDrawPile(new[] { quickShot });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, quickShot, enemy);

            Assert.That(player.ShotsPlayedThisTurn, Is.EqualTo(1));
        }

        [Test]
        public void PlayCard_NonShotCard_DoesNotIncrementShotsPlayedThisTurn()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var sideStep = Card.Skill("Side Step", energyCost: 1, block: 5);
            player.AddToDrawPile(new[] { sideStep });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, sideStep, enemy);

            Assert.That(player.ShotsPlayedThisTurn, Is.EqualTo(0));
        }

        [Test]
        public void PlayCard_ShotTaggedCardWithCostReduction_SpendsReducedEnergy()
        {
            var player = new Player(startingEnergy: 3);
            player.ReduceShotCostThisTurn(1);
            var enemy = new Enemy(maxHP: 42);
            var quickShot = Card.Attack("Quick Shot", energyCost: 1, damage: 6, tags: new[] { CardTag.Shot });
            player.AddToDrawPile(new[] { quickShot });
            player.StartTurn(cardsToDraw: 1);
            player.ReduceShotCostThisTurn(1);

            CombatResolver.PlayCard(player, quickShot, enemy);

            Assert.That(player.CurrentEnergy, Is.EqualTo(3));
        }

        [Test]
        public void PlayCard_NonShotCardWithCostReduction_SpendsFullEnergy()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var sideStep = Card.Skill("Side Step", energyCost: 1, block: 5);
            player.AddToDrawPile(new[] { sideStep });
            player.StartTurn(cardsToDraw: 1);
            player.ReduceShotCostThisTurn(1);

            CombatResolver.PlayCard(player, sideStep, enemy);

            Assert.That(player.CurrentEnergy, Is.EqualTo(2));
        }

        [Test]
        public void PlayCard_ShotCostReductionExceedsCost_FlooredAtZero()
        {
            var player = new Player(startingEnergy: 3);
            var enemy = new Enemy(maxHP: 42);
            var quickShot = Card.Attack("Quick Shot", energyCost: 1, damage: 6, tags: new[] { CardTag.Shot });
            player.AddToDrawPile(new[] { quickShot });
            player.StartTurn(cardsToDraw: 1);
            player.ReduceShotCostThisTurn(5);

            CombatResolver.PlayCard(player, quickShot, enemy);

            Assert.That(player.CurrentEnergy, Is.EqualTo(3));
        }

        [Test]
        public void ExecuteEnemyIntent_AttackIntent_DealsDamageToPlayer()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var moves = new[] { new EnemyMove("Bite", IntentType.Attack, value: 4) };
            var enemy = new Enemy(maxHP: 12, moves, new FakeRandom());

            CombatResolver.ExecuteEnemyIntent(enemy, player);

            Assert.That(player.CurrentHP, Is.EqualTo(66));
        }

        [Test]
        public void ExecuteEnemyIntent_BlockIntent_EnemyGainsBlock()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var moves = new[] { new EnemyMove("Guard", IntentType.Block, value: 8) };
            var enemy = new Enemy(maxHP: 42, moves, new FakeRandom());

            CombatResolver.ExecuteEnemyIntent(enemy, player);

            Assert.That(enemy.CurrentBlock, Is.EqualTo(8));
            Assert.That(player.CurrentHP, Is.EqualTo(70));
        }

        [Test]
        public void ExecuteEnemyIntent_DebuffIntent_AppliesStatusToPlayerWithoutDamage()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var moves = new[] { new EnemyMove("Shriek", IntentType.Debuff, value: 2, status: StatusType.Drained) };
            var enemy = new Enemy(maxHP: 30, moves, new FakeRandom());

            CombatResolver.ExecuteEnemyIntent(enemy, player);

            Assert.That(player.Drained, Is.EqualTo(2));
            Assert.That(player.CurrentHP, Is.EqualTo(70));
        }

        [Test]
        public void ExecuteEnemyIntent_DebuffIntentAppliesSundered_AppliesStatusToPlayerWithoutDamage()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var moves = new[] { new EnemyMove("Hex", IntentType.Debuff, value: 2, status: StatusType.Sundered) };
            var enemy = new Enemy(maxHP: 28, moves, new FakeRandom());

            CombatResolver.ExecuteEnemyIntent(enemy, player);

            Assert.That(player.Sundered, Is.EqualTo(2));
            Assert.That(player.CurrentHP, Is.EqualTo(70));
        }

        [Test]
        public void ExecuteEnemyIntent_NoIntent_DoesNothing()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var enemy = new Enemy(maxHP: 42, attackDamage: 10);

            CombatResolver.ExecuteEnemyIntent(enemy, player);

            Assert.That(player.CurrentHP, Is.EqualTo(70));
        }

        [Test]
        public void ExecuteEnemyIntent_AfterExecuting_ChoosesNewIntent()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var moves = new[]
            {
                new EnemyMove("Attack", IntentType.Attack, value: 9, weight: 3),
                new EnemyMove("Guard", IntentType.Block, value: 8, weight: 1)
            };
            var enemy = new Enemy(maxHP: 42, moves, new SequenceRandom(0, 3));
            var firstIntent = enemy.CurrentIntent;

            CombatResolver.ExecuteEnemyIntent(enemy, player);

            Assert.That(firstIntent, Is.EqualTo(moves[0]));
            Assert.That(enemy.CurrentIntent, Is.EqualTo(moves[1]));
        }

        [Test]
        public void FireTriggers_MatchingEvent_AppliesEffect()
        {
            var player = new Player(startingEnergy: 3);
            player.AddTrigger(new TriggeredEffect(TriggerEvent.TurnStarted, new GainVolleyEffect(1)));
            var enemy = new Enemy(maxHP: 42);

            CombatResolver.FireTriggers(player, TriggerEvent.TurnStarted, enemy, new[] { enemy });

            Assert.That(player.Volley, Is.EqualTo(1));
        }

        [Test]
        public void FireTriggers_NonMatchingEvent_DoesNotApply()
        {
            var player = new Player(startingEnergy: 3);
            player.AddTrigger(new TriggeredEffect(TriggerEvent.TurnStarted, new GainVolleyEffect(1)));
            var enemy = new Enemy(maxHP: 42);

            CombatResolver.FireTriggers(player, TriggerEvent.TurnEnded, enemy, new[] { enemy });

            Assert.That(player.Volley, Is.EqualTo(0));
        }

        [Test]
        public void FireTriggers_MultipleMatchingTriggers_AppliesAll()
        {
            var player = new Player(startingEnergy: 3);
            player.AddTrigger(new TriggeredEffect(TriggerEvent.TurnStarted, new GainVolleyEffect(1)));
            player.AddTrigger(new TriggeredEffect(TriggerEvent.TurnStarted, new GainVolleyEffect(1)));
            var enemy = new Enemy(maxHP: 42);

            CombatResolver.FireTriggers(player, TriggerEvent.TurnStarted, enemy, new[] { enemy });

            Assert.That(player.Volley, Is.EqualTo(2));
        }

        [Test]
        public void PlayCard_CardWithTriggers_RegistersThemOnPlayer()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var trigger = new TriggeredEffect(TriggerEvent.TurnStarted, new GainVolleyEffect(1));
            var card = new Card("Practiced Hand", energyCost: 1, CardType.Power, new CardEffect[0], tags: new[] { CardTag.Exhaust }, triggers: new[] { trigger });
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, card, enemy);

            Assert.That(player.Triggers.Contains(trigger), Is.True);
        }

        [Test]
        public void PlayCard_ExhaustTaggedCard_RemovesFromHandWithoutDiscarding()
        {
            var player = new Player(startingEnergy: 1);
            var enemy = new Enemy(maxHP: 42);
            var card = new Card("Practiced Hand", energyCost: 1, CardType.Power, new CardEffect[0], tags: new[] { CardTag.Exhaust });
            player.AddToDrawPile(new[] { card });
            player.StartTurn(cardsToDraw: 1);

            CombatResolver.PlayCard(player, card, enemy);

            Assert.That(player.Hand.Contains(card), Is.False);
            Assert.That(player.DiscardPile.Contains(card), Is.False);
            Assert.That(player.ExhaustPile.Contains(card), Is.True);
        }
    }
}
