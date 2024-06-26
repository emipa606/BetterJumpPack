﻿using HarmonyLib;
using RimWorld;
using Verse;

namespace betterJumpPack;

[HarmonyPatch(typeof(PawnFlyer), nameof(PawnFlyer.MakeFlyer))]
public static class patch_PawnFlyer_MakeFlyer
{
    [HarmonyPostfix]
    private static bool Prefix(Pawn pawn, IntVec3 destCell)
    {
        if (destCell.Fogged(pawn.Map))
        {
            FloodFillerFog.FloodUnfog(destCell, pawn.Map);
        }

        // 출발시 지붕 파괴
        if (!pawn.Position.Roofed(pawn.Map))
        {
            return true;
        }

        RoofCollapserImmediate.DropRoofInCells(pawn.Position, pawn.Map);
        return !pawn.Dead;
    }
}