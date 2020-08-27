using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using BlinkStickDotNet;
using Caliburn.Micro;
using cRGB.Domain.Models.Device;
using cRGB.Domain.Models.Enums;
using cRGB.Domain.Services.Device;
using cRGB.Domain.Services.System;
using cRGB.Tools.Interfaces.ViewModel;
using cRGB.WPF.Helpers;
using cRGB.WPF.Messages;
using cRGB.WPF.ServiceLocation.Factories;
using cRGB.WPF.ViewModels.Event;
using cRGB.WPF.ViewModels.Navigation;
using MaterialDesignThemes.Wpf;

namespace cRGB.WPF.ViewModels.Device
{
    public class BlinkStickViewModel : LedDeviceViewModel, IHandle<DeviceSelectedMessage>, IRefresh, INotifyMeOnAppExit, IDisposable
    {
        #region Fields

        readonly IBlinkStickService _blinkStickService;
        BlinkStick _device;
        readonly IBlinkStickSettingsViewModelFactory _settingsFactory;

        #endregion

        #region Properties

        public BlinkStick Device
        {
            get => _device;
            set
            {
                _device = value ?? throw new ArgumentNullException(nameof(value));
                SettingsService.RegisterBlinkStickSettings(_device.Serial);
                _blinkStickService.AddBlinkStick(Device);
                InitDevice();
            }
        }
        
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 1)]
        public override string DeviceName
        {
            get => Settings?.DeviceName;
            set
            {
                if (value == null || Settings.DeviceName == value) return;

                Settings.DeviceName = value;
                DisplayName = DeviceName;
                ValidateProperty(value);
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
        
        public BlinkStickSettingsViewModel Settings { get; set; }

        public bool IsDeviceSelectionOpen { get; set; } = true;

        public BindableCollection<ILedViewModel> RChannelLedColors { get; set; }                        
        public BindableCollection<ILedViewModel> GChannelLedColors { get; set; }
        public BindableCollection<ILedViewModel> BChannelLedColors { get; set; }

        #endregion Properties


        public BlinkStickViewModel (
            IEventAggregator aggregator, ILocalizationHelper loc, ISettingsService settingsService,
            IBlinkStickService blinkStickService, IEventListViewModel eventListViewModel, DeviceSelectionViewModel deviceSelectionViewModel,
            IBlinkStickSettings settings, IBlinkStickSettingsViewModelFactory settingsFactory, ILedViewModelFactory ledFactory) 
            : base(aggregator, loc, settingsService, eventListViewModel, ledFactory)
        {
            _blinkStickService = blinkStickService;
            DeviceSelection = deviceSelectionViewModel;
            _settingsFactory = settingsFactory;

            DeviceSelection.AddBlinkSticks(_blinkStickService.FindAllNotAlreadyConfigured());
            InitSettings(settings);
        }

        private void InitSettings(IBlinkStickSettings settings)
        {
            if (Settings != null) return;

            Settings = _settingsFactory.Create(settings);
            Settings.PropertyChanged += Settings_PropertyChanged;

            Device ??= _blinkStickService.Find(Settings.SerialNumber);
            EventListViewModel.InitSettings(Settings.EventSettings);
        }

        private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DeviceName)) DisplayName = Settings.DeviceName;

            OnPropertyChanged(e);
        }

        public void Create(IMenuItemViewModel menu)
        {
            Menu = menu;
            // Device settings
            Menu.CreateChild(Settings, PackIconKind.Cog, true, Loc.GetByKey("Settings"));
            // Event
            Menu.CreateChild((EventListViewModel)EventListViewModel, PackIconKind.LightningBolt, true, Loc.GetByKey("Events"));
        }

        public void InitDevice()
        {
            IsDeviceSelectionOpen = false;
            Device.Mode = 2;
            Device.OpenDevice();
            InitLeds();
            Device.SetColorDelay = 0;
        }

        void InitLeds()
        {
            for (var i = 0; i < Settings.LedCount; i++)
            {
                var led = LedFactory.Create();
                led.Index = i;
                led.Enabled = !Settings.DisabledLeds.Contains(i);
                LedStates.Add(led);
            }

            Settings.RChannelLedColors = RChannelLedColors = new BindableCollection<ILedViewModel>(LedStates.Take(Settings.RChannelLedCount));
            Settings.GChannelLedColors = GChannelLedColors = new BindableCollection<ILedViewModel>(LedStates.Skip(Settings.RChannelLedCount).Take(Settings.GChannelLedCount));
            Settings.BChannelLedColors = BChannelLedColors = new BindableCollection<ILedViewModel>(LedStates.Skip(Settings.RChannelLedCount + Settings.GChannelLedCount).Take(Settings.BChannelLedCount));
            EventListViewModel.SetHighestLedIndex(LedStates.Where(o => o.Enabled).Max(o => o.Index));
            Device.SetColor(0,0,0);
        }

        public IList<ILedViewModel> GetEnabledLedViewModels()
        {
            return LedStates.Where(o => o.Enabled).ToList();
        }


        public Task HandleAsync(DeviceSelectedMessage message, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                if (!(message.SelectedDevice is BlinkStick))
                    return ;

                Device = (BlinkStick) message.SelectedDevice;
                SerialNumber = Device.Serial;
                CloseDialog();
            }, cancellationToken);
        }

        public override void RunEvents()
        {
            Cts = new CancellationTokenSource();
            Task.Run(async () =>
            {
                foreach (var eventViewModel in EventListViewModel.Events.Where(o => o.IsEnabled))
                {
                    if (!eventViewModel.CanActivate) continue;

                    // Do Nothing if Event is already Running
                    if (eventViewModel == EventListViewModel.ActiveEvent)
                        break;

                    // Stop Active Event
                    EventListViewModel.ActiveEvent?.Stop();
                    // Start New Event
                    EventListViewModel.ActiveEvent = eventViewModel;
                    try
                    {
                        if (false)
                        {
                            await foreach (var result in eventViewModel.Activate(Cts.Token, LedStates))
                            {
                            await SetBlinkStickColors(result);
                            }
                        }
                        else
                        {
                             Device.SetColor(255, 0, 0);
                            await Task.Delay(2000);
                             Device.SetColor(0, 255, 0);
                            await Task.Delay(2000);
                             Device.SetColor(0, 0, 255);
                            await Task.Delay(2000);
                             Device.SetColor(255, 0, 0);
                            await Task.Delay(2000);
                             Device.SetColor(0, 255, 255);
                            await Task.Delay(2000);
                             Device.SetColor(128, 128, 128);
                            await Task.Delay(2000);
                             Device.SetColor(128, 0, 0);
                            await Task.Delay(2000);
                             Device.SetColor(0, 128, 128);
                            await Task.Delay(2000);
                             Device.SetColor(55, 55, 55);
                            await Task.Delay(2000);
                             Device.SetColor(0, 0, 0);
                            await Task.Delay(2000);
                            Device.TurnOff();
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        EventListViewModel.ActiveEvent = null;
                    }
                    break;
                }
            }, Cts.Token);
        }

        private Task SetBlinkStickColors(IList<ILedViewModel> leds)
        {
            return Task.Run(() =>
            {
                var channels = new List<(int, int, bool)>()
                {
                    ((int)EBlinkStickChannel.R, Settings.RChannelLedCount, Settings.RChannelLedInvert),
                    ((int)EBlinkStickChannel.G, Settings.GChannelLedCount, Settings.GChannelLedInvert),
                    ((int)EBlinkStickChannel.B, Settings.BChannelLedCount, Settings.BChannelLedInvert)
                };

                var count = 0;
                foreach (var (channel, amountOfLeds, invertLeds) in channels)
                {
                    var bytes = new List<byte>();
                    // for each LED in each Channel
                    //if (invertLeds)
                    //{
                    //    for (var i = count; i > amountOfLeds; i--)
                    //    {
                    //        bytes.AddRange(GetLedColorsAsBytes(leds[count]));
                    //        count++;
                    //    }
                    //}
                    //else
                    //{
                        for (var i = 0; i < amountOfLeds; i++)
                        {
                            bytes.AddRange(GetLedColorsAsBytes(leds[count]));
                            count++;
                        }
                    //}
                    /*
                     * G count 64 index 63
                     *
                     */
                    Device.SetColors((byte)channel, bytes.ToArray());
                }
            });

        }
        
        private IEnumerable<byte> GetLedColorsAsBytes(ILedViewModel led)
        {
            // if not enabled stay black
            if (!led.Enabled)
                return new byte[] {0, 0, 0};

            var brightness = (double)Settings.Brightness / 100;
            // get colors from LedViewModel, multiply with brightness (example: Settings.Brightness = 95 (%) => ledR * 0.95)
            return new[] {(byte)(led.G * brightness), (byte)(led.R * brightness), (byte)(led.B * brightness)};
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

        public new void Dispose()
        {
            EventAggregator.Unsubscribe(this);
            Menu = null;
            _device?.Dispose();

            base.Dispose();
        }

        public override void Shutdown()
        {
            Cts?.Cancel();
            //Todo: not working. BlinkStick still active °_° wtf
            Device.TurnOff();
        }
    }
}
