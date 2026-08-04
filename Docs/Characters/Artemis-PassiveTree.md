# Artemis — Passive Tree (Mythos, first full example)

First concrete build-out of the passive tree rule shape from [GameDesignDocument.md §8](../GameDesignDocument.md), same role the original Volley card sample played for the in-run kit: prove the shape works with real content before treating it as a template for the other 3 Blessings. Numbers below are placeholders (illustrative, round) — nothing here is tuned.

Recap of the rules this has to satisfy: one tree per Blessing, hung off that Blessing's 3 existing archetype paths (Barrage / Full Draw / Huntress — see [Artemis-CardDraft.md](Artemis-CardDraft.md)), a small universal trunk, depth-over-access node design (never touch reward-pool odds, only reward in-run commitment), choice-pair nodes that respec freely between runs, and a Mythic-tier capstone per branch that differs from existing Rares in *kind*, not just magnitude.

## Trunk (5 nodes, universal, permanent buy-and-keep, no respec needed)

Independent of the 3 branches — can be bought in any order relative to them, but nodes within the trunk unlock sequentially (each requires the previous).

| Node | Cost (Mythos) | Effect |
|---|---|---|
| Huntress's Vigor | 40 | +5 Max HP, every run. |
| Favor of the Hunt | 50 | Start each run with +25 Divine Essence. |
| Steady Quiver | 60 | Start each run with 1 additional copy of Quick Shot in your deck. |
| Hunter's Instinct | 70 | In your first combat each run, enemy intents are revealed one turn earlier than normal. |
| Well-Aimed Start | 80 | Your first godly trial encountered each run costs 20% less Divine Essence. |

## Branch: Barrage (scale within the turn, dump-as-you-go)

Linear chain — each node requires the previous. Recap of the archetype: Shot cards raise Volley, payoffs read *current* Volley without spending it, rewarding chaining many Shots in one turn.

| Node | Cost | Effect |
|---|---|---|
| Nimble Draw | 60 | If your deck has 3+ Shot cards, your opening hand each combat always includes at least 1 Shot card. |
| Momentum's Edge (choice pair) | 90 | Pick one, freely toggle between runs: **(A)** Flurry-type cards (Shots that scale off Shots played earlier this turn) deal +1 additional damage per prior Shot. **(B)** The first Shot you play each turn costs 0 Energy. |
| Overdraw Mastery | 110 | If you play 4 or more Shot cards in a single turn, gain 1 Strength for the rest of combat. |
| **Hail of a Thousand Arrows** (Mythic capstone) | 200 | New card unlocked into the Barrage card pool: *Attack, Shot, 3 Energy — Deal 4 damage to ALL enemies. If this is the 5th or later Shot played this turn, return this card to your hand and reduce its cost to 0 for the rest of this turn.* |

Momentum's Edge is a real choice between two different flavors of "more Barrage" — flat damage scaling (A) vs. tempo/enabler (B) — not a strictly-better/worse pair, so which side is active actually matters.

Hail of a Thousand Arrows differs from Rain of Arrows (the base pool's Barrage Rare) in *kind*, not just number: Rain of Arrows reads current Volley once; this card can chain and re-trigger within a single extreme-commitment turn, which nothing in the base 75 does. That's the "reward a level of commitment no in-run Rare rewards" design rule from GDD §8 in practice.

## Branch: Full Draw (load, then discharge same turn)

| Node | Cost | Effect |
|---|---|---|
| Practiced Patience | 60 | If you end a turn without playing an Attack, gain 1 Volley at the start of your next turn (in addition to normal generation). |
| Momentum's Reserve (choice pair) | 90 | Pick one, freely toggle between runs: **(A)** Discharge cards that consume all Volley (like Full Draw) deal +1 damage per point consumed. **(B)** Once per combat, the next time you'd consume all Volley, consume it twice instead. |
| Loaded Quiver | 110 | If you have 4 or more Volley at any point during your turn, draw 1 card. |
| **The Unbroken Nock** (Mythic capstone) | 200 | New card unlocked into the Full Draw card pool: *Attack, 2 Energy — Consume all Volley (minimum 1). Deal 5 damage per point consumed. Gain Strength equal to the amount consumed, for the rest of combat.* |

Practiced Patience is a deliberate, flagged exception to Volley's own rule (§3.1: resets every turn, no exceptions in the base kit) — a small crack in that rule reserved specifically for deep meta-investment in the one path that actually wants banking behavior. Worth keeping as a documented technique: the passive tree is allowed to bend a Blessing's core resource rule slightly for a specific committed path, something no in-run card is allowed to do, which is itself a point of differentiation for what the tree is *for*.

The Unbroken Nock differs in kind from base-pool Full Draw (which is pure one-turn burst) by converting discharge into a *persistent* buff (Strength lasts the rest of combat, not just one hit) — rewards sustained investment across a whole fight rather than a single big turn.

## Branch: Huntress (crit/single-target, Volley-light)

| Node | Cost | Effect |
|---|---|---|
| Marked for the Hunt | 60 | The first enemy you damage each combat is marked: it takes +2 damage from all your Attacks for the rest of combat. |
| Hunter's Reflex (choice pair) | 90 | Pick one, freely toggle between runs: **(A)** Apex Predator-type effects (bonus damage vs. low-HP enemies) trigger at 60% HP instead of 50%. **(B)** Whenever you apply Vulnerable, also apply 1 Weak. |
| Silent Step | 110 | The first Attack you play against a full-HP enemy each combat deals +3 damage. |
| **One Perfect Shot** (Mythic capstone) | 200 | New card unlocked into the Huntress card pool: *Attack, Shot, 2 Energy — Deal damage equal to 50% of the target's current HP (minimum 10, maximum 40).* |

Silent Step (opening burst vs. full-HP targets) and Marked for the Hunt / Hunter's Reflex Option A (execute vs. low-HP targets) deliberately give Huntress two payoff windows at opposite ends of a fight, not just one — matches its established identity as the path that's least about the Volley resource and most about raw target-state reads.

One Perfect Shot differs in kind from anything in the base 75: it's the only percentage-of-current-HP damage source in Artemis's entire kit (everything else is flat or Volley-scaled), capped both directions so it's neither useless against full-HP bosses nor a trivializing one-shot against anything already low.

## Sanity Checks

**Node count**: 5 trunk + (4 × 3 branches) = 17 nodes total. Close to the original ~20-node estimate from GDD §8's structural proposal — reasonable for a curated tree, not a sprawling one.

**Depth-over-access check**: confirmed no node in this tree touches what appears in a reward pool, chest, or shop — every effect either modifies the player's own deck contents/behavior directly (Nimble Draw, Steady Quiver) or only activates on in-run commitment (Overdraw Mastery, Loaded Quiver, Marked for the Hunt). The philosophy holds at real-content scale, not just as a stated rule.

**Choice-pair check**: all 3 pairs (Momentum's Edge, Momentum's Reserve, Hunter's Reflex) offer genuinely different playstyles on each side rather than a obviously-correct option, which is what makes the free-respec model matter — if one side were strictly better there'd be nothing to toggle back and forth for.

**Not attempted**: numeric tuning (all costs and effect magnitudes are placeholders), and no decision yet on whether trunk nodes should have any prerequisite relationship to the branches (currently independent).
