using System.IO;
using System;

namespace RRON_new
{
    public class Program
    {
        private static void Main()
        {
            SavedSettings settings = RronDeserializer.Deserialize<SavedSettings>(File.ReadAllText("data.rron"));
            /*Console.WriteLine(settings.SkinName);
            Console.WriteLine(settings.FullscreenMode);
            Console.WriteLine(settings.VSyncIndex);
            Console.WriteLine(settings.Volume);
            Console.WriteLine(settings.ElementsSize);
            Console.WriteLine(settings.IncomingSpeed);
            Console.WriteLine(settings.Resolution.Width);
            Console.WriteLine(settings.Resolution.Height);
            Console.WriteLine(settings.Resolution.RefreshRate);
            Console.WriteLine(settings.Resolutions[0].Width);
            Console.WriteLine(settings.Resolutions[0].Height);
            Console.WriteLine(settings.Resolutions[0].RefreshRate);
            Console.WriteLine(settings.Resolutions[1].Width);
            Console.WriteLine(settings.Resolutions[1].Height);
            Console.WriteLine(settings.Resolutions[1].RefreshRate);
            Console.WriteLine(settings.Keys[0]);
            Console.WriteLine(settings.Keys[1]);
            Console.WriteLine(settings.Keys[2]);
            Console.WriteLine(settings.Keys[3]);*/
        }
    }
}