#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System.Runtime.CompilerServices;
using Caliburn.Micro;
using cRGB.Domain.Models.Effect;
using cRGB.Domain.Models.Event;
using cRGB.Domain.Models.Event.Settings;
using cRGB.WPF.Helpers;

namespace cRGB.WPF.ViewModels.Event.Events
{
    public sealed class TimerEventViewModel : EventViewModel
    {

        #region Fields


        #endregion

        #region Properties

        public override bool CanActivate { get; }

        #endregion

        #region ctor

        // ReSharper disable once SuggestBaseTypeForParameter
        public TimerEventViewModel(IEventAggregator aggregator, ILocalizationHelper loc, ITimerEvent timerEvent) : base(aggregator, loc, timerEvent)
        {
            timerEvent.EventType = GetType().Name;
            DisplayName = loc.GetByKey("TimerEvent");
        }

        #endregion

        #region Methods



        #endregion
    }
}