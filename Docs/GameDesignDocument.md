# Project Pantheon — Game Design Document

Status: living draft. Sections marked **[OPEN]** are unresolved and need a decision. Sections marked **[DEFAULT]** are a reasonable starting choice that hasn't been explicitly confirmed — treat as changeable, not locked.

## 1. Pillars

- Turn-based deckbuilder roguelite in the lineage of Slay the Spire / Monster Train.
- Mythology is the genre skin *and* the mechanical skeleton: Greek, Norse, Egyptian, Chinese each have a distinct combat identity, not just a distinct paint job.
- **Single-player is the primary, fully-functioning mode.** Co-op is a planned future layer, not a launch requirement — see [Section 9](#9-co-op-future-layer).
- No item/relic rarity tiers. Keep the relic pool flat per mythology to avoid the tier-count multiplication (5 types × rarity tiers) blowing up scope.
- **Multiple viable build paths per Blessing.** Each Blessing's 75-card pool should support at least 2-3 distinct archetypes, including at least one archetype that barely touches the Blessing's signature keyword mechanic — not a single linear "solved" deck. See the pressure-test in [Docs/Characters/Artemis-CardDraft.md](Characters/Artemis-CardDraft.md) for the pattern to replicate per Blessing.
- **Originality beyond the StS/Monster Train framework is an active, unsolved concern**, not assumed. See [Docs/DifferentiationHooks.md](DifferentiationHooks.md) for the honest gap assessment and candidate hooks (cross-mythology "Syncretism" combination system is the current leading candidate).

## 2. Scale Target

Modeled against Slay the Spire's actual numbers: ~370 total cards, made up of 75 unique cards per character × 4 characters (300) plus a shared colorless/curse/status pool (~70).

- 4 playable Blessings (1 per mythology), **75 unique cards each = ~300 character cards.**
- Colorless/neutral card pool (usable by any Blessing, found via events/shops) — target **~70**, matching StS's ratio.
- Relics: flat pool per mythology, **no rarity tiers — confirmed.** Rarity is explicitly being left on the table as a possible future layer if the flat pool ever feels like it needs more depth, but not part of the current design. **[OPEN]** target count per mythology, suggest ~30-40 each ≈ 120-160 total, plus a handful of boss-exclusive relics per mythology.
- **Cards use Common / Uncommon / Rare tiers (plus Basic for starter cards) — confirmed.** Independent of the relic no-rarity rule directly above; that ruling only ever applied to items/relics, not cards.
- Potions-equivalent: not yet designed — **[OPEN]** whether Pantheon has a StS-potion analog (a consumable, single-use, drawn-mid-combat resource) or whether Divine Essence spend covers that role instead.

## 3. The Four Blessings — confirmed picks

| Mythology | God | Combat Focus |
|---|---|---|
| Greek | **Artemis** | Offense / Mobility |
| Norse | **Thor** | Offense + Defense hybrid |
| Egyptian | **Anubis** | Utility / Sustain |
| Chinese | **Sun Wukong** | Transformation / Utility |

Each Blessing needs, before card-writing starts:
- A primary resource/keyword mechanic (formalized below).
- A start-of-run signature relic (StS-style starter relic) that telegraphs the mechanic immediately.
- A 75-card curve: attacks / skills / powers split, plus a small number of build-defining rares.

### 3.1 Artemis — Volley (Greek: Offense/Mobility)

> First card-draft pressure test done — see [Docs/Characters/Artemis-CardDraft.md](Characters/Artemis-CardDraft.md) for a sample across 3 distinct build paths (Barrage, Full Draw, Huntress) plus findings.

- **Resource: Volley**, a per-turn counter starting at 0 each turn, uncapped.
- Any card tagged **Shot** (most Attacks in her kit) increases Volley by 1 when played, in addition to its normal effect.
- Payoff cards read or spend Volley: e.g. an Attack that deals bonus damage equal to current Volley, or a Skill that discharges all Volley for a multi-hit burst (1 hit per stack) then resets it to 0.
- Design tension: dumping Volley early for a small guaranteed hit vs. holding it for one big discharge — mirrors StS Silent's poison-stacking payoff structure but on a per-turn timer instead of per-fight.
- Directly matches the player's original example: "increase the amount of arrows shot at a time," stacking with the passive tree's arrow-count nodes.

### 3.2 Thor — Storm (Norse: Offense + Defense hybrid)

> Taken to full 75-card depth — see [Docs/Characters/Thor-FullCardDraft.md](Characters/Thor-FullCardDraft.md). Confirms the "banked across turns" timing plays meaningfully different from Artemis's Volley at full scale, not just in a small sample. Two issues found needing cleanup before final: a near-duplicate card pair, and one Rare (Deathless Rage) that may belong as a relic instead of a card.

- **Resource: Storm**, stacks persist turn-to-turn (unlike Volley), gained as a *side effect* of Block-generating cards (e.g., a Skill that grants 8 Block also grants 2 Storm).
- Storm does nothing passively — it's inert potential energy. Payoff cards **discharge** Storm: consume all stacks for bonus Attack damage (1:1 or better ratio) or to trigger a Power effect.
- Encourages a turtle-then-retaliate rhythm: block for a turn or two building Storm, then unleash — matches "defensive and offensive" identity directly rather than blending the two into one generic stat.
- Contrast with Artemis: Volley is short-fuse (per-turn, use-it-or-lose-it), Storm is long-fuse (banked across turns, bigger but slower payoff). Keeps the two Blessings from feeling like reskins of the same stacking-resource idea.

### 3.3 Anubis — Scale (Egyptian: Utility/Sustain)

> Taken to full 75-card depth — see [Docs/Characters/Anubis-FullCardDraft.md](Characters/Anubis-FullCardDraft.md). Confirms the "3 paths for free" hypothesis from the light sketch, and surfaced a genuinely new payoff shape (Judgment Incarnate rewards staying at exactly 0, distinct from "more extreme = more reward").

- **Resource: Scale**, a signed value (e.g. -5 to +5) representing the Ma'at balance (Feather of Truth vs. the Heart). Starts at 0 each combat.
- Cards push the Scale one direction or the other as a side effect: "Order" effects (cleanse, heal, block) push toward Feather (+); "Chaos" effects (curses on enemies, self-damage-for-power, DoT) push toward Heart (−).
- Payoff cards read the Scale's *position*, not a stack count: near-0 ("Balanced") grants consistency effects (cleanse + small heal), pushed to either extreme grants a bigger one-sided effect (heavy Chaos → damage equal to |Scale|; heavy Order → strong sustain/cleanse-all).
- This is deliberately a different resource *shape* than Volley/Storm (a magnitude that can swing two directions, not a one-way counter) — gives Anubis's deckbuilding puzzle a distinct feel: balance-around-zero vs. commit-to-an-extreme, rather than just "stack more."

### 3.4 Sun Wukong — Form (Chinese: Transformation/Utility)

> Taken to full 75-card depth — see [Docs/Characters/SunWukong-FullCardDraft.md](Characters/SunWukong-FullCardDraft.md). Surfaced a real rule-interaction question (does Beast Form's double-hit apply to cards that already hit multiple times?) — resolved: double-hit applies only to Attacks that normally hit once.

- **Resource: Form**, a 3-state stance (not 2, to differentiate from StS Watcher's Calm/Wrath): **Mortal** (baseline), **Beast**, **Immortal**.
- Stance-change cards cycle Form. Each Form modifies how *other* cards behave while active:
  - Beast: Attacks hit twice, but Skills cost +1 energy.
  - Immortal: Powers/buffs gained are stronger or permanent, but Attacks are weakened.
  - Mortal: no modifier — the neutral transition state.
- A secondary, lower-frequency payoff independent of Form: **Clone** cards that replay/copy the last card played (his hair-clone trick) — gives Wukong a combo-repeat utility tool that Artemis/Thor/Anubis don't have, reinforcing "transformation/utility" as distinct from the other three's more combat-stat-focused identities.

**[OPEN]**: exact numeric tuning (Volley discharge ratio, Storm conversion rate, Scale thresholds, Form energy costs) — these are rule shapes, not balanced numbers. Tuning happens once cards exist to test against.

> All 4 Blessings now at full 75-card depth: [Artemis](Characters/Artemis-CardDraft.md), [Thor](Characters/Thor-FullCardDraft.md), [Anubis](Characters/Anubis-FullCardDraft.md), [Sun Wukong](Characters/SunWukong-FullCardDraft.md), plus a [cross-Blessing comparison](Characters/CrossBlessing-Comparison.md). Verdict: all 4 resource shapes are structurally distinct (one-way magnitude / banked magnitude / signed two-way magnitude / categorical state) and all 4 support multiple build paths, satisfying the build-diversity pillar in Section 1. Two cross-cutting findings surfaced at full depth, both now resolved: a "safety net" Rare pattern appeared independently in 3 of 4 Blessings (Thor's Deathless Rage, Anubis's Osiris's Rebirth, Wukong's Elixir of Immortality) — ruled to stay a card, not a relic, in all 3, not forced onto Artemis; and Wukong's Beast Form double-hit was ruled to apply only to Attacks that normally hit once. Numeric tuning still entirely open everywhere.

## 4. Relics / Items

Carried over from the original concept almost unchanged:

- **No rarity tiers.** Every relic is just "a relic" — some are minor, some are build-defining, but that's a design axis, not a data field.
- **4 mythology flavors**, each with a primary focus and occasional secondary-focus outliers:
  - Greek: Offense, Mobility
  - Norse: Defense, Offense
  - Egyptian: Utility, Sustain
  - Chinese: Transformation, Utility
- **Acquisition**:
  - Divine Essence (currency, earned from defeating enemies on a stage) opens mythology-flavored chests — chest skin tells the player what flavor of relic they're about to get before they commit currency to it.
  - Stage bosses are randomized between 2 boss options per stage mythology. Defeating one drops a **Soul**, which opens a reward screen: 2 relics of that mythology's flavor, or a chance at a boss-exclusive relic.
  - Divine Essence has other uses beyond chests (shops, enhancement trials — see Section 5) — **[OPEN]** full list of essence sinks.
- **Regular enemies (mobs) are tied to stage mythology, like chests and bosses — not randomized independently like trials (§5.2) — confirmed.** Two reasons: enemies are the highest-frequency content in any stage by a wide margin, so decoupling them would erase "stage mythology" as a meaningful signal far more than trials ever could; and it's precisely *because* enemies stay orderly and mythology-coupled that a stray off-mythology trial reads as a notable exception rather than the norm — the trial system's "blurring between mythologies" lore only lands if that blurring is rare. First minimal sample set (Greek, paired with Artemis): [Docs/Enemies/MinimalSampleSet-Greek.md](Enemies/MinimalSampleSet-Greek.md) — a training dummy plus 3 basic enemy types, scoped only to unblock a first playable slice, not a real roster.

## 5. Enhancements

Three tiers, each reworked slightly from the original ROR2-shaped version to fit a turn-based, no-continuous-XP genre.

### 5.1 Player Enhancements
Legendary-relic-tier, game-altering picks (3-option choice screen), same payload as originally conceived. The trigger changes:

- **Original**: real-time XP level thresholds — doesn't exist in a deckbuilder.
- **Confirmed new trigger**: combat-count thresholds (e.g., every 3rd fight cleared). Functionally equivalent to a floor-count trigger given stages have a roughly fixed number of fights per floor, so either framing works — combat-count is the one to build against since it's more granular. Exact cadence still needs tuning once floor length is defined, but the trigger *type* is settled.
- Also obtainable via the **Forge of Hephaestus**: spend a number of regular relics to convert them into one Player Enhancement. (Unchanged from original concept — this gives players a sink for relics that don't fit their build.)

### 5.2 Minor Enhancements
Skill upgrades, obtained by spending Divine Essence at a stage's godly trial (statue/altar, mythology-flavored). High risk/high reward. Maps closely to StS's campfire card-upgrade, but gated behind a currency cost and (per original concept) some risk element rather than being free and guaranteed.

**Trial mythology is randomized independently of the stage's mythology — confirmed.** A trial on a Greek stage is not guaranteed (or even more likely, beyond the anti-repeat weighting below) to be Greek-flavored. Reasoning: tying trial flavor to stage flavor means every visit to a given stage-type teaches the same lesson and offers the same flavor of upgrade, which hurts replayability across runs. Trials work as genuine random encounters, closer to StS's "?" event nodes than to a stage-reskinned system. Recommend a light anti-repeat weight (mythologies seen in the last trial or two are less likely to come up again) rather than pure uniform 25%-each RNG, since uniform random tends to produce streaks that read as "broken" to players even when mathematically fine — **[OPEN]** exact weighting.

- Effect example (player's own): Artemis's arrow-count skill upgraded by a trial stacks with her passive tree — i.e., these compound with the permanent meta-progression, they don't replace it.

**Confirmed mechanic — three player-chosen commitment tiers ("Offerings"), not blind RNG by default:**

Design reasoning: pure chance-to-fail is the obvious reading of "high risk," but it's the version players tend to resent most in this genre (a coinflip that just deletes your currency). A tiered, player-chosen commitment model gets real risk/reward tension without forcing randomness on players who don't want it — cautious players always have a safe option, thrill-seekers can opt into the gamble. Worth treating as the standard shape for risk/reward nodes generally in Pantheon, not just this one system.

| Tier | Cost | Outcome |
|---|---|---|
| **Cautious Offering** | Base Divine Essence cost | Guaranteed, minor skill upgrade. No other cost. |
| **Bold Offering** | Higher Essence cost + lose a fixed chunk of **current** HP (not max) | Guaranteed, stronger skill upgrade. No randomness — the cost is HP you must be healthy enough to spend right now, so it's a real in-the-moment decision, not a permanent tax. |
| **Defiant Offering** | Highest Essence cost | Coin-flip (exact odds TBD in tuning). Success: strongest-tier skill upgrade, noticeably better than Bold. Failure: lose the Essence spent and gain 1 **Curse** card in your deck. |

- Introduces **Curse cards** as a formal piece of the base combat vocabulary (dead-weight cards that clog the hand/deck, standard genre convention) — needs folding into the "assumed base combat vocabulary" note alongside Block/Strength/Exposed/Drained/Sundered/Exhaust (see [Thor-FullCardDraft.md](Characters/Thor-FullCardDraft.md)) next time that list is formalized.
- Now that trial mythology is fully randomized (independent of stage), it's a *stronger* natural gate for a **Syncretism** unlock than the stage-mythology approach was — see [DifferentiationHooks.md §Hook 1](DifferentiationHooks.md). A player can stumble into an altar of a different mythology than their own Blessing on any stage, not only by deliberately traveling to a foreign-mythology stage, so this becomes a frequent, low-commitment touchpoint for Syncretism rather than a rare one gated behind a whole stage completion. Not required for the trial to function, but worth keeping in mind as a second use for the same node rather than building a separate trigger for Syncretism later.
- **[OPEN]**: exact numeric costs and Defiant's success odds — rule shape is settled, numbers need tuning once enough of the run's economy (Essence income rate, HP totals) is defined to tune against.

### 5.3 Major Enhancements
Rewarded for completing a stage, delivered via a "rift" interaction — the portal/gateway interacted with at stage-end. Mythology of the stage determines the enhancement's flavor. Strongest tier — can grant an entirely new passive, e.g. unlocking Hephaestus's Forge access even if Hephaestus isn't one of the run's active Blessings.

**Next-stage mythology selection — confirmed:**

- **Default**: next stage's mythology is randomized, using the same anti-repeat weighting approach as trials (§5.2) rather than pure uniform RNG, for consistency across the two systems.
- **Upgraded by a "Prophecy" family of Major Enhancements** — deliberately not one fixed enhancement per mythology. Each mythology has more than enough relevant deities/concepts to support several variants, so this is an open, extensible family rather than a closed list of exactly 4. Every variant shares the same **core effect**: at each future rift, reveal 2 of the 4 mythologies (randomly drawn) and choose which one the next stage will be. Each variant also carries its own **unique minor bonus** on top of that shared core, so picking between two same-mythology variants (when both show up as options) is a real choice, not a cosmetic one.
- Deliberately **not** full unconstrained choice of all 4 mythologies: that would let players always pick their favorite combo once unlocked, cutting against the same replayability goal that drove the trial-randomization decision.
- Bonuses stay generic enough to matter regardless of which Blessing the player is running — a bonus tied to one specific Blessing's unique resource (e.g., Anubis's Scale) would be dead text for the other 3 players, since Major Enhancements aren't Blessing-locked.

Confirmed starting set (2 per mythology, extensible — more variants can be added later using the same template: a relevant deity/concept + the shared reveal-2-pick-1 core + one small generic bonus):

| Mythology | Variant | Figure/Concept | Unique bonus |
|---|---|---|---|
| Greek | Oracle of Delphi | Apollo's oracle, prophecy | Before picking, preview one relic or Enhancement type available on each of the 2 choices. |
| Greek | Thread of the Moirai | The Fates, who spin/measure/cut the thread of life | The stage you don't choose is guaranteed to reappear as an option at your very next rift. |
| Norse | The Norns' Thread | Norns, fate-weavers at Yggdrasil | Before picking, preview which of the 2 possible bosses awaits on each choice. |
| Norse | Sight of the Ravens | Huginn & Muninn, Odin's scouting ravens | Reveals 3 of the 4 mythologies instead of 2 (still pick only 1). |
| Egyptian | Thoth's Ledger | Thoth, scribe/record-keeper, judgment alongside Anubis | Heal a small amount (e.g. 10% max HP) the moment you make your choice. |
| Egyptian | Ma'at's Balance | Ma'at, goddess of truth and balance | Your first source of damage taken on the chosen stage is reduced. |
| Chinese | Mandate of the Jade Emperor | Jade Emperor, ruler of heaven | Trial and shop costs are reduced for the entirety of the chosen stage. |
| Chinese | Guanyin's Guidance | Guanyin, guided Sun Wukong's journey in *Journey to the West* | Once per run, retroactively swap to the stage you declined, if you regret the choice later. |

- **[OPEN]**: exact bonus numbers (tuning), and whether the family should grow beyond 2 variants per mythology before launch or expand post-launch as content updates.

## 6. Currency — Divine Essence

Earned from defeating enemies on a stage. Known sinks so far:
- Mythology-flavored chests (Section 4)
- Godly trials for Minor Enhancements (Section 5.2)
- The merchant/shop node (Section 7)

## 7. Merchant / Shop Node

**Confirmed to exist** — the Mandate of the Jade Emperor Major Enhancement (§5.3) already assumed a shop with reducible costs, so this resolves that dependency rather than leaving it dangling.

**Greek instance named Hermes' Exchange** (Hermes already established as the trade-god fit for the future cross-mythology Bazaar, §below — reusing him for the mundane single-mythology shop instance is consistent, not competing branding). The other 3 mythologies' shop names aren't decided here (out of scope for M3, which is Artemis/Greek-only).

**Mythology-flavored by stage, tied like chests and bosses — not decoupled like trials — confirmed.** This is a deliberate inconsistency with the trial-randomization decision (§5.2): trials were decoupled from stage mythology specifically because a Minor Enhancement's *flavor of upgrade* is the main source of its replay variety, and "stumbling on a foreign altar" reads well thematically as a discovery moment. A merchant is a much more mundane, expected fixture — a Greek merchant in a Greek marketplace makes more sense than a random foreign trader appearing in Valhalla — and shop replay variety already comes from *which specific* cards/relics roll into stock from that mythology's pool, so it doesn't need the same decoupling to stay fresh.

**Future idea, not committed — "The Bazaar":** a shop variant that breaks the stage-coupling above on purpose, offering an assortment pulled from *all four* mythologies at once. Would follow the same shape as the Prophecy family (§5.3) — a per-mythology family of Major Enhancements sharing one core unlock (access to a cross-mythology Bazaar node) plus each variant's own minor bonus — tied to trade/commerce/travel deities: **Hermes** (Greek — messenger and trade god, an easy fit), **Njörðr** (Norse — Vanir god of seafaring, wealth, and trade), **Cai Shen** (Chinese — literally the God of Wealth, about as direct a fit as exists in any of the four pantheons), **Min** (Egyptian — god of the Eastern Desert, venerated as protector of caravans and trade routes to the Red Sea/Punt; a "protector of the trade route" angle rather than a direct commerce-god like the other three, but a legitimate fit). Worth noting this would be the *second* instance of the same "Major Enhancement family" template the Prophecy system established — a reusable pattern for future systems, not a one-off.

**What it sells**, each using the same "certainty costs more than a gamble" economic logic already established by the Minor Enhancement Offering tiers (§5.2):
- **Cards**: a small selection drawn from the player's own Blessing pool plus the shared colorless pool, priced by rarity tier.
- **Relics**: a small selection drawn from the stage's mythology-flavored relic pool (§4) — guaranteed, not random like a chest, priced at a premium over a chest's average expected cost since the player is paying for certainty instead of gambling.
- **Card/Curse removal**: pay Essence to permanently remove one card from the deck. Doubles as the direct relief valve for the Defiant Offering's curse risk (§5.2) — trials can inject Curses, the shop is where you pay to clean them out. Together these close a real risk/relief loop across the run rather than leaving curse risk as a pure downside with no in-game answer.
- **Potion-equivalent**: pending the still-open §2 question of whether one exists at all; the shop is the natural sale point if/when that gets resolved, not resolved here.

Browsing is free — only individual purchases cost Essence, standard genre convention.

- **[OPEN]**: exact pricing per rarity/type, and the premium multiplier for guaranteed relics vs. chest gamble odds — rule shape settled, numbers need the broader Essence economy defined first (same caveat as Minor Enhancement and next-stage selection tuning).

## 8. Meta-Progression — Passive Tree

Each Blessing has its own passive tree. This is the persistent-unlock layer across runs, separate from in-run Enhancements (which reset every run).

> All 4 trees now built: [Artemis](Characters/Artemis-PassiveTree.md), [Thor](Characters/Thor-PassiveTree.md), [Anubis](Characters/Anubis-PassiveTree.md), [Sun Wukong](Characters/SunWukong-PassiveTree.md). Each is 17 nodes (5-node trunk + 3 branches of 4, one per archetype path), confirming the rule shape holds consistently across all 4 resource shapes. Confirmed cross-Blessing design principle: **Mythic capstones are the one place in the game allowed to bend a Blessing's own core rule** — seen 3 times now (Artemis's Practiced Patience bends Volley's per-turn reset, Sun Wukong's Great Sage's Rampage bends the Beast Form × multi-hit ruling from §10), always as a singular, deliberate exception gated behind deep Mythos investment, never a general loophole.

**Currency: Mythos.** Earned via run-level milestones — clearing a stage, defeating a stage boss, completing a full run, with partial credit for progress even on a run that ends in death (Hades' Darkness-style). Not earned from in-run kills, keeping it functionally distinct from Divine Essence rather than a reskin of the same resource.

**One tree per Blessing, not shared — built directly on that Blessing's existing 3 archetype paths (§3)**, not a separate invented axis:
- A small shared **trunk** of universal nodes (max HP, starting Divine Essence, etc.) — permanent buy-and-keep, no respec needed since these don't compete with anything.
- Then splits into **3 branches, one per archetype path** (e.g., Artemis: Barrage / Full Draw / Huntress).

**Node philosophy — depth over access.** Nodes must never change what shows up in a run's reward pool: every archetype stays equally likely to appear every run regardless of Mythos investment, so meta-progression never narrows a player's freedom to experiment with a different path. Instead, nodes only matter once a player has actually drafted into that path during a given run:
- Dormant bonuses that activate on commitment, e.g. "if your deck has 3+ Barrage cards, gain +1 starting Volley."
- Payoff-scoped buffs (bonus applies only to that archetype's own cards) rather than a flat blanket "+X% damage" that would apply to everything regardless of build.
- A capstone per branch: unlocking a new **Mythic**-tier card added to that archetype's pool (below).

**Choice-pair nodes with free respec, Hades' Mirror of the Night-style.** Some nodes come in mutually exclusive pairs. Unlocking a pair costs Mythos once; which side of the pair is *active* can be freely toggled between runs (not mid-run) at no additional cost. Investing in one archetype never permanently locks a player out of another.

**Mythic — a new card tier above Rare, exclusive to passive-tree unlocks.** Not obtainable in-run any other way.
- **Design rule to keep it rewarding without trivializing runs**: a Mythic card should differ from existing Rares in *kind*, not just magnitude. It should reward a level of commitment no in-run Rare rewards (e.g., a payoff that only triggers on an extreme Shot count in one turn), rather than simply being "a stronger Rain of Arrows." Avoids raw power creep stacking on top of an already-tuned Rare tier.
- **Appears via its own dedicated reward moment** once unlocked (e.g., guaranteed once per run at a specific point) rather than folding into the standard Rare reward pool at the same frequency as the other Rares. This gives a second, independent dial — appearance frequency — for controlling its run-to-run impact, separate from its raw power, and makes it feel earned/ceremonial rather than just one more line in the loot table.
- Still just one card among 74 others in a 75-card deck — energy costs, draw variance, and the need for supporting synergy are already natural guardrails against any single card trivializing a run, same as any other card in this game.

- **[OPEN]**: exact Mythos costs, node counts per branch, the trunk node list, and specific Mythic card designs per Blessing — rule shape is settled, content and numbers aren't started.

## 9. Co-op (Future Layer)

Not part of the initial build. Design single-player systems so this can be bolted on later without a rearchitecture — i.e., avoid baking in single-player-only assumptions where a cheap alternative exists (e.g., keep "the run's active party" as a concept internally rather than hardcoding a single Blessing reference, even though only 1 slot is filled at launch).

Notes preserved from earlier discussion, for whenever this gets picked back up:
- Target 2 players, not full ROR2-scale 4 — referenced Slay the Spire 2's co-op as a positive comp worth studying when this gets revisited.
- Leaning individual HP pools with a "downed, revivable" state rather than a shared party HP bar (more forgiving, closer to ROR2's feel) — not finalized.
- Leaning shared currency + shared map node, individually-owned relics.
- With 4 Blessings, 2-player co-op gives C(4,2) = 6 pairing combinations — worth designing a handful of explicit cross-mythology relic/card synergy hooks (e.g., a Norse Block-generation card that a Greek card can consume for bonus damage) once co-op design resumes, so pairings feel distinct rather than just "two solo decks in the same room."

## 10. Core Combat Vocabulary

Formalizes terminology that's been used implicitly across all four Blessing card drafts and the Minor Enhancement risk mechanic (§5.2), previously only stated as an aside in [Thor-FullCardDraft.md](Characters/Thor-FullCardDraft.md). Standard genre conventions throughout (StS numbers used as defaults) — nothing in this project has given a reason to deviate from them yet, so these are inherited defaults, not bespoke design.

**Turn structure**:
- **Energy**: 3 per turn. Does not carry over to the next turn unless a card or relic explicitly says otherwise.
- **Draw**: 5 cards at the start of each turn. Hand discards at the end of turn unless a card says "Retain." Max hand size 10 — draws beyond that are lost.
- **Draw pile / discard pile**: played or discarded cards move to the discard pile. When the draw pile empties, the discard pile is shuffled into a new draw pile.

**Combat stats & statuses**:
- **Block**: prevents damage 1:1. Expires at the start of the player's next turn by default — cards that preserve it (e.g. Thor's Unbreakable) call that out explicitly as an exception, which is what confirms this is the baseline rule rather than an inconsistency.
- **Strength**: +1 damage per stack to Attacks. Persists for the whole combat with no natural decay, unless a card says otherwise.
- **Exposed** (debuff, formerly named "Vulnerable" — renamed in the [M1 design session](ImplementationRoadmap.md#design-session-log), same effect): +50% damage taken from Attacks. Decrements by 1 at the start of the afflicted's turn.
- **Drained** (debuff, formerly "Weak"): -25% damage dealt. Decrements by 1 at the start of the afflicted's turn.
- **Sundered** (debuff, formerly "Frail"): -25% Block gained from Skills. Decrements by 1 at the start of the afflicted's turn.
- **Cleanse**: removes a stated number (or "all") of the afflicted's debuffs — Exposed/Drained/Sundered and similar status effects. Does **not** remove Curse cards from the deck; that's a separate system (card removal, sold at the Shop — §7). Universal names used regardless of which mythology's card or enemy applies them — only mythology-flavored card *text* varies, not the underlying status name (e.g., a future Burn/Shock-style debuff tied to specific enemy types would be a genuinely new, additional status, not a reskin of these three).

**Deck-level mechanics**:
- **Exhaust**: when a card marked Exhaust is played, it's removed from the deck for the remainder of the current combat (returns for the next one).
- **Curse**: a card added to the deck (currently only via the Minor Enhancement Defiant Offering, §5.2) that cannot be played — pure dead weight until removed. Removable at the Shop (§7).

- **[OPEN]**: whether any Blessing or relic should ever interact directly with Curse cards beyond removing them (e.g., a card that benefits from having Curses in hand) — not needed by anything designed so far, flagging as a possible future design space rather than deciding now.

## 11. Open Questions Summary

Quick index of everything flagged **[OPEN]** above, for whenever design attention returns to each:
- Relic count per mythology + boss-exclusive relic count
- Whether a potion-equivalent consumable exists
- Numeric tuning for the 4 keyword mechanics (Volley discharge ratio, Storm conversion rate, Scale thresholds, Form energy costs) — rule shapes are settled, numbers aren't
- Player Enhancement exact cadence (every N combats — N not yet chosen)
- Minor Enhancement exact Essence costs and Defiant Offering's success odds — rule shape (3-tier Offering system) is settled, numbers aren't
- Shop pricing per rarity/type, and the guaranteed-relic premium multiplier (Section 7) — rule shape settled, numbers aren't
- Passive tree: all 4 trees built (Section 8) — remaining work is real Mythos cost tuning and node-effect balancing (current numbers are placeholders), plus deciding whether trunk nodes should have any prerequisite relationship to branches (currently independent)
- Co-op HP model, and everything else in Section 9
- Anti-repeat weighting exact values (shared open question for both trial mythology §5.2 and next-stage mythology §5.3)
- Prophecy family bonus numbers (tuning), and whether more than 2 variants per mythology are needed before launch
- "The Bazaar" (§7) — parked future idea, not committed scope. All 4 mythology figures now identified (Hermes/Njörðr/Cai Shen/Min).
- Attack/Skill/Power ratio check across all 4 Blessings side by side — Wukong's 33% Attack share is a bigger deviation from the ~40% genre norm than Anubis's, worth a direct comparison pass once all 4 exist (they do now) rather than judging each in isolation.
- Whether any Blessing or relic should ever interact directly with Curse cards beyond removing them (§10) — not needed by anything designed so far.
- Formalize the enemy intent system (telegraphing next action before the player's turn) as part of Core Combat Vocabulary (§10) — currently only stated inline in [MinimalSampleSet-Greek.md](Enemies/MinimalSampleSet-Greek.md), same situation vocabulary itself was in before that section existed.
- Full enemy/boss design for all 4 mythologies, Hook 2 integration (enemies using mythology-flavored resource systems), and Core Combat Vocabulary's actual redesign away from StS's named keywords — all deliberately deferred until after the minimal sample set proves out in real play.

Resolved this pass: card scale (75/character + ~70 colorless), no-rarity relics confirmed as current design (not permanently ruled out), Player Enhancement trigger type (combat-count), all 4 keyword mechanics formalized into rule shapes, Minor Enhancement risk mechanic (3-tier Cautious/Bold/Defiant Offering system), trial mythology randomized independent of stage, next-stage mythology selection (default random + extensible "Prophecy" family of Major Enhancements, 2 variants per mythology so far, each with a unique minor bonus on top of a shared reveal-2-pick-1 core), merchant/shop node confirmed to exist and designed (Section 7), **all 4 Blessings taken to full 75-card depth** (Artemis, Thor, Anubis, Sun Wukong), safety-net Rare pattern ruling (stays a card, not a relic; not forced onto Artemis), Sun Wukong's Beast Form × multi-hit ruling (double-hit applies to single-hit Attacks only), **Core Combat Vocabulary formalized as its own section** (Section 10), **passive tree rule shape** (Mythos currency, one tree per Blessing built on existing archetype paths, depth-over-access node philosophy, Hades-style respec-able choice pairs, Mythic card tier), **all 4 Blessings' passive trees built** (17 nodes each), confirming "Mythic capstones can deliberately bend a Blessing's core rule" as a recurring, intentional cross-tree design principle, **enemies confirmed tied to stage mythology** (not decoupled like trials), and a **minimal Greek enemy sample set built** to unblock a first playable slice.
