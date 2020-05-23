#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using cRGB.Domain.Models.Effect;
using cRGB.Modules.Common.Base;
using cRGB.WPF.ViewModels.Device;

namespace cRGB.WPF.ViewModels.Effect
{
    public abstract class EffectViewModel : ViewModelBase, IEffectViewModel
    {
        protected IEventAggregator EventAggregator;

        public bool IsEnabled { get; set; }
        public ILedEffect Config { get; internal set; }

        protected EffectViewModel(IEventAggregator aggregator, ILedEffect config)
        {
            EventAggregator = aggregator;
            EventAggregator.SubscribeOnBackgroundThread(this);
            Config = config;
        }

        public abstract Task<IList<ILedViewModel>> TickAsync(CancellationToken ct,IList<ILedViewModel> leds);

        public void Dispose()
        {
            Config = null;
            EventAggregator.Unsubscribe(this);
        }
    }
}