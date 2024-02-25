using System;
using UnityEngine;

namespace EshkaBooster.Utils
{
    public class UI
    {
        private static Vector2 CurrentWindowPosition = Vector2.zero;
        private static Vector2 CurrentWindowSize = Vector2.zero;
        private static float CurrentWindowItemOffsetY = 0;
        private static float CurrentCategoryItemOffsetY = 0;
        
        public static void BeginWindow(float x, float y, float w, float h)
        {
            CurrentWindowItemOffsetY = 0;
            CurrentWindowPosition = new Vector2(x, y);
            CurrentWindowSize = new Vector2(w, h);
            
            Render.Rectangle(x, y, w, h, new Color32(25, 25, 25, 255), 16);
            
            GUI.BeginGroup(new Rect(x, y, w, h), string.Empty, Render.None());
        }

        public static void EndWindow()
        {
            GUI.EndGroup();
        }

        public static void Title(string text)
        {
            Render.String(0, 0, CurrentWindowSize.x, 64, text, new Color32(105, 105, 105, 255), 20, TextAnchor.MiddleCenter, FontStyle.Bold);

            CurrentWindowItemOffsetY += (64 + 24);
        }

        public static void BeginCategory(string name, string description, float h)
        {
            CurrentCategoryItemOffsetY = 10;
            
            Render.String(30, CurrentWindowItemOffsetY, name, Color.white, 16, FontStyle.Bold);

            Vector2 nameSize = Render.StringSize(name, 16, FontStyle.Bold);
            
            Render.String(30, CurrentWindowItemOffsetY + nameSize.y, 300, 32, description, Color.gray, 12);
            
            Render.Rectangle(20, CurrentWindowItemOffsetY + nameSize.y + 48, 360, h, new Color32(55, 55, 55, 255), 16);
            GUI.BeginGroup(new Rect(20, CurrentWindowItemOffsetY + nameSize.y + 48, 360, h));

            CurrentWindowItemOffsetY += (CurrentWindowItemOffsetY + nameSize.y + 48 + h + 12);
        }

        public static void EndCategory()
        {
            GUI.EndGroup();
        }

        public static void Toggle(string name, string description, ref bool state)
        {
            Render.Rectangle(10, CurrentCategoryItemOffsetY, 340, 60, new Color32(70, 70, 70, 255), 12);

            Render.String(30, CurrentCategoryItemOffsetY + 12, name, Color.white, 14, FontStyle.Bold);
            Render.String(30, CurrentCategoryItemOffsetY + 30, description, Color.gray, 12);
            
            Render.Rectangle(310, CurrentCategoryItemOffsetY + 24, 24, 12, state ? new Color32(30, 169, 68, 100) : new Color32(255, 255, 255, 50), 12);
            
            Render.Rectangle(310 + (state ? 13 : 1), CurrentCategoryItemOffsetY + 25, 10, 10, state ? new Color32(47, 253, 106, 255) : new Color32(255, 255, 255, 100), 10);

            if (GUI.Button(new Rect(10, CurrentCategoryItemOffsetY, 340, 60), string.Empty, Render.None()))
                state = !state;

            CurrentCategoryItemOffsetY += 65;
        }

        private static bool isHover(float x, float y, float w, float h)
        {
            float mouse_x = Input.mousePosition.x;
            float mouse_y = Screen.height - Input.mousePosition.x;

            bool hover_x = (mouse_x >= x && mouse_x <= x + w);
            bool hover_y = (mouse_y >= y && mouse_y <= y + h);

            return (hover_x && hover_y);
        }
    }
}