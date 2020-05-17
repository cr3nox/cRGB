#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using Caliburn.Micro;
using cRGB.Domain.Models.Event;
using cRGB.Domain.Models.Event.Settings;
using cRGB.WPF.Helpers;

namespace cRGB.WPF.ViewModels.Event.Events
{
    public sealed class StaticEventViewModel : EventViewModel
    {

        #region Fields

        #endregion

        #region Properties

        // Static event always returns true
        public override bool CanActivate => true;

        #endregion

        #region ctor

        // ReSharper disable once SuggestBaseTypeForParameter
        public StaticEventViewModel(IEventAggregator aggregator, ILocalizationHelper loc, IStaticEvent ledEvent) : base(aggregator, loc, ledEvent)
        {
            ledEvent.EventType = GetType().Name;
            DisplayName = loc.GetByKey("StaticEvent");
        }

        #endregion

        #region Methods



        #endregion
    }
}