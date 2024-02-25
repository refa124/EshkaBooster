using System;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;

namespace EshkaBooster
{
    [BepInPlugin("org.refa124.eshka", "E-Class Booster", "0.1")]
    public class EshkaPlugin : BasePlugin
    {
        public override void Load()
        {
            try
            {
                InitializeHarmony();
            }
            catch (Exception exception)
            {
                Logger.Output(exception.ToString(), "Failed to InitializeHarmony()");
            }
        }

        private void InitializeHarmony()
        {
            Harmony harmony = new Harmony("org.refa124.eshka");
            harmony.PatchAll();
        }
    }
}