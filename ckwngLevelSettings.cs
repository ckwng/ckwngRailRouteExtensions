using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ckwng.RailRoute
{
    static class ckwngLevelSettings
    {
        public static bool Active => ExtensionSettings != null;

        public static string ExtensionSettings => extensionDefinition?.CkwngExtension;

        public static ckwngSavedLevelDefinition extensionDefinition;

        public static string LevelUuid;

        public static ckwngTrainTypeConfig CommuterConfig;

        public static ckwngTrainTypeConfig ICConfig;

        public static ckwngTrainTypeConfig UrbanConfig;

        public static ckwngTrainTypeConfig FreightConfig;

        //public static Dictionary<string, ckwngTrainTypeConfig> customConfigs;

        public static void Load(string uuid, ckwngSavedLevelDefinition container)
        {
            extensionDefinition = container;
            LevelUuid = uuid;
            if (ExtensionSettings != null)
            {
                Debug.Log("[ckwng] Loaded map settings for " + LevelUuid + ": " + ExtensionSettings);
                foreach (var setting in ExtensionSettings.Split(','))
                {
                    var settingArray = setting.Split(new char[] { ':' }, 2);
                    var settingKey = settingArray[0];
                    var settingValue = settingArray[1];
                    switch (settingKey)
                    {
                        case "CommuterConfig":
                            CommuterConfig = ckwngTrainTypeConfig.Decode(settingValue);
                            Debug.Log("[ckwng] Set CommuterConfig to " + settingValue);
                            break;
                        case "ICConfig":
                            ICConfig = ckwngTrainTypeConfig.Decode(settingValue);
                            Debug.Log("[ckwng] Set ICConfig to " + settingValue);
                            break;
                        case "UrbanConfig":
                            UrbanConfig = ckwngTrainTypeConfig.Decode(settingValue);
                            Debug.Log("[ckwng] Set UrbanConfig to " + settingValue);
                            break;
                        case "FreightConfig":
                            FreightConfig = ckwngTrainTypeConfig.Decode(settingValue);
                            Debug.Log("[ckwng] Set FreightConfig to " + settingValue);
                            break;
                        default:
                            if (settingKey == null)
                            {
                                settingKey = "<null>";
                            }
                            Debug.Log("[ckwng] Unknown extension setting " + settingKey);
                            break;
                    }
                }
            }
        }
    }
}
