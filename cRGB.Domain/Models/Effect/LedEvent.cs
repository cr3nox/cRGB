#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;
using cRGB.Domain.Models.Event;

namespace cRGB.Domain.Models.Effect
{
    public class LedEvent : ILedEvent
    {
        public Type EventType { get; set; }
    }
}