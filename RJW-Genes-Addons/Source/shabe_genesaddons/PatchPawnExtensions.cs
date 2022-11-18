using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using rjw;
using Verse;
using RimWorld;
namespace shabe_genesaddons
{
    [HarmonyPatch(typeof(PawnExtensions), "RaceImplantEggs")]
    public static class PatchPawnExtensions
    {
        [HarmonyPostfix]
        public static void Postfix(Pawn pawn, ref bool __result)
        {
            if (!__result)
            {
                __result = GeneUtility.isInsectBreeder(pawn);
            }
        }
    }
}
