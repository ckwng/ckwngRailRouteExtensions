using Game;
using Game.Context;
using Game.Contract;
using Game.Controller;
using Game.Schedule;
using Game.Time;
using Game.Train;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ckwng.RailRoute
{
    [HarmonyPatch(typeof(ContractController), "ScheduleNextInstance")]
    class Patch_ScheduleNextInstance
    {
        static void SkipElapsedTrains(Contract contract, TimeSpan currentTime)
        {
            Debug.Log("[ckwng] Skipping elapsed trains.");
            TimeSpan elapsedTimeSincePrototype = currentTime - contract.Prototype.FirstScheduled.From;
            if (elapsedTimeSincePrototype < contract.Period)
            {
                Debug.Log("[ckwng] elapsedTimeSincePrototype was " + elapsedTimeSincePrototype.ToString() + ", no elapsed trains to skip.");
                return;
            }
            var quotient = (int)((elapsedTimeSincePrototype - contract.Period).Ticks / contract.Period.Ticks);
            contract.ScheduledCount += quotient;
            Debug.Log("[ckwng] elapsedTimeSincePrototype was " + elapsedTimeSincePrototype.ToString() + ", skipping " + quotient + " trains.");
        }

        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            var GetScheduledCount = AccessTools.PropertyGetter(typeof(Contract), "ScheduledCount");
            var foundFirstScheduledCount = false;
            var foundSecondScheduledCount = false;

            foreach (var instruction in instructions)
            {
                if (!foundSecondScheduledCount)
                {
                    if (!foundFirstScheduledCount)
                    {
                        if (instruction.Calls(GetScheduledCount))
                        {
                            foundFirstScheduledCount = true;
                        }
                    }
                    else if (instruction.Calls(GetScheduledCount))
                    {
                        foundSecondScheduledCount = true;
                        yield return new CodeInstruction(OpCodes.Ldarg_0);
                        yield return CodeInstruction.LoadField(typeof(AbstractController), "Deps");
                        yield return CodeInstruction.Call(typeof(IControllers), "get_GameControllers");
                        yield return CodeInstruction.Call(typeof(IGameControllers), "get_TimeController");
                        yield return CodeInstruction.Call(typeof(ITimeController), "get_CurrentTime");
                        yield return CodeInstruction.Call(typeof(Patch_ScheduleNextInstance), "SkipElapsedTrains");
                        yield return new CodeInstruction(OpCodes.Ldarg_1);
                    }
                }
                yield return instruction;
            }
            if (!foundSecondScheduledCount)
            {
                Debug.Log("[ckwng] Transpiling ScheduleNextInstance() didn't work!");
            } 
            else
            {
                Debug.Log("[ckwng] Transpiled ScheduleNextInstance() to call SkipElapsedTrains().");
            }
        }
    }
}
