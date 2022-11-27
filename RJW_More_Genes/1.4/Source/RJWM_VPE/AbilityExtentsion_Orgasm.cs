using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFECore.Abilities;
using Verse;
using RimWorld;
using RimWorld.Planet;
//using RJW_More_Genes;


namespace RJWMoreGenes_VPE
{
    public class AbilityExtentsion_Orgasm : AbilityExtension_AbilityMod
	{
		public override void Cast(GlobalTargetInfo[] targets, VFECore.Abilities.Ability ability)
		{
			base.Cast(targets, ability);
			foreach (GlobalTargetInfo globalTargetInfo in targets)
			{
				Pawn pawn = globalTargetInfo.Thing as Pawn;
				if(pawn != null && !pawn.Dead)
               {
					float num = stunduration * 60;
					RJW_More_Genes.AbilityUtility.Orgasm(pawn, (int)num);
				}
			}
		}
	
		public int stunduration;
	}
}
