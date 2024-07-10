using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace betterJumpPack;

[HarmonyPatch(typeof(Verb_Jump), nameof(Verb_Jump.DrawHighlight))]
public static class Verb_Jump_DrawHighlight
{
    private static bool Prefix(Verb_Jump __instance, LocalTargetInfo target)
    {
        if (__instance.EquipmentSource?.def == null ||
            !__instance.EquipmentSource.def.StatBaseDefined(StatDefOf.JumpRange))
        {
            return true;
        }

        var effectiveRange = __instance.EquipmentSource.GetStatValue(StatDefOf.JumpRange);


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