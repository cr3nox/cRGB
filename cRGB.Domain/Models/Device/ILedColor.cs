#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

namespace cRGB.Domain.Models.Device
{
    public interface ILedColor
    {
        public byte Alpha { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        string ToString();

        public void SetColor(byte r, byte g, byte b);
        public void SetColor(byte alpha, byte r, byte g, byte b);

        public void SetColor(int r, int g, int b);
        public void SetColor(int alpha, int r, int g, int b);
    }
}