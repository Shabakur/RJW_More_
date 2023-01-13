using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace RJW_More_Genes
{
    public class HediffComp_OrgasmCycle : HediffComp
	{
		public HediffCompProperties_OrgasmCycle Props
		{
			get
			{
				return (HediffCompProperties_OrgasmCycle)this.props;
			}
		}

        public override void CompPostMake()
        {
            base.CompPostMake();
			this.Orgasm();
        }

        public override void CompPostTick(ref float severityAdjustment)
        {
			if (Find.TickManager.TicksGame >= this.tickNext)
            {
				this.Orgasm();
            }
			base.CompPostTick(ref severityAdjustment);
        }

		public void Orgasm()
        {
			AbilityUtility.Orgasm(this.Pawn);
			this.tickNext = Find.TickManager.TicksGame + (int)this.Props.intervalticks.RandomInRange;
        }


		public int tickNext;
	}
}
