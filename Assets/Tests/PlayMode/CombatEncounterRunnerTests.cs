using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Pantheon.Unity;

namespace Pantheon.PlayTests
{
    public class CombatEncounterRunnerTests
    {
        [UnityTest]
        public IEnumerator CombatEncounterRunner_OnStart_CreatesEncounterWithStartingState()
        {
            var go = new GameObject();
            var runner = go.AddComponent<CombatEncounterRunner>();

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
            var go = new GameObject();
            var runner = go.AddComponent<CombatEncounterRunner>();
            yield return null;
            var card = runner.Encounter.Player.Hand[0];

            runner.PlayCard(card);

            Assert.That(runner.Encounter.Enemy.CurrentHP, Is.EqualTo(36));

            Object.Destroy(go);
        }

        [UnityTest]
        public IEnumerator PlayCard_CardAlreadyPlayed_DoesNotThrowOrDoubleApply()
        {
            var go = new GameObject();
            var runner = go.AddComponent<CombatEncounterRunner>();
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
            var go = new GameObject();
            var runner = go.AddComponent<CombatEncounterRunner>();
            yield return null;

            runner.EndTurn();

            // Hoplite Skirmisher rolls Attack (9 dmg -> HP 61) or Guard (0 dmg -> HP 70).
            Assert.That(runner.Encounter.Player.CurrentHP, Is.EqualTo(70).Or.EqualTo(61));

            Object.Destroy(go);
        }

        [UnityTest]
        public IEnumerator EndTurn_CombatContinues_DrawsNewHandForNextTurn()
        {
            var go = new GameObject();
            var runner = go.AddComponent<CombatEncounterRunner>();
            yield return null;

            runner.EndTurn();

            Assert.That(runner.Encounter.Outcome, Is.EqualTo(Pantheon.Core.Combat.CombatOutcome.InProgress));
            Assert.That(runner.Encounter.Player.Hand.Count, Is.EqualTo(5));

            Object.Destroy(go);
        }

        [UnityTest]
        public IEnumerator EndTurn_EnemyDefeated_DoesNotDrawNewHand()
        {
            var go = new GameObject();
            var runner = go.AddComponent<CombatEncounterRunner>();
            yield return null;
            runner.Encounter.Enemy.TakeDamage(999);

            runner.EndTurn();

            Assert.That(runner.Encounter.Outcome, Is.EqualTo(Pantheon.Core.Combat.CombatOutcome.PlayerWon));
            Assert.That(runner.Encounter.Player.Hand.Count, Is.EqualTo(0));

            Object.Destroy(go);
        }
    }
}
