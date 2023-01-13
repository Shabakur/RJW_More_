using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;
using rjw;
using RimWorld;
namespace RJW_More_Genes
{
    public class JobDriver_Mindbreak : JobDriver_SexBaseInitiator
    {
		//Slightly modifiied version of JobDriver_Masturbate from rjw
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return true;
        }

		protected override IEnumerable<Toil> MakeNewToils()
        {
            this.setup_ticks();
			List<Trait> list = new List<Trait>();
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
					Log.Message("cycle " + this.cycles);
					if ((this.pawn.story.traits.HasTrait(TraitDefOf.Masochist)))
                    {
						base.ReadyForNextToil();
					} 
					else
                    {
						this.ticks_left = this.duration;

						AfterSexUtility.processBrokenPawn(this.pawn, list);
						AfterSexUtility.processBrokenPawn(this.pawn, list);
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
		public int cycles = 1;
		public int min_cycles = 5;
    }
}
