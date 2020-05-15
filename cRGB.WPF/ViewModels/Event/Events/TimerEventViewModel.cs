#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using Caliburn.Micro;
using cRGB.Domain.Models.Effect;
using cRGB.Domain.Models.Event.Settings;
using cRGB.WPF.Helpers;

namespace cRGB.WPF.ViewModels.Event.Events
{
    public sealed class TimerEventViewModel : EventViewModel
    {

        #region Fields
        readonly IEventAggregator _eventAggregator;

        #endregion

        #region Properties

        public override bool CanActivate { get; }

        #endregion

        #region ctor

        public TimerEventViewModel(IEventAggregator aggregator, ILocalizationHelper loc)
        {
            _eventAggregator = aggregator;
            DisplayName = loc.GetByKey("TimerEvent");
            Model ??= new TimerEvent();
        }

        public TimerEventViewModel(IEventAggregator aggregator, ILocalizationHelper loc, TimerEvent timerEvent) : this(aggregator, loc)
        {
            Model = timerEvent;
        }

        #endregion

        #region Methods



        #endregion
    }
}