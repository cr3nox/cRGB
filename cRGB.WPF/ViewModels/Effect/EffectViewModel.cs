#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using cRGB.Domain.Models.Effect;
using cRGB.Modules.Common.Base;
using cRGB.WPF.ViewModels.Device;

namespace cRGB.WPF.ViewModels.Effect
{
    public abstract class EffectViewModel : ValidationViewModelBase, IEffectViewModel
    {
        protected IEventAggregator EventAggregator;

        public bool IsEnabled { get; set; }
        public ILedEffect Config { get; internal set; }

        [Required]
        [IntegerValidator(MinValue = 0)]
        public int LedStartIndex
        {
            get => Config.LedStartIndex;
            set => Config.LedStartIndex = value;
        }

        [Required]
        public int LedEndIndex
        {
            get => Config.LedEndIndex;
            set => Config.LedEndIndex = value;
        }

        protected EffectViewModel(IEventAggregator aggregator, ILedEffect config)
        {
            EventAggregator = aggregator;
            EventAggregator.SubscribeOnBackgroundThread(this);
            Config = config;
            Config.EffectType = GetType().Name;
        }

        public abstract Task<IList<ILedViewModel>> TickAsync(CancellationToken ct,IList<ILedViewModel> leds);

        public void Dispose()
        {
            EventAggregator.Unsubscribe(this);
        }
    }
}