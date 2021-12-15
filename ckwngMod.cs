using Game.Context;
using Game.Mod;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Utils;

namespace ckwng.RailRoute
{
    public class ckwngMod : AbstractMod
    {
        public ckwngMod()
        {
            var harmony = new Harmony("com.ckwng.railroute.ckwngExtensions");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
        public override CachedLocalizedString Description => "Test";

        public override CachedLocalizedString Title => "ckwng Extensions";

        public async Task ContextChanged(IControllers controllers)
        {
            Debug.Log("[ckwng] onContextChanged");
        }
    }
}
