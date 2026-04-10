# GitHub Copilot Instructions for RimWorld Modding Project: (NWN) Real Fog of War (Continued)

## Mod Overview and Purpose

**(NWN) Real Fog of War (Continued)** is an updated version of the original mod by Luca De Petrillo. This mod introduces a dynamic Fog of War system to RimWorld, requiring exploration to uncover map areas and adding realistic line-of-sight mechanics. It enhances gameplay by simulating visibility limitations and encouraging strategic exploration and engagement. 

## Key Features and Systems

- **Dynamic Fog of War**: Initially, the map is unrevealed and must be explored. Visibility is governed by a Field of View system affecting both players and AI.
- **Vision Mechanics**: Humans, animals, and mechanoids have configurable sight ranges impacted by darkness and weather conditions. Bionic eyes reduce the darkness penalty.
- **Adaptive Field of View**: Characters have increased view while standing, attacking, or when possessing bionic enhancements.
- **Surveillance Equipment**: Includes cameras and watchtowers for enhanced visibility, with relevant research required.
- **Animal Integration**: Animals contribute to Fog of War detection if trained and assigned a master.
- **Expanded Options**: Integration with other mods for night vision, auditory detection for blind characters, and additional vision dynamics.
- **Performance Adjustments**: Various fixes and performance improvements have been integrated.
- **Delayed Alerts**: Optional feature that defers threat and event alerts until the player actually sees the entity causing the alert, creating a more immersive experience where pawns don't magically know about unseen threats.

## Coding Patterns and Conventions

- **Static Classes for Utility Functions**: Utility and helper methods are organized in public static classes, such as `BeautyUtility` and `Designation`.
- **Modularity via Component Pattern**: Implementation of `ThingSubComp` classes allows modular addition of functionality to game objects.
- **MapComponent Pattern**: Custom MapComponents like `PendingAlertManager` are automatically instantiated by RimWorld for each map.
- **Comprehensive Method Naming**: Methods are clearly named to indicate their purpose, e.g., `updatePosition`, `RevealCell`, `UpdateVisibility`.
- **Extension Methods**: Used extensively for clean API design, especially in `FoWThingUtils` for visibility checks and utility access.

## XML Integration

- The mod integrates XML data for defining new objects and configurations, providing compatibility with existing game items and mechanics.
- XML is used for defining research projects needed to unlock mod features like surveillance.
- Localization strings are maintained in `Languages/English/Keyed/Preference.xml` for all user-facing settings and descriptions.

## Harmony Patching

- **Harmony Library**: Extensively used to patch existing game methods, enhancing or altering default behaviors without directly modifying the original code.
- **Conditional Patching**: Maintained compatibility with multiple mods by conditionally applying patches only when necessary.
- **Message Interception**: The `Messages` detour patches `Verse.Messages.Message()` to intercept and defer alerts when appropriate conditions are met.
- **Example Patches**: Implementations like `HarmonyPatches.Patch_RegisterSustainer` for sound handling enhancements.

## Recent Additions: Delayed Alerts Feature

### Feature Overview
The **Delayed Alerts** feature (`DelayAlertsUntilSeen` setting) allows threat and event alerts to be deferred until the player actually sees the entity causing the alert. This creates immersion by preventing pawns from magically knowing about threats outside their vision.

### Implementation Details

**Core Components**:
1. **PendingAlertManager.cs** (MapComponent)
   - Automatically instantiated for each map by RimWorld
   - Manages queue of pending alerts awaiting visibility
   - Fires alerts when `thing.FowIsVisible()` returns true
   - Auto-cleans despawned/dead entities
   - Includes comprehensive debug logging

2. **Messages.cs Detour**
   - Patches `Verse.Messages.Message()` to intercept alerts
   - When alert would be hidden AND feature enabled:
     - Registers with `PendingAlertManager` instead of blocking
     - Stores complete alert info for later firing
   - Includes verbose logging for diagnostics

3. **FoWThingUtils.cs Extensions**
   - `FowIsVisible(this Thing)` - Checks if thing visible to player
   - `GetPendingAlertManager(this Map)` - Accessor for alert manager
   - All methods properly implemented as extension methods (NOT `extension()` blocks)

4. **RFOWSettings.cs**
   - New setting: `DelayAlertsUntilSeen` (bool, default false)
   - UI checkbox with description
   - Persisted via `ExposeData()`
   - Included in settings reset

5. **Preference.xml**
   - Localization for setting name and description
   - Setting explanation for user understanding

### How It Works

**Alert Flow**:
```
Threat occurs → Messages.Message() called
    ↓
Message_Prefix() checks conditions:
  - Is message important? (AcceptsMessage)
  - Is there a Thing target?
  - Is Thing hidden by FoW?
  - Is HideThreatBig enabled?
    ↓
IF all true AND DelayAlertsUntilSeen enabled:
  → PendingAlertManager.RegisterPendingAlert()
  → Block message (return false)
ELSE:
  → Allow message normally
```

**Letter/Slowdown Flow**:
```
Threat occurs → LetterStack.ReceiveLetter() called
    ↓
ReceiveLetterPrefix() checks conditions:
  - Is it a ThreatBig letter?
  - Is HideThreatBig enabled?
    ↓
IF true AND DelayAlertsUntilSeen enabled:
  → Store current game speed
  → PendingAlertManager.RegisterPendingLetter(letter, thing, storedSpeed)
  → Block letter and prevent slowdown (return false)
ELSE:
  → Allow letter through normally
```

**Visibility Check**:
```
Each frame: MapComponentTick()
  For each pending alert/letter:
    → Check if thing still spawned
    → Check if thing is dead
    → Check if thing.FowIsVisible() == true
      ↓
    IF visible:
      → Messages.Message() for alerts
      → LetterStack.ReceiveLetter() for letters (with forced pause/slowdown)
      → Clean up tracking
```

### Key Design Decisions

1. **MapComponent Pattern**: Ensures manager exists for every map without manual registration
2. **No Persistence**: Alerts not saved to allow clean behavior across save/load boundaries
3. **Debug Logging**: Comprehensive logging to diagnose interception, registration, and firing
4. **Minimal Overhead**: O(1) lookups, early exit if no pending alerts
5. **Integration**: Uses existing visibility systems (`FowIsVisible`) for consistency

## Suggestions for Copilot

- **Code Completion**: Assist in predicting mod-specific configurations and code patterns based on established design.
- **Refactoring Suggestions**: Recommend code improvements for performance optimization and adherence to C# best practices.
- **Error Handling Enhancements**: Suggest robust error handling mechanisms, particularly in areas prone to cross-mod interactions.
- **Documentation Generation**: Use AI-generated summaries and explanations for complex methods and integration strategies in the mod.
- **Feature Extension**: When extending alert systems, follow the MapComponent pattern and use extension methods for clean APIs.
- **Debug Support**: Utilize `RealFoWModStarter.LogMessage()` for diagnostic logging that respects Developer Mode setting.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The .github/copilot-instructions.md file is included in the solution under the .github solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution .github copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.

By adhering to these instructions and suggestions, contributors can effectively work with the existing codebase of the (NWN) Real Fog of War (Continued) mod, ensuring consistency and performance in the mod's ongoing development.

