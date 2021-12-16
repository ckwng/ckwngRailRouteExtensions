using Game.Contract;
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
    [HarmonyPatch(typeof(ContractController), "OfferFinalContract")]
    class Patch_OfferFinalContract
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var foundContractPeriod = false;
            var branchOverwritten = false;
            var contractPeriodField = AccessTools.Field(typeof(Contract), "Period");
            Label? label;
            foreach (var instruction in instructions)
            {
                yield return instruction;
                if (branchOverwritten)
                {

                }
                else if (!foundContractPeriod)
                {
                    if (instruction.LoadsField(contractPeriodField))
                    {
                        foundContractPeriod = true;
                        Debug.Log("[ckwng] foundContractPeriod");
                    }
                }
                else if (!branchOverwritten)
                {
                    if (instruction.Branches(out label))
                    {
                        yield return new CodeInstruction(OpCodes.Br, label.Value);
                        branchOverwritten = true;
                        Debug.Log("[ckwng] branchOverwritten");
                    }
                } 
            }
            if (!branchOverwritten)
            {
                Debug.Log("[ckwng] Transpiling OfferFinalContract() didn't work!");
            }
            else
            {
                Debug.Log("[ckwng] Transpiled OfferFinalContract().");
            }
        }
    }
}
