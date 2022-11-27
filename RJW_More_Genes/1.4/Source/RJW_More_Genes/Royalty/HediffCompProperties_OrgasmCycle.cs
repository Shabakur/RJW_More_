using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
namespace RJW_More_Genes
{
    public class HediffCompProperties_OrgasmCycle : HediffCompProperties
    {
        public HediffCompProperties_OrgasmCycle()
        {
            this.compClass = typeof(HediffComp_OrgasmCycle);
        }
        public FloatRange intervalticks;
    }
}
