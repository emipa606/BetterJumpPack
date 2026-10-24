using HarmonyLib;
using RimWorld;
using Verse;

namespace betterJumpPack;

// 도착지점 강제 제한
[HarmonyPatch(typeof(Verb_Jump), nameof(Verb_Jump.OrderForceTarget))]
public static class Verb_Jump_OrderForceTarget
{
    public static bool Prefix(Verb_Jump __instance, LocalTargetInfo target)
    {
        var map = __instance.CasterPawn.Map;
        var cell = BetterJumpPackUtility.BestOrderedGotoDestNear(
            target.Cell,
            __instance.CasterPawn,
            acceptableDestination);
        var job = JobMaker.MakeJob(JobDefOf.CastJump, cell);
        job.verbToUse = __instance;
        if (!__instance.CasterPawn.jobs.TryTakeOrderedJob(job))
        {
            return false;
        }

        FleckMaker.Static(cell, map, FleckDefOf.FeedbackGoto);

        return false;

        bool acceptableDestination(IntVec3 c)
        {
            return __instance.CanHitTargetFrom(__instance.caster.Position, c);
        }
    }
}