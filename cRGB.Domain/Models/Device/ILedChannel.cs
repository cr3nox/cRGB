#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System.Collections;
using System.Collections.Generic;

namespace cRGB.Domain.Models.Device
{
    public interface ILedChannel
    {
        public IEnumerable<ILed> Leds { get; }
    }
}