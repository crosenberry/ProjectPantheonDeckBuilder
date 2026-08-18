using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Pantheon.Core;
using Pantheon.Core.Blessings.Artemis;
using Pantheon.Core.Blessings.Thor;
using Pantheon.Core.Combat;
using Pantheon.Core.Enemies.Greek;
using Pantheon.Core.Enemies.Norse;
using Pantheon.Core.Meta;
using Pantheon.Core.Meta.Greek;
using Pantheon.Core.Run;
using Pantheon.Core.Run.Greek;

namespace Pantheon.Unity
{
    public class RunController : MonoBehaviour
    {
        private const int RestSiteHealAmount = 15;
        private const int BossExclusiveChancePercent = 10;

        public CombatEncounterRunner CombatRunner;
        public GameObject BlessingSelectPanel;
        public GameObject MapPanel;
        public GameObject[] CombatPanels;
        public GameObject BossRewardPanel;

        public Core.Run.Run CurrentRun { get; private set; }
        public Player Player { get; private set; }
        public bool BlessingSelected { get; private set; }
        public bool InCombat { get; private set; }
        public bool AwaitingBossReward { get; private set; }
        public IReadOnlyList<Relic> OfferedBossRelics { get; private set; }
        public List<Relic> OwnedRelics { get; } = new List<Relic>();

        private IRandom _random;
        private List<Card> _deck;
        private SelectedBlessing _selectedBlessing;

        private void Start()
        {
            _random = new SystemRandom();
            TogglePanels();
        }

        // No selection screen existed before Thor - Artemis was simply the only
        // choice - so this is the first point a live game ever needs to pick
        // between Blessings. Relics stay tied to stage mythology regardless of
        // which Blessing is selected (GreekStages/GreekRelics reused as-is for
        // both) - that's the M3 relic design rule, not an oversight.
        public void SelectBlessing(SelectedBlessing blessing)
        {
            _selectedBlessing = blessing;
            Player = new Player(startingEnergy: 3, startingHP: 70, _random);
            _deck = (blessing == SelectedBlessing.Thor ? ThorCards.StarterDeck() : ArtemisCards.StarterDeck()).ToList();
            BlessingSelected = true;
            // stageCount: 2 to prove Run.AdvanceToNextStage's chaining, not a
            // tuned run length - both stages reuse the same sample layout until
            // a second mythology's content exists to chain to via a real Rift
            // pick (deferred, see the boss-reward-wiring slice's scope note).
            CurrentRun = new Core.Run.Run(stageCount: 2, GreekStages.SampleStage());
            TogglePanels();
        }

        public void EnterNode(int nodeIndex)
        {
            var node = CurrentRun.CurrentStage.Nodes[nodeIndex];
            CurrentRun.CurrentStage.MoveTo(nodeIndex);

            if (node.Type == NodeType.Rest)
            {
                Player.Heal(RestSiteHealAmount);
                return;
            }

            Player.PrepareForCombat(_deck);
            CombatRunner.BeginEncounter(Player, BuildEnemies(nodeIndex));
            InCombat = true;
            TogglePanels();
        }

        public void ClaimBossReward(Relic relic)
        {
            if (OfferedBossRelics == null || !OfferedBossRelics.Contains(relic))
            {
                return;
            }

            OwnedRelics.Add(relic);
            OfferedBossRelics = null;
            AwaitingBossReward = false;

            if (CurrentRun.CompletedStageCount + 1 >= CurrentRun.StageCount)
            {
                CurrentRun.CompleteFinalStage();
            }
            else
            {
                CurrentRun.AdvanceToNextStage(GreekStages.SampleStage());
            }

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
                TogglePanels();
                return;
            }

            var currentNode = CurrentRun.CurrentStage.Nodes[CurrentRun.CurrentStage.CurrentNodeIndex.Value];
            if (currentNode.Type == NodeType.Boss)
            {
                OfferedBossRelics = BossReward.Roll(GreekRelics.RegularPool().ToList(), GreekRelics.TalossCore(), _random, BossExclusiveChancePercent);
                AwaitingBossReward = true;
            }

            TogglePanels();
        }

        private IReadOnlyList<Enemy> BuildEnemies(int nodeIndex)
        {
            var node = CurrentRun.CurrentStage.Nodes[nodeIndex];

            // Placeholder bosses: the full minimal roster at once, until real
            // stage bosses are designed (deferred with the rest of full enemy/boss
            // design, GDD section 11).
            if (_selectedBlessing == SelectedBlessing.Thor)
            {
                if (node.Type == NodeType.Boss)
                {
                    return new[] { NorseEnemies.DraugrReaver(_random), NorseEnemies.SeidrHexer(_random) }
                        .Concat(NorseEnemies.WolfPack(count: 2, _random))
                        .ToList();
                }

                return nodeIndex % 2 == 0
                    ? new[] { NorseEnemies.DraugrReaver(_random) }
                    : new[] { NorseEnemies.SeidrHexer(_random) };
            }

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
            if (BlessingSelectPanel != null)
            {
                BlessingSelectPanel.SetActive(!BlessingSelected);
            }

            if (MapPanel != null)
            {
                MapPanel.SetActive(BlessingSelected && !InCombat && !AwaitingBossReward);
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

            if (BossRewardPanel != null)
            {
                BossRewardPanel.SetActive(AwaitingBossReward);
            }
        }
    }
}
