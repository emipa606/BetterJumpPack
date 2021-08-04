using HarmonyLib;
using RimWorld;
using Verse;

namespace betterJumpPack
{
    [HarmonyPatch(typeof(PawnJumper), "LandingEffects")]
    public static class patch_PawnJumper_LandingEffects
    {
        [HarmonyPostfix]
        private static bool Prefix(PawnJumper __instance)
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
}