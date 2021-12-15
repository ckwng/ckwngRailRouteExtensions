using Game.Level;
using Game.LevelStorage;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ckwng.RailRoute
{
    [HarmonyPatch(typeof(Codec), "WriteLevelDefinition")]
    class Patch_WriteLevelDefinition
    {
        static void Postfix(StringBuilder output, LevelDefinition levelDefinition)
        {
            if (!ckwngLevelSettings.Active)
            {
                Debug.Log("[ckwng] Not saving extended level definition as not active");
                return;
            }
            if (ckwngLevelSettings.LevelUuid != levelDefinition.Uuid)
            {
                Debug.Log("[ckwng] Extended level settings has different Uuid than level being saved, aborting saving of extended level definition.");
                return;
            }
            var extensionOutput = JsonUtility.ToJson(ckwngLevelSettings.extensionDefinition);
            if (extensionOutput.StartsWith("{"))
            {
                extensionOutput = extensionOutput.Substring(1);
            }
            else
            {
                Debug.Log("[ckwng] Failed to save extended level definition, JsonUtility returned " + extensionOutput + " for extension");
                return;
            }
            var foundEndOfJsonObject = false;
            //Debug.Log("[ckwng] Looking for } in output of length " + output.Length + " until position " + (output.Length - 5));
            for (var i = output.Length - 1; i >= output.Length - 5; i--)
            {
                //Debug.Log("[ckwng] Looking for } in output at position " + i + " which is " + output[i]);
                if (output[i] == '}')
                {
                    output.Remove(i, output.Length - i);
                    foundEndOfJsonObject = true;
                    break;
                }
            }
            if (!foundEndOfJsonObject)
            {
                Debug.Log("[ckwng] Failed to save extended level definition, could not find } in output which was " + output);
                return;
            }
            output.Append(",");
            output.AppendLine(extensionOutput);
            Debug.Log("[ckwng] Saved extended level definition, output was " + output);
        }
    }
}
