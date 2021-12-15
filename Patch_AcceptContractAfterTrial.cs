using Game.Context;
using Game.Contract;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ckwng.RailRoute
{
    [HarmonyPatch(typeof(ContractController), "AcceptContractAfterTrial")]
    class Patch_AcceptContractAfterTrial
    {
        public static void Prefix(ContractController __instance, Contract contract)
        {
            var currentTime = __instance.Deps.GameControllers.TimeController.CurrentTime;
            Debug.Log("[ckwng] Skipping elapsed trains.");
            TimeSpan elapsedTimeSincePrototype = currentTime - contract.Prototype.FirstScheduled.From;
            if (elapsedTimeSincePrototype < contract.Period)
            {
                Debug.Log("[ckwng] elapsedTimeSincePrototype was " + elapsedTimeSincePrototype.ToString() + ", no elapsed trains to skip.");
                return;
            }
            var quotient = (int)(elapsedTimeSincePrototype.Ticks / contract.Period.Ticks);
            contract.ScheduledCount += quotient;
            Debug.Log("[ckwng] elapsedTimeSincePrototype was " + elapsedTimeSincePrototype.ToString() + ", skipping " + elapsedTimeSincePrototype.Ticks / contract.Period.Ticks + " trains."
                + "\nScheduledCount: " + contract.ScheduledCount + ", contract.NextOffset: " + contract.NextOffset.ToString());
        }
    }
}
