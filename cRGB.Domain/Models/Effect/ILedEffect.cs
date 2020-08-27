﻿#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using cRGB.Domain.Models.Device;

namespace cRGB.Domain.Models.Effect
{
    public interface ILedEffect
    {

        public string EffectType { get; set; }
        public bool IsEnabled { get; set; }

        //public IList<int> EnabledLeds { get; set; }
        public int LedStartIndex { get; set; }
        public int LedEndIndex { get; set; }
    }
}