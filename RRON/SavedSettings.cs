using System.Collections.Generic;

namespace RRON_new
{
    public enum KeyCode
    {
        W,
        A,
        S,
        D
    }

    public enum FullScreenMode
    {
        ExclusiveFullScreen,
        FullScreen,
        Windowed
    }

    public enum VSyncMode
    {
        Off,
        On,
        EveryVBlank,
    }

    public struct Resolution
    {
        public Resolution(int width, int height, int refreshRate)
        {
            Width = width;
            Height = height;
            RefreshRate = refreshRate;
        }

        public int Width { get; set; }
        public int Height { get; set; }
        public int RefreshRate { get; set; }
    }

    public class SavedSettings
    {
        public string SkinName { get; set; }

        public FullScreenMode FullscreenMode { get; set; }

        public VSyncMode VSyncIndex { get; set; }

        public float Volume { get; set; }

        public float ElementsSize { get; set; }

        public float IncomingSpeed { get; set; }

        public Resolution Resolution { get; set; }
        
        public List<Resolution> Resolutions { get; set; } = new List<Resolution>();

        public List<KeyCode> Keys { get; set; } = new List<KeyCode>();
    }
}