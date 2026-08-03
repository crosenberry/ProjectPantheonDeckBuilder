---
name: regression-check
description: Pre-commit regression review of the current uncommitted changes — traces callers and consumers of everything changed, checks Core/Unity boundaries, lifecycle/event/serialization compatibility, and test coverage, runs the relevant tests, and ends with a safe-to-commit verdict. Makes no file modifications.
argument-hint: ""
disable-model-invocation: true
allowed-tools: Read, Glob, Grep, Bash(git status:*), Bash(git diff:*), Bash(git log:*), Bash(git show:*), Bash(git ls-files:*), mcp__unity-mcp__Unity_RunCommand, mcp__unity-mcp__Unity_GetConsoleLogs
---

# Regression Check (read-only)

Determine whether the current **uncommitted changes** could break existing behavior. This is a review: **make no file modifications**, stage nothing, commit nothing. The deliverable is the report.

## Establish what changed

1. Read `CLAUDE.md` and any `Docs/` material relevant to the systems being changed — the docs define intended behavior, which is what "regression" is measured against.
2. Run `git status` (including untracked files) and read the **complete** diff: `git diff` plus `git diff --staged`, and read new untracked files in full. Unity note: `.meta`, scene, prefab, and `.asset` changes are part of the change surface — review their YAML too, don't skip them as noise.
3. **Summarize the intended change** in a few sentences before hunting for problems — if the intent isn't inferable from the diff, say so.
4. Review every changed file, not a sample.

## Impact tracing

For each changed public method, event, interface, serialized field, data model, prefab, and ScriptableObject: find its callers and consumers (Grep across `Assets/`, including scene/prefab YAML references by GUID and by field name) and check each one still behaves correctly under the change. Renamed or removed serialized fields are a special hazard — existing scenes/prefabs/assets silently lose their data unless `[FormerlySerializedAs]` is used; flag every case.

## Checks to perform

Run through each, skip-with-reason if not applicable to this diff:

- **Core/Unity boundaries** — no new `UnityEngine` references in `Pantheon.Core`, no rules logic newly added to `Pantheon.Unity`, no asmdef reference changes that violate the dependency direction (Unity → Core, never the reverse).
- **Changed defaults and initialization** — altered field initializers, constructor defaults, serialized default values, or initialization order; who depended on the old values?
- **Null handling and exception paths** — newly possible nulls, removed guards, changed exception types or newly thrown exceptions that callers don't handle.
- **Event subscription and cleanup** — new subscriptions with missing unsubscription, changed subscription timing, handlers that can now fire on stale/destroyed objects.
- **MonoBehaviour lifecycle changes** — logic moved between Awake/Start/OnEnable/etc., changed execution-order assumptions, behavior across disable/re-enable.
- **Prefab and scene compatibility** — do existing scenes/prefabs still satisfy what the changed code expects (components present, fields assigned, hierarchy shape)?
- **Save-data compatibility** — changes to persisted types/fields: can existing saved data still load, and is missing/legacy data handled?
- **API and data-contract changes** — signature changes, renamed members, changed return semantics, serialization format changes; every consumer accounted for.
- **Test coverage of the changed behavior** — does an existing or changed test pin each behavioral change in this diff? Changed code with no covering test is a finding, not a footnote. Also flag tests that were modified to accommodate the change — verify the modification reflects a genuinely intended behavior change, not a weakened assertion.

## Run tests

Run the tests relevant to the changed code, and the full EditMode suite if it's cheap enough. Prefer the Unity MCP tools (trigger via `Unity_RunCommand`, read results and compile errors via `Unity_GetConsoleLogs` — check for compile errors first). If the editor is closed, the batch-mode CLI from `.claude/skills/tdd/SKILL.md` is the fallback (it may require a permission approval). If tests cannot be run at all, the report must say so and the verdict cannot be an unconditional "yes".

## Report format

Output exactly these sections:

- **Change summary**
- **Files reviewed** — complete list.
- **Checks performed** — which checks ran, which were skipped and why.
- **Test results** — actual counts from a real run, or "not run" with the reason.
- **Potential regressions** — each with the concrete break scenario and affected consumer, ordered by severity.
- **Missing tests** — changed behavior with no covering test.
- **Manual Unity checks** — anything only verifiable in the Editor or Play Mode.
- **Blocking issues** — must be fixed before commit.
- **Optional improvements** — worth doing, not blocking. Keep separate from blockers; do not inflate.
- **Safe to commit: yes or no** — one word plus at most one sentence of condition. "No" whenever there is any blocking issue, a failing test, or tests could not be run.
