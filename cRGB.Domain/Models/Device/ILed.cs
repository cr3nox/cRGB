using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace cRGB.Domain.Models.Device
{
    public interface ILed
    {
        public bool Enabled { get; set; }
        public int Index { get; set; }

        [Range(0, 255)]
        public int R { get; set; }
        [Range(0, 255)]
        public int G { get; set; }
        [Range(0, 255)]
        public int B { get; set; }

        public Color GetLedAsColor => Color.FromArgb(0, R, G, B);

        public byte[] GetLedAsByteArray => new byte[3] {(byte)R, (byte)G, (byte)B};
        
    }
}
