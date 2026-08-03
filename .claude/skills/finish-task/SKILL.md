---
name: finish-task
description: Finalize completed work for commit without pushing — full test/compile/console validation, unity-integration and regression-check passes with blocking fixes, hygiene sweep for accidental or sensitive files, suggested commit message, and a session handoff. Commits only when the invocation includes the word "commit"; never pushes.
argument-hint: [task description] [commit]
disable-model-invocation: true
---

# Finish Task

Prepare the current work for commit. Optional task description (and optional `commit` keyword) from the invocation:

> $ARGUMENTS

**Never push.** Commit only if the invocation text above contains the word `commit`; otherwise this run stages nothing and commits nothing.

## Authoritative sub-skills

Read and follow these as authoritative for their phase (their "stop and wait" endings are superseded by this skill's flow; their quality rules are not): `.claude/skills/unity-integration/SKILL.md`, `.claude/skills/regression-check/SKILL.md`, `.claude/skills/session-handoff/SKILL.md`. Any C# fix made during finalization goes through `.claude/skills/tdd/SKILL.md` — finalization is not an exemption.

## Establish scope

1. Read `CLAUDE.md` and the `Docs/` material relevant to the completed work.
2. Review `git status` (including untracked files) and the complete diff (staged + unstaged).
3. Determine the intended task from the argument, the conversation, and the diff. If the diff contains work from clearly different tasks, say so — this skill finalizes one task, and unrelated changes go on the excluded list.
4. Detect files that don't belong in the commit: accidental edits, generated output, `Library/`/`Temp/`/`Logs/`/`obj/` artifacts, test-result XMLs, editor logs, scratch scripts, and anything unrelated to the task. Check `.gitignore` covers the generated categories; flag gaps.

## Validate

5. Run all EditMode tests.
6. Run PlayMode tests relevant to the changed work (skip-with-reason if none exist or none are relevant).
7. Verify Unity compilation is clean and inspect the console for errors **and warnings** tied to this work (`Unity_GetConsoleLogs` when the editor is open; otherwise the batch-mode path from the TDD skill).
8. Follow `unity-integration` against the completed feature. Fix blocking integration problems (C# fixes via the TDD loop; Editor wiring via MCP per `unity-wire-up` rules).
9. Follow `regression-check` against the complete diff. Fix blocking regressions the same way.
10. Rerun all affected tests after any fix; end on a real, fully green run or report honestly that you couldn't.
11. Confirm no tests were weakened, skipped (`[Ignore]`), or removed improperly anywhere in the diff — a changed assertion needs a matching intended behavior change, cited.
12. Confirm nothing sensitive or ephemeral will be committed: secrets/keys/tokens, absolute local paths, leftover debug logging, test-result files, build output, generated cache folders.

## Documentation and outputs

13. Update documentation **only** when the completed work changed durable behavior, setup instructions, architecture, or project rules — update the specific `Docs/` file or CLAUDE.md line affected, nothing speculative. Task progress never goes in docs.
14. Produce a suggested commit message: imperative subject line under ~65 chars, a short body stating the behavior change and anything a reviewer must know (test coverage, Editor work performed), following any commit conventions visible in `git log`.
15. Produce the session handoff by following `session-handoff` (its full section list), included in the report.

## If `commit` was included

1. Stage **only** the files related to the completed task (explicit paths, never `git add -A` / `git add .`).
2. Show the staged-file list (`git status`) in the response.
3. Create the commit with the suggested message.
4. **Do not push.**

Do not commit if the verdict below is "no" — report the blockers instead, even when `commit` was requested.

## Completion report

End with exactly these sections:

- **Task finalized** — what the task was, one or two sentences.
- **Files included** — belongs in the commit.
- **Files excluded** — detected but left out, each with the reason.
- **Test results** — EditMode and PlayMode counts from real runs.
- **Compilation result** — clean / errors (quoted) / not verified (why).
- **Unity integration result** — outcome and fixes applied.
- **Regression-check result** — verdict and fixes applied.
- **Documentation updated** — files touched, or "none needed".
- **Remaining manual checks** — anything only the user can verify.
- **Suggested commit message** — in a fenced block.
- **Safe to commit: yes or no** — "no" if any blocker remains, any test fails, or validation couldn't run.
- **Session handoff** — the full handoff per `session-handoff`.
