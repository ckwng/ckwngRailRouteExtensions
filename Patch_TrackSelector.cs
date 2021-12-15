using Game.Hud;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ckwng.RailRoute
{
    [HarmonyPatch(typeof(TrackSelector), "Update")]
    class Patch_TrackSelector
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (var instruction in instructions)
            {
                yield return instruction;
            }
        }
    }
}
