using Game.Railroad.Sensor;
using Game.Train;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ckwng.RailRoute
{
    [HarmonyPatch(typeof(TwoSignalsSensor), "TriggerAutomation")]
    class Patch_TriggerAutomation
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var finishedScheduleRouteField = AccessTools.Field(typeof(StationSelector), "FinishedScheduleRoute");
            var trainTypeGetter = AccessTools.PropertyGetter(typeof(Train), "TrainType");
            var foundFinishedScheduleRoute = false;
            var complete = false;
            foreach (var instruction in instructions)
            {
                if (!complete)
                {
                    if (foundFinishedScheduleRoute)
                    {
                        if (instruction.Calls(trainTypeGetter))
                        {
                            complete = true; //start yielding again starting from the next instruction
                        }
                        continue; //don't yield the instruction
                    }
                    else if (instruction.LoadsField(finishedScheduleRouteField)) {
                        foundFinishedScheduleRoute = true;
                    }
                }
                yield return instruction;
            }
            if (!complete)
            {
                Debug.Log("[ckwng] Failed to transpile TriggerAutomation().");
            }
            else
            {
                Debug.Log("[ckwng] Transpiled TriggerAutomation().");
            }
        }
    }
}
