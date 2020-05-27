#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using cRGB.Domain.Models.Event;
using cRGB.Domain.Models.Event.Settings;
using cRGB.WPF.Helpers;
using cRGB.WPF.ServiceLocation.Factories;
using cRGB.WPF.ViewModels.Controls;
using cRGB.WPF.ViewModels.Device;

namespace cRGB.WPF.ViewModels.Event.Events
{
    public sealed class StaticEventViewModel : EventViewModel
    {

        #region Fields

        CancellationToken _ct;
        #endregion

        #region Properties

        // Static event always returns true
        public override bool CanActivate => true;

        #endregion

        #region ctor

        // ReSharper disable once SuggestBaseTypeForParameter
        public StaticEventViewModel(IEventAggregator aggregator, ILocalizationHelper loc,
            IEffectViewModelFactory effectViewModelFactory, IDialogComboBoxSelectionViewModel selection,
            IStaticEvent ledEvent)
            : base(aggregator, loc, effectViewModelFactory, selection, ledEvent)
        {
            ledEvent.EventType = GetType().Name;
            DisplayName = loc.GetByKey("StaticEvent");
        }

        #endregion

        #region Methods

        public override async IAsyncEnumerable<IList<ILedViewModel>> Activate(CancellationToken ct, IList<ILedViewModel> leds)
        {
            while (true)
            {
                ct.ThrowIfCancellationRequested();
                foreach (var effectViewModel in Effects)
                {
                    var task = await effectViewModel.TickAsync(ct, leds);
                    yield return task;
                }

                _ = Task.Delay(100, ct);
            }
        }

        public override void Stop()
        {

        }

        #endregion
    }
}