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
using cRGB.Domain.Enums;
using cRGB.Domain.Models.Device;
using cRGB.Domain.Services;
using cRGB.Tools.Interfaces.ViewModel;
using cRGB.WPF.Helpers;
using cRGB.WPF.Messages;
using cRGB.WPF.ViewModels.Menu;
using MaterialDesignThemes.Wpf;

namespace cRGB.WPF.ViewModels.Device
{
    [DataContract]
    public class BlinkStickViewModel : LedDeviceViewModel, IHandle<DeviceSelectedMessage>, IRefresh, INotifyMeOnAppExit
    {
        #region Properties

        readonly IBlinkStickService _blinkStickService;
        readonly IEventAggregator _eventAggregator;
        readonly ILocalizationHelper _loc;
        BlinkStick _device;

        public BlinkStick Device
        {
            get => _device;
            set
            {
                _device = value ?? throw new ArgumentNullException(nameof(value));
                Settings.SerialNumber = _device.Serial;
                _blinkStickService.AddBlinkStick(Device);
                InitDevice();
            }
        }

        public override string DeviceName
        {
            get => Settings.DeviceName;
            set
            {
                if (value == null || Settings.DeviceName == value) return;

                Settings.DeviceName = value;
            }
        }

        /// <summary>
        /// Gets called when DeviceName throws PropertyChanged
        /// </summary>
        public void OnDeviceNameChanged()
        {
            DisplayName = DeviceName;
        }

        public override string Description
        {
            get => Settings.Description;
            set
            {
                if (value == null || Settings.Description == value) return;

                Settings.Description = value;
            }
        }

        public string SerialNumber
        {
            get => Settings.SerialNumber;
            set
            {
                if (value == null || Settings.SerialNumber == value) return;

                Settings.SerialNumber = value;
            }
        }

        public DeviceSelectionViewModel DeviceSelection { get; set; }
        //public DeviceSelectionView DeviceSelectionView => ViewLocator.LocateForModel(DeviceSelection, null, null);
        
        [DataMember]
        public BlinkStickSettingsViewModel Settings { get; set; }

        public bool IsDeviceSelectionOpen { get; set; } = true;

        public BindableCollection<LedViewModel> RChannelLedColors { get; set; }                        
        public BindableCollection<LedViewModel> GChannelLedColors { get; set; }
        public BindableCollection<LedViewModel> BChannelLedColors { get; set; }

        #endregion Properties


        public BlinkStickViewModel(IBlinkStickService blinkStickService, IEventAggregator aggregator, ILocalizationHelper loc)
        {
            _blinkStickService = blinkStickService;
            DeviceSelection = IoC.Get<DeviceSelectionViewModel>();
            DeviceSelection.AddBlinkSticks(_blinkStickService.FindAllNotAlreadyConfigured());
            _eventAggregator = aggregator;
            _eventAggregator.SubscribeOnUIThread(this);
            _loc = loc;
            InitSettings();
        }
        private void InitSettings()
        {
            if (Settings != null) return;

            Settings = IoC.Get<BlinkStickSettingsViewModel>();
            Settings.PropertyChanged += Settings_PropertyChanged;
        }

        private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(DeviceName)) return;

            OnDeviceNameChanged();
            OnPropertyChanged(e);
        }

        public void Create(MenuItemViewModel menu)
        {
            if (Menu != null)
                return;

            Menu = menu;
            Menu.CreateChild(Settings, PackIconKind.Cog, true,_loc.GetByKey("Settings"));
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
            for (var i = 0; i < Settings.LedCount; i++)
            {
                var led = IoC.Get<LedViewModel>();
                led.Index = i;
                LedStates.Add(led);
            }

            Settings.RChannelLedColors = RChannelLedColors = new BindableCollection<LedViewModel>(LedStates.Take(Settings.RChannelLedCount));
            Settings.GChannelLedColors = GChannelLedColors = new BindableCollection<LedViewModel>(LedStates.Skip(Settings.RChannelLedCount).Take(Settings.GChannelLedCount));
            Settings.BChannelLedColors = BChannelLedColors = new BindableCollection<LedViewModel>(LedStates.Skip(Settings.RChannelLedCount + Settings.GChannelLedCount).Take(Settings.BChannelLedCount));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="channel">R = 0, G = 1, B = 2</param>
        /// <returns></returns>
        //public IEnumerable<Led> GetEnabledLedViewModels(EBlinkStickChannel channel)
        //{
        //    return channel switch
        //    {
        //        EBlinkStickChannel.R => RChannelLedColors.Where(o => o.Enabled),
        //        EBlinkStickChannel.G => GChannelLedColors.Where(o => o.Enabled),
        //        EBlinkStickChannel.B => BChannelLedColors.Where(o => o.Enabled),
        //        _ => throw new ArgumentOutOfRangeException(nameof(channel), channel, null)
        //    };
        //}

        //public (IEnumerable<Led> R, IEnumerable<Led> G, IEnumerable<Led> B) GetEnabledLedViewModels()
        //{
        //    return (RChannelLedColors.Where(o => o.Enabled), GChannelLedColors.Where(o => o.Enabled),
        //        BChannelLedColors.Where(o => o.Enabled));
        //}

        public void DeviceSelectionButton()
        {
            //var ser = (XmlSerializationService)IoC.Get<IXmlSerializationService>();
            //var o = ser.Serialize(this);
            //ser.CopyStream(o, Path.Combine(Directory.GetCurrentDirectory(), "BlinkStickSettings.xml").ToString());
            //DeviceSelection.Devices = new BindableCollection<string>();
            //DeviceSelection.AddBlinkSticks(_blinkStickService.FindAllNotAlreadyConfigured());
            //IsDeviceSelectionOpen = !IsDeviceSelectionOpen;
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

        public void SaveSettings()
        {
            Settings.DisabledLeds = new BindableCollection<int>();
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
    }
}
