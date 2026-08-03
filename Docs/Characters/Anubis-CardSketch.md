> Superseded by the full 75-card draft: [Anubis-FullCardDraft.md](Anubis-FullCardDraft.md). Kept here for history.

# Anubis — Card Sketch (Scale, light pass)

Purpose: quick check that Scale ([GameDesignDocument.md §3.3](../GameDesignDocument.md)) supports more than one build before going deep. Lighter than the [Artemis draft](Artemis-CardDraft.md).

Recap: **Scale** is a signed value (-5 to +5, Order/+ vs. Chaos/-), resets to 0 each combat. Cards push it as a side effect. Payoff cards read its *position*, not a stack count.

## Baseline (not archetype-specific)

- **Anoint** — Skill (Common), 1 Energy: Heal 3 HP. Scale +1.
- **Curse of the Damned** — Attack (Common), 1 Energy: Deal 6 damage. Apply 1 Weak. Scale -1.

## Path A — Balance-keeper (hover near 0)

- **Feather's Judgment** — Attack (Uncommon), 1 Energy: Deal 5 damage. If Scale is between -1 and +1, cleanse 1 debuff and heal 3.

## Path B — Reaper (push toward Chaos, -)

- **Devourer's Toll** — Attack (Rare), 2 Energy: Deal damage equal to 3× |Scale| to one enemy. If Scale is negative, also apply Vulnerable equal to |Scale|.

## Path C — Ascendant (push toward Order, +)

- **Rite of Ma'at** — Skill (Uncommon), 1 Energy: If Scale is 3 or higher, gain 10 Block and cleanse all debuffs.

## Findings

- Scale's bidirectionality generates 3 archetypes **for free**, straight out of the resource shape itself — Volley and Storm both needed a deliberately bolted-on "ignore the mechanic" path (Huntress, Unrelenting) to get build diversity; Anubis gets Balanced/Chaos/Order diversity just from the resource having two poles plus a middle. That's a structurally different — and cheaper — source of build diversity than the other two Blessings. Worth naming as a real distinction, not just noting it in passing: Anubis doesn't need an explicit 4th ignore-it path the way Artemis and Thor did, since going Chaos-only or Order-only already reads as "barely touching the balance half of the mechanic."
- Devourer's Toll (Reaper) has an obvious scaling ceiling question: |Scale| is capped at 5 by the resource's own definition (§3.3: -5 to +5), so this card's damage caps at 15 by design, not by accident — worth confirming that cap is the intended ceiling and not something that should be raised by relics/enhancements later.
- Only one card exists per path in this light pass, so it's not yet confirmed each pole has enough cards to be a *satisfying* full build, only that the resource shape supports 3 directions in principle.
