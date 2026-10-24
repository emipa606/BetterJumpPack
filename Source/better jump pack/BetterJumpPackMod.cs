using System.Reflection;
using HarmonyLib;
using Verse;

namespace betterJumpPack;

public class BetterJumpPackMod : Mod
{
    public BetterJumpPackMod(ModContentPack content) : base(content)
    {
        new Harmony("com.yayo.BetterJumpPack").PatchAll(Assembly.GetExecutingAssembly());
    }
}