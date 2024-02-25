using HarmonyLib;
using UnityEngine;

namespace EshkaBooster.Patches
{
    [HarmonyPatch(typeof(SunSettings), "Update")]
    public class OnShadowsUpdate
    {
        static bool Prefix(SunSettings __instance)
        {
            if (Globals.NoShadows && !MainMenuSystem.isOpen)
            {
                __instance.light.shadows = LightShadows.None;
                return false;
            }

            return true;
        }
    }
}