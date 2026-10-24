using System.Linq;
using RimWorld;
using Verse;

namespace betterJumpPack;

[StaticConstructorOnStartup]
public static class BetterJumpPack_DefPatcher
{
    static BetterJumpPack_DefPatcher()
    {
        foreach (var t in DefDatabase<ThingDef>.AllDefs
                     .Where(thing => thing.Verbs is { Count: > 0 } && thing.Verbs[0].verbClass == typeof(Verb_Jump)))
        {
            // Preserve original behavior
            t.Verbs[0].requireLineOfSight = false;
        }
    }
}