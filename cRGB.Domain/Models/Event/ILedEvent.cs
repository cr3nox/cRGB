﻿#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;

namespace cRGB.Domain.Models.Event
{
    public interface ILedEvent
    {
        public Type EventType { get; set; }
    }
}