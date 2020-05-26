#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

namespace cRGB.Domain.Models.Device
{
    public class LedColor : ILedColor
    {
        public byte Alpha { get; set; } = 255;
        public byte R { get; set; } = 0;
        public byte G { get; set; } = 0;
        public byte B { get; set; } = 0;

        public LedColor()
        {

        }

        public LedColor(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public LedColor(byte alpha, byte r, byte g, byte b)
        {
            Alpha = alpha;
            R = r;
            G = g;
            B = b;
        }

        public LedColor(int r, int g, int b)
        {
            R = (byte)r;
            G = (byte)g;
            B = (byte)b;
        }

        public LedColor(int alpha, int r, int g, int b)
        {
            Alpha = (byte)alpha;
            R = (byte)r;
            G = (byte)g;
            B = (byte)b;
        }

        public override string ToString()
        {
            return $"A: {Alpha}, R: {R}, G: {G}, B: {B}";
        }

        public void SetColor(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }
        public void SetColor(byte alpha, byte r, byte g, byte b)
        {
            Alpha = alpha;
            SetColor(r, g, b);
        }

        public void SetColor(int r, int g, int b)
        {
            SetColor((byte)r, (byte)g, (byte)b);
        }
        public void SetColor(int alpha, int r, int g, int b)
        {
            SetColor((byte)alpha, (byte)r, (byte)g, (byte)b);
        }
    }
}