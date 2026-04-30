# GitHub Copilot Instructions for (NWN) Real Fog of War (Continued)

## Mod Overview and Purpose
**(NWN) Real Fog of War (Continued)** is an unofficial continuation of Luca De Petrillo's original mod for RimWorld. This mod brings a dynamic Fog of War mechanic into the game, enhancing the strategic depth and realism by requiring players to explore and reveal the map actively. It is designed to work with other mods, provided specific features are adjusted accordingly.

## Key Features and Systems
- **Dynamic Fog of War:** The entire map is initially hidden and must be uncovered through exploration by players and AI factions.
- **Field of View (FoV) Mechanics:** Different entities such as humans, animals, and mechanoids have specific FoVs affected by their attributes, environmental conditions, and activities.
- **Combat Visibility:** Visibility is crucial for ranged attacks, though mortars can target unrevealed areas.
- **Adjustable Vision Range:** Mod settings allow tweaks in vision range to suit different playstyles or companion mods.
- **Integration with Other Mods:** Features like night vision are supported by apparel from vanilla and extended mods.
- **Surveillance Enhancements:** Includes cameras and watchtowers to expand the FoV for strategic information gathering.
- **Clear Fog During Targeting:** An option to temporarily hide the fog of war while selecting a target location on the map, making it easier to choose landing sites for gravships and shuttles on fogged maps.
- **Customizable Features and Bugfixes:** Offers adjustable features, bug fixes, and performance improvements, integrating changes from forked versions.

## Coding Patterns and Conventions
- **Static Utility Classes:** Many functionalities are encapsulated within static utility classes for ease of access, such as `BeautyUtility.cs` and `GenView.cs`.
- **Internal and Public Accessibility:** Classes and methods are appropriately scoped to internal or public depending on whether they need to interact across different components or are isolated to specific functionalities.
- **Consistent Naming Conventions:** Classes and methods adhere to CamelCase naming conventions for identifiers, improving readability and consistency across the codebase.

## XML Integration
- XML files define data-driven components such as vision-related properties, ensuring easy customization through `CompProperties_AffectVision` and similar classes.
- XML integration ensures scalability and maintenance of features like adjustable variables and additional content support.

## Harmony Patching
- **Patch Structure:** Harmony patches extend or modify game functionality without altering the original codebase, like in `HarmonyPatches.cs`.
- **Example Usage:** Methods such as `Patch_RegisterSustainer` and `Patch_LetterStackReceiveLetter` enable modification of underlying game mechanics.
- **Prevent Conflicts:** Ensures compatibility with other mods by safe detours and prefix/postfix methods to avoid potential conflicts with the base game and other mods.

## Suggestions for Copilot
- **Class Suggestions:** Copilot can assist with generating new classes following the provided patterns, for example, creating utility functions in static classes where appropriate.
- **Method Implementations:** Use Copilot to generate method stubs for new functionalities, especially when adhering to current design patterns like static utility methods.
- **XML Data Tags:** Copilot can help draft XML entries for defining new mod features or adjusting existing ones based on existing templates.
- **Harmony Patch Templates:** Utilize Copilot to suggest boilerplate code for new Harmony patches, ensuring consistency in structuring modifications.
- **Code Comments and Documentation:** Encourage Copilot to add XML comments and inline documentation, maintaining a comprehensive understanding of logic across the codebase.

By adhering to these described patterns and leveraging suggestions effectively, developers can enhance and maintain the functionality of (NWN) Real Fog of War (Continued) efficiently.

This detailed instruction file should help guide developers and contributors in working with the mod, ensuring best practices in coding and modding principles.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.
