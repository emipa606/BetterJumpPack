using System.Reflection;
using HarmonyLib;
using Verse;

namespace betterJumpPack;

[StaticConstructorOnStartup]
public static class HarmonyPatches
{
    static HarmonyPatches()
    {
        new Harmony("com.yayo.BetterJumpPack").PatchAll(Assembly.GetExecutingAssembly());
    }
}