#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System;
using System.Drawing;
using System.Threading.Tasks;
using Caliburn.Micro;
using cRGB.Modules.Common.Base;

namespace cRGB.WPF.ViewModels.Effect.Effects
{
    public abstract class EffectViewModel : ViewModelBase, IEffectViewModel, IDisposable
    {
        public bool IsEnabled { get; set; }
        protected IEventAggregator EventAggregator;

        protected EffectViewModel(IEventAggregator aggregator)
        {
            EventAggregator = aggregator;
            EventAggregator.SubscribeOnBackgroundThread(this);
        }

        public abstract Task<byte[]> TickAsync(int ledCount);

        public void Dispose()
        {
            EventAggregator.Unsubscribe(this);
        }
    }
}