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
    [HarmonyPatch(typeof(TrainTypeConfig))]
    static class Patch_TrainTypeConfig
    {
        [HarmonyPostfix]
        [HarmonyPatch("CargoCars")]
        static int CargoCars(int result, TrainType type)
        {
            if (ckwngLevelSettings.Active)
            {
                var amount = -1;
                switch (type)
                {
                    case TrainType.FREIGHT:
                        Debug.Log("[ckwng] custom CargoCars handled.");
                        amount = ckwngLevelSettings.FreightConfig.CargoCars;
                        break;
                }
                if (amount >= 0)
                {
                    return amount;
                }
            }
            return result;
        }
        [HarmonyPostfix]
        [HarmonyPatch("PassengerCars")]
        static int PassengerCars(int result, TrainType type)
        {
            if (ckwngLevelSettings.Active)
            {
                var amount = -1;
                switch (type)
                {
                    case TrainType.COMMUTER:
                        Debug.Log("[ckwng] custom PassengerCars handled.");
                        amount = ckwngLevelSettings.CommuterConfig.PassengerCars;
                        break;
                    case TrainType.URBAN:
                        Debug.Log("[ckwng] custom PassengerCars handled.");
                        amount = ckwngLevelSettings.UrbanConfig.PassengerCars; ;
                        break;
                    case TrainType.IC:
                        Debug.Log("[ckwng] custom PassengerCars handled.");
                        amount = ckwngLevelSettings.ICConfig.PassengerCars; ;
                        break;
                }
                if (amount >= 0)
                {
                    return amount;
                }
            }
            return result;
        }
        [HarmonyPostfix]
        [HarmonyPatch("Bidirectional")]
        static bool Bidirectional(bool result, TrainType type)
        {
            if (ckwngLevelSettings.Active)
            {
                var value = -1;
                switch (type)
                {
                    case TrainType.IC:
                        Debug.Log("[ckwng] custom Bidirectional handled.");
                        value = ckwngLevelSettings.ICConfig.Bidirectional;
                        break;
                }
                if (value >= 0)
                {
                    return value > 0;
                }
            }
            return result;
        }
    }
}
