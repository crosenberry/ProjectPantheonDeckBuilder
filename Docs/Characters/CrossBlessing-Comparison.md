# Cross-Blessing Comparison — all 4 at full 75-card depth

Updated after all four Blessings reached full 75-card drafts: [Artemis](Artemis-CardDraft.md), [Thor](Thor-FullCardDraft.md), [Anubis](Anubis-FullCardDraft.md), [Sun Wukong](SunWukong-FullCardDraft.md). Originally written after one deep pass (Artemis) plus three light sketches — the resource-shape and build-diversity findings below held up unchanged going to full depth; the new section at the bottom covers what only showed up once real card volume existed.

Original framing question, still the throughline: do the four keyword mechanics actually feel different from each other, or do they collapse into "stack a number, spend a number" four times over?

## Resource shape, side by side

| Blessing | Resource | Shape | Timing |
|---|---|---|---|
| Artemis | Volley | One-way magnitude | Per-turn, resets every turn |
| Thor | Storm | One-way magnitude | Persists across turns, banked |
| Anubis | Scale | Two-way magnitude (signed, has direction) | Persists across combat |
| Sun Wukong | Form | Categorical (3 discrete states, no magnitude) | Persists across turns, switchable |

Verdict: these are four genuinely different shapes, not reskins. Volley vs. Storm is the closest pair (both one-way magnitudes), but they're deliberately separated by timing — Artemis is forced to use it now, Thor is rewarded for sitting on it. Scale and Form are structurally unlike either of them or each other.

## Where build-diversity came from, per Blessing

This is the more interesting finding: the *source* of the "3 build paths" differed per Blessing, which is a good sign — it means the pillar isn't being satisfied the same mechanical way four times.

- **Artemis / Thor**: diversity needed a deliberate 3rd path bolted on that mostly ignores the mechanic (Huntress, Unrelenting), leaning on raw stats or a different keyword (Vulnerable, Strength) instead.
- **Anubis**: diversity is free — it falls straight out of Scale having two poles and a middle. Chaos-lean, Order-lean, and Balanced aren't three paths bolted onto one mechanic, they're three ways of using the *same* mechanic.
- **Sun Wukong**: diversity is also close to free, but via a different mechanism — 3 discrete states naturally produce "commit to Beast," "commit to Immortal," and "the switching itself is the payoff" (72 Changes), without needing an ignore-it path.

None of this is a problem — all four end up with real build diversity — but it's worth knowing that Artemis and Thor's card pools will need to keep deliberately writing "ignores the signature mechanic" cards throughout their full 75, whereas Anubis and Wukong get more of that for free from the resource shape itself. Something to keep in mind for effort-balancing when full card-writing starts.

## Shared open risks (not blockers, just flagged consistently across all 4)

- Every Blessing has at least one card whose scaling depends on external support (draw, energy, other stacking) that could get strong — normal for the genre, needs playtesting once pools are fuller, not a rule-level concern.
- No numeric tuning has been attempted anywhere yet. Every number in all 4 files is a placeholder for "does the rule shape make sense," not a balanced value.
- Card rarity (Common/Uncommon/Rare) has been used as a default assumption across all 4 sketches, separate from the relic no-rarity ruling — still needs explicit confirmation (carried over from the Artemis draft's open note).

## What only showed up at full depth

- **A "safety net" Rare pattern emerged independently in 3 of 4 Blessings, now resolved** — Thor's Deathless Rage, Anubis's Osiris's Rebirth, Sun Wukong's Elixir of Immortality all do the same thing: survive a killing blow once per combat, at Rare/Power. Nobody designed these to match each other; they converged on the same idea separately while filling out each Blessing's Rare slots. Ruling: stays a card in all 3, not a relic (Rare + must-be-drawn already gates it; a relic version would be an always-on insurance policy with no real opportunity cost), and Artemis isn't getting a 4th for forced symmetry. Thor's got retooled to key off damage taken this combat instead of a flat number, so each Blessing expresses the same beat through its own mechanic rather than reading as one card copy-pasted three times.
- **Going to full depth found a genuine rule-interaction problem, not just a numbers-tuning gap — now resolved.** Sun Wukong's Beast Form ("Attacks hit twice") combined with multi-hit Rares (a 5-hit card becoming 10 hits) needed an actual ruling, not just a numbers pass. Resolved: Beast Form's double-hit only applies to Attacks that normally hit once; cards that already hit multiple times are unaffected, so their printed numbers stay legible regardless of Form state. The 6-card light sketch never surfaced this because it never had a multi-hit Rare to collide with the Form passive — the clearest evidence yet that light sketches can validate a resource *shape* but can't be trusted to catch interaction problems, only full-depth drafts can.
- **Attack/Skill/Power ratios vary more than expected**: Thor 41/37/21, Anubis 39/44/17, Sun Wukong 33/45/21 (Artemis not yet re-tallied against this exact framing). Some skew toward Skills is explainable by focus (Anubis/Wukong are both Utility-leaning mythologies), but Wukong's 33% Attack share is a bigger deviation than the others and reads as a genuine finding worth a real look, not just flavor-justified away.
- Card-rarity assumption (Common/Uncommon/Rare) is now fully confirmed at the GDD level (§2), no longer just an assumption carried per-file.

## Recommendation for next session

All 4 resource shapes and all 4 full card pools are now on paper, and both rule-level questions from this pass (safety-net card-vs-relic, Beast Form × multi-hit) are resolved. Reasonable next steps, in rough order of value:
1. Zoom out to the still-fully-open systems not touched by this pass (passive tree, potion-equivalent, Core Combat Vocabulary formalization) since all 4 Blessings' core identities and full card pools are now de-risked enough to not be the critical path.
2. First real numeric tuning pass — everything up to now has been rule-shape work, not balance work, and there's no remaining rule-level blocker stopping that from starting.
