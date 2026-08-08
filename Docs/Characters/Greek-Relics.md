# Greek Relics

First relic pool drafted for any mythology, per [GameDesignDocument.md §4](../GameDesignDocument.md): no rarity tiers, mythology-flavored (Greek = Offense/Mobility primary focus, secondary-focus outliers allowed), no per-mythology count confirmed yet beyond the `[OPEN]` GDD §2 suggestion of ~30-40 per mythology. This is a curated first slice (14: 13 regular + 1 boss-exclusive) toward that eventual target, not the final Greek roster — matching how every other content pool in this project started (25 of Artemis's eventual 75 cards, 4 of the eventual Greek enemy roster).

Named `Greek-Relics.md`, not `Artemis-Relics.md` — relics are tied to **stage** mythology, not to whichever Blessing is active (GDD §4's chest/boss acquisition rules never gate on the current Blessing), so a Thor or Anubis player can pick these up on a Greek stage just as easily as an Artemis player can. Matches the naming precedent already set by [MinimalSampleSet-Greek.md](../Enemies/MinimalSampleSet-Greek.md).

## Design rule: every relic must do *something* for any Blessing

Caught during drafting, not before: a relic that reads a Blessing-exclusive resource or tag (Artemis's Volley, the Shot card tag) is **dead text** for the other 3 Blessings whenever they pick it up on a Greek stage — the exact same problem GDD §5.3 already ruled out for Major Enhancements ("a bonus tied to one specific Blessing's unique resource... would be dead text for the other 3 players"). Applied here as a hard rule: every relic's mechanical trigger must be something every Blessing can act on (Attack/Skill card type, Energy, Block, the 4 universal statuses, enemy deaths, Divine Essence) — no relic reads Volley, the Shot tag, or any other Blessing-exclusive concept directly.

Where a relic still wants to nod at Artemis specifically, it uses a **universal baseline + Blessing-specific bonus** shape instead of an all-or-nothing gate (Nyx's Embrace below is the example) — never dead for anyone, but reads as extra-attuned to the Blessing that matches the mythology.

## Design rule: flavor draws from the whole pantheon, not one figure

First draft of this pool named every relic after archery/hunting gear or Artemis's own specific myths (a quiver, a bowstring, "Huntress," her hunting nymphs, the Calydonian Boar). Caught in review: that's the same narrowness problem [DifferentiationHooks.md](../DifferentiationHooks.md) already flagged once for vocabulary, just recurring in a different system — and it wastes the breadth of an entire pantheon on one character's personal gear, when her cards and passive tree already correctly, tightly carry her archer identity. Fixed by re-flavoring every entry below around a different Greek figure (gods, heroes, monsters), while keeping every mechanical effect exactly as already balanced against the universal-effect rule above.

## Regular pool (13)

| Name | Figure | Effect |
|---|---|---|
| Ares' First Blood | Ares, god of war | Your first Attack card each turn deals 3 additional damage. |
| Hermes' Winged Sandals | Hermes, speed and travel | Your first card played each turn costs 1 less Energy (minimum 1). |
| Nyx's Embrace | Nyx, primordial night | At the start of each combat, gain 1 Strength. If you have Volley, gain 1 Volley instead. |
| Medusa's Glare | Medusa | Whenever you apply Exposed, apply 1 additional stack. |
| Styx's Blessing | The river Styx (Achilles' invulnerability myth) | The first time you would take damage each combat, gain 5 Block first. |
| Erinyes' Vengeance | The Erinyes (Furies) | Whenever an enemy dies, deal 2 damage to a random other enemy. |
| Aphrodite's Bathwater | Aphrodite | Curse cards do nothing when played and instead grant a small random bonus (Strength, Block, or Energy) for the rest of the turn. |
| Sisyphus's Gym Membership | Sisyphus | Gain 1 Strength at the start of each turn. Lose all Strength at the start of each combat. |
| Midas's Credit Card | Midas | Enemies drop additional Divine Essence when defeated. Cards cost 1 more Energy after the first each turn. |
| Zeus's Group Project | Zeus | The first card you play each turn triggers its effects twice. |
| Pandora's Group Chat | Pandora | When picked up, apply 2 random debuffs to yourself for your next combat only. Afterward, permanently gain a small bonus for the rest of the run. |
| Hades' Bulk Discount | Hades | Removing cards (including Curses) at Hermes' Exchange costs less Divine Essence. |
| Narcissus's Front-Facing Camera | Narcissus | Whenever you play a Skill card, gain 1 Block. |

Nyx's Embrace is the one relic using the universal-baseline-plus-Blessing-bonus shape — never dead for a non-Artemis player (they get Strength), reads as the mythology's own relic recognizing its own Blessing for anyone playing her (Volley instead).

Aphrodite's Bathwater, Sisyphus's Gym Membership, Midas's Credit Card, Zeus's Group Project, Pandora's Group Chat, Hades' Bulk Discount, and Narcissus's Front-Facing Camera are the "gag" tier — real myth beats given a modern comedic frame, same pattern as the user's original Aphrodite's Bathwater pitch. Not mechanically weaker or a separate category from the other 6; they follow the exact same universal-effect rule.

## Boss-exclusive (1)

| Name | Figure | Effect |
|---|---|---|
| Talos's Core | Talos, the bronze automaton guardian of Crete | Your Attack cards deal 2 additional damage to enemies below 50% HP. |

Deliberately **not** a safety-net effect (survive-lethal-once) — that pattern stays card-only per [CrossBlessing-Comparison.md](CrossBlessing-Comparison.md)'s existing ruling. Deliberately does **not** touch Volley's per-turn reset — that stays a Mythic-capstone-only privilege per [GameDesignDocument.md §8](../GameDesignDocument.md).

## Sanity checks

**Count**: 13 regular + 1 boss-exclusive, a first slice toward GDD §2's suggested ~30-40/mythology eventual target — not a final count, and not a count for the other 3 mythologies (out of scope, M3 is Greek-only).

**Universal-effect check**: every entry's trigger is Attack/Skill card type, Energy, Block, a universal status (Exposed), enemy death, Divine Essence, or Curse cards — nothing reads Volley or the Shot tag directly. Nyx's Embrace is the sole intentional exception, and even it has a non-Volley baseline.

**No rule collisions**: no safety-net-effect relic, no relic bending Volley's reset rule outside the Mythic-capstone exception, no relic restating the base Divine-Essence-on-kill income as if it were a bonus (an earlier draft of Hades' Bulk Discount did exactly this before being caught and reworded).

**Not attempted**: numeric tuning (all values placeholder, same standing rule as everywhere else per [DifferentiationHooks.md](../DifferentiationHooks.md)), and the ~15-25 additional relics needed to reach the ~30-40 target — ongoing content work, not a blocker on M3.
