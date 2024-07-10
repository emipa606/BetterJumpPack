using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace betterJumpPack;

[HarmonyPatch(typeof(Verb_CastAbilityJump), nameof(Verb_CastAbilityJump.DrawHighlight))]
public static class Verb_CastAbilityJump_DrawHighlight
{
    private static bool Prefix(Verb_CastAbilityJump __instance, LocalTargetInfo target)
    {
        if (__instance.Ability?.VerbProperties?.FirstOrFallback()?.range == null)
        {
            Log.Message("[betterJumpPack]: Something went wrong");
            return true;
        }

        var effectiveRange = __instance.Ability.VerbProperties.FirstOrFallback().range;


        var t = __instance.caster;
        var m = t.Map;
        var cell = t.Position;
        var canJump = !cell.Roofed(m) || !cell.GetRoof(m).isThickRoof && !cell.GetRoof(m).isNatural;

        if (target.IsValid && JumpUtility.ValidJumpTarget(__instance.caster.Map, target.Cell))
        {
            GenDraw.DrawTargetHighlightWithLayer(target.CenterVector3, AltitudeLayer.MetaOverlays);
        }

        GenDraw.DrawRadiusRing(__instance.caster.Position, effectiveRange, Color.white
            , c => canJump && JumpUtility.ValidJumpTarget(__instance.caster.Map, c));

        return false;
    }
}