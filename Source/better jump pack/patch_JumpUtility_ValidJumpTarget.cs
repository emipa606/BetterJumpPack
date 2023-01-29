using HarmonyLib;
using RimWorld;
using Verse;

namespace betterJumpPack;

[HarmonyPatch(typeof(JumpUtility), "ValidJumpTarget")]
public static class patch_JumpUtility_ValidJumpTarget
{
    [HarmonyPostfix]
    private static void Postfix(ref bool __result, Map map, IntVec3 cell)
    {
        if (__result)
        {
            return;
        }

        if (!cell.IsValid || !cell.InBounds(map))
        {
            return;
        }

        if (cell.Impassable(map) || !cell.Walkable(map))
        {
            return;
        }

        var edifice = cell.GetEdifice(map);
        Building_Door building_Door;
        __result = edifice == null || (building_Door = edifice as Building_Door) == null || building_Door.Open;
    }
}