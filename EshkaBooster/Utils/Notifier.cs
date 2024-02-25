using System.Collections.Generic;
using UnityEngine;

namespace EshkaBooster.Utils
{
    public class Notifier
    {
        private struct INotify
        {
            public string text;
            public string subtitle;
            public float createdIn;
            public float timeout;
            public Vector2 position;
        }

        private static List<INotify> Notifies = new List<INotify>();
        
        public static void Add(string text, float timeout = 3)
        {
            Notifies.Add(new INotify()
            {
                text = text,
                subtitle = string.Empty,
                timeout = timeout,
                createdIn = Time.realtimeSinceStartup,
                position = new Vector2(-350, 0)
            });
        }
        
        public static void Add(string text, string subtitle, float timeout = 3)
        {
            Notifies.Add(new INotify()
            {
                text = text,
                subtitle = subtitle,
                timeout = timeout,
                createdIn = Time.realtimeSinceStartup,
                position = new Vector2(-350, 0)
            });
        }

        public static void Render()
        {
            if (Notifies.Count == 0) return;

            float offset_y = 0;
            
            for (int i = 0; i < Notifies.Count; i++)
            {
                INotify notify = Notifies[i];

                bool withSubtitle = (notify.subtitle.Length > 0);
                
                float t = Time.realtimeSinceStartup - notify.createdIn;
                if (t < notify.timeout)
                {
                    notify.position = Vector2.Lerp(notify.position, new Vector2(20, 0),
                        Time.deltaTime * notify.timeout);
                }
                else
                {
                    if (t > (notify.timeout * 2))
                    {
                        notify.position = Vector2.Lerp(notify.position, new Vector2(-350, 0),
                            Time.deltaTime * notify.timeout);

                        if (notify.position.x < -300)
                        {
                            Notifies.RemoveAt(i);
                            continue;
                        }
                    }
                }

                Utils.Render.Rectangle(notify.position.x, 20 + offset_y, 300, withSubtitle ? 80 : 60, new Color32(24, 24, 24, 255), 8);
                Utils.Render.String(notify.position.x + 20, 20 + offset_y, 260, withSubtitle ? 48 : 56, notify.text, Color.white, 16, TextAnchor.MiddleLeft);
                if(withSubtitle) Utils.Render.String(notify.position.x + 20, 56 + offset_y, 260, 24, notify.subtitle, Color.gray, 12, TextAnchor.MiddleLeft);
                
                if (t > notify.timeout)
                {
                    float t_ = (t - notify.timeout);
                    if (t_ > notify.timeout) t_ = notify.timeout;
                    
                    Utils.Render.Rectangle(notify.position.x + 4, 20 + ((withSubtitle ? 80 : 60) - 10) + offset_y, 292, 6, new Color32(65, 65, 65, 255), 8);
                    Utils.Render.Rectangle(notify.position.x + 4, 20 + ((withSubtitle ? 80 : 60) - 10) + offset_y, (292 / notify.timeout) * t_, 6, new Color32(120, 120, 255, 255), 8);
                }
                
                Notifies[i] = notify;

                offset_y += (withSubtitle ? 100 : 80);
            }
        }
    }
}