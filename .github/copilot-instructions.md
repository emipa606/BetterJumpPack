# GitHub Copilot Instructions for Better Jump Pack (Continued) Mod

## Mod Overview and Purpose
The Better Jump Pack (Continued) mod is an update and continuation of YAYO's original Better Jump Pack mod. This mod enhances the functionality of jump packs in RimWorld, allowing players to perform various jumps that were previously impossible, such as crossing walls, breaking roofs, and jumping under roofs. The mod also introduces new limitations, such as preventing jumps to and from the mountain roof and under the mountain roof.

## Key Features and Systems
- **Wall Crossing**: Jump packs can now cross walls, adding a new dimension of movement and strategy to the game.
- **Roof Breaking**: Jumping under a roof will destroy the roof and take damage to the player, adding an element of risk and reward.
- **Mountain Restrictions**: Players cannot jump to or from the mountain roof or under the mountain roof, adding strategic depth to the gameplay.

## Coding Patterns and Conventions
The Better Jump Pack (Continued) mod follows best practices in C# and XML integration for RimWorld modding. Here are some key conventions and patterns used:

- **Harmony Patching**: The mod uses Harmony to patch existing RimWorld methods. This allows the mod to modify the behavior of vanilla game mechanics without altering the original codebase.
- **C# Structuring**: The C# code is organized into classes that handle specific aspects of the mod's functionality. For example, the `JumpUtility_ValidJumpTarget` class contains a postfix patch to validate jump targets, and the `Verb_CastAbilityJump_CanHitTargetFrom` class contains a prefix patch to modify jump targeting behavior.
- **XML Definitions**: XML files are used to define new or modified game elements, such as the mod's update information and patches.

## XML Integration
The mod utilizes XML files to define its behavior and integrate with the RimWorld game engine. Key XML files include:

- **UpdateInfo.xml**: Contains information about the mod, such as its version and update notes.
- **patches.xml**: Defines Harmony patches that modify the game's behavior.
- **About.xml**: Contains metadata about the mod, including its dependencies.

## Harmony Patching
The Better Jump Pack (Continued) mod uses Harmony to patch various methods in the RimWorld game engine. These patches are used to modify the behavior of jump packs and related game mechanics. For example:

- **Verb_CastAbilityJump_CanHitTargetFrom**: Contains a prefix patch that modifies the behavior of the `CanHitTargetFrom` method, allowing jump packs to cross walls and break roofs.
- **Verb_CastAbilityJump_DrawHighlight**: Contains a prefix patch that modifies the behavior of the `DrawHighlight` method, adding visual feedback for jump targets.
- **Verb_CastAbilityJump_OrderForceTarget**: Contains a prefix patch that modifies the behavior of the `OrderForceTarget` method, ensuring that jump targets are validated correctly.

## Suggestions for Copilot
When using GitHub Copilot with the Better Jump Pack (Continued) mod, consider the following suggestions:

- **Explore Harmony Patching**: Copilot can help you explore and understand Harmony patching, which is a core part of the mod's functionality. Use Copilot to generate or modify patches that alter game mechanics.
- **XML Defs Integration**: Copilot can assist you in creating or modifying XML definitions for new or modified game elements. Use Copilot to generate XML code that integrates seamlessly with the mod.
- **C# Structuring**: Copilot can help you organize and structure your C# code in a way that follows best practices for RimWorld modding. Use Copilot to generate or modify classes and methods that handle specific aspects of the mod's functionality.
- **Error Handling and Debugging**: Copilot can assist you in writing error handling and debugging code for the mod. Use Copilot to generate or modify code that logs errors and provides useful debugging information.

By following these guidelines and utilizing GitHub Copilot, you can contribute to the Better Jump Pack (Continued) mod and help create new and exciting variations of this popular RimWorld mod.

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
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

