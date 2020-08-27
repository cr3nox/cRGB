using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using PropertyChanged;

namespace cRGB.Domain.Models.Device
{
    [Serializable]
    public class Led : ILed
    {
        public bool Enabled { get; set; } = true;
        public int Index { get; set; } = -1;

        [Range(0, 255)]
        public int R { get; set; } = 0;
        [Range(0, 255)]
        public int G { get; set; } = 0;
        [Range(0, 255)]
        public int B { get; set; } = 0;
        
        public byte[] GetLedAsByteArray => new byte[3] {(byte)R, (byte)G, (byte)B};

        public void SwapEnabled()
        {
            Enabled = !Enabled;
        }

        public void SetColors(int r, int g, int b)
        {
            if (r > 255)
                r = 255;
            else if (r < 0)
                r = 0;

            if (g > 255)
                g = 255;
            else if (g < 0)
                g = 0;

            if (b > 255)
                b = 255;
            else if (b < 0)
                b = 0;

            R = r;
            G = g;
            B = b;
        }
    }
}
