> Superseded by the full 75-card draft: [SunWukong-FullCardDraft.md](SunWukong-FullCardDraft.md). Kept here for history.

# Sun Wukong — Card Sketch (Form, light pass)

Purpose: quick check that Form ([GameDesignDocument.md §3.4](../GameDesignDocument.md)) supports more than one build before going deep. Lighter than the [Artemis draft](Artemis-CardDraft.md).

Recap: **Form** is a 3-state stance (Mortal/Beast/Immortal) that modifies how *other* cards behave while active. Beast: Attacks hit twice, Skills cost +1. Immortal: Powers/buffs stronger or permanent, Attacks weakened. Mortal: neutral. Separate secondary tool: **Clone** cards that replay the last card played.

## Baseline (not archetype-specific)

- **Mortal Coil** — Skill (Common), 0 Energy: Change to Mortal Form.
- **Ape Fist** — Attack (Common), 1 Energy: Deal 5 damage. (Hits twice in Beast Form, per the Form's passive modifier.)

## Path A — Beast Rush (commit to Beast, double-hit tempo)

- **Beast Awakening** — Skill (Common), 1 Energy: Change to Beast Form. Gain 3 Block.

## Path B — Immortal Ascension (commit to Immortal, permanent-buff utility/sustain)

- **Immortal Ascension** — Skill (Uncommon), 1 Energy: Change to Immortal Form. Gain 2 Strength (permanent this combat, on top of Immortal's passive buff-strengthening).

## Path C — 72 Changes (cycle every turn, payoff is the switch itself, not sitting in a Form)

Tests a build shape neither Artemis, Thor, nor Anubis has: a mechanic where *changing state* is the payoff, not accumulating or committing to one state.

- **72 Changes** — Skill (Rare), 1 Energy: Change to a different Form than your current one. Draw 2 cards.

## Secondary tool — Clone (cuts across all paths)

- **Hair Clone** — Attack (Uncommon), 1 Energy: Deal 4 damage. Play a copy of the last card played this turn.

## Findings

- Form is structurally different from all three other resources: Volley/Storm are magnitudes (how much), Scale is a magnitude-with-direction (how much, which way), Form is purely categorical (which of 3 discrete states) — good, this is real variety across the roster rather than four reskins of "a number that goes up."
- Path C (72 Changes) is the one to watch: "switching is the payoff" is a genuinely different build shape from "committing to Beast" or "committing to Immortal," which is exactly the kind of 3rd path the other Blessings had to manufacture more deliberately (Huntress, Unrelenting). Here it fell out naturally from having 3 states instead of 2 — same free-diversity effect Anubis's bidirectional Scale had, via a different mechanism (states to cycle through, vs. poles to lean toward).
- Open risk, not yet resolved: Beast Form's "Attacks hit twice" is a flat multiplier that could get out of hand paired with strong Attacks from elsewhere in the pool (relics, other cards) — same class of scaling risk as Artemis's Rain of Arrows, flagged for tuning once more cards exist, not a rule problem.
- Clone is currently a single card, not a path — needs more support before it's a real 4th archetype rather than a one-off tool; leaving it as "cuts across paths" for now rather than forcing it into its own lane.
