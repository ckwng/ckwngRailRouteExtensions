using Game.Board;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ckwng.RailRoute
{
    [HarmonyPatch(typeof(LookupTrackBuilder), "TrackNumberFromChar")]
    class Patch_TrackNumberFromChar
    {
        static int Postfix(int result, char c)
        {
            if (result > 11)
            {
                result -= '\u2457' - ';';
            }
            return result;
        }
    }
}
