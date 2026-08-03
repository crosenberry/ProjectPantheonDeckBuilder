---
name: session-handoff
description: Produce a concise end-of-session handoff — objective, what got done, decisions, file/test/build state, Unity Editor work done and remaining, known issues, and a ready-to-paste prompt for the next Claude Code session. Read-only; modifies nothing.
argument-hint: ""
disable-model-invocation: true
allowed-tools: Read, Glob, Grep, Bash(git status:*), Bash(git diff:*), Bash(git log:*), Bash(git show:*), mcp__unity-mcp__Unity_GetConsoleLogs
---

# Session Handoff (read-only)

Produce a handoff for the next Claude Code session. **Modify no source files** — the deliverable is the handoff text itself, printed in the response.

## Gather state

- Review the current task and what actually happened this conversation: what was attempted, what succeeded, what was abandoned and why. The conversation is the primary source — git only shows the end state, not the reasoning.
- Read `CLAUDE.md` and the `Docs/` material relevant to the current work.
- Run `git status` (note untracked files), read the current `git diff` (staged and unstaged), and check recent relevant commits (`git log`) for where this session's work sits in history.
- Determine current test status and build/compile status from this session's actual runs (test output already in the conversation). If the Unity editor is open, `Unity_GetConsoleLogs` can confirm current compile state. If neither source is available, report status as "unknown — not run this session", never a guess.

## Handoff contents

Write it for a fresh session with zero context — no shorthand or codenames coined mid-conversation; spell out paths and system names. Concise but complete: every claim about state (tests green, editor wiring done) must come from something observed this session or verifiable in the repo, not assumed. Include exactly these sections:

- **Current objective** — the goal being worked toward, in one or two sentences.
- **What was completed** — behavior-level, with pointers to where.
- **Important implementation decisions** — choices the next session must not accidentally reverse, with the one-line why.
- **Files changed** — committed vs uncommitted, as paths.
- **Current test status** — counts from the last real run, and which suites.
- **Current build or compile status** — compiling cleanly / errors (quote them) / unknown.
- **Unity Editor changes already completed** — manual Editor work done this session.
- **Unity Editor changes still required** — outstanding Inspector/scene/prefab/asset work.
- **Known bugs or limitations** — anything discovered and deliberately left.
- **Uncommitted or experimental work** — what's in the working tree and whether it's keep, WIP, or discardable.
- **Exact recommended next step** — one concrete action, not a list of options.
- **Ready-to-paste prompt for the next session** — a fenced code block the user can paste verbatim. It should name the objective, the relevant files, the project skills to use (`/feature-plan`, `/implement-slice`, `/bug-hunt`, etc. as appropriate), and the recommended next step. Self-contained: assume the next session has read nothing but CLAUDE.md.

## CLAUDE.md recommendations

Do not edit `CLAUDE.md`. If this session surfaced something that belongs there, list it at the end under **Suggested CLAUDE.md additions** as exact text the user can add — but only for durable content: project rules, architecture decisions, naming conventions, or development requirements that will still be true in a month. Temporary task progress, current bug state, and in-flight work never go in CLAUDE.md — that's what this handoff is for. If nothing qualifies, omit the section.
