---
name: tdd
description: Mandatory test-driven development workflow for ALL code written in this project. Invoke BEFORE creating or modifying any C# code — gameplay systems, card logic, resources, combat, utilities, anything. Workflow - write failing tests first, verify they fail for the right reason, implement the minimum to pass, refactor green. Triggers on any request to implement, add, build, change, prototype, or fix code.
---

# TDD Workflow — Project Pantheon Deckbuilder

Every piece of code in this project is developed test-first. No implementation code is written until a failing test exists that specifies its behavior. This is not optional and does not have a "small change" exemption — a one-line fix still starts with a failing test that reproduces the problem.

## The Loop

1. **Spec as tests.** Before touching implementation, translate the requested behavior into a list of concrete test cases (happy path, edge cases, failure modes). For game rules, derive cases from the design docs in `Docs/` — cite the doc section in a comment above the test fixture, not per test.
2. **Write the failing tests.** Create test code plus the *minimal* stubs needed to compile (types, method signatures that `throw new System.NotImplementedException()`). Compile errors are not an acceptable "red" — the red must come from assertion failures or `NotImplementedException`, so the failure message proves the test exercises the right behavior.
3. **Run the tests and confirm red.** Actually run them (see "Running tests" below) and check each new test fails *for the expected reason*. A new test that passes immediately means it tests nothing — rewrite it. Report the red state to the user before moving on.
4. **Implement the minimum to go green.** Write only enough production code to pass the current tests. Resist speculative generality; if a behavior isn't pinned by a test, it doesn't get written.
5. **Run the tests and confirm green.** All tests, not just the new ones. Paste the pass/fail summary into your report.
6. **Refactor on green.** Clean up names, duplication, and structure with tests passing before and after. No behavior changes during refactor steps.

**Never weaken a test to make it pass.** If a test turns out to be wrong (misread spec, bad expectation), say so explicitly to the user, fix the test, and re-run red→green from step 3. Deleting or loosening assertions silently is forbidden.

## Architecture rules that make TDD possible

Game logic must be testable without booting a scene. Enforce this split:

- **`Pantheon.Core`** (`Assets/Scripts/Core/`) — pure C# rules engine: cards, decks, energy, Blessing resources (Volley/Storm/Scale/Form), combat resolution, enhancements, relics, run/map state. **No `UnityEngine` references** (`noEngineReferences: true`). All randomness behind an injectable interface (e.g. `IRandom` wrapping `System.Random`) so tests are deterministic with a seeded instance. Time, save IO, and anything else nondeterministic likewise injected.
- **`Pantheon.Unity`** (`Assets/Scripts/Unity/`) — MonoBehaviours, ScriptableObjects, UI, presentation. Thin adapters over `Pantheon.Core`; contains as little logic as possible, because logic here is expensive to test.
- **`Pantheon.Core.Tests`** (`Assets/Tests/EditMode/`) — EditMode NUnit tests against `Pantheon.Core`. This is where ~90% of tests live. Fast, no scene, no frames.
- **`Pantheon.PlayTests`** (`Assets/Tests/PlayMode/`) — PlayMode tests, only for behavior that genuinely needs the player loop (MonoBehaviour lifecycles, coroutines, scene wiring). Prefer moving logic into Core over writing a PlayMode test.

If a task tempts you to put rules logic in a MonoBehaviour, that's the signal to extract it into Core first, tests leading.

### asmdef templates (create on first use, alongside their folder)

`Assets/Scripts/Core/Pantheon.Core.asmdef`:
```json
{
    "name": "Pantheon.Core",
    "rootNamespace": "Pantheon.Core",
    "references": [],
    "noEngineReferences": true,
    "autoReferenced": true
}
```

`Assets/Scripts/Unity/Pantheon.Unity.asmdef`:
```json
{
    "name": "Pantheon.Unity",
    "rootNamespace": "Pantheon.Unity",
    "references": ["Pantheon.Core"],
    "autoReferenced": true
}
```

`Assets/Tests/EditMode/Pantheon.Core.Tests.asmdef`:
```json
{
    "name": "Pantheon.Core.Tests",
    "rootNamespace": "Pantheon.Core.Tests",
    "references": ["UnityEngine.TestRunner", "UnityEditor.TestRunner", "Pantheon.Core"],
    "includePlatforms": ["Editor"],
    "precompiledReferences": ["nunit.framework.dll"],
    "defineConstraints": ["UNITY_INCLUDE_TESTS"],
    "autoReferenced": false,
    "overrideReferences": true
}
```

`Assets/Tests/PlayMode/Pantheon.PlayTests.asmdef`: same shape as above but named `Pantheon.PlayTests`, referencing `Pantheon.Core` and `Pantheon.Unity`, **without** `includePlatforms` and without `UnityEditor.TestRunner`.

Do not create `.meta` files by hand — let the Unity editor generate them, then confirm they appeared before committing.

## Test style

- NUnit, one fixture per system under test: `CombatResolverTests`, `StormResourceTests`.
- Test names state behavior: `Method_Scenario_ExpectedOutcome` (e.g. `PlayCard_InsufficientEnergy_ThrowsAndHandUnchanged`) or plain behavioral sentences (`Storm_DischargesFullyWhenTriggered`). No `Test1`.
- Arrange/Act/Assert with a blank line between phases; no comments labeling the phases.
- One behavior per test. Multiple asserts are fine only when they verify one outcome (e.g. resulting state of one object). Use `Assert.That(...)` constraint syntax.
- Builders/factory helpers for common fixtures (`TestDecks.WithCards(...)`, seeded `IRandom`) live beside the tests; grow them as duplication appears, not preemptively.

## Running tests

**Preferred — Unity editor is open (usual case):** use the Unity MCP tools. Trigger a test run via `mcp__unity-mcp__Unity_RunCommand`, then read results/compile errors with `mcp__unity-mcp__Unity_GetConsoleLogs`. Always check console logs for compile errors first — a run that never compiled is not a red test.

**Fallback — editor closed:** run the Unity CLI in batch mode (this fails if the editor has the project open; check before using):

```powershell
& "C:\Program Files\Unity\Hub\Editor\6000.3.20f1\Editor\Unity.exe" `
  -batchmode -projectPath "c:\Users\caden\Project Pantheon Deckbuilder" `
  -runTests -testPlatform EditMode `
  -testResults "$env:TEMP\pantheon-test-results.xml" `
  -logFile "$env:TEMP\pantheon-test.log"
```

Wait for exit, then parse the results XML (`test-run` attributes `passed`/`failed`/`result`, and each failing `test-case`'s `failure/message`). Use `-testPlatform PlayMode` for PlayMode tests. Expect runs to take a few minutes on first import; use a generous timeout or run in background.

**Never claim red or green without a real run.** If neither path works (editor busy, CLI unavailable), stop and tell the user tests could not be executed — do not proceed to implementation on unverified tests.

## Reporting

Each TDD cycle's report to the user includes: the list of test cases written, the confirmed red output (counts + a representative failure message), the confirmed green output (total passed), and anything you refactored. If you had to change a test after writing it, call that out with the reason.
