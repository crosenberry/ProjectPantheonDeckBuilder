# Implementation Roadmap

Companion to [GameDesignDocument.md](GameDesignDocument.md). Tracks the order features get built in, when `develop` gets promoted to `main`, and the design-first process that governs each step.

## Process: design session before implementation, every milestone

Before starting the feature branch(es) for a milestone, hold a design session covering **terminology and mythology-lore tailoring** for whatever that milestone introduces — not just the mechanic's math. The goal is to avoid building something generic (or accidentally StS-shaped) in code and having to rename/refactor it later once the "real" naming is decided.

Scope of a design session is naming/flavor/terminology, not re-deriving mechanics or numbers from scratch — numeric tuning stays deferred until real playtesting exists, per the standing decision in [DifferentiationHooks.md](DifferentiationHooks.md). The two are separate: "what do we call this and how does it read in-fiction" is decided before implementation; "is 50% the right number" is decided after playtesting.

## Main-promotion policy

`develop` is promoted to `main` only at a milestone boundary the user explicitly calls for (never automatic). Criteria at that point:
- All tests green (EditMode + PlayMode) on `develop`.
- The milestone's own "done" bar (below) is met.
- Verified live in Play mode, not just under automated tests.
- Tagged (`v0.1`, `v0.2`, ...) for a clean rollback point.

## Sequencing: vertical-first

Artemis goes all the way through combat → relics → enhancements → passive tree → full run structure before Thor starts. The meta-systems (relics, enhancements, passive tree) are mostly architecture — once proven correct for one Blessing, adding the next three should mostly be new *data* against already-working code, not new systems. This also means Syncretism (needs 2 Blessings) doesn't get exercised for real until M5.

## Milestones

| # | Scope | Feature branch(es) | "Done" bar | Main promotion |
|---|---|---|---|---|
| **M0** | Engine foundation — Core primitives, TDD infra, branch workflow, one scripted proof-of-concept scene | *(already in develop)* | 37/37 green, one hardcoded encounter playable | **`v0.1`** — ready now |
| **M1** | Core Combat Vocabulary for real: Strength, Exposed, Drained, Sundered as actual `Pantheon.Core` mechanics (only Block exists today; names locked in the M1 design session below) | `feature/combat-vocabulary` | EditMode tests cover all 4 in `CombatResolver` | — |
| **M1.5** | Card effect architecture — composable effects so cards stop being hardcoded C# | `feature/card-effect-system` | A handful of real effects (damage, block, debuff, buff) expressed through it, old cards migrated | — |
| **M2** | Artemis playable: Volley resource, a curated card slice (not all 75 — enough to prove Barrage/Full Draw/Huntress), real turn loop, real Greek enemy roster (Hoplite/Harpy/Viper Brood) replacing the hardcoded demo | `feature/volley-resource`, `feature/artemis-card-slice`, `feature/greek-enemies` | A real fight, real deck, real enemies, verified in Play mode | — |
| **M3** | Artemis meta-layer: Relics (Greek subset), Enhancements (Player/Minor/Major incl. her Mythic cards), Passive Tree (Mythos + her 17 nodes), Shop | one branch per sub-system | A full run (not one fight) playable for Artemis | — |
| **M4** | Map & run structure: node graph, stage progression, boss fights, next-stage selection (Prophecy family) | `feature/run-map` | Start-to-finish run, win or lose, for Artemis alone | **`v0.2`** — first complete Blessing |
| **M5** | Thor: Storm resource, his card slice, Norse enemies, his passive tree, Syncretism made real | `feature/thor-*` | Second Blessing playable + a working cross-mythology fusion | **`v0.3`** |
| **M6** | Anubis (Scale) | `feature/anubis-*` | Third Blessing playable | **`v0.4`** |
| **M7** | Sun Wukong (Form) | `feature/wukong-*` | All 4 Blessings playable | **`v0.5`** |
| **M8** | Deferred differentiation work: Core Combat Vocabulary's *mechanical* redesign (if playtesting shows it's needed beyond the M1 naming pass) and Hook 2 (enemies using resource systems) | — | — | — |
| **M9** | Co-op | — | — | — |

## Design session log

Records what got decided in each pre-milestone design session, so the reasoning survives past the conversation it happened in.

### M1 — Core Combat Vocabulary naming (complete)

- **Scope was naming only.** Mechanics/percentages stay unchanged and stay placeholder — that's still deferred until real playtesting, per [DifferentiationHooks.md](DifferentiationHooks.md). This session decided what these debuffs are called and how they read, not whether the numbers are right.
- **Only 3 terms needed renaming**: Vulnerable, Weak, Frail — the specific matched trio that reads as recognizably StS at those exact definitions. Block, Strength, Exhaust, and Curse are genre-ubiquitous words that predate and extend beyond StS; left unchanged.
- **Renamed as a cohesive family**, not three unrelated words:
  - Vulnerable → **Exposed** (+50% damage taken, unchanged)
  - Weak → **Drained** (-25% damage dealt, unchanged)
  - Frail → **Sundered** (-25% Block gained, unchanged)
- **Universal naming confirmed**, not per-mythology flavor text for the status names themselves — same name regardless of which mythology's card or enemy applies it, matching how Block/Energy/Mythos/Divine Essence already work. Mythology flavor lives in card *text* ("Hunter's Mark inflicts..."), not in four different names for the same debuff. Reasoning: a shared status bar / enemy intent readout needs to read consistently across mythologies, especially once Syncretism or co-op puts two mythologies in one fight.
- **Future debuffs are explicitly out of scope for M1** — enemy-specific effects like Burn or Shock will get their own design pass when specific enemies that need them are being designed (M2 onward), not invented speculatively now.
- **Naming collision caught and avoided**: "Marked" was considered for Exposed but Artemis's passive tree already has a node called "Marked for the Hunt" — picked "Exposed" instead to keep the two systems unambiguous.
- **Explicitly not a fix for the "feels like a mod" concern** — see the note added to [DifferentiationHooks.md](DifferentiationHooks.md#peer-feedback--validates-the-baseline-gap). This is table-stakes (the code should never contain the literal string "Vulnerable"), not a claim that genericness is solved.
- **Propagated through existing design docs** the same session it was decided, so nothing downstream (all 4 Blessings' card drafts, both passive trees, the Greek enemy sample set, the Syncretism prototype) references the old names — implementation starts from already-consistent docs.
