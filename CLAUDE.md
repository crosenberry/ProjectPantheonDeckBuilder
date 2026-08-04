# Project Pantheon Deckbuilder

Unity 6000.3.20f1 (URP) roguelike deckbuilder. Design docs live in `Docs/` (start with `Docs/GameDesignDocument.md`); per-character card drafts are in `Docs/Characters/`.

## Non-negotiable: TDD for all code

Before writing or modifying ANY C# code, invoke the `tdd` skill and follow it: failing tests first (verified red), then the minimum implementation to pass (verified green), then refactor. This applies to every code change, including one-line fixes. Never write implementation code that no failing test has demanded, and never weaken a test to make it pass.

Architecture that follows from this (details in the skill): rules logic lives in the pure-C# `Pantheon.Core` assembly with injected randomness; MonoBehaviours/ScriptableObjects in `Pantheon.Unity` stay thin; tests live in `Assets/Tests/` (EditMode preferred over PlayMode).

## Non-negotiable: branch workflow

Never commit directly to `main` or `develop`. All work happens on a `feature/<short-name>` branch cut from `develop`:

1. Before starting a new feature (or before the first `implement-slice` of one), create `feature/<short-name>` from the current tip of `develop` — `git checkout develop && git pull && git checkout -b feature/<short-name>`.
2. Work the feature to green (one or more `tdd`/`implement-slice` cycles can happen on the same feature branch — branch per cohesive feature, not per slice).
3. Once all tests pass on the feature branch, merge it into `develop` (`git checkout develop && git merge --no-ff feature/<short-name>`), push `develop`, then delete the feature branch (local and remote).
4. `develop` is promoted to `main` only at a stable milestone the user calls out explicitly — never automatically.

Confirm the current branch (`git branch --show-current`) before writing code; if it's `main` or `develop`, stop and create the feature branch first rather than committing there. Only push directly, and only ever commit, when the user has asked for it — this rule governs *which branch*, not a standing permission to commit unprompted.
