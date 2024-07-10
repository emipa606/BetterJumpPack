using HarmonyLib;
using RimWorld;
using Verse;

namespace betterJumpPack;

[HarmonyPatch(typeof(Verb_CastAbilityJump), nameof(Verb_CastAbilityJump.CanHitTargetFrom))]
public static class Verb_CastAbilityJump_CanHitTargetFrom
{
    private static void Prefix(Verb_CastAbilityJump __instance, ref float ___cachedEffectiveRange)
    {
        if (__instance.Ability?.VerbProperties?.FirstOrFallback()?.range == null)
        {
            Log.Message("[betterJumpPack]: Something went wrong");
            return;
        }

        ___cachedEffectiveRange = __instance.Ability.VerbProperties.FirstOrFallback().range;
    }
}