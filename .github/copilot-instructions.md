# GitHub Copilot Instructions for "(NWN) Real Fog of War (Continued)"

## Mod Overview and Purpose

"(NWN) Real Fog of War (Continued)" is an updated version of the original mod by Luca De Petrillo. It introduces a dynamic fog of war system to RimWorld, requiring players to explore the map actively. The mod adds strategic depth by incorporating line of sight and field of view mechanics, affecting both players and AI units. Enhancements in gameplay are provided by new objects, tweaks in vision settings, and various bug fixes.

## Key Features and Systems

1. **Fog of War Mechanics**: 
   - The entire map is initially hidden and must be explored.
   - Units reveal areas based on their field of view, which is influenced by attributes such as sight, weather, and lighting conditions.

2. **Field of View Dynamics**:
   - Sight ranges are adjusted for different activities like standing, moving, and attacking.
   - Animals participate in fog mechanics if specifically trained.

3. **Additional Objects**:
   - **Surveillance Cameras and Watchtowers**: Enhance visibility and provide strategic advantages in revealed areas.
   - **Bionic Eyes and Night Vision**: Improve vision under low-light conditions.

4. **Custom Settings**:
   - Adjustable vision range settings.
   - Options for integrating with other mods like night vision equipment from Vanilla Expanded.

5. **Mod Compatibility**:
   - Compatible with mods like "CAI 5000 - Advanced AI" and others, provided specific settings are adjusted.

## Coding Patterns and Conventions

1. **C# Practices**:
   - Class naming follows a `PascalCase` convention.
   - Methods such as `PostSpawnSetup`, `CompTick`, `DeSpawn` are used for lifecycle management of components and building objects.

2. **XML Usage**:
   - XML files define new game objects, map flags, and research projects.
   - Follow structured and descriptive XML definition patterns, with well-named tags as seen in various `ThingDef` and `ResearchProjectDef`.

## XML Integration

- XML defines crucial gameplay elements and mod defaults using tags like `<Defs>`, `<ResearchProjectDef>`, and `<ThingDef>`.
- Ensure XML paths are properly structured within the `Mods\NWNRealFogOfWar\1.6\Defs` directory for seamless integration.

## Harmony Patching

- Utilize the `brrainz.harmony` library for patching existing game methods.
- Maintain a clean separation of original game logic and Harmony prefixes or postfixes to avoid conflicts with other mods.

## Suggestions for Copilot

1. **Code Generation**:
   - Assist in creating structured class templates for new components or building types.
   - Generate helper methods for managing field of view calculations or vision settings.

2. **XML Definitions**:
   - Scaffold new XML files using existing patterns as a template.
   - Suggest additions to XML for newly proposed gameplay features.

3. **Harmony Patches**:
   - Recommend patch structure in terms of Prefix and Postfix methods.
   - Aid in debugging potential conflicts between Harmony patches.

## Recommendations and Known Issues

- Integrate mods that provide new building types or enhance tactical gameplay.
- Be cautious of potential lag introduced by mods interacting with aiming or fog management systems.
- Use RimSort to optimize load order for best performance.
- Report bugs using the designated Discord channel and avoid opening discussion threads on GitHub for technical support.

### Credit and License

- Original mod by Luca De Petrillo under Apache License 2.0.
- Contributions by SaberVS7, YAYO, and inbae are acknowledged.

End of document.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).

