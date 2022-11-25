using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using rjw;
using RimWorld;
using Verse;
namespace RJW_More_Genes
{
    //[HarmonyPatch(typeof(SexUtility), "ProcessSex")]
    class PatchProcessSex
    {
            
        //[HarmonyPostfix]
        public static void Postfix(SexProps props)
        {
            Log.Message("triggered");
            AbilityUtility.PussyHeal(props);
        }
    }
}
