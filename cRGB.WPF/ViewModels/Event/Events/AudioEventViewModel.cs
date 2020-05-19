#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using Caliburn.Micro;
using cRGB.Domain.Models.Event.Settings;
using cRGB.WPF.Helpers;
using cRGB.WPF.ServiceLocation.Factories;

namespace cRGB.WPF.ViewModels.Event.Events
{
    public sealed class AudioEventViewModel : EventViewModel
    {
        #region Fields

        #endregion

        #region Properties

        public override bool CanActivate { get; }
        public override void Init()
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region ctor

        // ReSharper disable once SuggestBaseTypeForParameter
        public AudioEventViewModel(IEventAggregator aggregator, ILocalizationHelper loc, IEffectViewModelFactory effectViewModelFactory, IAudioEvent ledEvent) : base(aggregator, loc, effectViewModelFactory, ledEvent)
        {
            ledEvent.EventType = GetType().Name;
            DisplayName = loc.GetByKey("AudioEvent");
        }

        #endregion

        #region Methods

        

        #endregion
    }
}