# Known Issues

Small, real issues found during implementation that were deliberately **not** fixed at the time — either out of scope for the slice that found them, too minor to justify a detour, or waiting on something else to land first. Distinct from the design-session log in `ImplementationRoadmap.md` (which records *decisions*): this is a scannable backlog of *defects*, so they don't just get buried in a commit message and forgotten.

Each entry: what it is, where it lives, why it's deferred, and what it would take to fix. Move an entry to "Fixed" (with the commit/slice that fixed it) rather than deleting it, so there's a record.

## Open

### Combat-node enemy alternation repeats within a stage

`RunController.BuildEnemies` picks between two enemy types via `nodeIndex % 2 == 0`. In the current `GreekStages.SampleStage()` / Norse-equivalent layout (Combat=0, Rest=1, Combat=2, Boss=3), both Combat nodes are at even indices, so they always roll the *same* enemy type instead of alternating — e.g. a Thor run currently fights Draugr Reaver twice, never Seidr Hexer, outside the boss fight. Affects both Greek and Norse rosters identically; not something the M5 Thor-wiring slice introduced, just the first time it was actually surfaced and verified live.

- **Found**: M5 wiring slice (`feature/thor-wiring`), while live-verifying Thor's Norse enemy roster.
- **Why deferred**: Cosmetic/variety issue, not a functional bug — combat still resolves correctly either way. Fixing it properly means either changing the alternation key (e.g. a running combat-node counter instead of raw node index) or changing node layout, and either touches `GreekStages`/`NorseEnemies` wiring that's due for a real rework once node counts/layouts stop being placeholders (`Docs/GameDesignDocument.md` §12's still-open node-count/layout items).
- **Fix scope**: small — likely a dedicated counter incremented per Combat-node visit rather than reusing the node's own index for alternation.

## Fixed

*(none yet — first entries will move here once resolved, with a link to the fixing commit/slice.)*
