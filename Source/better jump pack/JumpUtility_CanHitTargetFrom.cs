using HarmonyLib;
using RimWorld;
using Verse;

namespace betterJumpPack;

[HarmonyPatch(typeof(JumpUtility), nameof(JumpUtility.CanHitTargetFrom))]
public static class JumpUtility_CanHitTargetFrom
{
    public static bool Prefix(ref bool __result, Pawn pawn, IntVec3 root, LocalTargetInfo targ, float range)
    {
        var pawnMap = pawn.Map;
        if (pawnMap == null)
        {
            return true;
        }

        var num = range * range;
        var cell = targ.Cell;
        if (!cell.IsValid || !cell.InBounds(pawnMap))
        {
            return true;
        }

        if (root.Roofed(pawnMap))
        {
            var roof = root.GetRoof(pawnMap);
            if (roof.isThickRoof || roof.isNatural || pawn.Map.Biome.inVacuum)
            {
                __result = false;
                return false;
            }
        }

        if (cell.Roofed(pawnMap))
        {
            var roof = cell.GetRoof(pawnMap);
            if (roof.isThickRoof || roof.isNatural || pawn.Map.Biome.inVacuum)
            {
                __result = false;
                return false;
            }
        }

        __result = pawn.Position.DistanceToSquared(cell) <= (double)num;

        return false;
    }
}