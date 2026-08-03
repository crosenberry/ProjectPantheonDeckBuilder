# Syncretism Prototype — Artemis + Thor (Greek + Norse)

First concrete test of Hook 1 from [DifferentiationHooks.md](../DifferentiationHooks.md): does combining two Blessings' mechanics actually produce decisions neither Blessing has alone, or does it just feel like "two decks taped together"? Picked Artemis + Thor as the pair since both are fully drafted ([Artemis](../Characters/Artemis-CardDraft.md), [Thor](../Characters/Thor-FullCardDraft.md)) — cheapest pair to test against.

Recap of the two resources being fused: **Volley** (Artemis — one-way, resets every turn, spend-it-now) and **Storm** (Thor — one-way, persists across turns, banked-then-discharged).

## How a solo player gets access to both

Solo runs are mono-Blessing by default (GDD §3) — Syncretism needs an explicit unlock, not a freebie, or hybrid decks are just strictly better than mono decks with no cost. Proposed trigger, using a rule that already exists rather than inventing a new one: GDD §5.3 says a Major Enhancement's flavor is set by the stage's mythology. Extend that — **completing a rift on a stage of a different mythology than your own Blessing yields a Syncretism Major Enhancement** instead of (or as an option alongside) a normal one. An Artemis player finishing a Norse-flavored stage could roll **"Path of the Twin Storms."**

Effect of that specific Major Enhancement:
- Grants a small, guaranteed passive bridge: *"Whenever you play a Shot card, gain 1 Storm."* Weak on purpose — it makes the other resource exist in your run, it doesn't make it good on its own.
- Adds a small set of Syncretism-only cards (below) into your card reward pool for the rest of the run. Not added to the base 75-card pool of either Blessing — these only ever appear after the unlock fires.

This keeps the fusion layer cheap to scale: it's a ~5-card bolt-on per pairing, not a third full character. With 4 Blessings there are C(4,2) = 6 possible pairings — if this prototype holds up, expect ~30 fusion cards total across all pairs eventually, which is a moderate addition on top of the ~370-card base target, not a doubling.

## Shape 1 — Relic + Relic Fusion (example)

- Artemis relic **Silver Quiver**: your first Shot each turn deals 3 additional damage.
- Thor relic **Stormheart Band**: whenever you gain Block, gain 1 additional Storm.
- Holding both simultaneously auto-fuses them (VS-style: both consumed, replaced by one result) into **Concordat of the Hunt and Storm**: your first Shot each turn deals 3 additional damage and grants 1 Storm; whenever you gain Block, gain 1 additional Storm and 1 Volley.

## Shape 2 — Card + Relic Evolution (example, closest to VS's actual mechanic)

Artemis's starter card **Hunter's Mark** (Attack: deal 8 damage, apply 2 Vulnerable), while **Stormheart Band** is held, permanently upgrades in your deck (once, on pickup, same moment the relic-fusion above would trigger if both relics are present — this and Shape 1 aren't meant to double-stack in the same prototype, just two different shapes being tested independently) into **Hunter's Mark+: Thunderstruck**: deal 8 damage, apply 2 Vulnerable, gain 2 Storm.

## Shape 3 — Resource Interaction Cards (the deep test)

Five cards, deliberately varied in *how* they reference the second resource, so this isn't just one trick repeated five times:

| Name | Type | Cost | Effect | Reads the other resource by... |
|---|---|---|---|---|
| Twin Storms | Attack, Shot | 2 | Deal 7 damage. Gain 1 Storm. If you have any Storm, deal 4 additional damage. | flat bonus if present |
| Hunter's Squall | Skill | 1 | Gain 6 Block. Gain 1 Storm. Gain 1 Volley. | dual-generation |
| Aegis of the Hunt | Power | 2 | Whenever you play a Shot card, gain 1 Storm. Whenever you gain Storm, your next Shot card this turn costs 1 less Energy. | permanent bridge (stronger, optional version of the unlock's built-in passive) |
| Thunderous Volley | Attack, hits ALL enemies | 2 | Consume all Storm. Deal 3 damage per point consumed, plus 2 additional damage per point of current Volley. | reads Volley without consuming it (keeps Volley's "per-turn only" rule intact) |
| Ragnarok's Quarry | Attack | 3 | Consume all Storm. Deal 6 damage per point consumed. If your Volley is 3 or higher this turn, this hits twice. | threshold gate rather than scaling bonus |

## Findings

- **The core question is answered yes**: Twin Storms and Thunderous Volley both force a tension neither pure Blessing has alone — do you spend the turn dumping Shots to build Volley (Artemis's normal pattern), knowing that also feeds Storm passively via the bridge, or do you hold back to protect banked Storm for a future Thunderclap/Full Discharge (Thor's normal pattern)? Mono-Artemis never thinks about banking; mono-Thor never thinks about per-turn Shot count. The hybrid has to weigh both every turn. That's a genuinely new decision shape, not two decks stapled together.
- **Power-level risk is real and expected**: a hybrid deck has strictly more tools available than a mono deck (both resources, plus fusion payoffs), so the unlock needs to stay rare and effortful (a Major Enhancement, not a shop item) and the fusion card *count* needs to stay small (5ish, not a competing 75-card pool) so it reads as a build-around bonus layer, not a dominant strategy that makes picking one Blessing feel like the wrong choice.
- **Variety of "how a card references the other resource" matters** — the five cards above deliberately use five different techniques (flat bonus, dual-gen, permanent bridge, non-consuming read, threshold gate) rather than one template copy-pasted. Worth holding future pairs to the same standard rather than defaulting to "deal bonus damage per point of the other resource" every time.
- **Untested here**: whether Shape 1 (relic fusion) and Shape 2 (card evolution) feel as compelling in practice as Shape 3's cards do on paper — they're each only a single example, not pressure-tested the way the resource-interaction cards were. Worth prototyping a second example of each before picking which shape(s) to commit to for real.
- **Not attempted**: numeric tuning, and no decision yet on whether all three shapes (relic fusion, card evolution, resource-interaction cards) ship together per pairing or whether one shape gets picked as *the* Syncretism mechanic and the others get dropped. That's the natural next decision once this prototype is reviewed.

## Recommended Next Step

Pick which of the 3 shapes felt most compelling (or confirm you want all 3 layered together), then either: (a) do a second pairing to make sure the pattern generalizes beyond Artemis/Thor specifically, or (b) commit to one shape and start scaling it across the other 5 pairings at this same light-sketch depth.
