using System.Linq;
using UnityEngine;
using Pantheon.Core;
using Pantheon.Core.Combat;
using Pantheon.Core.Blessings.Artemis;
using Pantheon.Core.Enemies.Greek;

namespace Pantheon.Unity
{
    public class CombatEncounterRunner : MonoBehaviour
    {
        private const int CardsDrawnPerTurn = 5;

        public CombatEncounter Encounter { get; private set; }

        private void Start()
        {
            var random = new SystemRandom();
            var player = new Player(startingEnergy: 3, startingHP: 70, random);
            var enemy = GreekEnemies.HopliteSkirmisher(random);

            player.AddToDrawPile(ArtemisCards.StarterDeck());

            Encounter = new CombatEncounter(player, enemy);
            Encounter.StartPlayerTurn(cardsToDraw: CardsDrawnPerTurn);
        }

        public void PlayCard(Card card)
        {
            if (!Encounter.Player.Hand.Contains(card))
            {
                return;
            }

            Encounter.PlayCard(card, Encounter.Enemy);
        }

        public void EndTurn()
        {
            Encounter.EndPlayerTurn();

            if (Encounter.Outcome == CombatOutcome.InProgress)
            {
                Encounter.StartPlayerTurn(cardsToDraw: CardsDrawnPerTurn);
            }
        }
    }
}
