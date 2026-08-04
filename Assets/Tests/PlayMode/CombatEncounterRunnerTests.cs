using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Pantheon.Unity;

namespace Pantheon.PlayTests
{
    public class CombatEncounterRunnerTests
    {
        [UnityTest]
        public IEnumerator CombatEncounterRunner_OnStart_RunsFullExchange()
        {
            var go = new GameObject();
            var runner = go.AddComponent<CombatEncounterRunner>();

            yield return null;

            Assert.That(runner.Encounter, Is.Not.Null);
            Assert.That(runner.Encounter.Enemy.CurrentHP, Is.EqualTo(36));
            Assert.That(runner.Encounter.Player.CurrentHP, Is.EqualTo(60));

            Object.Destroy(go);
        }
    }
}
