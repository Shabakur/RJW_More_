using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using UnityEngine;
namespace RJW_More_Genes
{
    public class RJWMGSettingController : Mod
    {
        public RJWMGSettingController(ModContentPack content) : base(content)
        {
            base.GetSettings<RJWMGSettings>();
        }

        public override string SettingsCategory()
        {
            return "RJW More Genes";
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            RJWMGSettings.DoWindowContents(inRect);
        }
    }
}
