#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

namespace cRGB.Domain.Models.Event.Settings
{
    public class TimerEvent : LedEvent, ITimerEvent
    {
        
    }

    public interface ITimerEvent : ILedEvent
    {

    }
}