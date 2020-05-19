#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;
using System.Collections.Generic;
using cRGB.Domain.Models.Effect;

namespace cRGB.Domain.Models.Event
{
    public class LedEvent : ILedEvent
    {
        public Type EventType { get; set; }
        public IList<ILedEffect> LedEffects { get; set; } = new List<ILedEffect>();
    }
}