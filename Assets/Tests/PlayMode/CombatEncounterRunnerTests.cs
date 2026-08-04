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
            Assert.That(runner.Encounter.Player.Hand.Count, Is.EqualTo(1));

            Object.Destroy(go);
        }

        [UnityTest]
        public IEnumerator PlayQuickShot_DealsDamageToEnemy()
        {
            var go = new GameObject();
            var runner = go.AddComponent<CombatEncounterRunner>();
            yield return null;

            runner.PlayQuickShot();

            Assert.That(runner.Encounter.Enemy.CurrentHP, Is.EqualTo(36));

            Object.Destroy(go);
        }

        [UnityTest]
        public IEnumerator EndTurn_TriggersEnemyAttackOnPlayer()
        {
            var go = new GameObject();
            var runner = go.AddComponent<CombatEncounterRunner>();
            yield return null;

            runner.EndTurn();

            Assert.That(runner.Encounter.Player.CurrentHP, Is.EqualTo(60));

            Object.Destroy(go);
        }
    }
}
