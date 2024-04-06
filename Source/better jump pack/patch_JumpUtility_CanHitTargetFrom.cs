using HarmonyLib;
using RimWorld;
using Verse;

namespace betterJumpPack;

[HarmonyPatch(typeof(JumpUtility), nameof(JumpUtility.CanHitTargetFrom))]
public static class patch_JumpUtility_CanHitTargetFrom
{
    [HarmonyPostfix]
    private static bool Prefix(ref bool __result, Pawn pawn, IntVec3 root, LocalTargetInfo targ, float range)
    {
        var num = range * range;
        var m = pawn.Map;
        var cell = targ.Cell;

        __result = (!root.Roofed(m) || !root.GetRoof(m).isThickRoof && !root.GetRoof(m).isNatural) &&
                   pawn.Position.DistanceToSquared(cell) <= (double)num && (!cell.Roofed(m) ||
                                                                            !cell.GetRoof(m).isThickRoof &&
                                                                            !cell.GetRoof(m).isNatural);


        return false;
    }
}