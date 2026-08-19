# Sun Wukong — Curated Card Slice (M7 Playable Draft)

Purpose: same role [Artemis-CardDraft.md](Artemis-CardDraft.md), [Thor-CardDraft.md](Thor-CardDraft.md), and [Anubis-CardDraft.md](Anubis-CardDraft.md) played for their milestones — curating a real slice of the already-drafted 75-card pool ([SunWukong-FullCardDraft.md](SunWukong-FullCardDraft.md)) big enough to implement and prove Beast Rush/Immortal Ascension/72 Changes all play distinctly, mirroring the "10 starter + 5 per path" shape exactly.

Recap of the rule being tested: **Form** is a 3-state stance (Mortal/Beast/Immortal), purely categorical, no magnitude. Beast: Attacks that normally hit once instead hit twice (multi-hit cards unaffected); Skills cost 1 additional Energy. Immortal: Strength and Block gained from cards are increased by 1; Attacks deal 3 less damage (minimum 1). Mortal: no modifier.

## Scope decision: Form-changing resolves as a deterministic cycle

Confirmed via direct question to the user: **"Change Form" always advances a fixed order — Mortal → Beast → Immortal → Mortal.** No player choice needed, so almost the entire pool of "change to a different Form" cards stays buildable without new choice-UI architecture (same "don't force new architecture into a content slice" reasoning as Thor's Chain Bolt/Full Discharge swap and Anubis's choice-card deferral, but cheaper here since cycling covers nearly every card rather than forcing a reword-or-drop choice per card).

- **Ruyi Strike** (starter signature card, originally "Change to a Form of your choice") is reworded to "Change Form" — same cycle rule, near-lossless: it's still the card that teaches Form-switching from turn one, just resolved deterministically instead of via a picker.
- **Wandering Form** ("Change Form, any including current") is the one card in the full 75 that doesn't fit the cycle rule (staying in place isn't a "cycle" step) — it doesn't make the curated 25; **Ever-Changing Form** ("Change to a different Form than your current one. Draw 1 card.") fills the same slot instead, near-identical text already in the same Uncommon pool.

## Starter Deck (10 cards)

| Qty | Name | Type | Cost | Effect |
|---|---|---|---|---|
| 5 | Ape Fist | Attack | 1 | Deal 5 damage. |
| 4 | Cloud Step | Skill | 1 | Gain 5 Block. |
| 1 | Ruyi Strike | Attack | 2 | Deal 9 damage. Change Form. *(Reworded from "Change to a Form of your choice" — see scope decision above.)* |

## Path A — Beast Rush (commit to Beast, double-hit tempo)

| Name | Type | Cost | Effect |
|---|---|---|---|
| Beast Awakening | Skill (Common) | 1 | Change to Beast Form. Gain 3 Block. |
| Primal Roar | Skill (Common) | 1 | Gain 1 Strength. |
| Reckless Ape | Attack (Common) | 1 | Deal 5 damage. Take 1 damage. |
| Rampage | Attack (Uncommon) | 2 | Deal 8 damage. |
| Havoc in Heaven | Attack (Rare) | 3 | Deal 8 damage three times. |

Havoc in Heaven is a deliberate showcase of the multi-hit ruling from the full draft's own findings: it stays a 3-hit card regardless of Form, never becoming a 6-hit card in Beast Form — the curated slice proves that rule in play, not just on paper.

## Path B — Immortal Ascension (commit to Immortal, permanent-buff utility/sustain)

| Name | Type | Cost | Effect |
|---|---|---|---|
| Immortal Ascension | Skill (Common) | 1 | Change to Immortal Form. Gain 2 Strength. |
| Sacred Peach | Skill (Common) | 1 | Heal 4 HP. |
| Celestial Ward | Skill (Common) | 1 | Gain 6 Block. |
| Peach of Longevity | Skill (Uncommon) | 2 | Heal 10 HP. Gain 1 Strength. |
| Ascension of the Sage | Skill (Rare) | 3 | Change to Immortal Form. Gain 5 Strength. Exhaust. |

## Path C — 72 Changes (cycle every turn, the switching itself is the payoff)

| Name | Type | Cost | Effect |
|---|---|---|---|
| Shifting Stance | Skill (Common) | 0 | Change Form. |
| Fickle Strike | Attack (Common) | 1 | Deal 5 damage. Change Form. |
| Adaptive Guard | Skill (Common) | 1 | Gain 5 Block. If you changed Form this turn, gain 3 additional Block. |
| Whirling Transformation | Attack (Uncommon) | 2 | Deal 10 damage. Change Form. |
| 72 Changes | Skill (Rare) | 1 | Change Form. Draw 2 cards. |

## Curation Notes

- **No Power cards in this slice.** Nearly every Power in the full 75 (Berserker Form, Feral Instinct, Wild Momentum, Trickster's Gambit, Mirror Stance, Adaptive Master, Divine Protection, Jade Emperor's Favor...) triggers on an event the current `TriggerEvent` enum doesn't have yet (`CombatStarted`/`TurnStarted`/`TurnEnded` only — no "whenever you change Form," "whenever you deal damage," or "whenever you gain Block"). Same architecture gap that held back Syncretism's Aegis of the Hunt in M5. A couple of Powers (Celestial Mandate, Monkey King's Resolve) only need the *existing* `TurnStarted` trigger and are buildable, but were left out of this slice to keep it Power-free and simple, matching how the other 3 Blessings' curated slices leaned on simple compositions over trigger-heavy Powers. Good candidates for a later pass once more Powers are worth adding.
- **"Changed Form this turn" is new state.** Adaptive Guard needs a per-turn boolean, same shape as `ShotsPlayedThisTurn` — reset in `StartTurn`, set whenever `ChangeFormEffect` (or its cyclic equivalent) fires.
- **No new `CardTag` needed this milestone** — first time in 3 milestones this has happened (Thor added `StormGenerating`, Anubis added `Order`/`Chaos`). Nothing in this curated slice (or the full 75, on inspection) filters on "is this a Form-changing card" the way Pathfinder filters on `Shot`; Form-reading cards check the *current Form state*, not a card tag.
- **Why these 5 per path**: each path's 5 cards span its cost curve (0-3) and include a clear "finisher" (Havoc in Heaven / Ascension of the Sage / 72 Changes) — same shape as the other 3 Blessings' curated slices.
