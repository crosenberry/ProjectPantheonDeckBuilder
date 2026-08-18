# Thor — Curated Card Slice (M5 Playable Draft)

Purpose: same role [Artemis-CardDraft.md](Artemis-CardDraft.md) played for M2 — not a new pressure test (that already happened at full 75-card depth in [Thor-FullCardDraft.md](Thor-FullCardDraft.md)), just curating a real slice of the already-drafted pool big enough to implement and prove Bulwark/Chain/Unrelenting all play distinctly, mirroring M2's "10 starter + 5 per path" shape exactly.

Recap of the rule being tested: **Storm** persists across turns (unlike Volley, which resets each turn). Cards tagged **StormGenerating** grant it as a side effect (mirrors the **Shot** tag's role for Volley). Payoff cards consume banked Storm for burst effects.

## Starter Deck (10 cards)

Already specified in [Thor-FullCardDraft.md](Thor-FullCardDraft.md)'s Basic tier — carried over unchanged, not re-picked.

| Qty | Name | Type | Cost | Effect |
|---|---|---|---|---|
| 5 | Hammer Strike | Attack | 1 | Deal 6 damage. |
| 4 | Storm Ward | Skill, StormGenerating | 1 | Gain 5 Block. Gain 1 Storm. |
| 1 | Mjolnir's Call | Attack, StormGenerating | 2 | Deal 9 damage. Gain 2 Storm. |

## Path A — Bulwark (turtle, then release)

Identity: high Block-plus-Storm dumps that reward *not* attacking for a turn, then cashing the banked Storm in for a big hit — the rhythm Volley can never produce, since it punishes waiting.

| Name | Type | Cost | Effect |
|---|---|---|---|
| Shield Wall | Skill, StormGenerating (Common) | 1 | Gain 9 Block. Gain 2 Storm. |
| Deflect | Skill (Common) | 1 | Gain 5 Block. If you have 5 or more Storm, gain 8 Block instead. |
| Thunderous Retort | Attack (Uncommon) | 2 | Deal damage equal to your current Block. |
| Titan's Bulwark | Skill, StormGenerating (Uncommon) | 2 | Gain 15 Block. Gain 4 Storm. Exhaust. |
| Thunderclap | Attack, hits ALL enemies (Rare) | 2 | Consume all Storm. Deal 4 damage per point consumed. |

## Path B — Chain (small discharge every turn, tempo)

Identity: cheap Storm generation paired with small, repeatable consumption every turn rather than one big dump — favors a steady rhythm over Bulwark's boom-bust.

| Name | Type | Cost | Effect |
|---|---|---|---|
| Charged Strike | Attack, StormGenerating (Common) | 1 | Deal 6 damage. Gain 1 Storm. |
| Discharge Bolt | Attack (Common) | 1 | Consume 1 Storm: deal 9 damage. If you have no Storm, deal 4 damage instead. |
| Static Grip | Skill, StormGenerating (Common) | 1 | Gain 1 Storm. Draw 1 card. |
| Chain Bolt | Attack (Uncommon) | 1 | Deal 6 damage. Consume 1 Storm: deal 6 damage to a different random enemy. |
| Full Discharge | Attack, hits ALL enemies (Rare) | 2 | Consume all Storm. Deal 6 damage per point consumed, split randomly among all enemies. |

## Path C — Unrelenting (Storm-light, HP-for-power berserker)

Identity: mostly ignores Storm, trades HP directly for Strength and raw hit size — exists to confirm a player can build Thor while barely touching his signature mechanic, same role Huntress played for Artemis.

| Name | Type | Cost | Effect |
|---|---|---|---|
| Reckless Swing | Attack (Common) | 1 | Deal 10 damage. Take 3 damage. |
| Bloodrage | Skill (Common) | 1 | Gain 2 Strength. Lose 3 HP. |
| Headlong Charge | Attack (Common) | 2 | Deal 14 damage. |
| Feral Roar | Attack (Uncommon) | 2 | Deal 12 damage. Gain 2 Strength. |
| Ymir's Wrath | Attack (Rare) | 3 | Deal 20 damage. Take 5 damage. |

## Curation Notes

- **Fixes the near-duplicate flagged in Thor-FullCardDraft.md**: Norn's Insight and Ravens' Sight were identical "look at top 3, discard any" Skills at Common and Uncommon respectively. Neither made this curated slice (both are Flex glue cards, not archetype-defining), but the underlying duplicate is fixed at the source — see the amendment in Thor-FullCardDraft.md.
- **Effect-shape reuse vs. new work**: several picks (Shield Wall, Titan's Bulwark, Charged Strike, Feral Roar, Headlong Charge, Reckless Swing, Bloodrage, Ymir's Wrath) map directly onto composition of existing `CardEffect` types (damage, block, status, self-damage) or trivial variants of them. A handful (Deflect's Storm-threshold Block, Thunderous Retort's read-current-Block, Discharge Bolt's consume-with-fallback, Chain Bolt's random-enemy targeting, Thunderclap/Full Discharge's consume-all-to-all-enemies) need new effect classes — same shape of work Artemis's slice needed for `ConditionalDoubleHitDamageEffect`/`ConsumeVolleyDamageEffect`/etc. Exact class design is implementation-level, worked out via TDD in `feature/thor-card-slice`, not pre-decided here.
- **Why these 5 per path over other candidates**: each path's 5 cards span its full cost curve (0-3) and include at least one Common, one Rare, and one card that reads as a clear archetype "finisher" (Thunderclap, Full Discharge, Ymir's Wrath) — mirrors Artemis's path shape (Rain of Arrows / Full Draw / Apex Predator each read as their path's capstone).
