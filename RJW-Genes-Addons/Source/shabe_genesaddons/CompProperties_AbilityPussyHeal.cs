using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace shabe_genesaddons
{
	// Token: 0x02000F2B RID: 3883
	public class CompProperties_AbilityPussyHeal : CompProperties_AbilityEffect
	{
		// Token: 0x06005C32 RID: 23602 RVA: 0x001F45C9 File Offset: 0x001F27C9
		public CompProperties_AbilityPussyHeal()
		{
			this.compClass = typeof(CompAbilityEffect_Coagulate);
		}

		// Token: 0x0400386B RID: 14443
		public FloatRange tendQualityRange;
	}
}
