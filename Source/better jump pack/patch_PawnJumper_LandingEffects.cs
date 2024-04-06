using HarmonyLib;
using RimWorld;
using Verse;

namespace betterJumpPack;

[HarmonyPatch(typeof(PawnFlyer), nameof(PawnFlyer.LandingEffects))]
public static class patch_PawnFlyer_LandingEffects
{
    [HarmonyPostfix]
    private static bool Prefix(PawnFlyer __instance)
    {
        var c = __instance.DestinationPos.ToIntVec3();

        var m = __instance.Map;
        if (c.Roofed(m))
        {
            RoofCollapserImmediate.DropRoofInCells(c, m);
        }


        return true;
    }
}