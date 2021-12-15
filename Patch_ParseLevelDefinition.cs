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
    [HarmonyPatch(typeof(Codec), "ParseLevelDefinition")]
    class Patch_ParseLevelDefinition
    {
        static void Postfix(string definitionLine, LevelDefinition __result)
        {
            if (definitionLine.StartsWith("{"))
            {
                var def = JsonUtility.FromJson<ckwngSavedLevelDefinition>(definitionLine);
                ckwngLevelSettings.Load(__result.Uuid, def);
            }
        }
    }
}
