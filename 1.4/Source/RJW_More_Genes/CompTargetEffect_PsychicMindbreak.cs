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
    public class CompTargetEffect_PsychicMindbreak : CompTargetEffect
    {
        public override void DoEffectOn(Pawn user, Thing target)
        {
            Pawn pawn = (Pawn)target;
            if (pawn.Dead)
            {
                return;
            }
            pawn.pather.StopDead();
            pawn.jobs.StopAll();
            //pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.RandomRape, null, true, false, null, false, false, false);
            Job newJob = JobMaker.MakeJob(JobDefOf.Mindbreak, pawn, null, pawn.Position);
            pawn.jobs.StartJob(newJob, JobCondition.InterruptForced, null, false, true, null, null, false, false, null, false, true);

        }
    }
}
