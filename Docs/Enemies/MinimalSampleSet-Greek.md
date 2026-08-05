# Minimal Enemy Sample Set — Greek

Purpose: not a real enemy roster — just enough content to unblock a first playable Unity slice and validate the core combat loop, per the plan in [GameDesignDocument.md](../GameDesignDocument.md). Paired with Greek since it matches Artemis, the most fully-proven Blessing so far ([card draft](../Characters/Artemis-CardDraft.md), [passive tree](../Characters/Artemis-PassiveTree.md)).

**Ruling this set assumes**: enemies are tied to stage mythology, not randomized independently like trials — see the reasoning in [GameDesignDocument.md](../GameDesignDocument.md) (frequency: enemies are the dominant, highest-frequency stage content, unlike trials; and it's what makes a stray off-mythology trial feel like a notable exception rather than the norm). This set is Greek-only on purpose — it doesn't need to prove cross-mythology variety, only that combat itself functions.

**Intent system assumed** (standard genre convention, not yet formalized in Core Combat Vocabulary §10 — should be folded in there once confirmed): before the player's turn, each enemy telegraphs its next action — a damage number for Attack intents, or an icon for Block/Buff/Debuff intents. This is what Hunter's Instinct and the equivalent trunk nodes in all 4 passive trees already assume exists ("enemy intents are revealed one turn earlier").

## Training Dummy (QA tool, not real content)

Not part of any actual encounter pool — exists purely for isolated mechanic testing (does this card do what its text says, in a controlled setting with no threat).

| | |
|---|---|
| HP | 999 (effectively undying) |
| Intent | None — never acts, never attacks, never blocks. |

## Hoplite Skirmisher (basic single attacker)

The simplest possible "read the intent, decide to attack or defend" enemy — the baseline every other enemy in the game will eventually be compared against.

| | |
|---|---|
| HP | 42 |
| Intent A (common) | Attack — deal 9 damage. |
| Intent B (occasional) | Guard — gain 8 Block. |

## Harpy Screecher (debuff applier)

Introduces threat-prioritization: is the debuffer worth killing first, or is it safe to ignore for a turn?

| | |
|---|---|
| HP | 30 |
| Intent A | Shriek — apply 2 Drained to the player. No damage. |
| Intent B | Claw — deal 6 damage. |

## Viper Brood (pack unit — spawns 2-3 per encounter)

Introduces multi-target decision-making and racing down small HP pools before their combined damage adds up.

| | |
|---|---|
| HP (each) | 12 |
| Intent | Bite — deal 4 damage. (Single intent, no variation — the complexity here is the pack count, not per-unit behavior.) |

## Sanity Checks

**Covers the core intent patterns a combat system needs to prove out**: pure damage sponge (Dummy), attack/defend alternation (Hoplite), debuff application (Harpy), and multi-enemy encounters (Viper Brood) — enough variety to exercise Artemis's Volley mechanic and both her Block- and debuff-interacting cards (e.g., Point-Blank's Exposed-into-Drained synergy) without needing a full roster.

**Deliberately not included**: a boss, elemental/status-heavy enemies, anything that reads or manipulates the player's Volley (that's Hook 2 territory — [DifferentiationHooks.md](../DifferentiationHooks.md) — and stays out of scope until the full enemy/boss design pass happens post-playtesting, per the peer-feedback plan).

**Not attempted**: numeric tuning against actual player damage output — these HP/damage numbers are placeholders sized to "feel roughly StS-Act-1-ish," not validated against Artemis's real kit yet. That validation is the whole point of building the playable slice.
