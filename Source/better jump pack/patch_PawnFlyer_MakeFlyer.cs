using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace betterJumpPack;

[HarmonyPatch(typeof(PawnFlyer), "MakeFlyer")]
public static class patch_PawnFlyer_MakeFlyer
{
    [HarmonyPostfix]
    private static bool Prefix(ref PawnFlyer __result, ThingDef flyingDef, Pawn pawn,
        IntVec3 destCell, EffecterDef flightEffecterDef, SoundDef landingSound, bool flyWithCarriedThing)
    {
        var tmp_pawnFlyer = (PawnFlyer)ThingMaker.MakeThing(flyingDef);

        if (!tmp_pawnFlyer.ValidateFlyer())
        {
            __result = null;
            return false;
        }

        if (destCell.Fogged(pawn.Map))
        {
            FloodFillerFog.FloodUnfog(destCell, pawn.Map);
        }

        // 출발시 지붕 파괴
        if (pawn.Position.Roofed(pawn.Map))
        {
            RoofCollapserImmediate.DropRoofInCells(pawn.Position, pawn.Map);
            if (pawn.Dead)
            {
                return false;
            }
        }

        tmp_pawnFlyer.startVec = pawn.TrueCenter();
        tmp_pawnFlyer.flightDistance = pawn.Position.DistanceTo(destCell);
        tmp_pawnFlyer.pawnWasDrafted = pawn.Drafted;
        tmp_pawnFlyer.flightEffecterDef = flightEffecterDef;
        tmp_pawnFlyer.soundLanding = landingSound;

        if (tmp_pawnFlyer.pawnWasDrafted)
        {
            Find.Selector.Deselect(pawn);
        }

        if (pawn.drafter != null)
        {
            tmp_pawnFlyer.pawnCanFireAtWill = pawn.drafter.FireAtWill;
        }

        if (pawn.CurJob != null)
        {
            if (pawn.CurJob.def == JobDefOf.CastJump)
            {
                pawn.jobs.EndCurrentJob(JobCondition.Succeeded);
            }
            else
            {
                pawn.jobs.SuspendCurrentJob(JobCondition.InterruptForced);
            }
        }

        tmp_pawnFlyer.jobQueue = pawn.jobs.CaptureAndClearJobQueue();
        if (flyWithCarriedThing && pawn.carryTracker.CarriedThing != null &&
            pawn.carryTracker.TryDropCarriedThing(pawn.Position, ThingPlaceMode.Direct, out tmp_pawnFlyer.carriedThing))
        {
            tmp_pawnFlyer.carriedThing.holdingOwner?.Remove(tmp_pawnFlyer.carriedThing);

            tmp_pawnFlyer.carriedThing.DeSpawn();
        }

        pawn.DeSpawn(DestroyMode.WillReplace);

        if (!tmp_pawnFlyer.innerContainer.TryAdd(pawn))
        {
            Log.Error($"Could not add {pawn.ToStringSafe()} to a flyer.");
            pawn.Destroy();
        }

        if (tmp_pawnFlyer.carriedThing != null && !tmp_pawnFlyer.innerContainer.TryAdd(tmp_pawnFlyer.carriedThing))
        {
            Log.Error($"Could not add {tmp_pawnFlyer.carriedThing.ToStringSafe()} to a flyer.");
        }

        __result = tmp_pawnFlyer;

        return false;
    }
}