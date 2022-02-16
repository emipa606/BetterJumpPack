using HarmonyLib;
using RimWorld;
using Verse;

namespace betterJumpPack;

[HarmonyPatch(typeof(Verb_Jump), "ValidJumpTarget")]
public static class patch_Verb_Jump_ValidJumpTarget
{
    [HarmonyPostfix]
    private static bool Prefix(ref bool __result, Map map, IntVec3 cell)
    {
        //if (!cell.IsValid || !cell.InBounds(map) || (cell.Impassable(map) || !cell.Walkable(map)) || cell.Fogged(map))
        if (!cell.InBounds(map) || cell.Impassable(map) || !cell.Walkable(map))
        {
            __result = false;
            return false;
        }

        var edifice = cell.GetEdifice(map);
        __result = edifice == null || !(edifice is Building_Door buildingDoor) || buildingDoor.Open;
        return false;
    }
}