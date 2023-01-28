using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using UnityEngine;
namespace RJW_More_Genes
{
    public class RJWMGSettings : ModSettings
    {
        public static void DoWindowContents(Rect inRect)
        {
            //Copied from RJW settings mostly
            Rect outRect = new Rect(0f, 30f, inRect.width, inRect.height - 30f);
            Rect rect = new Rect(0f, 0f, inRect.width - 16f, inRect.height + 300f);
            //Widgets.BeginScrollView(outRect, ref RJWSettings.scrollPosition, rect, true);
            Listing_Standard listing_Standard = new Listing_Standard();
            listing_Standard.maxOneColumn = true;
            listing_Standard.ColumnWidth = rect.width / 2.05f;
            listing_Standard.Begin(rect);
            listing_Standard.Gap(24f);
            listing_Standard.CheckboxLabeled("gene pussyheal", ref gene_pussyheal, "disable the effects of the gene", 0f, 1f);
            listing_Standard.Gap(3f);
            listing_Standard.CheckboxLabeled("sexfrenzy", ref sexfrenzy, "disable the effects of the gene", 0f, 1f);
            listing_Standard.Gap(3f);
            listing_Standard.CheckboxLabeled("consensual_pussyheal", ref consensual_pussyheal, "pussyheal counts as consensual sex instead of rape", 0f, 1f);
            listing_Standard.End();
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<bool>(ref RJWMGSettings.gene_pussyheal, "gene_pussyheal", RJWMGSettings.gene_pussyheal, true);
            Scribe_Values.Look<bool>(ref RJWMGSettings.sexfrenzy, "sexfrenzy", RJWMGSettings.sexfrenzy, true);
            Scribe_Values.Look<bool>(ref RJWMGSettings.consensual_pussyheal, "consensual_pussyheal", RJWMGSettings.consensual_pussyheal, false);
        }
        public static bool gene_pussyheal = true;
        public static bool sexfrenzy = true;
        public static bool consensual_pussyheal = true;
    }
}
