using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace RJW_More_Genes
{
	public class CompProperties_AbilityOrgasm : CompProperties_AbilityEffect
	{
		public CompProperties_AbilityOrgasm()
		{
			this.compClass = typeof(CompAbilityEffect_Orgasm);
		}
	}
}
