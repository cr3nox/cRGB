#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System.Collections.Generic;
using Caliburn.Micro;
using cRGB.Domain.Models.Event;
using cRGB.Modules.Common.Base;
using cRGB.WPF.Helpers;
using cRGB.WPF.ViewModels.Effect.Effects;

namespace cRGB.WPF.ViewModels.Event
{
    public abstract class EventViewModel : ViewModelBase, IEventViewModel
    {
        #region Fields

        protected IEventAggregator EventAggregator;
        readonly ILocalizationHelper _loc;

        #endregion

        #region Properties

        public abstract bool CanActivate { get; }
        public bool IsEnabled { get; set; }
        public ILedEvent Model { get; set; }

        public BindableCollection<EffectViewModel> Effects { get; set; } = new BindableCollection<EffectViewModel>();

        #endregion

        #region ctor

        protected EventViewModel(IEventAggregator aggregator, ILocalizationHelper loc, ILedEvent model)
        {
            EventAggregator = aggregator;
            _loc = loc;
            Model = model;
        }

        #endregion

        #region Methods

        public void Init()
        {
            if (Model == null)
                return;

        }

        #endregion
    }
}