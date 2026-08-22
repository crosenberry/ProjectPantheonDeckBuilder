# Hook 2 — Enemies That Read/Manage Their Own Resource Systems

Per [DifferentiationHooks.md](../DifferentiationHooks.md) Hook 2: "If specific enemies bank their own Storm, or manage their own Scale, then learning a Blessing's resource shape becomes useful both offensively (playing it) and defensively (reading/countering it in enemies)." First pass, scoped small per the M8 design session: one new enemy per mythology, each mirroring that mythology's Blessing resource, added alongside (not replacing) the existing minimal sample sets. Full boss/roster design and the Core Combat Vocabulary mechanical redesign both stay deferred past this pass.

## Why Volley doesn't appear here

Volley's defining shape — build up *within a single turn* via multiple actions, reset every turn — depends on the player playing several cards in one turn. Every enemy in this codebase takes exactly one action per turn (`Enemy.ChooseNextIntent` picks one `EnemyMove` per round). A literal Volley-on-an-enemy would reset to 0 before it ever got to act again, making accumulation impossible. Confirmed via direct question to the user: rather than build multi-action-per-turn enemies (real new architecture, out of scope for this pass) or skip Greek entirely, the Greek entry below uses a **Storm-shaped mechanic instead** — banked across turns, discharged at a threshold — just Greek-flavored in name and text, not literally `Player.Volley`. Two of the four enemies below share this underlying shape (banked-counter-then-discharge); that's an honest, acknowledged cost of the resolution, not a hidden one.

## Shared mechanism

Extends `Enemy` with `Storm`/`Scale`/`Form` fields mirroring `Player`'s (same default state, same persistence-within-combat rules — no reset in `StartTurn`, since enemies are freshly constructed per encounter rather than needing a `PrepareForCombat`-style reset). Extends `EnemyMove` with optional resource deltas and eligibility gates (`StormDelta`, `ConsumesStorm`, `MinStorm`/`MaxStorm`, `ScaleDelta`, `MinScale`/`MaxScale`, `FormTarget`, `RequiredForm` — all default to no-op/no-gate). `ChooseNextIntent` filters to only currently-eligible moves before its existing weighted-random pick, so a move gated by (e.g.) `MinStorm: 6` simply isn't in the pool until the enemy's own Storm reaches 6. Exact class shape is implementation-level, worked out via TDD in the slice itself.

## Wrathful Erinys (Greek — Storm-flavored)

An avenging spirit building toward a single devastating strike. Deliberately does zero damage while charging — a "silence before the storm" read, distinct from the Norse entry's steadier pressure.

| | |
|---|---|
| HP | 34 |
| Seethe (eligible while Storm ≤ 5) | Buff — no damage. Gain 3 (own) Storm. |
| Vengeance Strike (eligible once Storm ≥ 6) | Attack — deal 20 damage. Consumes all Storm. |

Fully deterministic once built: below 6 Storm only Seethe is eligible, at 6+ only Vengeance Strike is — no randomness in *when* it strikes, only readable buildup.

## Thunderhide Jötunn (Norse — Storm)

A frost/storm giant, thematically tied to the same Storm Thor himself wields. Builds more slowly than the Erinys (3 turns to threshold instead of 2) — same shape, different cadence, so the two don't play identically despite sharing a mechanism.

| | |
|---|---|
| HP | 38 |
| Gather Squall (eligible while Storm ≤ 5) | Buff — no damage. Gain 2 (own) Storm. |
| Storm Slam (eligible once Storm ≥ 6) | Attack — deal 16 damage. Consumes all Storm. |

## Ammit's Shade (Egyptian — Scale)

A judgment-spirit whose own Scale drifts with its actions, mirroring Anubis's own bidirectional resource rather than a one-way buildup — the only one of the four with genuinely branching (not just delayed) behavior.

| | |
|---|---|
| HP | 36 |
| Sway Toward Chaos (always eligible) | Attack — deal 6 damage. Scale −2 (own). |
| Sway Toward Order (always eligible) | Block — gain 6 Block. Scale +2 (own). |
| Chaos Surge (eligible once Scale ≤ −4) | Attack — deal 14 damage. |

Reading its current Scale value tells you whether Chaos Surge is imminent — two Sway Toward Chaos picks in a row is enough to unlock it.

## Stone Guardian (Chinese — Form)

Reuses the literal `Form` enum (Mortal/Beast/Immortal) rather than inventing a parallel 3-state system — mythology-tied resources aren't Blessing-locked (a Greek relic works for any Blessing on a Greek stage; this is the same principle applied to an enemy's own state). Cycles deterministically, one exclusively-eligible move per state.

| | |
|---|---|
| HP | 40 |
| Guard (eligible only in Mortal) | Block — gain 8 Block. Changes own Form to Beast. |
| Savage Claw (eligible only in Beast) | Attack — deal 12 damage. Changes own Form to Immortal. |
| Radiant Ward (eligible only in Immortal) | Block — gain 14 Block. Changes own Form to Mortal. |

Starts in Mortal (the default), so the opening move is always Guard — fully predictable from turn one for a player who's learned the cycle.

## Sanity Checks

**Deliberately small**: 4 enemies, not a full roster addition — same "minimal, not real content" sizing as the existing sample sets, proving the mechanism works before committing to full boss/enemy design.

**Not attempted**: numeric tuning, same standing caveat as every other numeric placeholder in this project.

**Deliberately out of scope**: the Core Combat Vocabulary mechanical redesign (a new mythology-specific debuff family) — per DifferentiationHooks' own recommended sequencing, that decision waits until these enemies (or later Hook 2 content) actually surface a need for one, rather than being invented speculatively alongside this pass.
