using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFECore.Abilities;
using Verse;
using Verse.AI;
using RimWorld;
using RimWorld.Planet;
using rjw;
using RJW_More_Genes;
//using RJW_More_Genes;


namespace RJWMoreGenes_VPE
{
    public class AbilityExtentsion_Sexfrenzy : AbilityExtension_AbilityMod
	{
		public override void Cast(GlobalTargetInfo[] targets, VFECore.Abilities.Ability ability)
		{
			Log.Message("trigger");
			base.Cast(targets, ability);
			foreach (GlobalTargetInfo globalTargetInfo in targets)
			{
				foreach (Pawn pawn in AffectedPawns(globalTargetInfo.Cell, globalTargetInfo.Map))
				{
					if (pawn != null)
					{ 
						pawn.health.AddHediff(RJW_More_Genes.HediffDefOf.SexFrenzy);
					}
				}

				foreach (Pawn pawn in AffectedPawns(globalTargetInfo.Cell, globalTargetInfo.Map))
				{
					if (pawn == null || !xxx.can_rape(pawn, true) || pawn.jobs.curJob.def.defName == "GettinRaped" || pawn.jobs.curJob.def.defName == "RapeRandom")
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

		}

        public override bool IsEnabledForPawn(VFECore.Abilities.Ability ability, out string reason)
        {
			reason = null;
			if (!RJWSettings.rape_enabled)
			{
				reason = "Rape is disabled";
				return false;
			}
			else if (!RJWMGSettings.sexfrenzy)
			{
				reason = "Disabled in modsettings";
				return false;
			}
			return true;
		}

        private IEnumerable<Pawn> AffectedPawns(IntVec3 cell, Map map)
		{
			foreach (Pawn pawn in map.mapPawns.AllPawns)
			{
				if (cell.DistanceTo(pawn.Position) < this.abilityDef.radius)
				{
					yield return pawn;
				}

			}
			//IEnumerator<Pawn> enumerator = null;
			yield break;
		}

		public Pawn FindVictim(Pawn pawn)
		{
			Map m = pawn.Map;
			IEnumerable<Pawn> source = from x in m.mapPawns.AllPawnsSpawned
									   where x != pawn && xxx.is_not_dying(x) && (xxx.can_get_raped(x) || (xxx.can_be_fucked(x) && x.health.hediffSet.HasHediff(RJW_More_Genes.HediffDefOf.SexFrenzy))) &&
									   !x.Suspended && !x.IsForbidden(pawn) && x.jobs.curJob.def.defName != "GettinRaped" && x.jobs.curJob.def.defName != "RandomRape" &&
									   IntVec3Utility.DistanceTo(pawn.Position, x.Position) < 15 && pawn.CanReserveAndReach(x, PathEndMode.Touch, Danger.Deadly, 1, 0, null, false)
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
