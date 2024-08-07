﻿using HarmonyLib;
using RimWorld;
using Verse;

namespace betterJumpPack;

[HarmonyPatch(typeof(PawnFlyer), "LandingEffects")]
public static class PawnFlyer_LandingEffects
{
    private static bool Prefix(PawnFlyer __instance)
    {
        var c = __instance.DestinationPos.ToIntVec3();

        var m = __instance.Map;
        if (!c.Roofed(m))
        {
            return true;
        }

        // (Blame Thathitmann) only does roof collapse if it's not the devourer leap or fleshbeast explosion
        if (__instance.def != ThingDefOf.PawnFlyer_Stun && __instance.def != ThingDefOf.PawnFlyer_ConsumeLeap)
        {
            RoofCollapserImmediate.DropRoofInCells(c, m);
        }


        return true;
    }
}