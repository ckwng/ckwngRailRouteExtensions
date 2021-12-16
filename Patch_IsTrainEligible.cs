using Game.Railroad.Sensor;
using Game.Train;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ckwng.RailRoute
{
    [HarmonyPatch(typeof(AutoSignalSensor), "IsTrainEligible")]
     class Patch_IsTrainEligible
    {
        public static bool Postfix(bool result, Train train)
        {
            return result ? !train.Head.ShallDisposeTrain(train) : result;
        }
    }
}
