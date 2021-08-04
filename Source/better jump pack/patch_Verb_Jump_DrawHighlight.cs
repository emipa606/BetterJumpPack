using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace betterJumpPack
{
    [HarmonyPatch(typeof(Verb_Jump), "DrawHighlight")]
    public static class patch_Verb_Jump_DrawHighlight
    {
        [HarmonyPostfix]
        private static bool Prefix(Verb_Jump __instance, LocalTargetInfo target)
        {
            var EffectiveRange = __instance.EquipmentSource.GetStatValue(StatDefOf.JumpRange);

            var t = __instance.caster;
            var m = t.Map;
            var cell = t.Position;
            var canJump = !cell.Roofed(m) || !cell.GetRoof(m).isThickRoof && !cell.GetRoof(m).isNatural;

            if (target.IsValid && Verb_Jump.ValidJumpTarget(__instance.caster.Map, target.Cell))
            {
                GenDraw.DrawTargetHighlightWithLayer(target.CenterVector3, AltitudeLayer.MetaOverlays);
            }

            GenDraw.DrawRadiusRing(__instance.caster.Position, EffectiveRange, Color.white
                , c => canJump && Verb_Jump.ValidJumpTarget(__instance.caster.Map, c));

            return false;
        }
    }
}