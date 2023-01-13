using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using rjw;
using Verse;
namespace RJW_More_Genes
{
    //[HarmonyPatch(typeof(SexUtility), "ProcessSex")]
    class PatchProcessSex
    {
            
        //[HarmonyPostfix]
        public static void Postfix(SexProps props)
        {
            if(RJWMGSettings.gene_pussyheal)
            {
                AbilityUtility.PussyHeal(props);
                Log.Message("triggered");
            }    
        }
    }
}
