# GitHub Copilot Instructions for RimWorld Mod Development

## Mod Overview and Purpose
The "Better Jump Pack" mod enhances the functionality of jump pack abilities in RimWorld, offering players more intuitive and versatile ways to utilize their jump packs during gameplay. The mod aims to provide more realistic physics and mechanics around jumping and landing, increasing both the strategic and immersive aspects of using jump packs.

## Key Features and Systems
- **Targeting System Enhancements**: Implements more accurate targeting functionality, allowing players to clearly identify feasible jump destinations.
- **Jump Mechanics Overhaul**: Enhances jumping mechanics with realistic arc calculations ensuring jumps follow plausible paths.
- **Landing Effects**: Introduces varied landing effects based on terrain and jump conditions to add depth to the gameplay experience.

## Coding Patterns and Conventions
- **Static Utility Classes**: Utilize static classes for jump mechanics to ensure consistency across the mod.
  - Example classes: `Verb_Jump_CanHitTargetFrom`, `JumpUtility_CanHitTargetFrom`.
- **Modular Code Structure**: Encapsulate logic within small, purpose-focused static methods for clarity and ease of maintenance.
- **Clear Naming Conventions**: Methods and classes are explicitly named to describe their functionality, using a "Verb_Action_Target" format.

## XML Integration
- Ensure parameter changes and new definitions are loaded properly by integrating seamlessly with RimWorld's XML configuration files.
- Extend the game’s Defs by creating new XML files or appending to existing ones, such as `ThingDef` and `AbilityDef`, to include upgraded jump pack features.

## Harmony Patching
- Use Harmony to patch original RimWorld methods without modifying the core game logic directly.
- **Example Class**: `HarmonyPatches` contains Harmony patches required to override base game behaviors, allowing for enhanced jump mechanics and effects.

## Suggestions for Copilot
1. **Generating Target Selection Logic**:
   - Use Copilot to suggest target validation methods that consider line of sight and pathfinding constraints.
   
2. **Enhancing Landing Effects**:
   - Implement suggestions for varied landing effects based on environmental factors using `PawnFlyer_LandingEffects`.
   
3. **Expanding XML Definitions**:
   - Help generate XML entries to integrate new abilities or features more deeply into existing systems using RimWorld's modding framework.

4. **Patching Strategies**:
   - Propose optimized Harmony patches to intercept and modify methods related to jump pack usage, minimizing potential conflicts with other mods.

5. **Refactoring and Code Suggestions**:
   - Leverage Copilot for code refactoring suggestions to maintain high code quality and performance, particularly focusing on utility functions.

By utilizing GitHub Copilot effectively based on these guidelines, developers can streamline the process of expanding and maintaining the Better Jump Pack mod, enhancing gameplay while adhering to best practices in mod development.
