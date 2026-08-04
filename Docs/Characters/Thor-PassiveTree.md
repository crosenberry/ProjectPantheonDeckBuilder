# Thor — Passive Tree (Mythos)

Second full tree, following the template validated by [Artemis-PassiveTree.md](Artemis-PassiveTree.md). Same structure: 5-node universal trunk (independent of branches) + 3 branches of 4 nodes (linear chain within each), one per archetype path (Bulwark / Chain / Unrelenting — see [Thor-FullCardDraft.md](Thor-FullCardDraft.md)), each branch ending in a Mythic capstone. Numbers are placeholders, same caveat as Artemis's tree.

## Trunk (5 nodes)

| Node | Cost (Mythos) | Effect |
|---|---|---|
| Ironbound Vigor | 40 | +5 Max HP, every run. |
| Hearth of Asgard | 50 | Start each run with +25 Divine Essence. |
| Tempered Grip | 60 | Start each run with 1 additional copy of Hammer Strike in your deck. |
| Stormsight | 70 | In your first combat each run, enemy intents are revealed one turn earlier than normal. |
| Well-Forged Start | 80 | Your first godly trial encountered each run costs 20% less Divine Essence. |

## Branch: Bulwark (turtle, then release)

| Node | Cost | Effect |
|---|---|---|
| Steadfast Stance | 60 | If your deck has 3+ Bulwark cards, your opening hand each combat always includes at least 1 of them. |
| Runed Resolve (choice pair) | 90 | Pick one, freely toggle between runs: **(A)** Block-and-Storm cards (like Shield Wall) grant +1 additional Block per point of Storm gained. **(B)** The first Block-granting card you play each turn costs 0 Energy. |
| Immovable Wall | 110 | If you end your turn with 10 or more Block, gain 2 Storm at the start of your next turn (in addition to normal generation). |
| **Wrath of the Aesir** (Mythic capstone) | 200 | New card unlocked into the Bulwark pool: *Attack, 3 Energy, hits ALL enemies — Consume all Storm. Deal 5 damage per point consumed. If you consumed 8 or more, gain Block equal to the damage dealt (capped at 30).* |

Wrath of the Aesir differs from Thunderclap (the base pool's Bulwark Rare) in kind, not just number: it can refund a full defensive turn's worth of Block on the same card that just spent everything offensively, letting a deeply-invested Bulwark player go from full turtle to full nuke and immediately be defended again — nothing in the base 75 does both halves of that on one card.

## Branch: Chain (small discharge every turn, tempo)

| Node | Cost | Effect |
|---|---|---|
| Live Current's Grace | 60 | If your deck has 3+ Chain cards, your opening hand each combat always includes at least 1 of them. |
| Arcing Focus (choice pair) | 90 | Pick one, freely toggle between runs: **(A)** Cards that consume Storm for bonus damage (like Spark) deal +1 additional damage per point consumed. **(B)** Whenever you consume Storm, draw 1 card (once per turn). |
| Overcharged Reflexes | 110 | If you consume Storm 2 or more times in a single turn, gain 1 Strength for the rest of combat. |
| **Endless Current** (Mythic capstone) | 200 | New card unlocked into the Chain pool: *Attack, 1 Energy — Deal 3 damage. Consume 1 Storm: instead of going to your discard pile, shuffle this card back into your draw pile.* |

Endless Current differs in kind from every other Chain payoff (all immediate-damage-focused) by converting Storm-spending into deck cycling and inevitability across a whole fight, rather than another burst-damage source — a different currency to build around entirely.

## Branch: Unrelenting (Storm-light, HP-for-power berserker)

| Node | Cost | Effect |
|---|---|---|
| Battle Scars | 60 | If your deck has 3+ Unrelenting cards, your opening hand each combat always includes at least 1 of them. |
| Berserker's Will (choice pair) | 90 | Pick one, freely toggle between runs: **(A)** Whenever you lose HP from your own cards (not enemy attacks), gain 1 additional Strength beyond the card's stated effect. **(B)** The first time you fall below 50% HP each combat, gain 5 Block immediately. |
| Undying Resolve | 110 | If you end a turn below 50% HP, gain 1 Strength at the start of your next turn. |
| **Ragnarok's Defiance** (Mythic capstone) | 200 | New card unlocked into the Unrelenting pool: *Power, 2 Energy — The first time you would die this combat, instead survive with 1 HP and gain Strength equal to the full damage you've taken this combat (not halved). This Power triggers a second time later in the same combat if you take fatal damage again.* |

Ragnarok's Defiance differs in kind from Deathless Rage (the base pool's Unrelenting Rare, one safety net at half the damage-taken value) by granting two separate near-death saves in the same fight at full value — a genuinely different tier of insurance, appropriate as the deepest expression of a path built entirely around punishing endurance.

## Sanity Checks

**Node count**: 5 + (4 × 3) = 17, matching Artemis's tree exactly — confirms the template produces a consistent scope per Blessing rather than drifting.

**Depth-over-access check**: holds — every node either edits the player's own deck/behavior directly or only triggers on in-combat commitment (Immovable Wall, Overcharged Reflexes, Undying Resolve). No node touches reward-pool odds.

**Rule-bending technique reused**: Artemis's tree established that the passive tree is allowed to bend a Blessing's own core rule for deep investment (Practiced Patience bending Volley's per-turn reset). Thor's tree doesn't need an equivalent bend for Storm itself (Storm already persists across turns by design, so there's no analogous "reset" rule to break) — worth noting this isn't a gap, just a case where the base mechanic doesn't have a rule that needs bending. Ragnarok's Defiance instead pushes past the *card-level* pattern (one safety net) rather than a resource rule.
