using HarmonyLib;
using RimWorld;

namespace betterJumpPack;

[HarmonyPatch(typeof(Verb_Jump), nameof(Verb_Jump.CanHitTargetFrom))]
public static class patch_Verb_Jump_CanHitTargetFrom
{
    [HarmonyPostfix]
    private static void Prefix(Verb_Jump __instance, ref float ___cachedEffectiveRange)
    {
        if (__instance.EquipmentSource?.def == null ||
            !__instance.EquipmentSource.def.StatBaseDefined(StatDefOf.JumpRange))
        {
            return;
        }

        ___cachedEffectiveRange = __instance.EquipmentSource.GetStatValue(StatDefOf.JumpRange);
    }
}