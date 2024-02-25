using HarmonyLib;

namespace EshkaBooster.Patches
{
    [HarmonyPatch(typeof(FoliageCell), "CalculateLOD")]
    public class OnGrassUpdate
    {
        static bool Prefix(ref float __result)
        {
            if (Globals.NoGrass)
            {
                __result = 1f;
                return false;
            }

            return true;
        }
    }
}