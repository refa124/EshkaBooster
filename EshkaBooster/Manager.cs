using System.Collections;
using BepInEx.Unity.IL2CPP.Utils.Collections;
using EshkaBooster.Utils;
using UnityEngine;

namespace EshkaBooster
{
    public class Manager : MonoBehaviour
    {
        private bool isOpen = false;
        
        private void Start()
        {
            Notifier.Add("Бустер загружен!", "Открыть меню F2", 3);

            StartCoroutine(GC_Cleaning().WrapToIl2Cpp());
        }

        IEnumerator GC_Cleaning()
        {
            while (true)
            {
                if(Globals.CleanGC)
                    Rust.GC.Collect();
                
                yield return new WaitForSeconds(30 * 60);
            }
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.F2))
                isOpen = !isOpen;
        }
        
        private void OnGUI()
        {
            Notifier.Render();

            if (!isOpen) return;
            
            UI.BeginWindow(200, 100, 400, 400);
            
            UI.Title("EshkaBooster");
            
            UI.BeginCategory("Оптимизация", "Раздел отвечающий за основной буст вашего FPS\nблагодаря отключениям ненужных функций", 210);

            UI.Toggle("Тени", "Отключает тени в игре", ref Globals.NoShadows);
            UI.Toggle("Трава", "Полностью убирает траву", ref Globals.NoGrass);
            UI.Toggle("Очистка оперативной памяти", "Очищает оп.память с интервалом 30 мин", ref Globals.CleanGC);
            
            UI.EndCategory();
            
            UI.EndWindow();
        }
    }
}