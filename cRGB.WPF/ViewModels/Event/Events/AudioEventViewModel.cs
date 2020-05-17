#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using Caliburn.Micro;
using cRGB.Domain.Models.Event.Settings;
using cRGB.WPF.Helpers;

namespace cRGB.WPF.ViewModels.Event.Events
{
    public sealed class AudioEventViewModel : EventViewModel
    {
        #region Fields
        //ILocalizationHelper _localizationHelper;
        //IEventAggregator _eventAggregator;

        #endregion

        #region Propertieshttps://github.com/cr3nox/cRGB

        public override bool CanActivate { get; }

        #endregion

        #region ctor

        public AudioEventViewModel(IEventAggregator aggregator, ILocalizationHelper loc, IAudioEvent ledEvent) : base(aggregator, loc, ledEvent)
        {
            DisplayName = loc.GetByKey("AudioEvent");
        }

        #endregion

        #region Methods

        

        #endregion
    }
}