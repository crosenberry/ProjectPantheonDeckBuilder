# Minimal Enemy Sample Set — Norse

Same purpose and constraints as [MinimalSampleSet-Greek.md](MinimalSampleSet-Greek.md): not a real roster, just enough to unblock a first playable Thor slice and validate combat against Storm's actual kit. Paired with Norse since it matches Thor, the second Blessing taken through this same playable-slice process.

Mirrors the Greek set's structure and power level directly (same HP tiers, same intent-count shapes) so the two sets read as siblings, not a redesign — the one deliberate point of variety is using **Sundered** where Greek used **Drained** for its debuffer, so the two minimal sets aren't a pure reskin of each other.

**Same standing rulings as the Greek set apply**: enemies tied to stage mythology (not randomized independently), the intent-reveal system assumed, Training Dummy shared as-is (a QA tool, not mythology-flavored, so it isn't redrafted here).

## Training Dummy

Reused unchanged from [MinimalSampleSet-Greek.md](MinimalSampleSet-Greek.md#training-dummy-qa-tool-not-real-content) — not mythology-flavored, no Norse variant needed.

## Draugr Reaver (basic single attacker)

Norse counterpart to Hoplite Skirmisher — same "read the intent, decide to attack or defend" role, an undead warrior risen to guard its hoard.

| | |
|---|---|
| HP | 44 |
| Intent A (common) | Attack — deal 10 damage. |
| Intent B (occasional) | Guard — gain 7 Block. |

## Seidr Hexer (debuff applier)

Norse counterpart to Harpy Screecher. A practitioner of *seiðr* (Norse witchcraft/prophecy) — uses **Sundered** rather than Harpy's Drained, so the two mythologies' minimal sets don't apply the identical debuff.

| | |
|---|---|
| HP | 28 |
| Intent A | Hex — apply 2 Sundered to the player. No damage. |
| Intent B | Claw Strike — deal 6 damage. |

## Wolf Pack (pack unit — spawns 2-3 per encounter)

Norse counterpart to Viper Brood — same role (multi-target decision-making, racing down small HP pools), reskinned as Fenrir's kin rather than serpents.

| | |
|---|---|
| HP (each) | 12 |
| Intent | Bite — deal 5 damage. (Single intent, no variation — same reasoning as Viper Brood: the complexity is the pack count, not per-unit behavior.) |

## Sanity Checks

**Covers the same core intent patterns as the Greek set**, at matching power level (Draugr Reaver ≈ Hoplite Skirmisher, Seidr Hexer ≈ Harpy Screecher, Wolf Pack ≈ Viper Brood) — enough to exercise Storm's banked-then-discharged shape (Bulwark's turtle-then-release needs a real attacker to punish holding Block too long; Chain's per-turn discharges need a target worth chipping at every turn) without needing a full roster.

**Deliberately not included**: a boss, and anything that reads or manipulates Storm directly — same Hook 2 deferral as the Greek set.

**Not attempted**: numeric tuning against Thor's actual kit — placeholder numbers sized to match the Greek set's already-placeholder scale, not independently validated.
