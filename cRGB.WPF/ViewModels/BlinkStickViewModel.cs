using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using BlinkStickDotNet;
using Caliburn.Micro;
using cRGB.Domain;
using cRGB.Domain.Models;
using cRGB.WPF.Messages;
using cRGB.WPF.ViewModels.Devices;
using cRGB.WPF.ViewModels.Menu;
using PropertyChanged;

namespace cRGB.WPF.ViewModels
{
    [Serializable]
    public class BlinkStickViewModel : LedDeviceViewModel, IHandle<DeviceSelectedMessage>
    {
        #region Properties

        readonly BlinkStickController _blinkStickController;
        readonly IEventAggregator _eventAggregator;
        public MenuItemViewModel Menu { get; set; }

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

        public DeviceSelectionViewModel DeviceSelection { get; set; }
        
        public BlinkStickSettingsViewModel Settings { get; set; }

        public BindableCollection<LED> RChannelLedColors { get; set; } = new BindableCollection<LED>();
        public BindableCollection<LED> GChannelLedColors { get; set; } = new BindableCollection<LED>();
        public BindableCollection<LED> BChannelLedColors { get; set; } = new BindableCollection<LED>();


        public BlinkStickViewModel(BlinkStickController blinkStickController, IEventAggregator aggregator)
        {
            _blinkStickController = blinkStickController;
            DeviceSelection = IoC.Get<DeviceSelectionViewModel>();
            DeviceSelection.AddBlinkSticks(_blinkStickController.FindAllNotAlreadyConfigured());
            _eventAggregator = aggregator;
            _eventAggregator.SubscribeOnUIThread(this);
        }

        public void LoadExistingSettings()
        {
            //loading
        }

        #endregion Properties

        public void Init()
        {
            if (Device == null)
                if (!string.IsNullOrEmpty(Settings?.SerialNumber))
                {
                    Device = _blinkStickController.Find(Settings.SerialNumber);
                }
                else
                    return;
                
            Device.Mode = 2;
            Device.OpenDevice();
        }

        public Task HandleAsync(DeviceSelectedMessage message, CancellationToken cancellationToken)
        {
            Device = message.SelectedDevice as BlinkStick;
            if (Device == null)
                return null;
        
            _blinkStickController.AddBlinkStick(Device);
            Settings.Name = string.IsNullOrEmpty(Device.InfoBlock1)
                    ? "New Device"
                    : Device.InfoBlock1;
            Init();
            return Task.CompletedTask;
        }

        public void OpenOptions()
        {
            
        }
    }
}
