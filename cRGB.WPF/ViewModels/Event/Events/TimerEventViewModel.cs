#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System.Runtime.CompilerServices;
using Caliburn.Micro;
using cRGB.Domain.Models.Effect;
using cRGB.Domain.Models.Event;
using cRGB.Domain.Models.Event.Settings;
using cRGB.WPF.Helpers;
using cRGB.WPF.ServiceLocation.Factories;
using cRGB.WPF.ViewModels.Controls;

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
        public TimerEventViewModel(IEventAggregator aggregator, ILocalizationHelper loc, IEffectViewModelFactory effectViewModelFactory, IDialogComboBoxSelectionViewModel selection, ITimerEvent timerEvent)
            : base(aggregator, loc, effectViewModelFactory, selection,  timerEvent)
        {
            timerEvent.EventType = GetType().Name;
            DisplayName = loc.GetByKey("TimerEvent");
        }

        #endregion

        #region Methods



        #endregion
    }
}