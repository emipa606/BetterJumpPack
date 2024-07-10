using HarmonyLib;
using RimWorld;
using Verse;

namespace betterJumpPack;

// 도착지점 강제 제한
[HarmonyPatch(typeof(Verb_CastAbilityJump), nameof(Verb_CastAbilityJump.OrderForceTarget))]
public static class Verb_CastAbilityJump_OrderForceTarget
{
    private static bool Prefix(Verb_CastAbilityJump __instance, LocalTargetInfo target)
    {
        var map = __instance.CasterPawn.Map;
        var cell = betterJumpPack.BestOrderedGotoDestNear_NewTemp(target.Cell, __instance.CasterPawn,
            AcceptableDestination);
        var job = JobMaker.MakeJob(JobDefOf.CastJump, cell);
        job.verbToUse = __instance;
        if (!__instance.CasterPawn.jobs.TryTakeOrderedJob(job))
        {
            Log.Message("[betterJumpPack]: Failed to do job.");
            return false;
        }

        FleckMaker.Static(cell, map, FleckDefOf.FeedbackGoto);

        return false;

        bool AcceptableDestination(IntVec3 c)
        {
            return __instance.CanHitTargetFrom(__instance.caster.Position, c);
        }
    }
}