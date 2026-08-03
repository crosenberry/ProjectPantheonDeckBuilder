# Artemis — Card Draft (Volley Pressure Test)

Purpose: draft a real slice of Artemis's 75-card pool, with actual numbers, to check whether the Volley mechanic ([GameDesignDocument.md §3.1](../GameDesignDocument.md)) supports multiple distinct builds rather than one dominant line. Not a complete card list — a sample big enough to reason about.

**Assumptions carried over from Slay the Spire conventions**, not yet stated elsewhere in the GDD:
- Standard turn energy: 3 per turn.
- Cards (unlike relics) use StS-style **Common / Uncommon / Rare** tiers. This is separate from the relic no-rarity decision in §4 — that ruling was about items/relics specifically, not cards. Flagging this as a default in need of confirmation.

Recap of the rule being tested: **Volley** is a per-turn counter, resets to 0 each turn. Cards tagged **Shot** raise it by 1 when played. Payoff cards read or spend it. Per §3.1, Volley is deliberately short-fuse (within-turn only) — that's what separates it from Thor's Storm (banked across turns).

## Starter Deck (10 cards)

| Qty | Name | Type | Cost | Effect |
|---|---|---|---|---|
| 5 | Quick Shot | Attack, Shot | 1 | Deal 6 damage. |
| 4 | Side Step | Skill | 1 | Gain 5 Block. |
| 1 | Hunter's Mark | Attack, Shot | 2 | Deal 8 damage. Apply 2 Vulnerable. |

## Path A — Barrage (scale within the turn, dump-as-you-go)

Identity: each Shot gets stronger the more you've already fired this turn. No single finisher — the payoff is spread across the whole hand.

| Name | Type | Cost | Effect |
|---|---|---|---|
| Loose Arrow | Attack, Shot (Common) | 0 | Deal 3 damage. |
| Flurry | Attack, Shot (Common) | 1 | Deal 4 damage. Deal 2 additional damage for each Shot played earlier this turn. |
| Quickdraw | Skill (Uncommon) | 1 | Draw 1 card. Shot cards cost 1 less Energy this turn (min 0). |
| Practiced Hand | Power (Uncommon) | 1 | At the start of your turn, gain 1 Volley. |
| Rain of Arrows | Attack, Shot, hits ALL enemies (Rare) | 2 | Deal 3 damage. Deal 3 additional damage for each point of current Volley. |

## Path B — Full Draw (load, then discharge same turn)

Identity: cheap generators that do little on their own, feeding one finisher that consumes everything. Classic combo-piece tension — risk of drawing the finisher before the generators.

| Name | Type | Cost | Effect |
|---|---|---|---|
| Nock | Skill (Common) | 0 | Gain 1 Volley. |
| Warning Shot | Attack, Shot (Common) | 1 | Deal 4 damage. Gain 1 additional Volley. |
| Steady Aim | Skill (Uncommon) | 1 | Gain 2 Volley. Gain 4 Block. |
| Called Shot | Attack, Shot (Uncommon) | 2 | Deal 6 damage. If Volley is 4 or higher, this hits twice. |
| Full Draw | Attack (Rare) | 2 | Consume all Volley (min 1). Deal 5 damage for each point consumed. |

## Path C — Huntress (crit/single-target, Volley-light)

Identity: mostly ignores the stacking resource, leans on raw hit size, Vulnerable synergy (off the starter card), and execute-style payoffs. Exists to confirm a player can build Artemis while barely touching her signature mechanic.

| Name | Type | Cost | Effect |
|---|---|---|---|
| Precise Shot | Attack, Shot (Common) | 1 | Deal 9 damage. |
| Pathfinder | Skill (Common) | 1 | Draw 2 cards, then discard 1 non-Shot card. |
| Point-Blank | Attack, Shot (Uncommon) | 1 | Deal 7 damage. If the enemy is Vulnerable, apply 1 Weak. |
| Predator's Focus | Power (Uncommon) | 2 | Whenever you end your turn with 0 Volley, gain 2 Strength. |
| Apex Predator | Power (Rare) | 3 | Your Shot attacks deal double damage against enemies below 50% HP. |

Note: **Predator's Focus** deliberately straddles Barrage and Huntress (rewards fully dumping Volley, which Barrage does naturally) — a cross-path synergy card rather than a hard-locked lane. Worth doing more of these once the full 75 gets drafted, so paths blend instead of acting like rigid MTG-style color lanes.

## Pressure-Test Findings

- **Path A risk**: Rain of Arrows scaling off current Volley, combined with 0-cost Loose Arrow and card draw, is the classic "stacking counter + draw fuel" shape (same pattern as StS Silent's poison or Watcher's stance-combo decks). Expect it to get strong with support; that's normal for the archetype and a tuning problem, not a rule problem.
- **Path B risk**: order-dependency — drawing Full Draw before any generator wastes the turn. This is inherent to combo-piece design (same issue StS Defect's orb setups have) and is normally solved by adding tutor/dig effects later in the 75-card pool, not by changing the resource rule.
- **Path C validates the pillar**: it's a fully playable Artemis build that barely interacts with Volley at all, leaning on Vulnerable + raw damage instead. Confirms "not one set-in-stone path to glory" holds even within a single Blessing's pool, not just across the 4 Blessings.
- No card here breaks the "resets every turn" rule from §3.1 — all three paths operate entirely within a single turn's Volley count, which keeps Artemis's design space distinct from Thor's cross-turn Storm banking.

**Open follow-ups**: exact numeric tuning needs playtesting once more of the 75 exist; need a handful of explicit anti-synergy or archetype-defining Rares beyond the 3 above; starter Hunter's Mark's Vulnerable-application hook is currently only paid off by one Uncommon (Point-Blank) — may want at least one more Vulnerable-synergy card so that hook isn't a starter-deck orphan.
