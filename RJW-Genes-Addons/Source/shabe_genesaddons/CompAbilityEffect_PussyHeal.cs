using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using UnityEngine;
using RimWorld;
using rjw;
using rjw.Modules.Interactions.Helpers;

namespace shabe_genesaddons
{
    public class CompAbilityEffect_PussyHeal : CompAbilityEffect
    {
		private new CompProperties_AbilityPussyHeal Props
		{
			get
			{
				return (CompProperties_AbilityPussyHeal)this.props;
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
			this.AfterSex(this.parent.pawn, pawn);
			//FleckMaker.AttachedOverlay(pawn, FleckDefOf.FlashHollow, Vector3.zero, 1.5f, -1f);
		}

		public void AfterSex(Pawn pawn, Pawn target)
        {
			List<Hediff> hediffs = target.health.hediffSet.hediffs;
			for (int i = 0; i < hediffs.Count; i++)
			{
				if ((hediffs[i] is Hediff_Injury || hediffs[i] is Hediff_MissingPart) && hediffs[i].TendableNow(false))
				{
					target.needs.mood.thoughts.memories.TryGainMemory(ThoughtDefOf.Pussy_Healed, pawn, null);
					break;
				}
			}
			//InteractionHelper.GetWithExtension(dictionaryKey).DominantHasTag("CanBePenetrated")


		}

		public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
        {
			Pawn pawn = target.Pawn;
            if (pawn != null)
            {
				

				//to be replaced with severel checks to make it clear why target is unable to have sex
				if (!CasualSex_Helper.CanHaveSex(pawn))
				{
					if (throwMessages)
					{
						Messages.Message(pawn.Name + " is unable to have sex", pawn, MessageTypeDefOf.RejectInput, false);
					}
					return false;
				}
				Pawn parent = this.parent.pawn;
				if (parent == null || !Genital_Helper.has_vagina(parent))
				{
					if (throwMessages && parent != null)
					{
						Messages.Message(parent.Name + " has no vagina to use", pawn, MessageTypeDefOf.RejectInput, false);
					}
					return false;
				}
				AbilityUtility.ValidateHasTendableWound(pawn, throwMessages, this.parent);
				
            }
            return base.Valid(target, throwMessages);
        }
    }
}
