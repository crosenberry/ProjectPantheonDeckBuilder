using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Pantheon.Core.Combat;

namespace Pantheon.Unity
{
    public class CombatEncounterRunner : MonoBehaviour
    {
        private const int CardsDrawnPerTurn = 5;

        public CombatEncounter Encounter { get; private set; }

        public void BeginEncounter(Player player, IReadOnlyList<Enemy> enemies)
        {
            Encounter = new CombatEncounter(player, enemies);
            Encounter.StartPlayerTurn(cardsToDraw: CardsDrawnPerTurn);
        }

        public void PlayCard(Card card)
        {
            if (!Encounter.Player.Hand.Contains(card))
            {
                return;
            }

            var target = Encounter.Enemies.FirstOrDefault(enemy => enemy.CurrentHP > 0);
            if (target == null)
            {
                return;
            }

            Encounter.PlayCard(card, target);
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
