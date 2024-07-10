using HarmonyLib;
using RimWorld;
using Verse;

namespace betterJumpPack;

[HarmonyPatch(typeof(Verb_CastAbilityJump), nameof(Verb_CastAbilityJump.CanHitTargetFrom))]
public static class patch_Verb_CastAbilityJump_CanHitTargetFrom
{
    [HarmonyPostfix]
    private static void Prefix(Verb_CastAbilityJump __instance, ref float ___cachedEffectiveRange)
    {
        if (__instance.Ability?.VerbProperties?.FirstOrFallback()?.range == null)
        {
            Log.Message("Something fucked up");
            return;
        }

        ___cachedEffectiveRange = __instance.Ability.VerbProperties.FirstOrFallback().range;
    }
}