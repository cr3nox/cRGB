#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;
using System.Collections.Generic;
using cRGB.Domain.Models.Event;

namespace cRGB.Domain.Models.Effect
{
    public class LedEffect : ILedEffect
    {
        public Type EffectType { get; set; }
    }
}