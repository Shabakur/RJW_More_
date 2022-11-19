﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using UnityEngine;
using RimWorld;

namespace shabe_genesaddons
{
    public class CompAbilityEffect_PussyHeal : CompAbilityEffect
    {
		private new CompProperties_AbilityCoagulate Props
		{
			get
			{
				return (CompProperties_AbilityCoagulate)this.props;
			}
		}
		public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
		{
			base.Apply(target, dest);
			Pawn pawn = target.Pawn;
			if (pawn == null)
			{
				return;
			}
			int num = 0;
			List<Hediff> hediffs = pawn.health.hediffSet.hediffs;
			for (int i = hediffs.Count - 1; i >= 0; i--)
			{
				if ((hediffs[i] is Hediff_Injury || hediffs[i] is Hediff_MissingPart) && hediffs[i].TendableNow(false))
				{
					hediffs[i].Tended(this.Props.tendQualityRange.RandomInRange, this.Props.tendQualityRange.TrueMax, 1);
					num++;
				}
			}
			if (num > 0)
			{
				MoteMaker.ThrowText(pawn.DrawPos, pawn.Map, "NumWoundsTended".Translate(num), 3.65f);
			}
			FleckMaker.AttachedOverlay(pawn, FleckDefOf.FlashHollow, Vector3.zero, 1.5f, -1f);
		}

		public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
        {
            Pawn pawn = target.Pawn;
            if (pawn != null)
            {
                AbilityUtility.ValidateHasTendableWound(pawn, throwMessages, this.parent);
            }
            return base.Valid(target, throwMessages);
        }
    }
}
