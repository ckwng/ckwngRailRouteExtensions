using Game.Hud.LeftPanel;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ckwng.RailRoute
{
    [HarmonyPatch(typeof(StationsPanelItem), "InitializePlatformPanelStates")]
    class Patch_InitializePlatformPanelStates
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var found = false;
            foreach (var instruction in instructions)
            {
                if (!found && instruction.LoadsConstant(12))
                {
                    instruction.operand = 80;
                    found = true;
                }
                yield return instruction;
            }
            if (!found)
            {
                Debug.Log("ckwng Extensions: Unable to increase platform panel states size.");
            }
        }
    }
}
