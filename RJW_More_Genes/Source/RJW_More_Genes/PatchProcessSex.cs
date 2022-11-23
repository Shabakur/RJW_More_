using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using rjw;
namespace RJW_More_Genes
{
    [HarmonyPatch(typeof(SexUtility), "ProcessSex")]
    class PatchProcessSex
    {
        [HarmonyPostfix]
        public static void ProcessSexGenes(SexProps props)
        {
            AbilityUtility.PussyHeal(props);
        }
    }
}
