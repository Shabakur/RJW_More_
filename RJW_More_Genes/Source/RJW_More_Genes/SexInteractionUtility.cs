using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using rjw;
using rjw.Modules.Interactions.Objects;
using rjw.Modules.Interactions.Helpers;
using rjw.Modules.Interactions.Enums;
using rjw.Modules.Interactions.Implementation;
using rjw.Modules.Interactions.Defs.DefFragment;
//using rjw.Modules.Interactions.Enums;

namespace RJW_More_Genes
{
    class SexInteractionUtility
    {
        public static List<InteractionDef> GenerateDomVaginaInteractionDefList(Pawn pawn, Pawn pawn2)
        {
			List<InteractionDef> list2 = new List<InteractionDef>();
			foreach (InteractionDef interactionDef in SexUtility.SexInterractions)
			{
				InteractionWithExtension withExtension = InteractionHelper.GetWithExtension(interactionDef);
				if((withExtension.DominantHasTag(GenitalTag.CanBePenetrated) || withExtension.DominantHasFamily(GenitalFamily.Vagina)) 
					&& LewdInteractionValidatorService.Instance.IsValid(interactionDef, pawn, pawn2))
                {
					//Log.Message(interactionDef.ToString());
					list2.Add(interactionDef);
                }
			}
			return list2;
		}

		public static List<InteractionDef> GenerateInteractionDefList(Pawn pawn, Pawn pawn2, CompProperties_SexInteractionRequirements sexpropsreq)
		{
			List<InteractionTag> tags = sexpropsreq.tags;
			InteractionRequirement dominantRequirement = sexpropsreq.dominantRequirement;
			InteractionRequirement submissiveRequirement = sexpropsreq.submissiveRequirement;
			List<InteractionDef> sexinteractions = SexUtility.SexInterractions;
			List<InteractionDef> list = new List<InteractionDef>();
			//List<InteractionDef> a = from interaction in sexinteractions
			//where InteractionHelper.GetWithExtension(interaction).DominantHasFamily(dominantRequirement.families.)
			//						 select interaction;
			foreach (InteractionDef interactionDef in SexUtility.SexInterractions)
			{
				if (!LewdInteractionValidatorService.Instance.IsValid(interactionDef, pawn, pawn2))
                {
					continue;
                }
				InteractionWithExtension withExtension = InteractionHelper.GetWithExtension(interactionDef);
				bool add_interaction = false;
				foreach (InteractionTag tag in tags)
                {
					if (withExtension.HasInteractionTag(tag))
                    {
						add_interaction = true;
                    }
                }
				if (!add_interaction)
                {
					continue; 
                }
				if (dominantRequirement != null)
				{
					foreach (GenitalFamily genitalFamily in dominantRequirement.families)
					{
						if (!withExtension.DominantHasFamily(genitalFamily))
						{
							add_interaction = false;

						}
					}
					if (!add_interaction)
					{
						continue;
					}
					foreach (GenitalTag tag in dominantRequirement.tags)
					{
						if (!withExtension.DominantHasTag(tag))
						{
							add_interaction = false;

						}
					}
				}
				if (submissiveRequirement != null)
				{
					foreach (GenitalFamily genitalFamily in submissiveRequirement.families)
					{
						if (!withExtension.SubmissiveHasFamily(genitalFamily))
						{
							add_interaction = false;

						}
					}
					if (!add_interaction)
					{
						continue;
					}
					foreach (GenitalTag tag in submissiveRequirement.tags)
					{
						if (!withExtension.SubmissiveHasTag(tag))
						{
							add_interaction = false;

						}

					}
				}
				if (add_interaction)
                {
					list.Add(interactionDef);
                }

			}
			return list;
		}

		public static SexProps GenerateSexProps(Pawn pawn, Pawn pawn2, CompProperties_SexInteractionRequirements sexpropsreq)
        {
			List<InteractionDef> interactionlist = GenerateInteractionDefList(pawn, pawn2, sexpropsreq);
			if (interactionlist == null)
			{
				return null;
			}
			//foreach(InteractionDef interaction in interactionlist)
            //{
			//	Log.Message(interaction.ToString());
            //}
			InteractionDef dictionaryKey = interactionlist.RandomElement();
			bool rape = InteractionHelper.GetWithExtension(dictionaryKey).HasInteractionTag(InteractionTag.Rape);
			SexProps sexProps = new SexProps();
			sexProps.pawn = pawn;
			sexProps.partner = pawn2;
			sexProps.sexType = SexUtility.rjwSextypeGet(dictionaryKey);
			sexProps.isRape = rape;
			sexProps.isRapist = rape;
			sexProps.canBeGuilty = false;
			sexProps.dictionaryKey = dictionaryKey;
			sexProps.rulePack = SexUtility.SexRulePackGet(dictionaryKey);
			return sexProps;
		}

		public static SexProps GenerateSexpropsDomVagina(Pawn pawn, Pawn pawn2)
        {
			List<InteractionDef> interactionlist = GenerateDomVaginaInteractionDefList(pawn, pawn2);
			if (interactionlist == null)
            {
				return null;
            }
			InteractionDef dictionaryKey = interactionlist.RandomElement();
			bool rape = InteractionHelper.GetWithExtension(dictionaryKey).HasInteractionTag(InteractionTag.Rape);
			SexProps sexProps = new SexProps();
			sexProps.pawn = pawn;
			sexProps.partner = pawn2;
			sexProps.sexType = SexUtility.rjwSextypeGet(dictionaryKey);
			sexProps.isRape = rape;
			sexProps.isRapist = rape;
			sexProps.canBeGuilty = false;
			sexProps.dictionaryKey = dictionaryKey;
			sexProps.rulePack = SexUtility.SexRulePackGet(dictionaryKey);
			return sexProps;
		}
    }
}
