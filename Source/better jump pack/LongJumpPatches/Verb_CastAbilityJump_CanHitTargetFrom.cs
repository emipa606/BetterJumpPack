using HarmonyLib;
using RimWorld;
using Verse;

namespace betterJumpPack;

[HarmonyPatch(typeof(Verb_CastAbilityJump), nameof(Verb_CastAbilityJump.CanHitTargetFrom))]
public static class Verb_CastAbilityJump_CanHitTargetFrom
{
    public static void Prefix(Verb_CastAbilityJump __instance, ref float ___cachedEffectiveRange)
    {
        var firstOrFallback = __instance.Ability?.VerbProperties?.FirstOrFallback();
        if (firstOrFallback?.range == null)
        {
            Log.Message("[betterJumpPack]: Something went wrong");
            return;
        }

        ___cachedEffectiveRange = firstOrFallback.range;
    }
}