﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using Castle.Core;
using cRGB.Domain.Models.Enums;
using cRGB.Domain.Services.System;
using cRGB.Modules.Common.Base;
using cRGB.WPF.Helpers;
using cRGB.WPF.ServiceLocation.Factories;
using cRGB.WPF.ViewModels.Event;
using cRGB.WPF.ViewModels.Navigation;
using PropertyChanged;

namespace cRGB.WPF.ViewModels.Device
{
    public abstract class LedDeviceViewModel : ValidationViewModelBase, ILedDeviceViewModel, IDisposable
    {
        protected readonly IEventAggregator EventAggregator;
        protected readonly ILocalizationHelper Loc;
        protected readonly ISettingsService SettingsService;
        protected readonly IEventListViewModel EventListViewModel;
        protected readonly ILedViewModelFactory LedFactory;
        protected CancellationTokenSource Cts;

        public virtual string DeviceName { get; set; }
        public virtual string Description { get; set; }
        public virtual ELedDeviceType DeviceType { get; set; }
        [DoNotNotify]
        public IList<ILedViewModel> LedStates { get; set; } = new List<ILedViewModel>();



        [DoNotWire]
        public IMenuItemViewModel Menu { get; set; }

        protected LedDeviceViewModel(IEventAggregator aggregator, ILocalizationHelper loc, ISettingsService settingsService, IEventListViewModel eventListViewModel, ILedViewModelFactory ledFactory)
        {
            EventAggregator = aggregator;
            Loc = loc;
            SettingsService = settingsService;
            EventListViewModel = eventListViewModel;
            LedFactory = ledFactory;
            EventAggregator.SubscribeOnUIThread(this);
        }

        public void Dispose()
        {
            EventAggregator.Unsubscribe(this);
            foreach (var ledViewModel in LedStates)
            {
                LedFactory.Release(ledViewModel);
            }
        }

        public abstract void RunEvents();

        public void StopEvents()
        {
            Cts.Cancel();
            EventListViewModel.ActiveEvent?.Stop();
            EventListViewModel.ActiveEvent = null;
        }

        public abstract void Shutdown();
    }
}
