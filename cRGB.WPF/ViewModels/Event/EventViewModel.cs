﻿#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System.Collections.Generic;
using Caliburn.Micro;
using cRGB.Domain.Models.Event;
using cRGB.Modules.Common.ViewModelBase;
using cRGB.WPF.ViewModels.Effect.Effects;

namespace cRGB.WPF.ViewModels.Event
{
    public abstract class EventViewModel : ViewModelBase, IEventViewModel
    {
        public abstract bool CanActivate { get; }
        public bool IsEnabled { get; set; }
        public ILedEvent Model { get; set; }

        public BindableCollection<EffectViewModel> Effects { get; set; } = new BindableCollection<EffectViewModel>();

    }
}