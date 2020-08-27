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
        public string EffectType { get; set; }
        public bool IsEnabled { get; set; } = true;
        public int LedStartIndex { get; set; } = 0;
        public int LedEndIndex { get; set; } = 0;
    }
}