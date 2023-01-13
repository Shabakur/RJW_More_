using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse.Sound;
using Verse;
using RimWorld;
using rjw;
using rjw.Modules.Interactions.Helpers;
using rjw.Modules.Interactions.Enums;

namespace RJW_More_Genes
{
    public class AbilityUtility
    {
        public static void PussyHeal(SexProps props)
        {
            if (InteractionHelper.GetWithExtension(props.dictionaryKey).DominantHasFamily(GenitalFamily.Vagina) || InteractionHelper.GetWithExtension(props.dictionaryKey).SubmissiveHasFamily(GenitalFamily.Vagina))
            {
                Pawn pawn = props.pawn;
                Pawn partner = props.partner;
                FloatRange tendQualityRange;
                tendQualityRange.min = 0.4f;
                tendQualityRange.max = 0.8f;
                if (GeneUtility.isPussyHealer(pawn))
                {
                    Heal(partner, tendQualityRange);
                }
                if (GeneUtility.isPussyHealer(partner))
                {
                    Heal(pawn, tendQualityRange);
                }
            }
        }

        public static bool Heal(Pawn pawn, FloatRange tendQualityRange)
        {
            bool any_wound_tended = false;
            List<Hediff> hediffs = pawn.health.hediffSet.hediffs;
            for (int i = hediffs.Count - 1; i >= 0; i--)
            {
                if ((hediffs[i] is Hediff_Injury || hediffs[i] is Hediff_MissingPart) && hediffs[i].TendableNow(false))
                {
                    hediffs[i].Tended(tendQualityRange.RandomInRange, tendQualityRange.TrueMax, 1);
                    any_wound_tended = true;
                }
            }
            return any_wound_tended;
        }

        //Modified copies of jobdriver_sex functions
        public static void Orgasm(Pawn pawn, int duration = 180, bool isrape = false)
        {
            pawn.stances.stunner.StunFor(duration, pawn, false, false);
            //pawn.pather.StopDead();
            PlayCumSound(pawn);
            FleckMaker.ThrowMetaIcon(pawn.Position, pawn.Map, FleckDefOf.Heart, 0.42f);
            SexUtility.CumFilthGenerator(pawn);
            //Add some sex satisfaction?
        }

        public static void PlayCumSound(Pawn pawn)
        {
            if (RJWSettings.sounds_enabled)
            {
                SoundInfo info = new TargetInfo(pawn.Position, pawn.Map, false);
                info.volumeFactor = RJWSettings.sounds_cum_volume;
                SoundDef.Named("Cum").PlayOneShot(info);
                ExtraCumSound(pawn, info);
            }
        }

        public static void ExtraCumSound(Pawn pawn, SoundInfo info)
        {
            if (ModsConfig.IsActive("Tory187.RJWAnimAddons.VoicePatch"))
            {
                if (pawn.gender == Gender.Female)
                {
                    SoundDef.Named("Voiceline_FGrunt").PlayOneShot(info);
                }
                if (pawn.gender == Gender.Male)
                {
                    SoundDef.Named("Voiceline_MGrunt").PlayOneShot(info);
                }

            }
            else
            {
                SoundDef.Named("Sex").PlayOneShot(info);
            }
        }

    }
}
