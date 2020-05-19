#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;

namespace cRGB.Domain.Models.Event.Settings
{
    public class AudioEvent : LedEvent, IAudioEvent
    {

    }

    public interface IAudioEvent : ILedEvent
    {

    }
}