---
name: unity-wire-up
description: Perform Unity Editor setup for a feature through the Unity MCP tools — GameObjects, components, prefabs, ScriptableObjects, serialized references, layers, tags, input bindings — actually doing the wiring rather than only listing instructions, then verifying via console and Play Mode/tests.
argument-hint: <feature or object>
disable-model-invocation: true
---

# Unity Wire-Up

Perform the Unity Editor configuration required by:

> $ARGUMENTS

**Do the work, don't just describe it.** Use the Unity MCP tools to execute every supported Editor operation. Instructions to the user are the fallback for the unsupported remainder, not the default output.

## Before touching the Editor

1. Read `CLAUDE.md` and the `Docs/` material relevant to this feature.
2. Inspect the relevant scripts, prefabs, scenes, ScriptableObjects, and other assets in the repo (code and Unity YAML both) to learn what the code expects: required components, serialized field names and types, tag/layer strings, input action names.
3. Read `.claude/skills/unity-integration/SKILL.md` and use its inspection areas as the **authoritative checklist** for what "fully wired" means — lifecycle assumptions, serialized references, physics, input, events, the lot.
4. From that, write down the exact Editor configuration required before performing any of it: which scene(s), which GameObjects, which components, which values. Confirm the Unity editor is reachable via MCP; if it isn't, stop and say so — this skill cannot run without it.

## Performing changes

- Use the Unity MCP tools (primarily `mcp__unity-mcp__Unity_RunCommand` for Editor operations, `mcp__unity-mcp__Unity_GetConsoleLogs` for verification; scene captures where visual confirmation helps) to create or update GameObjects, components, prefabs, ScriptableObject assets, serialized references, layers, tags, and input bindings as required.
- **No C# without TDD.** If wiring reveals that a script change is needed (missing field, wrong type, missing component class), load the `tdd` skill (fallback: read `.claude/skills/tdd/SKILL.md`) and follow it fully before creating or modifying any C# — this skill grants no exemption.
- **Never guess a reference.** Assign an asset or object reference only after confirming it is the right one — by path, GUID, type, and its role in the code that consumes it. If two candidates are plausible and nothing in the repo or docs disambiguates, that is a stop condition, not a coin flip.
- **Never replace an existing reference without verifying its purpose.** If a field is already assigned, find out why before changing it; an existing assignment is evidence of intent.
- Save modified scenes and assets through the Editor after changes. Let Unity generate `.meta` files itself — never author them by hand; confirm they appeared for new assets.
- Prefer prefab edits over per-scene-instance edits when the object is prefab-backed, so changes propagate; call out any instance overrides you create.

## Verification

- Enter Play Mode via MCP when the change is observable there, or run the appropriate tests (per the TDD skill's running instructions) when the change is test-verifiable. Do at least one of these when possible; capture what was observed.
- Inspect the Unity console (`Unity_GetConsoleLogs`) for errors **and warnings** caused by the changes — missing-reference warnings and serialization complaints count as failures to fix, not noise.
- Fix configuration problems that can be safely resolved under the rules above; re-verify after fixing.
- Before finishing, review all Editor changes made this run (`git status` / `git diff` on scenes, prefabs, `.asset`, ProjectSettings, and new `.meta` files) and confirm each diff corresponds to an intended change — revert accidental ones. Do not commit unless the user asked.

## Stop conditions

Stop and ask the user **only** when a required Editor operation is unsupported by the available Unity MCP tools, or when the correct asset or object reference cannot be determined safely. When stopping, report everything already completed and the exact operation or decision that remains.

## Completion report

End with exactly these sections (use "none" where empty):

- **Scene changes performed** — per scene, what changed.
- **Prefab changes performed**
- **Components added or configured** — on which GameObjects.
- **Serialized references assigned** — field → value/asset, per object.
- **ScriptableObjects created or modified** — asset paths.
- **Layers, tags, or inputs changed**
- **Play Mode verification result** — what was run and observed, or why it wasn't possible.
- **Console errors or warnings** — remaining ones and their disposition.
- **Manual Editor work still required** — exact steps for anything MCP could not do.
