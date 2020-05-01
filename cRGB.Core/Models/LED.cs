using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace cRGB.Domain.Models
{
    public class LED
    {
        public bool Enabled { get; set; } = true;

        [Range(0, 255)]
        public int R { get; set; } = 0;
        [Range(0, 255)]
        public int G { get; set; } = 0;
        [Range(0, 255)]
        public int B { get; set; } = 0;

        public Color GetLedAsColor => Color.FromArgb(0, R, G, B);

        public byte[] GetLedAsBytes => new byte[3] {(byte)R, (byte)G, (byte)B};
        
    }
}
