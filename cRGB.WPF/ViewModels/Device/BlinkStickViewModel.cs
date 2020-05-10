using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using BlinkStickDotNet;
using Caliburn.Micro;
using cRGB.Domain.Models.Device;
using cRGB.Domain.Services;
using cRGB.Domain.Services.Device;
using cRGB.Domain.Services.System;
using cRGB.Tools.Interfaces.ViewModel;
using cRGB.WPF.Helpers;
using cRGB.WPF.Messages;
using cRGB.WPF.ViewModels.Event;
using cRGB.WPF.ViewModels.Menu;
using MaterialDesignThemes.Wpf;

namespace cRGB.WPF.ViewModels.Device
{
    public class BlinkStickViewModel : LedDeviceViewModel, IHandle<DeviceSelectedMessage>, IRefresh, INotifyMeOnAppExit
    {
        #region Fields

        readonly IBlinkStickService _blinkStickService;
        readonly IEventAggregator _eventAggregator;
        readonly ILocalizationHelper _loc;
        readonly ISettingsService _settingsService;
        BlinkStick _device;
        readonly IEventListViewModel _eventListViewModel;

        #endregion

        #region Properties

        public BlinkStick Device
        {
            get => _device;
            set
            {
                _device = value ?? throw new ArgumentNullException(nameof(value));
                InitSettings(_device.Serial);
                _blinkStickService.AddBlinkStick(Device);
                InitDevice();
            }
        }

        public override string DeviceName
        {
            get => Settings?.DeviceName;
            set
            {
                if (value == null || Settings.DeviceName == value) return;

                Settings.DeviceName = value;
                DisplayName = DeviceName;
            }
        }

        public override string Description
        {
            get => Settings?.Description;
            set
            {
                if (value == null || Settings.Description == value) return;

                Settings.Description = value;
            }
        }

        public string SerialNumber
        {
            get => Settings?.SerialNumber;
            set
            {
                if (value == null || Settings.SerialNumber == value) return;

                Settings.SerialNumber = value;
            }
        }

        public DeviceSelectionViewModel DeviceSelection { get; set; }
        //public DeviceSelectionView DeviceSelectionView => ViewLocator.LocateForModel(DeviceSelection, null, null);
        
        public BlinkStickSettingsViewModel Settings { get; set; }

        public bool IsDeviceSelectionOpen { get; set; } = true;

        public BindableCollection<LedViewModel> RChannelLedColors { get; set; }                        
        public BindableCollection<LedViewModel> GChannelLedColors { get; set; }
        public BindableCollection<LedViewModel> BChannelLedColors { get; set; }

        #endregion Properties


        public BlinkStickViewModel(IBlinkStickService blinkStickService, IEventAggregator aggregator, ILocalizationHelper loc, ISettingsService settingsService, 
            IEventListViewModel eventListViewModel, DeviceSelectionViewModel deviceSelectionViewModel)
        {
            _blinkStickService = blinkStickService;
            DeviceSelection = deviceSelectionViewModel;
            DeviceSelection.AddBlinkSticks(_blinkStickService.FindAllNotAlreadyConfigured());
            _eventAggregator = aggregator;
            _eventAggregator.SubscribeOnUIThread(this);
            _loc = loc;
            _settingsService = settingsService;
            _eventListViewModel = eventListViewModel;
        }

        public void InitSettings(string serial)
        {
            if (Settings != null) return;

            Settings = new BlinkStickSettingsViewModel(_settingsService.RegisterBlinkStickSettings(serial));
            IoC.BuildUp(Settings);
            Settings.PropertyChanged += Settings_PropertyChanged;

            Device ??= _blinkStickService.Find(Settings.SerialNumber);

            Menu.CreateChild(Settings, PackIconKind.Cog, true, _loc.GetByKey("Settings"));
        }

        public void InitSettings(IBlinkStickSettings settings)
        {
            if (Settings != null) return;

            Settings = new BlinkStickSettingsViewModel(settings);
            IoC.BuildUp(Settings);
            Settings.PropertyChanged += Settings_PropertyChanged;

            Device ??= _blinkStickService.Find(Settings.SerialNumber);

            Menu.CreateChild(Settings, PackIconKind.Cog, true, _loc.GetByKey("Settings"));
        }

        private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(DeviceName))
                DisplayName = Settings.DeviceName;
            OnPropertyChanged(e);
        }

        public void Create(MenuItemViewModel menu)
        {
            Menu = menu;
        }

        public void LoadExistingSettings()
        {
            //loading
        }

        public void InitDevice()
        {
            IsDeviceSelectionOpen = false;
            Device.Mode = 2;
            Device.OpenDevice();
            InitLeds();
        }

        void InitLeds()
        {
            for (var i = 0; i < Settings.LedCount; i++)
            {
                var led = IoC.Get<LedViewModel>();
                led.Index = i;
                led.Enabled = !Settings.DisabledLeds.Contains(i);
                LedStates.Add(led);
            }

            Settings.RChannelLedColors = RChannelLedColors = new BindableCollection<LedViewModel>(LedStates.Take(Settings.RChannelLedCount));
            Settings.GChannelLedColors = GChannelLedColors = new BindableCollection<LedViewModel>(LedStates.Skip(Settings.RChannelLedCount).Take(Settings.GChannelLedCount));
            Settings.BChannelLedColors = BChannelLedColors = new BindableCollection<LedViewModel>(LedStates.Skip(Settings.RChannelLedCount + Settings.GChannelLedCount).Take(Settings.BChannelLedCount));
        }

        public List<LedViewModel> GetEnabledLedViewModels()
        {
            return LedStates.Where(o => o.Enabled).ToList();
        }


        public Task HandleAsync(DeviceSelectedMessage message, CancellationToken cancellationToken)
        {
            if (!(message.SelectedDevice is BlinkStick))
            {
                return Task.CompletedTask;
            }
            Device = (BlinkStick)message.SelectedDevice;
            SerialNumber = Device.Serial;
            CloseDialog();
            return Task.CompletedTask;
        }

        private void CloseDialog()
        {
            IsDeviceSelectionOpen = false;
        }

        public void RefreshProperties()
        {
            foreach (var ledState in LedStates)
            {
                ledState.FirePropertyChanged();
            }
        }

        /// <summary>
        /// Has to be called before the App Exits.
        /// Saves the current Disabled LED's to Settings
        /// </summary>
        public void OnAppExit()
        {
            if (Settings == null)
                return;
            Settings.DisabledLeds = new BindableCollection<int>(LedStates.Where(o => !o.Enabled).Select(o => o.Index));
        }

        public void Init()
        {
            InitEventVm();
        }

        void InitEventVm()
        {
            Menu.CreateChild((EventListViewModel)_eventListViewModel, PackIconKind.LightningBolt, true, _loc.GetByKey("Events"));
        }
    }
}
