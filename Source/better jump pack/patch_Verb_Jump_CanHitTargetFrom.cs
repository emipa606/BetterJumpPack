using HarmonyLib;
using RimWorld;
using Verse;

namespace betterJumpPack
{
    [HarmonyPatch(typeof(Verb_Jump), "CanHitTargetFrom")]
    public static class patch_Verb_Jump_CanHitTargetFrom
    {
        [HarmonyPostfix]
        private static bool Prefix(ref bool __result, Verb_Jump __instance, IntVec3 root, LocalTargetInfo targ)
        {
            var EffectiveRange = __instance.EquipmentSource.GetStatValue(StatDefOf.JumpRange);
            var num = EffectiveRange * EffectiveRange;
            var m = __instance.CasterPawn.Map;
            var cell = targ.Cell;

            __result = (!root.Roofed(m) || !root.GetRoof(m).isThickRoof && !root.GetRoof(m).isNatural) &&
                       __instance.caster.Position.DistanceToSquared(cell) <= (double) num && (!cell.Roofed(m) ||
                           !cell.GetRoof(m).isThickRoof && !cell.GetRoof(m).isNatural);


            return false;
        }
    }
}