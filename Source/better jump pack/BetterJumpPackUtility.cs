using System;
using RimWorld;
using Verse;

namespace betterJumpPack;

public static class BetterJumpPackUtility
{
    public static IntVec3 BestOrderedGotoDestNear(IntVec3 root, Pawn searcher, Predicate<IntVec3> cellValidator = null)
    {
        var map = searcher.Map;

        if (predicate(root))
        {
            return root;
        }

        var radius = 1;
        var result = default(IntVec3);
        var highestCover = -1000f;
        var hasCover = false;
        var cellsInRadius = GenRadial.NumCellsInRadius(30f);

        do
        {
            var intVec = root + GenRadial.RadialPattern[radius];
            if (predicate(intVec))
            {
                var coverScore = CoverUtility.TotalSurroundingCoverScore(intVec, map);
                if (coverScore > highestCover)
                {
                    highestCover = coverScore;
                    result = intVec;
                    hasCover = true;
                }
            }

            if (radius >= 8 && hasCover)
            {
                return result;
            }

            radius++;
        } while (radius < cellsInRadius);

        return searcher.Position;

        bool predicate(IntVec3 c)
        {
            if (cellValidator != null && !cellValidator(c))
            {
                return false;
            }

            if (!map.pawnDestinationReservationManager.CanReserve(c, searcher, true) || !c.Standable(map))
            {
                return false;
            }

            var thingList = c.GetThingList(map);
            foreach (var thing in thingList)
            {
                if (thing is Pawn pawn &&
                    pawn != searcher &&
                    pawn.RaceProps.Humanlike &&
                    (
                        // Player pawn standing in player square
                        searcher.Faction == Faction.OfPlayer && pawn.Faction == searcher.Faction ||
                        // Non-player pawn in non-player pawn square
                        searcher.Faction != Faction.OfPlayer && pawn.Faction != Faction.OfPlayer))
                {
                    return false;
                }
            }

            return true;
        }
    }
}