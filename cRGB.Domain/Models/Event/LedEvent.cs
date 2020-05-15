#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;

namespace cRGB.Domain.Models.Event
{
    public class LedEvent : ILedEvent
    {
        public Type EventType { get; set; }
    }
}