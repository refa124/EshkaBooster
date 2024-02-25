using System;
using HarmonyLib;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EshkaBooster.Patches
{
    [HarmonyPatch(typeof(GameManager), "Destroy", new Type[] { typeof(GameObject), typeof(float) })]
    public class OnBootstrapDestroy
    {
        static bool isInitialized = false;
        
        static void Prefix(GameObject instance, float delay = 0f)
        {
            if (instance == SingletonComponent<Bootstrap>.Instance.gameObject && !isInitialized)
            {
                try
                {
                    GameObject gameObject = new GameObject()
                    {
                        name = "EClassBooster",
                        hideFlags = HideFlags.HideAndDontSave
                    };
            
                    if (!ClassInjector.IsTypeRegisteredInIl2Cpp(typeof(Manager)))
                        ClassInjector.RegisterTypeInIl2Cpp(typeof(Manager));
                    gameObject.AddComponent(Il2CppType.From(typeof(Manager)));
            
                    Object.DontDestroyOnLoad(gameObject);
                    
                    isInitialized = true;
                }
                catch (Exception exception)
                {
                    Logger.Output(exception.ToString(), "Failed to InitializeUI()");
                }
            }
        }
    }
}