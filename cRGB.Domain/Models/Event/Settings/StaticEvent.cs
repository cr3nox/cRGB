#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;

namespace cRGB.Domain.Models.Event.Settings
{
    public class StaticEvent : LedEvent, IStaticEvent
    {

    }

    public interface IStaticEvent : ILedEvent
    {

    }
}