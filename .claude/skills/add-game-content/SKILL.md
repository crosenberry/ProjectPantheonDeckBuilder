---
name: add-game-content
description: Add repeatable game content — a card, relic, blessing, status effect, enemy, encounter, or similar — by cloning the closest completed example's pattern end to end (Core rules, tests, Unity adapter, data asset, registration) via the TDD loop and Unity MCP wiring. Never invents new architecture for content.
argument-hint: <content description>
disable-model-invocation: true
---

# Add Game Content

Create this content:

> $ARGUMENTS

Content work is **pattern-following work**. The architecture question was answered when the first card/relic/status/enemy was built; this skill's job is to make the Nth one identical in shape and correct in rules. Design docs in `Docs/` (GDD, `Docs/Characters/*.md` card drafts) are the rules source of truth.

## Workflow

1. Read `CLAUDE.md` and the design documents under `Docs/` that specify this content — for cards, find the exact entry in the character's card draft and treat its text as the spec.
2. Determine the content type (card, relic, blessing, status effect, enemy, encounter, other).
3. **Find the closest existing completed example** of that type in the codebase — same type first, then nearest mechanic (e.g. another Storm card for a new Storm card).
4. Inspect the example end to end: Core logic, its tests, Unity adapter, data asset (ScriptableObject), prefab, UI hookup, registration (pools/databases/encounter tables), and runtime wiring.
5. **Follow the example's pattern exactly** — same folders, naming, base types, test structure, registration path. Do not invent a new architecture, base class, or data format for content. If the existing pattern genuinely cannot express the new content's rules, stop and tell the user what's missing rather than working around it ad hoc.
6. **If no completed example of the type exists yet**, this is a first-of-its-kind feature, not content: follow `.claude/skills/feature-plan/SKILL.md` first to plan the content type's architecture, present that plan, and only then implement it (this becomes the reference example for all future content of the type).

## Implementation (TDD, mandatory)

- Load the `tdd` skill (fallback: read `.claude/skills/tdd/SKILL.md`) before creating or modifying any C#. Its rules are authoritative and unweakened here.
- Write tests for **all** rules of the content: every stated effect, edge cases, resource costs, targeting behavior, stacking behavior, and failure conditions (can't afford, invalid target, empty piles, zero/max stacks). Use seeded `IRandom` for anything random.
- Confirm red from a real run, implement the **minimum** Core behavior, confirm green on the full suite, refactor on green.
- Then create or update the Unity-facing pieces the pattern requires: data asset, adapter, prefab/UI hookup, registration.
- Use Unity MCP (`Unity_RunCommand` etc.) to create ScriptableObject assets, configure prefabs, assign serialized references, and register the content when possible — follow `.claude/skills/unity-wire-up/SKILL.md`'s rules for references (never guess, never blindly replace) and saving. Defer to the manual-work list only what MCP cannot do.

## Type-specific verification

Verify every applicable item for the content type; note each as tested, wired, or deferred:

**Cards** — cost; targeting; play validation; resolution order; draw/discard/exhaust/retain behavior; Blessing resource interactions (Volley/Storm/Scale/Form generation and spending); Enhancement interactions; card data asset and artwork placeholder; deck and reward-pool registration.

**Status effects** — application; stacking rules; duration; refresh behavior (re-application while active); trigger timing (which phase/hook); removal; cleanup on owner death/combat end; UI display; save compatibility.

**Enemies** — stats; intent selection logic; targeting; deterministic randomness (seeded, testable); interactions with statuses; death behavior (rewards, on-death triggers); encounter registration; prefab and presentation wiring.

**Other types** (relics, blessings, encounters, …) — derive the equivalent checklist from the closest example's tests and wiring, and say what you checked.

## Final validation

- Verify the content is **discoverable and usable at runtime**: registered where the game looks for it (pool/database/encounter table), and reachable — not just compiled.
- Run all relevant tests (full EditMode suite at minimum).
- Inspect the Unity console for errors and warnings tied to the new content.
- Review the final `git diff` (and untracked files) for unrelated changes; remove them. Do not commit unless asked.

## Completion report

End with exactly these sections (use "none" where empty):

- **Content created** — name, type, and the doc entry it implements.
- **Existing pattern followed** — which example was cloned, with paths.
- **Rules implemented** — each rule from the spec, one line each.
- **Tests added** — fixture and test names.
- **Confirmed red and green results** — counts from the actual runs.
- **Assets created or modified** — ScriptableObjects, prefabs, scenes, with paths.
- **Runtime registration completed** — where it was registered and how that was verified.
- **Unity Editor work completed** — what was done via MCP.
- **Placeholder assets still required** — art, audio, VFX, and where each plugs in.
- **Manual verification still required** — Play Mode checks or Editor steps left for the user.
