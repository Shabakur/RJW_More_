using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace RJW_More_Genes
{
	public class CompProperties_AbilitySexFrenzy : CompProperties_AbilityEffect
	{
		public CompProperties_AbilitySexFrenzy()
		{
			this.compClass = typeof(CompAbilityEffect_SexFrenzy);
		}

		public float radius;
	}
}
