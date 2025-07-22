using HarmonyLib;
using RimWorld;
using Verse;

namespace betterJumpPack;

[HarmonyPatch(typeof(JumpUtility), nameof(JumpUtility.ValidJumpTarget))]
public static class JumpUtility_ValidJumpTarget
{
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
        Building_Door buildingDoor;
        __result = edifice == null || (buildingDoor = edifice as Building_Door) == null || buildingDoor.Open;
    }
}