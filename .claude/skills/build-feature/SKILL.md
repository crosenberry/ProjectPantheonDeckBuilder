---
name: build-feature
description: End-to-end feature orchestrator — plans via feature-plan, implements every slice via implement-slice and the TDD loop, wires the Unity side via MCP, verifies with unity-integration and regression-check, and fixes what they find. Runs autonomously; pauses only for genuine blockers.
argument-hint: <feature description>
disable-model-invocation: true
---

# Build Feature (orchestrator)

Complete this feature from planning through verification with as little user involvement as possible:

> $ARGUMENTS

## Authoritative sub-skills

This skill sequences the project's existing skills; it does not replace them. At each phase, read the sub-skill's `SKILL.md` and follow its instructions as authoritative for that phase's *conduct* (each one's "stop and wait for the user" endings are superseded by this orchestrator's continue-automatically rule; their quality and safety rules are not):

- `.claude/skills/feature-plan/SKILL.md` — planning
- `.claude/skills/implement-slice/SKILL.md` — per-slice implementation
- `.claude/skills/tdd/SKILL.md` — **mandatory before creating or modifying any C# code**, never weakened
- `.claude/skills/unity-integration/SKILL.md` — runtime-wiring verification
- `.claude/skills/regression-check/SKILL.md` — final diff review

## Workflow

**Phase 1 — Plan.** Read `CLAUDE.md` and the relevant `Docs/` files. Follow `feature-plan` to inspect the project and produce the plan, divided into small, independently testable slices. **Show the plan briefly** (acceptance criteria + slice list, not the full document), then **continue without waiting for approval** — unless a major design choice genuinely cannot be resolved from the design docs and existing code (see stop conditions).

**Phase 2 — Implement, slice by slice.** For each slice in order, follow `implement-slice`: scope statement, then the TDD loop per the `tdd` skill — failing tests written first, **confirmed red** from a real run, minimum implementation, **confirmed green** on the full suite, refactor on green. When a slice passes, continue automatically to the next; post a one-line progress note per slice, not a full report. Where Editor-side configuration is needed (assets, components, serialized wiring, settings), perform it via the Unity MCP tools when possible instead of deferring it to manual work.

**Phase 3 — Integration verification.** After all slices, follow `unity-integration` for the feature as a whole. Fix the integration problems it finds (through the TDD loop for any C# change; via MCP for Editor-side wiring), then re-verify what was fixed.

**Phase 4 — Final validation.** Run the complete test suite and inspect the Unity console for errors and warnings tied to this work. Follow `regression-check` against the complete git diff of the feature. Fix blocking regressions and rerun the affected verification (tests + the failed checks) until clean. Review the final diff for unrelated or accidental changes and remove them. Do not commit unless the user asked.

## Autonomy rules

Do not pause between normal steps — no "shall I proceed?", no waiting after showing the plan, no stopping between slices. Stop and ask the user **only** when:

- The design documents contain conflicting requirements (cite both passages).
- A major irreversible architecture choice is required and not resolvable from the existing project.
- A destructive action would delete existing work or data.
- Required credentials or external access are unavailable.
- Tests cannot be executed by any available path — never proceed on unverified red/green.
- Unity MCP is unavailable for a required Editor-only operation with no code-side alternative. (If the work can continue and the Editor step deferred, defer it to the manual-work list instead of stopping.)

When stopping, state precisely what is blocked, what was already completed, and the exact decision or action needed to resume.

## Standing constraints

- Do not create unnecessary managers, singletons, services, abstractions, or frameworks — a new one requires a one-sentence justification against a specific acceptance criterion.
- Preserve the `Pantheon.Core` / `Pantheon.Unity` architecture throughout: rules logic pure C# in Core with injected nondeterminism; Unity-side adapters thin.
- Never weaken, skip, or shortcut the TDD skill's rules to keep the pipeline moving; a slice that can't go red-then-green honestly is a blocker, not a formality.

## Completion report

End with exactly these sections:

- **Feature completed** — behavior-level statement of what now works.
- **Acceptance criteria satisfied** — each criterion from the plan with its status; call out any not met and why.
- **Implementation slices completed** — the list, in order, each with its test names.
- **Files created or changed** — clickable paths, created vs modified.
- **Confirmed test results** — full-suite counts from the final real run.
- **Unity Editor changes performed** — what was done via MCP.
- **Manual Unity work still required** — exact steps, or "none".
- **Regression-check result** — the verdict and any non-blocking findings left open.
- **Known limitations** — what the feature deliberately does not do yet.
- **Recommended next feature** — the natural next `/build-feature` or `/feature-plan` invocation.
