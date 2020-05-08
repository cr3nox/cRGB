using System.Collections.Generic;

namespace cRGB.Domain.Models.Device
{
    public interface IBlinkStickSettings : IDeviceSettings
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
        public int RChannelLedCount { get; set; }
        public int GChannelLedCount { get; set; }
        public int BChannelLedCount { get; set; }
        public bool RChannelLedInvert { get; set; }
        public bool GChannelLedInvert { get; set; }
        public bool BChannelLedInvert { get; set; }
        public IEnumerable<int> DisabledLeds { get; set; }
        public int Brightness { get; set; }
        public bool CombineChannels { get; set; }

    }
}
