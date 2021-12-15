using Game;
using Game.Railroad;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ckwng.RailRoute
{
    [HarmonyPatch(typeof(StationRepository))]
    class Patch_StationRepository
    {
        static ISet<char> stationChars = new HashSet<char>("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZàáâãäåæçèéêëìíîïðñòóôõöøùúûüýþÿāăąćĉċčďđēĕėęěĝğġģĥħĩīĭįıĳĵķĸĺļľŀłńņňŋōŏőŕŗřśŝşšţťŧũūŭůűųŵŷźżžſÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝÞŸĀĂĄĆĈĊČĎĐĒĔĖĘĚĜĞĠĢĤĦĨĪĬĮİĲĴĶĹĻĽĿŁŃŅŇŊŌŎŐŔŖŘŚŜŞŠŢŤŦŨŪŬŮŰŲŴŶŹŻŽß");
        static FieldInfo stationsByRef = AccessTools.Field(typeof(StationRepository), "stationsByRef");

        [HarmonyPrefix]
        [HarmonyPatch("GetNextChar")]
        static bool GetNextChar(StationRepository __instance, ref char __result)
        {
            __result = stationChars.FirstOrDefault((char c) => !((Dictionary<char, Station>)stationsByRef.GetValue(__instance)).ContainsKey(c));
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch("IsStationChar")]
        static bool IsStationChar(char nextChar, ref bool __result)
        {
            __result = stationChars.Contains(nextChar);
            return false;
        }
    }
}
