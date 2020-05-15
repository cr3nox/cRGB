using System.Collections.Generic;
using Caliburn.Micro;
using Castle.Core;
using cRGB.Domain.Models.Device;
using cRGB.Domain.Models.Enums;
using cRGB.Domain.Services.System;
using cRGB.Modules.Common.ViewModelBase;
using cRGB.WPF.Helpers;
using cRGB.WPF.ViewModels.Event;
using cRGB.WPF.ViewModels.Menu;

namespace cRGB.WPF.ViewModels.Device
{
    public abstract class LedDeviceViewModel : ViewModelBase, ILedDeviceViewModel
    {
        protected readonly IEventAggregator _eventAggregator;
        protected readonly ILocalizationHelper _loc;
        protected readonly ISettingsService _settingsService;
        protected readonly IEventListViewModel _eventListViewModel;
        public virtual string DeviceName { get; set; }
        public virtual string Description { get; set; }
        public virtual ELedDeviceType DeviceType { get; set; }

        public List<LedViewModel> LedStates { get; set; } = new List<LedViewModel>();

        [DoNotWire]
        public MenuItemViewModel Menu { get; set; }

        protected LedDeviceViewModel(IEventAggregator aggregator, ILocalizationHelper loc, ISettingsService settingsService, IEventListViewModel eventListViewModel)
        {
            _eventAggregator = aggregator;
            _loc = loc;
            _settingsService = settingsService;
            _eventListViewModel = eventListViewModel;
        }

    }
}
