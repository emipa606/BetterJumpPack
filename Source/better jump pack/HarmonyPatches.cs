using System.Reflection;
using HarmonyLib;
using Verse;

namespace betterJumpPack;

[StaticConstructorOnStartup]
public static class HarmonyPatches
{
    static HarmonyPatches()
    {
        var harmony = new Harmony("com.yayo.BetterJumpPack");
        harmony.PatchAll(Assembly.GetExecutingAssembly());
    }
}