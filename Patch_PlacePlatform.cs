using Game.Board.Build;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ckwng.RailRoute
{
    [HarmonyPatch]
    class Patch_PlacePlatform
    {
        [HarmonyPatch(typeof(GameGrid), "PlacePlatform")]
        static void Postfix(GameGrid __instance, GridCell gridCell)
        {
            GridCell endOfPlatform = FindEndOfPlatform(__instance, gridCell, GridDirection.E);
            var platformNumCell = gridCell.Move(GridDirection.E).Move(GridDirection.E).Move(GridDirection.E);
            if ((platformNumCell.Symbol >= 'A' && platformNumCell.Symbol <= 'Z')
                || (platformNumCell.Symbol >= '\u00e0' && platformNumCell.Symbol <= '\u017f'))
            {
                Debug.Log("ckwng Extensions: PlacePlatform located station number instead of platform number");
            }
            else if (platformNumCell.Symbol == '_')
            {
                Debug.Log("ckwng Extensions: PlacePlatform located platform instead of platform number");
            }
            else if (platformNumCell.Symbol > ';')
            {
                platformNumCell.Symbol += (char)('\u2457' - ';');
            }
        }

        [HarmonyReversePatch]
        [HarmonyPatch(typeof(GameGrid), "FindEndOfPlatform")]
        static GridCell FindEndOfPlatform(GameGrid instance, GridCell currentCell, GridDirection gridDirection)
        {
            throw new NotImplementedException("ckwng Extensions: Reverse patch of FindEndOfPlatform() failed?");
        }
    }
}
