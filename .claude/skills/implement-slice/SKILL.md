---
name: implement-slice
description: Implement exactly one small vertical slice of functionality via the project's TDD workflow — scoped, tests-first, smallest change that fits the existing architecture, with a diff review and completion report. Invoke with the slice to implement (typically one slice from a /feature-plan output).
argument-hint: <specific implementation slice>
disable-model-invocation: true
---

# Implement Slice

Implement **only** this slice, nothing beyond it:

> $ARGUMENTS

## Before writing any code

1. Read `CLAUDE.md` and the documentation under `Docs/` relevant to this slice.
2. Inspect the existing code the slice touches or should imitate (Glob/Grep/Read): current implementations, tests, asmdefs, conventions.
3. If an approved feature plan exists for this work (from `/feature-plan` — check the conversation, or a plan file if the user points to one), read it and follow its acceptance criteria and slice boundaries.
4. **State the exact slice being implemented** — one or two sentences, concrete enough that "done" is unambiguous.
5. **State what related work will NOT be changed** — adjacent behaviors, planned later slices, and tempting cleanups that are out of scope for this invocation.
6. If the slice as requested is too large for one red→green→refactor cycle, divide it further, tell the user the division, and implement **only the first** smaller slice. Report the rest under "Recommended next slice".

## Implementation rules

- Before creating or modifying any C# code, load the `tdd` skill (Skill tool; if it cannot be loaded, read `.claude/skills/tdd/SKILL.md` directly) and follow it exactly. It is the **authoritative** testing and architecture workflow: failing tests first with a verified red run, minimum implementation to verified green, refactor on green. Do not duplicate, summarize, replace, weaken, or create exceptions to its rules — where this skill and the TDD skill appear to conflict, the TDD skill wins.
- Make the **smallest** production-code change that completes the slice. No speculative parameters, no unused hooks for future slices.
- Match existing naming, architecture, formatting, and file organization. New files go where the TDD skill's layout puts them.
- Preserve the `Pantheon.Core` / `Pantheon.Unity` separation: rules logic stays pure C# in Core with injected nondeterminism; Unity-side code stays a thin adapter.
- Do not create a new manager, service, singleton, abstraction, event bus, or framework when the existing architecture can already support the behavior.
- No unrelated cleanup, refactoring, comment editing, or formatting churn. Refactoring is allowed only within the TDD loop's green-refactor step and only on code this slice touched.

## Validation

- Run all tests and validation the TDD skill requires: confirmed red before implementing, confirmed full green after (all tests, not only the new ones). Never report red or green without a real run.
- Review the final `git diff` (and `git status` for untracked files) for accidental or unrelated changes — stray files, drive-by edits, editor-generated noise. Remove anything that isn't part of the slice before reporting. Do not commit unless the user asked.
- For Unity-specific work, identify what remains that code cannot do: prefab or scene edits, component attachment, serialized-field wiring, layer/tag setup, Input System actions, ScriptableObject asset creation, `.meta` generation, or other Inspector configuration.

## Completion report

End with exactly these sections:

- **Slice implemented** — what now works, in behavior terms.
- **Files changed** — created and modified, as clickable paths.
- **Why each file changed** — one line per file.
- **Confirmed red test result** — counts and a representative failure message from the actual pre-implementation run.
- **Confirmed green test result** — total passed from the actual post-implementation run.
- **Refactoring performed** — or "none".
- **Unity Editor work still required** — manual steps for the user, or "none".
- **Known limitations** — what this slice deliberately does not handle yet.
- **Recommended next slice** — the natural next `/implement-slice` invocation, phrased so it can be pasted directly.
