using System.ComponentModel.DataAnnotations;

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

        public byte[] GetLedAsByteArray => new byte[3] {(byte)R, (byte)G, (byte)B};
        
    }
}
