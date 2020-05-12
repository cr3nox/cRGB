#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using Caliburn.Micro;
using cRGB.WPF.Helpers;

namespace cRGB.WPF.ViewModels.Event.Events
{
    public sealed class StaticEventViewModel : EventViewModel
    {

        #region Fields
        ILocalizationHelper _localizationHelper;
        IEventAggregator _eventAggregator;

        #endregion

        #region Properties

        // Static event always returns true
        public override bool CanActivate => true;

        #endregion

        #region ctor

        public StaticEventViewModel(IEventAggregator aggregator, ILocalizationHelper loc)
        {
            _eventAggregator = aggregator;
            _localizationHelper = loc;
            DisplayName = loc.GetByKey("StaticEvent");
        }

        #endregion

        #region Methods



        #endregion
    }
}