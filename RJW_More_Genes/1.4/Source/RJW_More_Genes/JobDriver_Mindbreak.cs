using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;
using rjw;
using RimWorld;
using Multiplayer.API;
namespace RJW_More_Genes
{
    public class JobDriver_Mindbreak : JobDriver_SexBaseInitiator
    {
		//Slightly modifiied version of JobDriver_Masturbate from rjw
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return true;
        }

		[SyncMethod(SyncContext.None)]
		public void rapid_orgasms()
        {	
			this.ticks_left = this.duration;
			this.cycle++;
		}
		protected override IEnumerable<Toil> MakeNewToils()
        {
            this.setup_ticks();
			//this.duration = (int)(100 * Rand.Range(1.5f, 2.5f));
			//this.rapid_orgasms();
			Toil toil2 = Toils_General.Wait(this.max_duration, TargetIndex.None);
			toil2.handlingFacing = true;
			toil2.initAction = delegate ()
			{
				base.Start();
				//this.Sexprops.isRape = true;
			};
			toil2.tickAction = delegate ()
			{
				if (this.pawn.IsHashIntervalTick(this.ticks_between_hearts))
				{
					base.ThrowMetaIconF(this.pawn.Position, this.pawn.Map, FleckDefOf.Heart);
				}
				base.SexTick(this.pawn, null, true, true);
				SexUtility.reduce_rest(this.pawn, 1f);
				if (this.ticks_left <= 0)
				{
					Log.Message("cycle " + this.cycle);
					if ((this.pawn.story.traits.HasTrait(TraitDefOf.Masochist)))
                    {
						base.ReadyForNextToil();
					} 
					else
                    {
						this.ticks_left = this.duration;
						AfterSexUtility.processBrokenPawn(this.pawn);
                    }
				}
			};
			toil2.AddFinishAction(delegate
			{
				base.End();
			});
			yield return toil2;
			yield return new Toil
			{
				initAction = delegate ()
				{
					SexUtility.Aftersex(this.Sexprops);
				},
				defaultCompleteMode = ToilCompleteMode.Instant
			};
			yield break;
		}

		public int max_duration = 60000;
		public int cycle = 0;
		public int min_cycles = 5;
    }
}
