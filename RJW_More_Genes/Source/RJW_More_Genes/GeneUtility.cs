using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;


namespace RJW_More_Genes
{
    public class GeneUtility
    {     
        public static bool isPussyHealer(Pawn pawn)
        {
            if (pawn.genes == null)
            {
                return false;
            }
            return pawn.genes.HasGene(GeneDefOf.rjw_genes_pussyhealer);
        }

        
    }
}
