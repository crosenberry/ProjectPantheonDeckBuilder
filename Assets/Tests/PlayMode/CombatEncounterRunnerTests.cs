using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Pantheon.Core;
using Pantheon.Core.Blessings.Artemis;
using Pantheon.Core.Combat;
using Pantheon.Core.Enemies.Greek;
using Pantheon.Unity;

namespace Pantheon.PlayTests
{
    public class CombatEncounterRunnerTests
    {
        private static CombatEncounterRunner CreateRunner(out GameObject go)
        {
            go = new GameObject();
            var runner = go.AddComponent<CombatEncounterRunner>();
            var random = new SystemRandom();
            var player = new Player(startingEnergy: 3, startingHP: 70, random);
            player.AddToDrawPile(ArtemisCards.StarterDeck());
            var enemy = GreekEnemies.HopliteSkirmisher(random);
            runner.BeginEncounter(player, new[] { enemy });
            return runner;
        }

        [UnityTest]
        public IEnumerator BeginEncounter_CreatesEncounterWithStartingState()
        {
            var runner = CreateRunner(out var go);
            yield return null;

            Assert.That(runner.Encounter, Is.Not.Null);
            Assert.That(runner.Encounter.Enemy.CurrentHP, Is.EqualTo(42));
            Assert.That(runner.Encounter.Player.CurrentHP, Is.EqualTo(70));
            Assert.That(runner.Encounter.Player.Hand.Count, Is.EqualTo(5));

            Object.Destroy(go);
        }

        [UnityTest]
        public IEnumerator PlayCard_ValidCardInHand_AppliesItsEffect()
        {
            var runner = CreateRunner(out var go);
            yield return null;
            var card = runner.Encounter.Player.Hand[0];

            runner.PlayCard(card);

            Assert.That(runner.Encounter.Enemy.CurrentHP, Is.EqualTo(36));

            Object.Destroy(go);
        }

        [UnityTest]
        public IEnumerator PlayCard_CardAlreadyPlayed_DoesNotThrowOrDoubleApply()
        {
            var runner = CreateRunner(out var go);
            yield return null;
            var card = runner.Encounter.Player.Hand[0];
            runner.PlayCard(card);

            Assert.DoesNotThrow(() => runner.PlayCard(card));
            Assert.That(runner.Encounter.Enemy.CurrentHP, Is.EqualTo(36));

            Object.Destroy(go);
        }

        [UnityTest]
        public IEnumerator EndTurn_TriggersEnemyIntentOnPlayer()
        {
            var runner = CreateRunner(out var go);
            yield return null;

            runner.EndTurn();

            // Hoplite Skirmisher rolls Attack (9 dmg -> HP 61) or Guard (0 dmg -> HP 70).
            Assert.That(runner.Encounter.Player.CurrentHP, Is.EqualTo(70).Or.EqualTo(61));

            Object.Destroy(go);
        }

        [UnityTest]
        public IEnumerator EndTurn_CombatContinues_DrawsNewHandForNextTurn()
        {
            var runner = CreateRunner(out var go);
            yield return null;

            runner.EndTurn();

            Assert.That(runner.Encounter.Outcome, Is.EqualTo(CombatOutcome.InProgress));
            Assert.That(runner.Encounter.Player.Hand.Count, Is.EqualTo(5));

            Object.Destroy(go);
        }

        [UnityTest]
        public IEnumerator EndTurn_EnemyDefeated_DoesNotDrawNewHand()
        {
            var runner = CreateRunner(out var go);
            yield return null;
            runner.Encounter.Enemy.TakeDamage(999);

            runner.EndTurn();

            Assert.That(runner.Encounter.Outcome, Is.EqualTo(CombatOutcome.PlayerWon));
            Assert.That(runner.Encounter.Player.Hand.Count, Is.EqualTo(0));

            Object.Destroy(go);
        }
    }
}
