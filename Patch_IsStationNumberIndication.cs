using Game.Board;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ckwng.RailRoute
{
    [HarmonyPatch(typeof(GameBoard), "IsStationIndication")]
    class Patch_IsStationNumberIndication
    {
        static bool Postfix(bool result, char nextChar)
        {
            if (!result)
            {
                return nextChar >= '\u2457' && nextChar <= '\u249b';
            }
            return result;
        }
    }
}
