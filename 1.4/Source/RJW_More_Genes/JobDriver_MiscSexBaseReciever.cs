using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;
using Verse.AI;
using rjw;

namespace RJW_More_Genes
{
	//Made this quickly may have some mistakes
    public class JobDriver_MiscSexBaseReciever: JobDriver_SexBaseReciever
	{
		protected override IEnumerable<Toil> MakeNewToils()
		{
			base.setup_ticks();
			this.parteners.Add(base.Partner);
			this.FailOnDespawnedOrNull(this.iTarget);
			this.FailOn(() => !base.Partner.health.capacities.CanBeAwake);
			this.FailOn(() => this.pawn.Drafted);
			this.FailOn(() => base.Partner.Drafted);
			Toil toil = this.MakeSexToil();
			toil.FailOn(() => base.Partner.CurJob.def != JobDefOf.HealPussy); //or other jobs in the future
			yield return toil;
		

			yield break;
		}

		private Toil MakeSexToil()
		{
			Toil toil = new Toil();
			toil.defaultCompleteMode = ToilCompleteMode.Never;
			toil.socialMode = RandomSocialMode.Off;
			toil.handlingFacing = true;
			toil.tickAction = delegate ()
			{
				if (this.pawn.IsHashIntervalTick(this.ticks_between_hearts))
				{
					base.ThrowMetaIconF(this.pawn.Position, this.pawn.Map, FleckDefOf.Heart);
				}
			};
			toil.AddEndCondition(delegate
			{
				if (this.parteners.Count <= 0)
				{
					return JobCondition.Succeeded;
				}
				return JobCondition.Ongoing;
			});
			toil.AddFinishAction(delegate
			{
				if (xxx.is_human(this.pawn))
				{
					this.pawn.Drawer.renderer.graphics.ResolveApparelGraphics();
				}
				GlobalTextureAtlasManager.TryMarkPawnFrameSetDirty(this.pawn);
			});
			toil.socialMode = RandomSocialMode.Off;
			return toil;
		}
	}
}
