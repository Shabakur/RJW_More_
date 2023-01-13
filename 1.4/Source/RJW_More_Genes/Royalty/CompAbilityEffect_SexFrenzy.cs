using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using rjw;
using Verse.AI;

namespace RJW_More_Genes
{
    public class CompAbilityEffect_SexFrenzy : CompAbilityEffect
    {
		private new CompProperties_AbilitySexFrenzy Props
		{
			get
			{
				return (CompProperties_AbilitySexFrenzy)this.props;
			}
		}
		public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
		{
			base.Apply(target, dest);
			foreach (Pawn pawn in AffectedPawns(target, this.parent.pawn.Map))
            {
				if (pawn != null)
                {
					pawn.health.AddHediff(HediffDefOf.SexFrenzy);
                }
            }

			foreach (Pawn pawn in AffectedPawns(target,this.parent.pawn.Map))
            {
				if(pawn == null || !xxx.can_rape(pawn, true)|| pawn.jobs.curJob.def.defName == "GettinRaped" || pawn.jobs.curJob.def.defName == "RapeRandom")
                {
					continue;
                }
				Pawn pawn2 = FindVictim(pawn);
				if (pawn2 == null)
                {
					continue;
                }
				pawn.pather.StopDead();
				pawn.jobs.StopAll();
				//pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.RandomRape, null, true, false, null, false, false, false);
				Job newJob = JobMaker.MakeJob(xxx.RapeRandom, pawn2);
				pawn.jobs.StartJob(newJob, JobCondition.InterruptForced, null, false, true, null, null, false, false, null, false, true);
			}
		}

        public override bool GizmoDisabled(out string reason)
        {
			reason = null;
			if (!RJWSettings.rape_enabled)
            {
				reason = "Rape is disabled";
				return true;
			}
			else if (!RJWMGSettings.sexfrenzy)
			{
				reason = "Disabled in modsettings";
				return true;
			}
			return false;
        }

		private IEnumerable<Pawn> AffectedPawns(LocalTargetInfo target, Map map)
		{
			foreach (Pawn pawn in map.mapPawns.AllPawns)
            {
				if (target.Cell.DistanceTo(pawn.Position) < this.Props.radius && pawn.RaceProps.Humanlike)
                {
					yield return pawn;
                }

			}
			//IEnumerator<Pawn> enumerator = null;
			yield break;
		}
		public override void DrawEffectPreview(LocalTargetInfo target)
		{
			GenDraw.DrawRadiusRing(target.Cell, this.Props.radius);
		}

		public Pawn FindVictim(Pawn pawn)
        {
			Map m = pawn.Map;
			IEnumerable<Pawn> source = from x in m.mapPawns.AllPawnsSpawned
									   where x != pawn && xxx.is_not_dying(x) && (xxx.can_get_raped(x) || (xxx.can_be_fucked(x) && x.health.hediffSet.HasHediff(HediffDefOf.SexFrenzy))) &&
									   !x.Suspended && !x.IsForbidden(pawn) && x.jobs.curJob.def.defName != "GettinRaped" && x.jobs.curJob.def.defName != "RandomRape" &&
									   IntVec3Utility.DistanceTo(pawn.Position, x.Position) < 15 && pawn.CanReserveAndReach(x, PathEndMode.Touch, Danger.Deadly, 1, 0, null, false) && SexAppraiser.would_fuck(pawn, x, false, true, false) > 0f
				select x;
			if (source != null)
			{
				return source.RandomElement();
			}
			else
            {
				return null;
            }
		}
	}
}
