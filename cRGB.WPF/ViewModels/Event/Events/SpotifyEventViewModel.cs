#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using Caliburn.Micro;
using cRGB.WPF.Helpers;

namespace cRGB.WPF.ViewModels.Event.Events
{
    public sealed class SpotifyEventViewModel : EventViewModel
    {
        #region Fields
        ILocalizationHelper _localizationHelper;
        IEventAggregator _eventAggregator;

        #endregion

        #region Properties

        public override bool CanActivate { get; }

        #endregion

        #region ctor

        public SpotifyEventViewModel(IEventAggregator aggregator, ILocalizationHelper loc)
        {
            _eventAggregator = aggregator;
            _localizationHelper = loc;
            DisplayName = loc.GetByKey("SpotifyEvent");
        }

        #endregion

        #region Methods

        

        #endregion
    }
}