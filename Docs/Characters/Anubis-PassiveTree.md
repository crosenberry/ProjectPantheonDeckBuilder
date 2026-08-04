# Anubis — Passive Tree (Mythos)

Third full tree, following the template validated by [Artemis-PassiveTree.md](Artemis-PassiveTree.md) and [Thor-PassiveTree.md](Thor-PassiveTree.md). Same structure: 5-node universal trunk + 3 branches of 4 nodes, one per archetype path (Balance-keeper / Reaper / Ascendant — see [Anubis-FullCardDraft.md](Anubis-FullCardDraft.md)), each ending in a Mythic capstone. Numbers are placeholders.

## Trunk (5 nodes)

| Node | Cost (Mythos) | Effect |
|---|---|---|
| Embalmed Resilience | 40 | +5 Max HP, every run. |
| Tomb's Wealth | 50 | Start each run with +25 Divine Essence. |
| Bound Jackal | 60 | Start each run with 1 additional copy of Jackal's Bite in your deck. |
| Anubis's Sight | 70 | In your first combat each run, enemy intents are revealed one turn earlier than normal. |
| Rite of Passage | 80 | Your first godly trial encountered each run costs 20% less Divine Essence. |

## Branch: Balance-keeper (hover near 0)

| Node | Cost | Effect |
|---|---|---|
| Steady Scale | 60 | If your deck has 3+ Balance-keeper cards, your opening hand each combat always includes at least 1 of them. |
| Ma'at's Whisper (choice pair) | 90 | Pick one, freely toggle between runs: **(A)** Whenever Scale is between -1 and +1 at the start of your turn, gain 1 Block. **(B)** Cards that check "if Scale is between -1 and +1" also heal 1 HP when that condition is met. |
| Centered Focus | 110 | The first time each combat you end a turn with Scale exactly 0, draw 1 card. |
| **Perfect Balance** (Mythic capstone) | 200 | New card unlocked into the Balance-keeper pool: *Skill, 2 Energy — Set Scale to 0. For the rest of this combat, whenever Scale would change, it changes by 1 less toward 0 (minimum 1).* |

Perfect Balance differs in kind from Scales Eternal (the base pool's Balance-keeper Rare, a one-time reset) by dampening *future* Scale volatility for the rest of the fight — a stabilizing effect nothing else in the pool does, since every other Scale card either pushes or reads the value, never blunts how far future cards can push it.

## Branch: Reaper (push toward Chaos, -)

| Node | Cost | Effect |
|---|---|---|
| Bound to Chaos | 60 | If your deck has 3+ Reaper cards, your opening hand each combat always includes at least 1 of them. |
| Apep's Favor (choice pair) | 90 | Pick one, freely toggle between runs: **(A)** Cards that push Scale negative push it 1 additional point. **(B)** Whenever Scale becomes negative for the first time each turn, apply 1 Weak to a random enemy. |
| Devourer's Patience | 110 | If Scale is -3 or lower at the start of your turn, gain 1 Strength. |
| **Ammit's Feast** (Mythic capstone) | 200 | New card unlocked into the Reaper pool: *Attack, 3 Energy — Scale -2. Deal damage equal to 8 × \|Scale\| (after this reduction) to one enemy.* |

Ammit's Feast pushes past Devourer's Toll's 3×\|Scale\| multiplier to 8× — a steep escalation reserved for deep investment, and it guarantees its own Scale contribution rather than depending entirely on setup, so it's never a dead draw even early.

## Branch: Ascendant (push toward Order, +)

| Node | Cost | Effect |
|---|---|---|
| Blessed by Ma'at | 60 | If your deck has 3+ Ascendant cards, your opening hand each combat always includes at least 1 of them. |
| Osiris's Grace (choice pair) | 90 | Pick one, freely toggle between runs: **(A)** Cards that push Scale positive push it 1 additional point. **(B)** Whenever Scale becomes positive for the first time each turn, heal 1 HP. |
| Eternal Vigil | 110 | If Scale is +3 or higher at the start of your turn, gain 2 Block. |
| **Osiris's Dominion** (Mythic capstone) | 200 | New card unlocked into the Ascendant pool: *Skill, 3 Energy — Scale +2. Gain Block equal to 8 × Scale (after this increase). Cleanse all debuffs.* |

Osiris's Dominion mirrors Ammit's Feast structurally (guaranteed Scale push + steep multiplier) but bundles a full cleanse on top — a "wall of order" moment distinct from Eternal Order (the base pool's Ascendant Rare), which grants a flat amount rather than scaling with current investment.

## Sanity Checks

**Node count**: 5 + (4 × 3) = 17, consistent with both prior trees.

**Depth-over-access check**: holds — Centered Focus, Devourer's Patience, and Eternal Vigil all require an in-combat state to trigger, and none of the trunk or branch nodes touch reward-pool odds.

**Deliberate mirror structure**: Reaper and Ascendant's branches were built as intentional mirrors of each other (matching choice-pair shapes, matching capstone math) since Scale itself is bidirectional by design (§3.3) — this is the first tree where two branches are meant to visibly rhyme with each other, unlike Artemis's or Thor's three branches, which were each designed independently. Worth keeping as an option for any future Blessing whose resource has a similarly symmetric shape, not a rule to force elsewhere.
