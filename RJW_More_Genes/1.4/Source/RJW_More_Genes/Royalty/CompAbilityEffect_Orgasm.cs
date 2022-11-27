using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using Verse.AI;
namespace RJW_More_Genes
{
	public class CompAbilityEffect_Orgasm : CompAbilityEffect
	{
		private new CompProperties_AbilityOrgasm Props
		{
			get
			{
				return (CompProperties_AbilityOrgasm)this.props;
			}
		}

		public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
		{
			if (target.HasThing)
			{
				base.Apply(target, dest);
				Pawn pawn = target.Thing as Pawn;
				if (pawn.Dead)
				{
					return;
				}
				float num = this.parent.def.GetStatValueAbstract(StatDefOf.Ability_Duration, this.parent.pawn) * 60;
				Log.Message(num.ToString());
				AbilityUtility.Orgasm(pawn, (int)num);
			}
		}
	}
}

