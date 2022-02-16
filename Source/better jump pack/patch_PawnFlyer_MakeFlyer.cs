using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace betterJumpPack;

[HarmonyPatch(typeof(PawnFlyer), "MakeFlyer")]
public static class patch_PawnFlyer_MakeFlyer
{
    private static MethodInfo a_ValidateFlyer = AccessTools.Method(typeof(PawnFlyer), "ValidateFlyer");


    [HarmonyPostfix]
    private static bool Prefix(ref PawnFlyer __result, ThingDef flyingDef, Pawn pawn,
        IntVec3 destCell)
    {
        var tmp_pawnFlyer = (PawnFlyer)ThingMaker.MakeThing(flyingDef);

        if (!(bool)AccessTools.Method(typeof(PawnFlyer), "ValidateFlyer").Invoke(tmp_pawnFlyer, null))
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

        Traverse.Create(tmp_pawnFlyer).Field("startVec").SetValue(pawn.TrueCenter());
        Traverse.Create(tmp_pawnFlyer).Field("flightDistance").SetValue(pawn.Position.DistanceTo(destCell));
        Traverse.Create(tmp_pawnFlyer).Field("pawnWasDrafted").SetValue(pawn.Drafted);
        Traverse.Create(tmp_pawnFlyer).Field("pawnWasSelected").SetValue(Find.Selector.IsSelected(pawn));

        if (Traverse.Create(tmp_pawnFlyer).Field("pawnWasDrafted").GetValue<bool>())
        {
            Find.Selector.ShelveSelected(pawn);
        }

        Traverse.Create(tmp_pawnFlyer).Field("jobQueue").SetValue(pawn.jobs.CaptureAndClearJobQueue());
        pawn.DeSpawn();
        if (!Traverse.Create(tmp_pawnFlyer).Field("innerContainer").GetValue<ThingOwner<Thing>>().TryAdd(pawn))
        {
            Log.Error("Could not add " + pawn.ToStringSafe() + " to a flyer.");
            pawn.Destroy();
        }

        __result = tmp_pawnFlyer;


        return false;
    }
}