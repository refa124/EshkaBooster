using UnityEngine;

namespace EshkaBooster.Utils
{
    public class Render
    {
        private static Color textureColor;
        private static Texture2D texture;
        private static GUIStyle none;
        
        private static Texture2D Texture(Color color)
        {
            if (texture == null)
            {
                texture = new Texture2D(1, 1);
                textureColor = new Color(1, 0, 0, 0);
            }

            if (textureColor != color)
            {
                texture.SetPixel(0, 0, color);
                texture.Apply();
                textureColor = color;
            }

            return texture;
        }

        public static void Rectangle(float x, float y, float w, float h, Color color, float rounding = 0)
        {
            GUI.DrawTexture(new Rect(x, y, w, h), Texture(color), ScaleMode.StretchToFill, false, 1, color, 0,
                rounding);
        }

        public static void RectangleOutlined(float x, float y, float w, float h, float thickness, Color color,
            float rounding = 0)
        {
            GUI.DrawTexture(new Rect(x, y, w, h), Texture(color), ScaleMode.StretchToFill, false, 1, color, thickness,
                rounding);
        }

        public static Vector2 StringSize(string text, int fontSize = 16, FontStyle fontStyle = FontStyle.Normal)
        {
            GUI.skin.label.fontSize = fontSize;
            GUI.skin.label.alignment = TextAnchor.UpperLeft;
            GUI.skin.label.fontStyle = fontStyle;

            return GUI.skin.label.CalcSize(new GUIContent(text));
        }

        public static void String(float x, float y, string text, Color color, int fontSize = 16, FontStyle fontStyle = FontStyle.Normal)
        {
            GUI.skin.label.fontSize = fontSize;
            GUI.skin.label.alignment = TextAnchor.UpperLeft;
            GUI.skin.label.fontStyle = fontStyle;

            GUI.skin.label.normal.textColor = color;

            Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text));
            
            GUI.Label(new Rect(x, y, size.x + 4, size.y + 4), text);
        }
        
        public static void String(float x, float y, float w, float h, string text, Color color, int fontSize = 16, TextAnchor alignment = TextAnchor.UpperLeft, FontStyle fontStyle = FontStyle.Normal)
        {
            GUI.skin.label.fontSize = fontSize;
            GUI.skin.label.alignment = alignment;
            GUI.skin.label.fontStyle = fontStyle;

            GUI.skin.label.normal.textColor = color;
            
            GUI.Label(new Rect(x, y, w, h), text);
        }

        public static GUIStyle None()
        {
            if (none == null)
                none = new GUIStyle();

            return none;
        }
    }
}