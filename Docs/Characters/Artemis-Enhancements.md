# Artemis — Enhancements (Player + Minor examples)

First Enhancement content drafted for any Blessing, per [GameDesignDocument.md §5](../GameDesignDocument.md). Covers Player Enhancements (a real 4-option pool a 3-choice screen draws from) and Minor Enhancement upgrades (worked examples for 3 cards, proving the Cautious/Bold/Defiant shape rather than a complete upgrade table for all 25 cards). Numbers are placeholders. Major Enhancements are not Blessing-specific (the Prophecy family in GDD §5.3 already covers all 4 mythologies) so nothing new is needed here for those.

## Player Enhancements (4 drafted, screen shows 3 of them)

Legendary-tier, combat-count-triggered (GDD §5.1). All 4 read as genuinely different builds, not variations on one idea, matching the pillar validated back at the card-draft stage (build diversity within one Blessing).

| Name | Effect |
|---|---|
| Twin Moons | Draw 1 additional card at the start of each turn. |
| Endless Quiver | Your first Shot card each turn costs 0 Energy. |
| Apex Predator's Mantle | At the start of your turn, any enemy below 25% HP becomes Exposed (2 stacks). |
| Moonlit Momentum | At the start of each combat, gain Volley equal to half your max Energy (rounded down). |

- **Twin Moons**: raw card-advantage, archetype-agnostic — the "safe, always good" pick.
- **Endless Quiver**: reads like a stronger, permanent version of the passive tree's Momentum's Edge (option B) — deliberately overlapping territory is fine at this tier since Player Enhancements are rarer and meant to feel more decisive than a tree node.
- **Apex Predator's Mantle**: an automatic, always-on version of the Huntress path's execute theme (Apex Predator card, Marked for the Hunt node) — rewards a player already leaning Huntress without requiring it.
- **Moonlit Momentum**: a Barrage/Full Draw enabler — guarantees a non-zero Volley floor every fight, which neither archetype's base kit does on its own.

## Minor Enhancement upgrade examples (3 of 25 cards)

Confirms the Cautious/Bold/Defiant shape from GDD §5.2 actually produces 3 meaningfully different outcomes per card, not just "+1 damage, +2 damage, +3 damage" scaling. The other 22 cards' upgrade tables are explicitly **not** drafted here — that's a content-authoring pass for later, not a rule-shape question.

| Card | Cautious | Bold | Defiant |
|---|---|---|---|
| Quick Shot (deal 6 dmg) | Deal 7 damage. | Deal 8 damage. | Deal 8 damage. Gains the Shot tag's normal behavior at 0 Energy cost. |
| Side Step (gain 5 Block) | Gain 6 Block. | Gain 7 Block. | Gain 7 Block. Also gain 1 Volley. |
| Hunter's Mark (deal 8 dmg, apply 2 Exposed) | Deal 8 damage, apply 3 Exposed. | Deal 9 damage, apply 3 Exposed. | Deal 9 damage, apply 4 Exposed. |

Deliberate pattern: **Cautious** is a small, single-stat bump (matches "minor" in GDD's own language); **Bold** pushes that same stat further; **Defiant** matches Bold's headline number but adds a second, qualitatively different bonus (a cost reduction, a resource grant, an extra debuff stack) rather than just another flat increment — so a Defiant success feels like it bought something Bold couldn't, not just "Bold plus a bit," which is what makes gambling on it (versus the Curse risk) a real decision instead of a strictly-better-if-you're-lucky button.

## Sanity checks

**Player Enhancement count**: 4 drafted for a 3-option screen — intentionally more than 3 so the screen has real variance run to run, matching how card/relic reward screens already work elsewhere in the design.

**Not attempted**: numeric tuning, upgrade tables for the remaining 22 cards, and exact combat-count cadence for the Player Enhancement trigger (still an open GDD §5.1 item, not resolved here).
