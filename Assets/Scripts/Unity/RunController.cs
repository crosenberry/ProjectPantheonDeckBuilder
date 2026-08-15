using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Pantheon.Core;
using Pantheon.Core.Blessings.Artemis;
using Pantheon.Core.Combat;
using Pantheon.Core.Enemies.Greek;
using Pantheon.Core.Run;
using Pantheon.Core.Run.Greek;

namespace Pantheon.Unity
{
    public class RunController : MonoBehaviour
    {
        public CombatEncounterRunner CombatRunner;
        public GameObject MapPanel;
        public GameObject[] CombatPanels;

        public Core.Run.Run CurrentRun { get; private set; }
        public Player Player { get; private set; }
        public bool InCombat { get; private set; }

        private IRandom _random;
        private List<Card> _deck;

        private void Start()
        {
            _random = new SystemRandom();
            Player = new Player(startingEnergy: 3, startingHP: 70, _random);
            _deck = ArtemisCards.StarterDeck().ToList();
            CurrentRun = new Core.Run.Run(stageCount: 1, GreekStages.SampleStage());
            TogglePanels();
        }

        public void EnterNode(int nodeIndex)
        {
            CurrentRun.CurrentStage.MoveTo(nodeIndex);
            Player.PrepareForCombat(_deck);
            CombatRunner.BeginEncounter(Player, BuildEnemies(nodeIndex));
            InCombat = true;
            TogglePanels();
        }

        private void Update()
        {
            if (!InCombat || CombatRunner.Encounter == null)
            {
                return;
            }

            var outcome = CombatRunner.Encounter.Outcome;
            if (outcome == CombatOutcome.InProgress)
            {
                return;
            }

            InCombat = false;
            if (outcome == CombatOutcome.PlayerLost)
            {
                CurrentRun.RecordDefeat();
            }
            else if (CurrentRun.CurrentStage.IsComplete)
            {
                CurrentRun.CompleteFinalStage();
            }

            TogglePanels();
        }

        private IReadOnlyList<Enemy> BuildEnemies(int nodeIndex)
        {
            var node = CurrentRun.CurrentStage.Nodes[nodeIndex];

            // Placeholder boss: the full minimal Greek roster at once, until real
            // stage bosses are designed (deferred with the rest of full enemy/boss
            // design, GDD section 11).
            if (node.Type == NodeType.Boss)
            {
                return new[] { GreekEnemies.HopliteSkirmisher(_random), GreekEnemies.HarpyScreecher(_random) }
                    .Concat(GreekEnemies.ViperBrood(count: 2, _random))
                    .ToList();
            }

            return nodeIndex % 2 == 0
                ? new[] { GreekEnemies.HopliteSkirmisher(_random) }
                : new[] { GreekEnemies.HarpyScreecher(_random) };
        }

        private void TogglePanels()
        {
            if (MapPanel != null)
            {
                MapPanel.SetActive(!InCombat);
            }

            if (CombatPanels != null)
            {
                foreach (var panel in CombatPanels)
                {
                    if (panel != null)
                    {
                        panel.SetActive(InCombat);
                    }
                }
            }
        }
    }
}
