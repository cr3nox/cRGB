#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;

namespace cRGB.Domain.Models.Event.Settings
{
    public class AudioEvent : IAudioEvent
    {
        public string EventType { get; set; }
    }

    public interface IAudioEvent : ILedEvent
    {

    }
}