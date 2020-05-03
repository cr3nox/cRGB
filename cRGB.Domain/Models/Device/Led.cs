using System.ComponentModel.DataAnnotations;

namespace cRGB.Domain.Models.Device
{
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

        public void SetColors(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }
        
    }
}
