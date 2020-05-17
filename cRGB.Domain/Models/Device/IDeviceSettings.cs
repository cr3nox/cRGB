#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System.Collections.Generic;
using cRGB.Domain.Models.Event;

namespace cRGB.Domain.Models.Device
{
    public interface IDeviceSettings
    {
        public int EventPollingSeconds { get; set; }
        public IList<ILedEvent> Events { get; set; }
    }
}