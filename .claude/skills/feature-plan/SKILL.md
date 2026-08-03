---
name: feature-plan
description: Read-only planning pass for a requested feature. Produces acceptance criteria, the smallest implementation approach that fits the existing architecture, affected files, ordered implementation slices, a test plan, and Unity Editor steps. Makes no file changes.
argument-hint: <feature description>
disable-model-invocation: true
allowed-tools: Read, Glob, Grep
---

# Feature Plan (read-only)

Produce an implementation plan for the feature described in the arguments:

> $ARGUMENTS

This is a **planning-only** pass. Do not write implementation code, do not create or modify any file, do not run anything that changes state. The deliverable is the plan itself, printed as your response.

## Investigation (do this before writing the plan)

1. Read `CLAUDE.md`, then the relevant documentation under `Docs/` — always `Docs/GameDesignDocument.md` sections that touch the feature, plus `Docs/DifferentiationHooks.md` and `Docs/Characters/*.md` when the feature involves those systems. Cite the specific doc sections the plan relies on.
2. Read `.claude/skills/tdd/SKILL.md` and treat its architecture rules as binding: rules logic in pure-C# `Pantheon.Core` (no UnityEngine, injected `IRandom`), thin adapters in `Pantheon.Unity`, tests in `Assets/Tests/EditMode` (preferred) or `Assets/Tests/PlayMode`. The plan must preserve this separation and must be executable via the TDD loop (tests first).
3. Inspect the existing codebase with Glob/Grep/Read: find similar or adjacent systems, naming conventions, folder layout, existing asmdefs, existing tests, prefabs, scenes, and ScriptableObjects that the feature touches or should imitate. If the project has no code yet in an area, say so explicitly rather than inventing "existing" systems.

## Plan requirements

- Restate the requested behavior as concrete, verifiable **acceptance criteria** — each one phrased so it can become a test or a manual Editor check. Where the request is ambiguous, state the interpretation you chose as an assumption rather than silently picking one.
- Identify everything affected: scripts, test fixtures, assemblies (asmdef changes), prefabs, scenes, ScriptableObjects, events, interfaces, and UI elements. Distinguish *created* from *modified*.
- Propose the **smallest implementation that fits the existing architecture**. No new managers, services, singletons, abstractions, event buses, or frameworks unless an acceptance criterion is impossible without one — and if you believe one is required, justify it in one sentence against the specific criterion that demands it.
- Divide the work into small implementation slices, each independently testable and shippable in one TDD cycle (red → green → refactor). Order them so each slice builds on green from the previous one. For each slice name the tests that define it.
- Call out anything that cannot be done in code and requires the Unity Editor: Inspector wiring, serialized fields, prefab/scene edits, asmdef meta generation, ScriptableObject asset creation.
- Surface assumptions, risks, and edge cases (including determinism/RNG, save-state, and null/empty-collection cases for Core logic).

## Output format

Print the plan with exactly these sections, in this order:

1. **Existing system summary** — what's already there (or explicitly absent), with file references.
2. **Acceptance criteria** — numbered, testable statements.
3. **Proposed approach** — the minimal design, and where each piece lives (Core vs Unity).
4. **Files affected** — two lists: create, modify. Include asmdef and test files.
5. **Implementation slices in order** — numbered; per slice: goal, tests that define it, files touched.
6. **Test plan** — EditMode vs PlayMode split, key fixtures/helpers, seeded-RNG strategy where relevant.
7. **Unity Editor and Inspector steps** — manual steps the user (or a later session) must perform in the Editor.
8. **Risks and assumptions** — including open questions worth confirming before slice 1.

End the response with the plan. Do not begin implementation, do not offer to "go ahead and start" — the user will invoke the work separately.
