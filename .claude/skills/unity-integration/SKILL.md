---
name: unity-integration
description: Review a Unity feature as a complete runtime integration — lifecycle, scene/prefab/serialized wiring, input, physics, events, performance, save/load — not just as C# source. Reports what is done, what needs manual Editor work, what needs Play Mode verification, and likely integration defects, ending with an exact Unity Editor checklist.
argument-hint: <feature or change>
disable-model-invocation: true
---

# Unity Integration Review

Review this feature or change as a **complete runtime integration**:

> $ARGUMENTS

C# that compiles and passes EditMode tests is not the bar. The question is: when this runs in a real scene, with real prefabs, real serialized references, and real player input, does it actually work? Review from the repository's evidence — code, `.unity` scenes, `.prefab` files, `.asset` ScriptableObjects, `.inputactions`, `ProjectSettings/` (TagManager, InputManager, Physics settings), asmdefs, and `.meta` files.

**This is a review, not a fix pass.** Do not modify any file unless the invocation text above explicitly asks for fixes. If it does, review first, report in full, then fix only the identified issues (loading the `tdd` skill before touching any C#).

## What to inspect

Work through each area below that applies to the feature; explicitly skip-with-reason ones that don't:

- **MonoBehaviour lifecycle** — correctness of `Awake` / `Start` / `OnEnable` / `OnDisable` / `OnDestroy` usage: initialization order dependencies, work done in the wrong phase, behavior across disable/re-enable cycles, domain-reload assumptions.
- **Core/Unity separation** — rules logic that has leaked into `Pantheon.Unity` MonoBehaviours, or `UnityEngine` types that have leaked into `Pantheon.Core` (see `.claude/skills/tdd/SKILL.md` for the binding rules).
- **Serialized references** — every `[SerializeField]`/public field: is it assigned somewhere findable in a scene/prefab/asset in the repo, or does it rely on Inspector wiring that hasn't happened?
- **Required GameObjects and components** — what the code assumes exists (`RequireComponent`, `GetComponent` targets, expected children/parents) versus what scenes and prefabs actually contain.
- **Prefab, scene, and ScriptableObject dependencies** — assets the feature needs, whether they exist, and whether their serialized data matches what the code expects.
- **Input System bindings** — actions/maps referenced in code versus what `Assets/InputSystem_Actions.inputactions` defines; subscription and unsubscription to input events.
- **Tags, layers, layer masks, collision matrix** — string tags and layer names used in code versus `ProjectSettings/TagManager.asset`; layer-mask math; physics collision-matrix expectations.
- **Colliders and Rigidbodies** — required physics components, trigger vs collision, 2D vs 3D API consistency, kinematic assumptions.
- **Event subscriptions and cleanup** — every subscribe (C# events, UnityEvents, input callbacks, static events) paired with an unsubscribe in the mirroring lifecycle phase; leaked handlers on destroyed objects.
- **Object creation, ownership, lifetime, destruction** — who Instantiates, who Destroys, orphaned references after destruction, DontDestroyOnLoad implications, scene-reload behavior.
- **Null-reference risks** — unassigned serialized fields, destroyed-but-referenced objects, `GetComponent` results used unchecked, execution-order races.
- **Update placement** — logic in `Update` vs `FixedUpdate` vs `LateUpdate`: physics in the right loop, camera/follow logic in `LateUpdate`, anything per-frame that should be event-driven.
- **Time.timeScale behavior** — does the feature behave correctly when paused or time-scaled (`deltaTime` vs `unscaledDeltaTime`, coroutine wait types, animation update modes)?
- **Per-frame allocations** — allocations in `Update`-family methods, LINQ/closures/string concatenation in hot paths, `Camera.main` per frame.
- **Repeated lookups** — `GetComponent`, `Find`, `FindObjectOfType`, scene searches inside loops or per-frame code that should be cached.
- **Save/load implications** — state the feature adds: does it need persisting, does it survive scene reload, does it interact with existing save code?
- **Editor-only vs runtime code** — `UnityEditor` usage properly fenced (`#if UNITY_EDITOR` / Editor folders / editor asmdefs) so builds compile.
- **Project conventions** — consistency with existing naming, folder layout, and patterns in the repo.

**Do not invent configuration.** If a prefab, scene wiring, or Inspector value cannot be confirmed by reading repository files, report it as unverified/needs-manual-work — never assume it was set up. Unity YAML is readable: check scenes and prefabs directly for the relevant components and serialized fields (GUIDs in the YAML map to assets via their `.meta` files).

## Report structure

Separate findings into exactly these four groups:

1. **Changes already completed in project files** — confirmed by repository evidence, with file references.
2. **Changes that must be performed manually in Unity** — Editor-only work not yet done.
3. **Items that require Play Mode verification** — things that cannot be confirmed statically, with what to look for.
4. **Potential integration defects** — likely bugs, ordered by severity, each with the concrete failure scenario (what happens at runtime and when).

## Unity Editor checklist (end with this)

Finish with an exact, follow-along checklist the user can execute in the Editor. Include, where applicable: scene names; GameObject names (and where in the hierarchy); component names to add; serialized fields and the exact values or asset references to assign; prefabs involved; tags and layers to create or assign; input actions to add or rebind; and numbered Play Mode verification steps with expected observable results. Omit categories that genuinely don't apply rather than padding them. If nothing manual remains, say so explicitly.
