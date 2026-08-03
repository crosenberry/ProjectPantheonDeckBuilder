# Project Pantheon Deckbuilder

Unity 6000.3.20f1 (URP) roguelike deckbuilder. Design docs live in `Docs/` (start with `Docs/GameDesignDocument.md`); per-character card drafts are in `Docs/Characters/`.

## Non-negotiable: TDD for all code

Before writing or modifying ANY C# code, invoke the `tdd` skill and follow it: failing tests first (verified red), then the minimum implementation to pass (verified green), then refactor. This applies to every code change, including one-line fixes. Never write implementation code that no failing test has demanded, and never weaken a test to make it pass.

Architecture that follows from this (details in the skill): rules logic lives in the pure-C# `Pantheon.Core` assembly with injected randomness; MonoBehaviours/ScriptableObjects in `Pantheon.Unity` stay thin; tests live in `Assets/Tests/` (EditMode preferred over PlayMode).
