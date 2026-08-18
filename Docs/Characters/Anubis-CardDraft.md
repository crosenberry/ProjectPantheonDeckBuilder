# Anubis — Curated Card Slice (M6 Playable Draft)

Purpose: same role [Artemis-CardDraft.md](Artemis-CardDraft.md) and [Thor-CardDraft.md](Thor-CardDraft.md) played for their milestones — not a new pressure test (that already happened at full 75-card depth in [Anubis-FullCardDraft.md](Anubis-FullCardDraft.md)), just curating a real slice of the already-drafted pool big enough to implement and prove Balance-keeper/Reaper/Ascendant all play distinctly, mirroring the "10 starter + 5 per path" shape exactly.

Recap of the rule being tested: **Scale** is a signed value (-5 to +5), resetting to 0 each combat but persisting turn-to-turn within one (same timing as Storm — the GDD doesn't call out a per-turn reset the way Volley has one, and nothing about "swinging two directions" implies a timing difference, just a directional one). Cards push it toward Order (+) or Chaos (−) as a side effect. Payoff cards read its *position* — magnitude, sign, or closeness to 0 — never a stack count.

## Scope decision: "choose one" cards deferred

Three cards in the full draft use a "Choose one: Scale +1 or Scale -1" shape (Scales of Judgment, Twin Rites, Harmonize) — a genuinely new mechanic (an in-card player choice) that nothing in Artemis's or Thor's kits ever needed. Confirmed via direct question to the user: deferred for this slice, same "don't force new architecture into a content slice" reasoning as Thor's Chain Bolt/Full Discharge swap. Twin Rites and Harmonize simply don't make the curated 25. **Scales of Judgment** (the starter deck's signature card) is reworded below to a fixed direction rather than dropped, since a starter card needs to exist — losing its original "teaches Scale is bidirectional from turn one" framing is the real cost of this deferral, worth being honest about rather than glossing over.

## Starter Deck (10 cards)

| Qty | Name | Type | Cost | Effect |
|---|---|---|---|---|
| 5 | Jackal's Bite | Attack | 1 | Deal 6 damage. |
| 4 | Canopic Ward | Skill | 1 | Gain 5 Block. |
| 1 | Scales of Judgment | Attack | 2 | Deal 8 damage. Scale +1. *(Reworded from "Choose one: Scale +1 or Scale -1" — see scope decision above.)* |

## Path A — Balance-keeper (hover near 0)

Identity: rewards staying close to Scale 0 rather than pushing either extreme — a genuinely different payoff shape from Reaper/Ascendant's "more extreme = more reward," not just "the path that ignores the mechanic" (per the full draft's own finding about Judgment Incarnate).

| Name | Type | Cost | Effect |
|---|---|---|---|
| Even Keel | Skill (Common) | 1 | Draw 1 card. If Scale is between -1 and +1, draw 1 additional card. |
| Ma'at's Feather | Attack (Common) | 1 | Deal 4 damage. If Scale is between -1 and +1, apply 1 Exposed. |
| Equilibrium | Power (Uncommon) | 1 | At the end of your turn, if Scale is between -1 and +1, gain 3 Block. |
| Scale-Tipper | Skill (Uncommon) | 1 | Set Scale to 0. Draw 2 cards. |
| Judgment Incarnate | Attack (Rare) | 3 | Deal damage equal to 4 × (5 − \|Scale\|) to one enemy. |

## Path B — Reaper (push toward Chaos/−)

Identity: leans into negative Scale for bigger, riskier payoffs — self-damage and enemy debuffs as the cost of power.

| Name | Type | Cost | Effect |
|---|---|---|---|
| Ammit's Hunger | Attack (Common) | 1 | Deal 7 damage. Scale −1. |
| Chaos Rite | Skill (Common) | 0 | Lose 2 HP. Scale −2. |
| Serpent's Bite | Attack (Common) | 1 | Deal 5 damage. If Scale is negative, deal 3 additional damage. |
| Chaosbound Strike | Attack (Uncommon) | 1 | Deal 6 damage. If Scale is −3 or lower, deal 6 additional damage. |
| Devourer's Toll | Attack (Rare) | 2 | Deal damage equal to 3 × \|Scale\| to one enemy. If Scale is negative, also apply Exposed equal to \|Scale\|. |

## Path C — Ascendant (push toward Order/+)

Identity: leans into positive Scale for sustain and defense — healing, Block, and cleanse as the reward for staying disciplined.

| Name | Type | Cost | Effect |
|---|---|---|---|
| Ma'at's Shield | Skill (Common) | 1 | Gain 7 Block. Scale +1. |
| Sacred Rite | Skill (Common) | 1 | Heal 4 HP. Scale +1. |
| Sunbound Ward | Skill (Uncommon) | 1 | Gain 6 Block. If Scale is positive, gain 3 additional Block. |
| Osirian Renewal | Skill (Uncommon) | 2 | Heal 8 HP. Scale +3. |
| Ma'at's Ascension | Attack (Rare) | 3 | If Scale is positive, set it to 0: deal 5 damage per point consumed to ALL enemies. |

## Curation Notes

- **Effect-shape reuse vs. new work**: Ammit's Hunger, Chaos Rite (reuses `LoseHPEffect` from Thor's slice), Ma'at's Shield map onto trivial composition of existing effects plus one new primitive (`AdjustScaleEffect`, signed and clamped to [-5, 5] — Scale is the first bounded resource, unlike Volley/Storm). Sacred Rite/Osirian Renewal need a new `HealEffect` wrapping the `Player.Heal` method added back in the Rest Site slice, which never had a card-level wrapper since no card healed before now. Everything else needs a genuinely new effect class reading one of Scale's three query shapes (magnitude, sign, range) the full draft's own sanity checks flagged as more complex than Volley/Storm's simple "how much do I have." Exact class design is implementation-level, worked out via TDD, not pre-decided here.
- **No duplicate-card bug this time**: the full draft's own sanity checks explicitly checked for Thor's kind of accidental duplicate (Book of the Dead vs. Duat's Gaze) and found the pool clean.
- **Why these 5 per path**: each path's 5 cards span its cost curve and include a clear "finisher" (Judgment Incarnate / Devourer's Toll / Ma'at's Ascension) — same shape as Artemis's and Thor's curated slices.
