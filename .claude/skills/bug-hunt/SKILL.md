---
name: bug-hunt
description: Investigate a defect before changing anything — trace the execution path, rank root-cause hypotheses by evidence, reproduce when possible, then fix via a failing regression test and the full TDD loop. Fixes the underlying cause, never the symptom.
argument-hint: <bug description>
disable-model-invocation: true
---

# Bug Hunt

Investigate and fix this defect:

> $ARGUMENTS

**Investigation comes first. Do not change production behavior until the root cause is supported by evidence.** A plausible-sounding cause is not evidence; a stack trace, a failing test, a traced code path, or a reproduced result is.

## Phase 1 — Investigate (no production changes)

1. Read `CLAUDE.md` and any documentation under `Docs/` that defines the intended behavior — the design docs are the spec for what "correct" means here.
2. Inspect the relevant code (Glob/Grep/Read) and any existing tests covering it.
3. **Restate the expected behavior** — what should happen, citing the doc or code contract that says so.
4. **Restate the actual behavior** — what happens instead, as concretely as reported or observed.
5. Gather every available signal: reproduction steps from the user's description, logs (including Unity console via `mcp__unity-mcp__Unity_GetConsoleLogs` if the editor is open), error messages and stack traces, screenshots the user provided, and the current pass/fail state of existing tests. If reproduction steps are missing and you cannot derive them, ask before guessing.
6. **Trace the execution path** involved in the defect — walk the actual call chain from trigger to symptom, with `file:line` references, noting where state diverges from expectation.
7. **List possible root causes ranked by evidence**, strongest first. For each: what evidence supports it, what evidence would rule it out.
8. **Separate verified facts from hypotheses** explicitly — never present an inference as an observation. Label them.
9. **Reproduce the problem when possible** — ideally as a minimal failing scenario (this often becomes the regression test). If the defect only manifests in Play Mode or with specific scene state, say so and note what reproduction requires.

If investigation shows the reported bug is actually intended behavior, or the root cause lies outside the code (asset misconfiguration, missing Inspector wiring), stop and report that instead of writing code.

## Phase 2 — Fix (TDD, only once the root cause has evidence)

- Before creating or modifying any C# code, load the `tdd` skill (Skill tool; fallback: read `.claude/skills/tdd/SKILL.md`) and follow it. It is **mandatory and authoritative** — no exceptions, no summaries, no weakening.
- **Add a failing regression test that reproduces the defect** before implementing the fix. Its failure message must demonstrate the reported symptom or the diverging state you traced. Confirmed red is the proof the root-cause diagnosis is right — if the test you expected to fail passes, the diagnosis is wrong; return to Phase 1.
- **Fix the smallest underlying cause.** Do not mask the symptom (no null-check band-aids over an object that shouldn't be null, no clamping outputs whose inputs are wrong, no catching exceptions that shouldn't be thrown).
- Run the complete red → green → refactor workflow per the TDD skill: confirmed red, minimal fix, full suite green, refactor only on green.
- Remove temporary logging, debug prints, and diagnostic scaffolding added during investigation before finishing. Keep a diagnostic only if it has permanent value, and say so.
- **Check nearby code for the same defect pattern** (same misuse, same wrong assumption elsewhere). Report what you find, but do **not** automatically modify unrelated code — fixing siblings is a separate decision for the user.
- Review the final `git diff` for anything unrelated to the fix.

## Final report

End with exactly these sections:

- **Expected behavior**
- **Actual behavior**
- **Root cause**
- **Evidence supporting the root cause** — facts, not restated hypotheses.
- **Regression test added** — name and what it pins down.
- **Confirmed red result** — from the actual pre-fix run, with the failure message.
- **Files changed** — clickable paths.
- **Why the fix works** — mechanism, not just "test passes now".
- **Confirmed green result** — full-suite counts from the actual post-fix run.
- **Remaining risks** — including any sibling occurrences of the pattern found but not touched.
- **Unity Editor verification still required** — Play Mode checks or Inspector work needed to confirm the fix in the running game, or "none".
