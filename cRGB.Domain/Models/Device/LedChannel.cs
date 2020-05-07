#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System.Collections.Generic;

namespace cRGB.Domain.Models.Device
{
    public class LedChannel : ILedChannel
    {
        public IEnumerable<ILed> Leds { get; set; }


    }
}