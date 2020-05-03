using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlinkStickDotNet;
using Caliburn.Micro;
using cRGB.Domain;
using cRGB.Domain.Enums;
using cRGB.Domain.Models.Device;
using cRGB.Domain.Services;
using cRGB.WPF.Converters;
using cRGB.WPF.Helpers;
using cRGB.WPF.Messages;
using cRGB.WPF.ViewModels.Menu;
using cRGB.WPF.Views.Device;
using PropertyChanged;

namespace cRGB.WPF.ViewModels.Device
{
    [Serializable]
    public class BlinkStickViewModel : LedDeviceViewModel, IHandle<DeviceSelectedMessage>
    {
        #region Properties

        [NonSerialized]
        readonly IBlinkStickService _blinkStickService;
        [NonSerialized]
        readonly IEventAggregator _eventAggregator;

        [NonSerialized]
        BlinkStick _device;

        public BlinkStick Device
        {
            get => _device;
            set
            {
                _device = value ?? throw new ArgumentNullException(nameof(value));
                InitSettings();
                Settings.SerialNumber = _device.Serial;
                _blinkStickService.AddBlinkStick(Device);
                InitDevice();
            }
        }

        public override string DeviceName
        {
            get => Settings?.Name;
            set
            {
                InitSettings();

                if (value == null || Settings.Name == value) return;

                Settings.Name = value;
                DisplayName = DeviceName;
            }
        }

        public override string Description { get; set; }

        public DeviceSelectionViewModel DeviceSelection { get; set; }
        //public DeviceSelectionView DeviceSelectionView => ViewLocator.LocateForModel(DeviceSelection, null, null);
        
        public BlinkStickSettingsViewModel Settings { get; set; }

        public bool IsDeviceSelectionOpen { get; set; } = true;

        #endregion Properties


        public BlinkStickViewModel(IBlinkStickService blinkStickService, IEventAggregator aggregator)
        {
            _blinkStickService = blinkStickService;
            DeviceSelection = IoC.Get<DeviceSelectionViewModel>();
            DeviceSelection.AddBlinkSticks(_blinkStickService.FindAllNotAlreadyConfigured());
            _eventAggregator = aggregator;
            _eventAggregator.SubscribeOnUIThread(this);
        }
        private void InitSettings()
        {
            if (Settings == null)
                Settings = IoC.Get<BlinkStickSettingsViewModel>();
        }

        public void Create(MenuItemViewModel menu = null)
        {
            if (Settings == null)
            {
                Settings = IoC.Get<BlinkStickSettingsViewModel>();
            }

            if (menu != null)
                Menu = menu;
        }

        public void LoadExistingSettings()
        {
            //loading
        }

        public void InitDevice()
        {
            if (Device == null)
                if (!string.IsNullOrEmpty(Settings?.SerialNumber))
                {
                    Device = _blinkStickService.Find(Settings.SerialNumber);
                }
                else
                    return;
                
            Device.Mode = 2;
            Device.OpenDevice();
            InitLeds();
        }

        void InitLeds()
        {
            RChannelLedColors = new BindableCollection<LedViewModel>();
            GChannelLedColors = new BindableCollection<LedViewModel>();
            BChannelLedColors = new BindableCollection<LedViewModel>();

            for (var i = 0; i < Settings.RChannelLedCount; i++)
            {
                var led = IoC.Get<LedViewModel>();
                led.Index = i;
                RChannelLedColors.Add(led);
                led.SetLedColors(255,0,0);
            }            
            
            for (var i = 0; i < Settings.GChannelLedCount; i++)
            {
                var led = IoC.Get<LedViewModel>();
                led.Index = i;
                GChannelLedColors.Add(led);
            }            
            
            for (var i = 0; i < Settings.BChannelLedCount; i++)
            {
                var led = IoC.Get<LedViewModel>();
                led.Index = i;
                BChannelLedColors.Add(led);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="channel">R = 0, G = 1, B = 2</param>
        /// <returns></returns>
        public IEnumerable<LedViewModel> GetEnabledLedViewModels(EBlinkStickChannel channel)
        {
            return channel switch
            {
                EBlinkStickChannel.R => RChannelLedColors.Where(o => o.Enabled),
                EBlinkStickChannel.G => GChannelLedColors.Where(o => o.Enabled),
                EBlinkStickChannel.B => BChannelLedColors.Where(o => o.Enabled),
                _ => throw new ArgumentOutOfRangeException(nameof(channel), channel, null)
            }; 
        }

        public (IEnumerable<LedViewModel>, IEnumerable<LedViewModel>, IEnumerable<LedViewModel>) GetEnabledLedViewModels()
        {
            return (RChannelLedColors.Where(o => o.Enabled), GChannelLedColors.Where(o => o.Enabled),
                BChannelLedColors.Where(o => o.Enabled));
        }

        public void DeviceSelectionButton()
        {
            DeviceSelection.Devices = new BindableCollection<string>();
            DeviceSelection.AddBlinkSticks(_blinkStickService.FindAllNotAlreadyConfigured());
            IsDeviceSelectionOpen = !IsDeviceSelectionOpen;
        }

        public Task HandleAsync(DeviceSelectedMessage message, CancellationToken cancellationToken)
        {
            if (!(message.SelectedDevice is BlinkStick))
            {
                CloseDialog();
                return Task.CompletedTask;
            }
            Device = (BlinkStick)message.SelectedDevice;
            CloseDialog();
            return Task.CompletedTask;
        }

        private void CloseDialog()
        {
            IsDeviceSelectionOpen = false;
        }
    }
}
