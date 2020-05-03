using System;

namespace cRGB.Domain.Models.Device
{
    [Serializable]
    public class BlinkStickSettings
    {
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public int RChannelLedCount { get; set; } = 0;
        public int GChannelLedCount { get; set; } = 0;
        public int BChannelLedCount { get; set; } = 0;
        public bool RChannelLedInvert { get; set; } = false;
        public bool GChannelLedInvert { get; set; } = false;
        public bool BChannelLedInvert { get; set; } = false;

    }
}
