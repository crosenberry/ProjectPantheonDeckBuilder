using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Pantheon.Core.Combat;
using Pantheon.Core.Run;
using Pantheon.Unity;

namespace Pantheon.PlayTests
{
    public class RunControllerTests
    {
        private static RunController CreateController(out GameObject go, out CombatEncounterRunner combatRunner)
        {
            go = new GameObject();
            combatRunner = go.AddComponent<CombatEncounterRunner>();
            var controller = go.AddComponent<RunController>();
            controller.CombatRunner = combatRunner;
            return controller;
        }

        // Weakens every enemy to 1 HP first so the outcome is reached quickly and
        // deterministically - combat correctness itself is CombatResolverTests'
        // job (EditMode); this only needs a real PlayerWon to reach RunController's
        // reaction to it.
        private static IEnumerator WinCombat(RunController controller)
        {
            var encounter = controller.CombatRunner.Encounter;
            foreach (var enemy in encounter.Enemies)
            {
                enemy.TakeDamage(enemy.CurrentHP - 1);
            }

            var guard = 0;
            while (encounter.Outcome == CombatOutcome.InProgress && guard++ < 50)
            {
                var card = encounter.Player.Hand.FirstOrDefault(c => c.Type == CardType.Attack && c.EnergyCost <= encounter.Player.CurrentEnergy);
                if (card != null) { controller.CombatRunner.PlayCard(card); }
                else { controller.CombatRunner.EndTurn(); }
            }

            yield return null;
        }

        // Walks the GreekStages sample layout (Combat -> Rest -> Combat -> Boss)
        // from whatever the current stage's entry point is, up to and including
        // entering the Boss node, without winning that final fight - lets a
        // caller inspect the Boss encounter's enemy list before it's cleared.
        private static IEnumerator WalkStageToBossNodeWithoutWinning(RunController controller)
        {
            controller.EnterNode(0);
            yield return null;
            yield return WinCombat(controller);

            controller.EnterNode(1);
            yield return null;

            controller.EnterNode(2);
            yield return null;
            yield return WinCombat(controller);

            controller.EnterNode(3);
            yield return null;
        }

        // Walks the GreekStages sample layout (Combat -> Rest -> Combat -> Boss)
        // from whatever the current stage's entry point is, through to the Boss
        // node's victory.
        private static IEnumerator WalkStageToBoss(RunController controller)
        {
            yield return WalkStageToBossNodeWithoutWinning(controller);
            yield return WinCombat(controller);
        }

        [UnityTest]
        public IEnumerator Start_BeforeStartingPlay_MainMenuNotDismissed()
        {
            var controller = CreateController(out var go, out _);
            yield return null;

            Assert.That(controller.MainMenuDismissed, Is.False);

            Object.Destroy(go);
        }

        [UnityTest]
        public IEnumerator StartPlay_DismissesMainMenu()
        {
            var controller = CreateController(out var go, out _);
            yield return null;

            controller.StartPlay();

            Assert.That(controller.MainMenuDismissed, Is.True);

            Object.Destroy(go);
        }

        [UnityTest]
        public IEnumerator Start_BeforeSelection_AwaitsBlessingChoice()
        {
            var controller = CreateController(out var go, out _);
            yield return null;

            Assert.That(controller.BlessingSelected, Is.False);
            Assert.That(controller.CurrentRun, Is.Null);
            Assert.That(controller.Player, Is.Null);

            Object.Destroy(go);
        }

        [UnityTest]
        public IEnumerator SelectBlessing_CreatesRunAndPlayerWithInitialState()
        {
            var controller = CreateController(out var go, out _);
            yield return null;

            controller.SelectBlessing(SelectedBlessing.Artemis);

            Assert.That(controller.BlessingSelected, Is.True);
            Assert.That(controller.CurrentRun, Is.Not.Null);
            Assert.That(controller.CurrentRun.Outcome, Is.EqualTo(RunOutcome.InProgress));
            Assert.That(controller.Player, Is.Not.Null);
            Assert.That(controller.Player.CurrentHP, Is.EqualTo(70));
            Assert.That(controller.InCombat, Is.False);

            Object.Destroy(go);
        }

        [UnityTest]
        public IEnumerator SelectBlessing_Thor_UsesThorStarterDeckAndNorseEnemies()
        {
            var controller = CreateController(out var go, out var combatRunner);
            yield return null;
            controller.SelectBlessing(SelectedBlessing.Thor);

            controller.EnterNode(0);
            yield return null;

            Assert.That(combatRunner.Encounter.Player.Hand.Any(c => c.Name == "Hammer Strike"), Is.True);
            Assert.That(combatRunner.Encounter.Enemies[0].MaxHP, Is.EqualTo(44));

            Object.Destroy(go);
        }

        [UnityTest]
        public IEnumerator SelectBlessing_Anubis_UsesAnubisStarterDeckAndEgyptianEnemies()
        {
            var controller = CreateController(out var go, out var combatRunner);
            yield return null;
            controller.SelectBlessing(SelectedBlessing.Anubis);

            controller.EnterNode(0);
            yield return null;

            Assert.That(combatRunner.Encounter.Player.Hand.Any(c => c.Name == "Jackal's Bite"), Is.True);
            Assert.That(combatRunner.Encounter.Enemies[0].MaxHP, Is.EqualTo(40));

            Object.Destroy(go);
        }

        [UnityTest]
        public IEnumerator SelectBlessing_SunWukong_UsesSunWukongStarterDeckAndChineseEnemies()
        {
            var controller = CreateController(out var go, out var combatRunner);
            yield return null;
            controller.SelectBlessing(SelectedBlessing.SunWukong);

            controller.EnterNode(0);
            yield return null;

            Assert.That(combatRunner.Encounter.Player.Hand.Any(c => c.Name == "Ape Fist"), Is.True);
            Assert.That(combatRunner.Encounter.Enemies[0].MaxHP, Is.EqualTo(42));

            Object.Destroy(go);
        }

        [UnityTest]
        public IEnumerator EnterNode_ArtemisBossNode_IncludesWrathfulErinys()
        {
            var controller = CreateController(out var go, out var combatRunner);
            yield return null;
            controller.SelectBlessing(SelectedBlessing.Artemis);

            yield return WalkStageToBossNodeWithoutWinning(controller);

            Assert.That(combatRunner.Encounter.Enemies.Any(e => e.MaxHP == 34), Is.True);

            Object.Destroy(go);
        }

        [UnityTest]
        public IEnumerator EnterNode_ThorBossNode_IncludesThunderhideJotunn()
        {
            var controller = CreateController(out var go, out var combatRunner);
            yield return null;
            controller.SelectBlessing(SelectedBlessing.Thor);

            yield return WalkStageToBossNodeWithoutWinning(controller);

            Assert.That(combatRunner.Encounter.Enemies.Any(e => e.MaxHP == 38), Is.True);

            Object.Destroy(go);
        }

        [UnityTest]
        public IEnumerator EnterNode_AnubisBossNode_IncludesAmmitsShade()
        {
            var controller = CreateController(out var go, out var combatRunner);
            yield return null;
            controller.SelectBlessing(SelectedBlessing.Anubis);

            yield return WalkStageToBossNodeWithoutWinning(controller);

            Assert.That(combatRunner.Encounter.Enemies.Any(e => e.MaxHP == 36), Is.True);

            Object.Destroy(go);
        }

        [UnityTest]
        public IEnumerator EnterNode_SunWukongBossNode_IncludesStoneGuardian()
        {
            var controller = CreateController(out var go, out var combatRunner);
            yield return null;
            controller.SelectBlessing(SelectedBlessing.SunWukong);

            yield return WalkStageToBossNodeWithoutWinning(controller);

            Assert.That(combatRunner.Encounter.Enemies.Any(e => e.MaxHP == 40), Is.True);

            Object.Destroy(go);
        }

        [UnityTest]
        public IEnumerator EnterNode_CombatNode_BeginsEncounterAndSetsInCombat()
        {
            var controller = CreateController(out var go, out var combatRunner);
            yield return null;
            controller.SelectBlessing(SelectedBlessing.Artemis);

            controller.EnterNode(0);
            yield return null;

            Assert.That(controller.InCombat, Is.True);
            Assert.That(combatRunner.Encounter, Is.Not.Null);
            Assert.That(combatRunner.Encounter.Outcome, Is.EqualTo(CombatOutcome.InProgress));

            Object.Destroy(go);
        }

        [UnityTest]
        public IEnumerator Update_CombatWonAtNonBossNode_ReturnsToMapWithoutBossReward()
        {
            var controller = CreateController(out var go, out _);
            yield return null;
            controller.SelectBlessing(SelectedBlessing.Artemis);
            controller.EnterNode(0);
            yield return null;

            yield return WinCombat(controller);

            Assert.That(controller.InCombat, Is.False);
            Assert.That(controller.AwaitingBossReward, Is.False);
            Assert.That(controller.CurrentRun.Outcome, Is.EqualTo(RunOutcome.InProgress));

            Object.Destroy(go);
        }

        [UnityTest]
        public IEnumerator EnterNode_RestNode_HealsPlayerAndStaysOnMap()
        {
            var controller = CreateController(out var go, out _);
            yield return null;
            controller.SelectBlessing(SelectedBlessing.Artemis);
            controller.EnterNode(0);
            yield return null;
            yield return WinCombat(controller);
            var hpBeforeRest = controller.Player.CurrentHP;

            controller.EnterNode(1);
            yield return null;

            Assert.That(controller.Player.CurrentHP, Is.GreaterThan(hpBeforeRest).Or.EqualTo(controller.Player.MaxHP));
            Assert.That(controller.InCombat, Is.False);

            Object.Destroy(go);
        }

        [UnityTest]
        public IEnumerator Update_CombatWonAtBossNode_OffersTwoBossRelicsAndAwaitsReward()
        {
            var controller = CreateController(out var go, out _);
            yield return null;
            controller.SelectBlessing(SelectedBlessing.Artemis);

            yield return WalkStageToBoss(controller);

            Assert.That(controller.AwaitingBossReward, Is.True);
            Assert.That(controller.OfferedBossRelics.Count, Is.EqualTo(2));
            Assert.That(controller.CurrentRun.Outcome, Is.EqualTo(RunOutcome.InProgress));

            Object.Destroy(go);
        }

        [UnityTest]
        public IEnumerator ClaimBossReward_NotFinalStage_AdvancesToNextStageAndGrantsRelic()
        {
            var controller = CreateController(out var go, out _);
            yield return null;
            controller.SelectBlessing(SelectedBlessing.Artemis);
            yield return WalkStageToBoss(controller);
            var chosen = controller.OfferedBossRelics[0];

            controller.ClaimBossReward(chosen);
            yield return null;

            Assert.That(controller.OwnedRelics, Contains.Item(chosen));
            Assert.That(controller.CurrentRun.CompletedStageCount, Is.EqualTo(1));
            Assert.That(controller.CurrentRun.Outcome, Is.EqualTo(RunOutcome.InProgress));
            Assert.That(controller.AwaitingBossReward, Is.False);

            Object.Destroy(go);
        }

        [UnityTest]
        public IEnumerator ClaimBossReward_FinalStage_CompletesRunWithVictory()
        {
            var controller = CreateController(out var go, out _);
            yield return null;
            controller.SelectBlessing(SelectedBlessing.Artemis);
            yield return WalkStageToBoss(controller);
            controller.ClaimBossReward(controller.OfferedBossRelics[0]);
            yield return null;

            yield return WalkStageToBoss(controller);
            controller.ClaimBossReward(controller.OfferedBossRelics[0]);
            yield return null;

            Assert.That(controller.CurrentRun.Outcome, Is.EqualTo(RunOutcome.Victory));
            Assert.That(controller.OwnedRelics.Count, Is.EqualTo(2));

            Object.Destroy(go);
        }

        [UnityTest]
        public IEnumerator Update_PlayerLosesCombat_RecordsDefeat()
        {
            var controller = CreateController(out var go, out _);
            yield return null;
            controller.SelectBlessing(SelectedBlessing.Artemis);
            controller.EnterNode(0);
            yield return null;

            controller.CombatRunner.Encounter.Player.TakeDamage(999);
            controller.CombatRunner.EndTurn();
            yield return null;

            Assert.That(controller.CurrentRun.Outcome, Is.EqualTo(RunOutcome.Defeat));
            Assert.That(controller.InCombat, Is.False);

            Object.Destroy(go);
        }

        [UnityTest]
        public IEnumerator TogglePanels_ReflectsControllerStateAcrossSelectionMapCombatAndBossReward()
        {
            var go = new GameObject();
            var combatRunner = go.AddComponent<CombatEncounterRunner>();
            var controller = go.AddComponent<RunController>();
            controller.CombatRunner = combatRunner;
            var mainMenuPanel = new GameObject("MainMenuPanel");
            var blessingSelectPanel = new GameObject("BlessingSelectPanel");
            var mapPanel = new GameObject("MapPanel");
            var bossRewardPanel = new GameObject("BossRewardPanel");
            var combatPanel = new GameObject("CombatPanel");
            controller.MainMenuPanel = mainMenuPanel;
            controller.BlessingSelectPanel = blessingSelectPanel;
            controller.MapPanel = mapPanel;
            controller.BossRewardPanel = bossRewardPanel;
            controller.CombatPanels = new[] { combatPanel };
            yield return null;

            Assert.That(mainMenuPanel.activeSelf, Is.True, "Main menu should be visible before Play is pressed.");
            Assert.That(blessingSelectPanel.activeSelf, Is.False, "Blessing select should stay hidden until Play is pressed.");

            controller.StartPlay();

            Assert.That(mainMenuPanel.activeSelf, Is.False, "Main menu should hide once Play is pressed.");
            Assert.That(blessingSelectPanel.activeSelf, Is.True, "Blessing select should be visible before any choice is made.");
            Assert.That(mapPanel.activeSelf, Is.False, "Map should stay hidden until a Blessing is selected.");

            controller.SelectBlessing(SelectedBlessing.Artemis);

            Assert.That(blessingSelectPanel.activeSelf, Is.False, "Blessing select should hide once a choice is made.");
            Assert.That(mapPanel.activeSelf, Is.True, "Map should be visible at run start.");
            Assert.That(combatPanel.activeSelf, Is.False);
            Assert.That(bossRewardPanel.activeSelf, Is.False);

            controller.EnterNode(0);
            yield return null;

            Assert.That(mapPanel.activeSelf, Is.False, "Map should hide once combat starts.");
            Assert.That(combatPanel.activeSelf, Is.True, "Combat panel should show during combat.");

            yield return WinCombat(controller);

            Assert.That(combatPanel.activeSelf, Is.False, "Combat panel should hide once combat ends.");
            Assert.That(mapPanel.activeSelf, Is.True, "Map should reappear after a non-boss win.");

            controller.EnterNode(1);
            controller.EnterNode(2);
            yield return null;
            yield return WinCombat(controller);
            controller.EnterNode(3);
            yield return null;
            yield return WinCombat(controller);

            Assert.That(bossRewardPanel.activeSelf, Is.True, "Boss reward panel should show after the Boss falls.");
            Assert.That(mapPanel.activeSelf, Is.False, "Map should hide while awaiting the boss reward.");

            Object.Destroy(go);
            Object.Destroy(mainMenuPanel);
            Object.Destroy(blessingSelectPanel);
            Object.Destroy(mapPanel);
            Object.Destroy(bossRewardPanel);
            Object.Destroy(combatPanel);
        }
    }
}
