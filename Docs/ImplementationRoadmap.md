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
| **M3** | Artemis meta-layer: Relics (Greek subset), Enhancements (Player/Minor/Major incl. her Mythic cards), Passive Tree (Mythos + her 17 nodes), Shop | `feature/trigger-system`, `feature/artemis-passive-tree`, `feature/artemis-relics`, `feature/artemis-enhancements`, `feature/shop` | A full run (not one fight) playable for Artemis | — |
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

### M1.5 — Card effect architecture (complete)

This milestone is architecture, not new mythology-facing vocabulary — the session's job was mainly to confirm no naming here needs mythology tailoring, and lock the handful of internal names that *would* be painful to rename later (per `CLAUDE.md`'s "never hardcode a generic name in `Pantheon.Core` expecting to rename it" rule), since these are enums/method names, not card flavor text.

- **`CardType`: `Attack` / `Skill` / `Power`.** Not a new decision — every card draft doc already tags every card with exactly one of these three in its Type column. This session just formalizes it as a real field on `Card` instead of being implicit.
- **`StatusType`: `Strength` / `Exposed` / `Drained` / `Sundered`.** Exact reuse of the M1 names, now expressed as data (an enum value passed to a generic `ApplyStatusEffect`) instead of one hardcoded method per status on `CombatResolver`. Payoff: a future enemy-specific debuff (Burn, Shock — still explicitly deferred per M1) becomes a new enum value plus numeric handling, not a new C# type and a new set of call sites.
- **Effect vocabulary scoped to three kinds for this milestone**: `DealDamageEffect` (always targets the enemy — no drafted card deals self-damage yet), `GainBlockEffect` (always targets the player — no drafted card grants an ally or enemy Block), `ApplyStatusEffect(StatusType, amount, EffectTarget)` (needs an explicit Self/Enemy target since both buff-self and debuff-enemy are real, common patterns already in every card draft, e.g. "deal 8 damage, apply 2 Exposed" applies Exposed to the enemy while a Strength card applies to self).
- **`EffectTarget`: `Self` / `Enemy` only** — no `AllEnemies`/multi-target yet, since `CombatEncounter` only ever has one `Enemy` today. Not a rename risk: this is standard genre engineering language (identical across every StS-like), not mythology-flavored, so widening it later when multi-enemy fights exist is a pure addition, not a rename.
- **Power cards' triggered effects ("whenever you play an Attack," "at the start of your turn" — already present throughout the drafted Power card pools) are explicitly out of scope.** That needs an event/trigger system, which is a materially bigger architecture decision than "compose an on-play effect list" — deferred to whichever milestone first needs a working Power card (M2 checks whether Artemis's curated slice needs one; if not, it lands whenever the first Blessing's Power cards go live). `CardType.Power` exists now so cards can be tagged correctly in the meantime, but nothing executes its trigger yet.
- **Migration**: `Card` drops `DamageAmount`/`BlockAmount` entirely in favor of an `Effects` list; `CombatResolver.PlayCard` no longer knows about individual statuses, it just applies each effect. Two static factories (`Card.Attack(name, cost, damage)`, `Card.Skill(name, cost, block)`) cover the common single-effect case so card-authoring call sites (tests, and later real content) don't have to hand-build effect lists for the simple cases.

### M2 — Artemis playable (Volley, card slice, Greek enemies) design session (complete)

Most of M2's vocabulary was already locked by earlier design-phase work (before the TDD process existed), so this session's job was mostly confirming what's already decided still holds, plus naming the handful of things that genuinely don't exist in any doc yet.

**Already locked — confirmed, not re-decided:**
- Resource **Volley** and its rule shape (per-turn counter, resets to 0 each turn, uncapped) — [GDD §3.1](GameDesignDocument.md).
- Card tag **Shot** — the Attack subset that raises Volley by 1 on play.
- Archetype path names **Barrage / Full Draw / Huntress**, and the 25-card curated slice (10 starter + 5 per path) already drafted in [Artemis-CardDraft.md](Characters/Artemis-CardDraft.md) — this already satisfies M2's "curated slice, not all 75" bar. M2 implements these existing cards; it doesn't draft new ones.
- Greek enemy roster **Hoplite Skirmisher / Harpy Screecher / Viper Brood** (+ Training Dummy) with concrete HP/intent numbers already in [MinimalSampleSet-Greek.md](Enemies/MinimalSampleSet-Greek.md).

**New naming decisions** (things no doc had named yet, that implementation would otherwise have to invent on the fly):

1. **Enemy Intent formalization.** `MinimalSampleSet-Greek.md` flagged this explicitly as unresolved ("Intent system assumed... not yet formalized in Core Combat Vocabulary §10"). Decision: `IntentType` = `Attack`, `Block`, `Buff`, `Debuff` — matches the doc's own language ("a damage number for Attack intents, or an icon for Block/Buff/Debuff intents"). An enemy's flavor-named action (Hoplite's "Guard", Harpy's "Shriek"/"Claw") is presentation text bound to one of these 4 categories, not a category of its own — keeps intent-reveal and any future enemy-AI logic generic instead of growing a new case per enemy forever.
2. **Card tagging, generalized beyond just Shot.** Not speculative — Thor's already-drafted pool independently needs the same shape of concept ("Storm-generating cards grant 2 additional Block when played," Runed Shield, [Thor-FullCardDraft.md](Characters/Thor-FullCardDraft.md)). Decision: a `CardTag` enum (same pattern as `CardType`/`StatusType` from M1.5) holding just `Shot` for now; `Card` gains an `IReadOnlyList<CardTag> Tags` (empty by default). A future `StormGenerating` tag is an additive enum value when Thor's slice starts, not a rename.
3. **Volley stays Player-only, explicitly not a generalized "Blessing resource."** Confirming an existing finding, not deciding fresh: [CrossBlessing-Comparison.md](Characters/CrossBlessing-Comparison.md) already established Volley/Storm/Scale/Form are four structurally different resource shapes (one-way-per-turn / one-way-banked / two-way-magnitude / categorical). A shared generic-resource abstraction now would fight that finding. `Player` gets a dedicated `Volley` field plus a gain method and a per-turn reset, the same shape as `CurrentBlock` — no shared resource interface with Storm/Scale/Form.
4. **Volley-reading/consuming effect shapes (Rain of Arrows' scale-by-Volley, Called Shot's threshold-based double-hit, Full Draw's consume-and-scale) are explicitly out of scope for this session** — that's mechanics/architecture work for whichever slice actually implements those specific cards, not a vocabulary decision.

**Sequencing**: implemented as the roadmap's three M2 branches, in order — `feature/volley-resource` (Player-side mechanic + `GainVolleyEffect`) first since it's the smallest, self-contained piece everything else depends on; then `feature/artemis-card-slice` (the 25-card starter + 3-path pool, including the Volley-reading effects from point 4 above); then `feature/greek-enemies` (Intent system + the 4-enemy roster).

### M3 — Artemis meta-layer design session (complete)

Unlike M1/M1.5/M2, most of M3's subject matter had **never been drafted at all** for any Blessing (relics, Player Enhancements, and Minor Enhancement upgrade specifics are all new content here, not confirmations of existing docs) — so this session did real content drafting, not just naming confirmation, alongside the naming decisions.

**Already locked — confirmed, not re-decided:** Divine Essence, Mythos, Player/Minor/Major Enhancement, Godly Trial, Cautious/Bold/Defiant Offering, Curse cards, Rift, the Prophecy family (Oracle of Delphi / Thread of the Moirai for Greek), Merchant/Shop node, the full card-rarity ladder (Basic/Common/Uncommon/Rare, confirmed at [GDD §2](GameDesignDocument.md); Mythic above Rare, confirmed at §8) — and Artemis's entire 17-node passive tree, which was already fully drafted in [Artemis-PassiveTree.md](Characters/Artemis-PassiveTree.md) before this session even started.

**New content drafted this session:**
1. **[Artemis-Relics.md](Characters/Artemis-Relics.md)** — 6 regular Greek relics + 1 boss-exclusive, the first relic pool drafted for any mythology. Carries forward **Silver Quiver**, already established in the Syncretism prototype, rather than re-inventing it. Resolves GDD §11's "relic count" open item for Greek specifically (not a claimed final count for the other 3 mythologies). Explicitly avoided two rule collisions while drafting: no relic duplicates the safety-net-stays-a-card ruling, and no relic bends Volley's per-turn reset (that stays a Mythic-capstone-only privilege per GDD §8).
2. **[Artemis-Enhancements.md](Characters/Artemis-Enhancements.md)** — 4 Player Enhancement options (a 3-choice screen draws from a pool bigger than 3, matching how reward screens already work elsewhere) and 3 worked Minor Enhancement upgrade examples (Quick Shot, Side Step, Hunter's Mark) proving the Cautious/Bold/Defiant shape — Defiant matches Bold's headline number but adds a second, different bonus, so a successful gamble feels like it bought something Bold couldn't. The other 22 cards' upgrade tables are explicitly deferred as a content-authoring task, not a rule-shape question.
3. **Greek shop instance named Hermes' Exchange** ([GDD §7](GameDesignDocument.md)) — reuses Hermes rather than inventing competing Greek trade-god branding, since he's already the established fit for the future cross-mythology Bazaar. The other 3 mythologies' shop names are out of scope (M3 is Artemis/Greek-only).

**Real finding, not just naming — surfaced while drafting, not before:** most of the relics just drafted (Silver Quiver, Huntress's Snare, Nymph's Ward, Calydon's Mark) and several passive-tree branch nodes already locked in Artemis-PassiveTree.md (Overdraw Mastery, Loaded Quiver, the Momentum's Edge/Reserve choice pairs) all need a "whenever/at-the-start-of X happens" trigger hook — the exact same architecture gap Power cards were deferred on back in the M1.5 and M2 design sessions. This isn't a new decision the design session is making; it's confirmation that the trigger/event system is a real M3 prerequisite, not just an M2 Power-card loose end, exactly as predicted when M2 wrapped. `feature/trigger-system` is added as M3's first branch for this reason — building it once here unblocks relics, the passive tree's conditional nodes, *and* Artemis's 3 still-deferred Power cards as a side effect, rather than deferring it a third time.

**Deliberately not decided here**: the trigger system's own internal architecture (event types, subscription shape, Exhaust semantics) — that's implementation-level design for `feature/trigger-system` itself to work out via TDD, the same way M1.5 didn't pre-design `CardEffect`'s exact class shape in its own design session, only its naming/scope boundaries.
