using UnityEngine;
using Pantheon.Core;
using Pantheon.Core.Combat;

namespace Pantheon.Unity
{
    public class CombatEncounterRunner : MonoBehaviour
    {
        public CombatEncounter Encounter { get; private set; }

        private Card _quickShot;

        private void Start()
        {
            var player = new Player(startingEnergy: 3, startingHP: 70, new SystemRandom());
            var enemy = new Enemy(maxHP: 42, attackDamage: 10);
            _quickShot = new Card("Quick Shot", energyCost: 1, damageAmount: 6);

            player.AddToDrawPile(new[] { _quickShot });
            player.StartTurn(cardsToDraw: 1);

            Encounter = new CombatEncounter(player, enemy);
        }

        public void PlayQuickShot()
        {
            CombatResolver.PlayCard(Encounter.Player, _quickShot, Encounter.Enemy);
        }

        public void EndTurn()
        {
            Encounter.EndPlayerTurn();
        }
    }
}
