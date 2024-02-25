using System.IO;

namespace EshkaBooster
{
    public class Logger
    {
        public static void Output(string text, string tag = "~")
        {
            File.AppendAllText("eshka.log", $"[{tag}] {text}\r\n");
        }
    }
}