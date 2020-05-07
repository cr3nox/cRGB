using System.Collections.Generic;
using System.Runtime.Serialization;

namespace cRGB.Domain.Models.Device
{
    [DataContract]
    public class BlinkStickSettings : IBlinkStickSettings
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string SerialNumber { get; set; }
        [DataMember]
        public int RChannelLedCount { get; set; } = 64;
        [DataMember]
        public int GChannelLedCount { get; set; } = 64;
        [DataMember]
        public int BChannelLedCount { get; set; } = 64;
        [DataMember]
        public bool RChannelLedInvert { get; set; } = false;
        [DataMember]
        public bool GChannelLedInvert { get; set; } = false;
        [DataMember]
        public bool BChannelLedInvert { get; set; } = false;
        [DataMember]
        public IEnumerable<int> DisabledLeds { get; set; } = new List<int>();

        public int Brightness { get; set; } = 100;
        public bool CombineChannels { get; set; } = true;

        public BlinkStickSettings()
        {

        }
        public BlinkStickSettings(string serial)
        {
            if (string.IsNullOrEmpty(serial))
                return;
            SerialNumber = serial;
        }

    }
}
