using System;
using BlinkStickDotNet;
using Caliburn.Micro;
using cRGB.Domain;
using cRGB.WPF.Helpers;
using cRGB.WPF.ViewModels.Menu;
using cRGB.WPF.Views.Device;

namespace cRGB.WPF.ViewModels.Device
{
    [Serializable]
    public class BlinkStickViewModel : LedDeviceViewModel, INotifyMeOnMenuSelect
    {
        #region Properties

        readonly BlinkStickService _blinkStickService;
        readonly IEventAggregator _eventAggregator;

        BlinkStick _device;
        public BlinkStick Device
        {
            get => _device;
            set
            {
                _device = value ?? throw new ArgumentNullException(nameof(value));
                if(Settings == null)
                    Settings = IoC.Get<BlinkStickSettingsViewModel>();
                Settings.SerialNumber = _device.Serial;
            }
        }
        public override string Name
        {
            get => Settings?.Name;
            set
            {
                if (value == null || Settings.Name == value) return;
                Settings.Name = value;
            }
        }

        public override string Description { get; set; }



        public DeviceSelectionViewModel DeviceSelection { get; set; }
        //public DeviceSelectionView DeviceSelectionView => ViewLocator.LocateForModel(DeviceSelection, null, null);
        
        public BlinkStickSettingsViewModel Settings { get; set; }

        public bool IsDeviceSelectionOpen { get; set; } = true;

        #endregion Properties


        public BlinkStickViewModel(BlinkStickService blinkStickService, IEventAggregator aggregator)
        {
            _blinkStickService = blinkStickService;
            DeviceSelection = IoC.Get<DeviceSelectionViewModel>();
            DeviceSelection.AddBlinkSticks(_blinkStickService.FindAllNotAlreadyConfigured());
            _eventAggregator = aggregator;
            _eventAggregator.SubscribeOnUIThread(this);
        }

        public void Create(string name = "", MenuItemViewModel menu = null)
        {
            if (Settings == null)
            {
                Settings = IoC.Get<BlinkStickSettingsViewModel>();
                Name = name;
            }

            if (menu != null)
                Menu = menu;
        }

        public void LoadExistingSettings()
        {
            //loading
        }

        public void Init()
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
        }

        public void OpenOptions()
        {
            
        }

        public void OnMenuSelect()
        {
            //throw new NotImplementedException();
        }

        public void DeviceSelectionButton()
        {
            DeviceSelection.Devices = new BindableCollection<string>();
            DeviceSelection.AddBlinkSticks(_blinkStickService.FindAllNotAlreadyConfigured());
            IsDeviceSelectionOpen = !IsDeviceSelectionOpen;
        }
    }
}
