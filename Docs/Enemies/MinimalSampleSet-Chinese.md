# Minimal Enemy Sample Set — Chinese

Same purpose and constraints as [MinimalSampleSet-Greek.md](MinimalSampleSet-Greek.md), [MinimalSampleSet-Norse.md](MinimalSampleSet-Norse.md), and [MinimalSampleSet-Egyptian.md](MinimalSampleSet-Egyptian.md): not a real roster, just enough to unblock a first playable Wukong slice and validate combat against Form's actual kit. Paired with Chinese since it matches Sun Wukong, the fourth Blessing taken through this same playable-slice process.

Mirrors the other three sets' structure and power level directly (same HP tiers, same intent-count shapes) so all four read as siblings.

**Debuff variety is exhausted, so this set pivots to Buff instead.** Greek/Norse/Egyptian already cover all 3 universal debuffs (Drained/Sundered/Exposed respectively — M1's naming session only ever locked those 3, and inventing a 4th is explicitly out of scope until real enemy design calls for it, per the M1 log). Rather than repeat one of the 3 existing debuffs, this set's second enemy applies **Strength to itself** instead — an `IntentType.Buff` enemy, which none of the 3 prior sets have ever actually exercised (all three "second enemy" slots used `Debuff`; `Buff` exists in the enum but has zero live coverage before this). Reframes the "no repeat" pattern from strict debuff-variety into full intent-variety: this set is the first to touch all 4 `IntentType` values across its own roster (basic attacker covers Attack + Block, buffer covers Buff, pack covers Attack again) rather than needing a debuff repeat.

**Same standing rulings as the other three sets apply**: enemies tied to stage mythology, the intent-reveal system assumed, Training Dummy shared as-is.

## Training Dummy

Reused unchanged from [MinimalSampleSet-Greek.md](MinimalSampleSet-Greek.md#training-dummy-qa-tool-not-real-content).

## Heavenly Soldier (basic single attacker)

Chinese counterpart to Hoplite Skirmisher / Draugr Reaver / Ushabti Sentinel — same "read the intent, decide to attack or defend" role. Part of the Celestial army from Journey to the West, initially sent to capture/subdue Wukong.

| | |
|---|---|
| HP | 42 |
| Intent A (common) | Attack — deal 9 damage. |
| Intent B (occasional) | Guard — gain 8 Block. |

## Nine-Tailed Fox Spirit (self-buff applier)

Chinese counterpart to the other sets' debuff-applier slot, reframed as a **Buff** intent (see the note above). A classic shapeshifting trickster spirit from Chinese folklore, gathering its own power rather than afflicting the player.

| | |
|---|---|
| HP | 27 |
| Intent A | Gather Power — gain 2 Strength. No damage. |
| Intent B | Attack — deal 6 damage. |

## Yaksha Swarm (pack unit — spawns 2-3 per encounter)

Chinese counterpart to Viper Brood / Wolf Pack / Scarab Swarm — same role (multi-target decision-making, racing down small HP pools). Yaksha are minor nature-spirit/demon servants across Buddhist and Chinese mythology.

| | |
|---|---|
| HP (each) | 12 |
| Intent | Strike — deal 5 damage. (Single intent, no variation — same reasoning as the other three sets: the complexity is the pack count, not per-unit behavior.) |

## Sanity Checks

**Covers the same core intent patterns as the other three sets**, at matching power level (Heavenly Soldier ≈ Hoplite Skirmisher ≈ Draugr Reaver ≈ Ushabti Sentinel; Nine-Tailed Fox Spirit ≈ the other sets' debuffer slot, power-equivalent; Yaksha Swarm ≈ Viper Brood ≈ Wolf Pack ≈ Scarab Swarm) — enough to exercise Form's stance-based read (Beast Form's double-hit passive punishes/rewards differently against a Strength-stacking enemy than a flat attacker) without needing a full roster.

**Deliberately not included**: a boss, and anything that reads or manipulates Form directly — same Hook 2 deferral as the other three sets.

**Not attempted**: numeric tuning against Wukong's actual kit — placeholder numbers sized to match the other three sets' already-placeholder scale, not independently validated.
