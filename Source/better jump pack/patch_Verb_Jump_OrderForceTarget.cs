using HarmonyLib;
using RimWorld;
using Verse;

namespace betterJumpPack
{
    // 도착지점 강제 제한
    [HarmonyPatch(typeof(Verb_Jump), "OrderForceTarget")]
    public static class patch_Verb_Jump_OrderForceTarget
    {
        [HarmonyPostfix]
        private static bool Prefix(Verb_Jump __instance, LocalTargetInfo target)
        {
            var map = __instance.CasterPawn.Map;
            var cell = betterJumpPack.BestOrderedGotoDestNear_NewTemp(target.Cell, __instance.CasterPawn,
                AcceptableDestination);
            var job = JobMaker.MakeJob(JobDefOf.CastJump, cell);
            job.verbToUse = __instance;
            if (!__instance.CasterPawn.jobs.TryTakeOrderedJob(job))
            {
                return false;
            }

            FleckMaker.Static(cell, map, FleckDefOf.FeedbackGoto);

            bool AcceptableDestination(IntVec3 c)
            {
                return __instance.CanHitTargetFrom(__instance.caster.Position, c);
            }

            return false;
        }
    }
}