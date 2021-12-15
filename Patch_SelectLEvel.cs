using Game;
using Game.Level;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ckwng.RailRoute
{
    [HarmonyPatch(typeof(LevelController), "SelectLevel")]
    class Patch_SelectLevel
    {
        //public static ISet<string> longTrainLevels = new HashSet<string>(new string[] { "2d0e5b90-c99f-4e40-a3b2-4c323b8af1d3" });
        static void Postfix(LevelDefinition levelDefinition)
        {
            switch (levelDefinition.Uuid)
            {
                case "2d0e5b90-c99f-4e40-a3b2-4c323b8af1d3":
                    if (!ckwngLevelSettings.Active)
                    {
                        Debug.Log("Tokyo Megaloop loaded without extension");
                    }
                    break;
            }
        }
    }
}
