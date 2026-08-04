# Sun Wukong — Passive Tree (Mythos)

Fourth and final tree, completing the set alongside [Artemis](Artemis-PassiveTree.md), [Thor](Thor-PassiveTree.md), and [Anubis](Anubis-PassiveTree.md). Same structure: 5-node universal trunk + 3 branches of 4 nodes, one per archetype path (Beast Rush / Immortal Ascension / 72 Changes — see [SunWukong-FullCardDraft.md](SunWukong-FullCardDraft.md)), each ending in a Mythic capstone. Numbers are placeholders.

## Trunk (5 nodes)

| Node | Cost (Mythos) | Effect |
|---|---|---|
| Sage's Vigor | 40 | +5 Max HP, every run. |
| Peachwood Fortune | 50 | Start each run with +25 Divine Essence. |
| Iron-Skinned Ape | 60 | Start each run with 1 additional copy of Ape Fist in your deck. |
| Heavenly Sight | 70 | In your first combat each run, enemy intents are revealed one turn earlier than normal. |
| Auspicious Start | 80 | Your first godly trial encountered each run costs 20% less Divine Essence. |

## Branch: Beast Rush (commit to Beast, double-hit tempo)

| Node | Cost | Effect |
|---|---|---|
| Primal Reflex | 60 | If your deck has 3+ Beast Rush cards, your opening hand each combat always includes at least 1 of them. |
| Feral Awakening (choice pair) | 90 | Pick one, freely toggle between runs: **(A)** Whenever you change to Beast Form, gain 1 Strength. **(B)** While in Beast Form, the first Attack you play each turn deals +2 damage. |
| Unshakable Rage | 110 | If you deal damage 4 or more times in a single turn (counting each individual hit), gain 1 Block that does not expire at the start of your next turn. |
| **Great Sage's Rampage** (Mythic capstone) | 200 | New card unlocked into the Beast Rush pool: *Attack, 3 Energy — Deal 6 damage three times. While in Beast Form, this card costs 1 less Energy and deals its damage a fourth time.* |

**Note on this capstone**: GDD §10's Beast Form ruling states the Form's double-hit only applies to Attacks that normally hit once, specifically so multi-hit cards' printed numbers stay legible regardless of Form. Great Sage's Rampage is a deliberate, singular, named exception to that rule — worth being explicit that this doesn't quietly contradict the earlier ruling, it's a one-off granted specifically as the deepest capstone reward for a fully-committed Beast Rush build, the same category of "the tree is allowed to bend a rule the base kit can't" already established by Artemis's Practiced Patience (bending Volley's reset) and reused here for Wukong's Beast Form ruling specifically.

## Branch: Immortal Ascension (commit to Immortal, permanent-buff utility/sustain)

| Node | Cost | Effect |
|---|---|---|
| Heavenly Reflex | 60 | If your deck has 3+ Immortal Ascension cards, your opening hand each combat always includes at least 1 of them. |
| Celestial Favor (choice pair) | 90 | Pick one, freely toggle between runs: **(A)** Whenever you change to Immortal Form, gain 1 additional Strength. **(B)** While in Immortal Form, the first Skill you play each turn costs 0 Energy. |
| Boundless Patience | 110 | If you spend 3 or more turns in Immortal Form across a single combat (need not be consecutive), gain 2 Strength for the rest of that combat. |
| **Ascended Sage's Blessing** (Mythic capstone) | 200 | New card unlocked into the Immortal Ascension pool: *Power, 3 Energy — While in Immortal Form, Attacks no longer deal reduced damage (removes the Form's own -3 penalty for the rest of combat).* |

Ascended Sage's Blessing differs in kind from every other card in Wukong's pool: it's the first effect that removes one of Form's own built-in downsides rather than adding a bonus on top of it — "you've mastered Immortality so thoroughly its usual weakness no longer applies to you," rather than another stacking buff.

## Branch: 72 Changes (cycle every turn, the switching itself is the payoff)

| Node | Cost | Effect |
|---|---|---|
| Fluid Reflex | 60 | If your deck has 3+ 72 Changes cards, your opening hand each combat always includes at least 1 of them. |
| Endless Metamorphosis (choice pair) | 90 | Pick one, freely toggle between runs: **(A)** Whenever you change Form, gain 1 Block. **(B)** Clone cards (Hair Clone, Hundred-Fold Clone) also change your Form to a different Form when played. |
| Master of the Myriad Forms | 110 | If you change Form 3 or more times in a single turn, draw 1 card and deal 2 damage to a random enemy. |
| **The Sage's Infinite Self** (Mythic capstone) | 200 | New card unlocked into the 72 Changes pool: *Skill, 2 Energy — Change to a different Form than your current one. This turn, you may change Form up to 2 additional times at no Energy cost.* |

Endless Metamorphosis Option B is a deliberate, small tie-in for the Clone sub-tool flagged as underused in the [DifferentiationHooks.md peer-feedback note](../DifferentiationHooks.md) — giving Clone actual synergy with the archetype system it currently sits outside of, without a full redesign (which stays deferred until real playtesting per that same note). The Sage's Infinite Self differs in kind from the entire base 75: no other card allows more than one Form change per card play, making this the purest possible expression of "72 Changes" as an identity.

## Sanity Checks

**Node count**: 5 + (4 × 3) = 17, consistent across all 4 trees now — the template holds its shape regardless of how structurally different the underlying resource is (categorical Form vs. Artemis's magnitude-based Volley).

**Depth-over-access check**: holds across all branches — Unshakable Rage, Boundless Patience, and Master of the Myriad Forms all require in-combat commitment, no reward-pool odds touched anywhere.

**Rule-bending technique, third instance**: Great Sage's Rampage is the third documented case of a Mythic capstone deliberately bending a rule the base kit enforces strictly (Artemis/Volley's per-turn reset, this file/Beast Form's single-hit-only doubling). Worth treating this as a confirmed, intentional pattern across the whole passive tree system now, not a one-off: **Mythic capstones are the one place in the game allowed to break a Blessing's own core rule**, precisely because they're rare, singular, and gated behind deep Mythos investment rather than in-run luck.
