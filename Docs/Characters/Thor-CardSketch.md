> Superseded by the full 75-card draft: [Thor-FullCardDraft.md](Thor-FullCardDraft.md). Kept here for history.

# Thor — Card Sketch (Storm, light pass)

Purpose: quick check that Storm ([GameDesignDocument.md §3.2](../GameDesignDocument.md)) supports more than one build before going deep. Lighter than the [Artemis draft](Artemis-CardDraft.md) — just enough per path to sanity-check the resource shape.

Recap: **Storm** persists across turns (unlike Artemis's per-turn Volley). Block-generating cards grant it as a side effect. Payoff cards discharge banked Storm for burst.

## Baseline (not archetype-specific)

- **Hammer Strike** — Attack, 1 Energy: Deal 6 damage.
- **Storm Ward** — Skill, 1 Energy: Gain 5 Block. Gain 1 Storm.

## Path A — Bulwark Discharge (the "default" reading: turtle, then release)

- **Aegis Wall** — Skill (Common), 2 Energy: Gain 12 Block. Gain 3 Storm.
- **Thunderclap** — Attack (Rare), 2 Energy, hits ALL enemies: Consume all Storm. Deal 4 damage per Storm consumed.

## Path B — Chain Lightning (trickle discharge every turn, tempo instead of turtle)

Tests whether Storm can support a non-turtle playstyle — small, constant conversion instead of one big release.

- **Spark** — Attack (Common), 1 Energy: Deal 5 damage. Consume up to 2 Storm: deal 3 additional damage per point consumed.
- **Static Field** — Power (Uncommon), 1 Energy: At the end of your turn, if you have Storm, deal damage equal to your Storm to a random enemy, then lose 1 Storm.

## Path C — Unrelenting (Storm-light, HP-for-damage berserker)

Tests the build-diversity pillar again: a Thor build that mostly ignores Storm, leaning on the "Offense" half of his hybrid identity directly.

- **Reckless Swing** — Attack (Common), 1 Energy: Deal 10 damage. Take 3 damage.
- **Warrior's Resolve** — Power (Uncommon), 1 Energy: Gain 2 Strength. The first time you take damage each turn, gain 1 additional Strength.

## Findings

- Storm's cross-turn banking (vs. Artemis's per-turn Volley) reads as genuinely different in practice, not just in description — Path A actually wants you to pass up damage for a turn or two, which Artemis's kit never asks of the player.
- Path B is the interesting find: it shows Storm doesn't *require* the turtle rhythm — a build can keep Storm low and spend it every turn like a secondary damage-scaler instead. Worth keeping both A and B as intentionally distinct Rares/Uncommons rather than letting one crowd out the other.
- Path C confirms the ignore-the-mechanic path exists here too, same as Artemis's Huntress — good, this isn't a one-off, it looks like a repeatable pattern for the pillar across Blessings.
- No numeric tuning attempted — Thunderclap's 4-per-Storm on an AoE and Static Field's free recurring ping both need real playtesting once more cards exist.
