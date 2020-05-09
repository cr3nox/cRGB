using System.Linq;
using Caliburn.Micro;
using cRGB.Domain.Models.Device;
using cRGB.Domain.Models.Enums;
using cRGB.Domain.Services;
using cRGB.Domain.Services.System;
using cRGB.WPF.Extensions;
using cRGB.WPF.Helpers;
using cRGB.WPF.Messages;
using cRGB.WPF.ViewModels.Menu;
using MaterialDesignThemes.Wpf;

namespace cRGB.WPF.ViewModels.Device
{
    public class DeviceListViewModel : ViewModelBase
    {
        #region Properties

        readonly ILocalizationHelper _loc;
        readonly IEventAggregator _eventAggregator;
        readonly ISettingsService _settingsService;

        public MenuItemViewModel Menu { get; set; }

        public BindableCollection<LedDeviceViewModel> LedDevices { get; set; } = new BindableCollection<LedDeviceViewModel>();

        public LedDeviceViewModel SelectedLedDevice { get; set; }

        #endregion

        public DeviceListViewModel(IEventAggregator aggregator, ILocalizationHelper loc, ISettingsService settingsService)
        {
            _eventAggregator = aggregator;
            _eventAggregator.SubscribeOnUIThread(this);
            _loc = loc;
            _settingsService = settingsService;
        }

        public async void DeleteDevice(LedDeviceViewModel deviceViewModel)
        {
            var result = await DialogHost.Show(deviceViewModel, delegate (object sender, DialogClosingEventArgs args)
            {
                if (!(args.Parameter is DialogAnswer answer) || answer != DialogAnswer.Yes)
                    return;

                if (deviceViewModel is BlinkStickViewModel vm)
                {
                    DeleteBlinkStick(vm);
                }
            });
        }

        void DeleteBlinkStick(BlinkStickViewModel vm)
        {
            _settingsService.Settings.ConfiguredDevices.Remove(vm.Settings?.BlinkStickSettings);
            if (SelectedLedDevice == vm)
                SelectedLedDevice = null;
            LedDevices.Remove(vm);
            Menu.Children.RemoveAll(o => o.ViewModel == vm);
        }

        #region Methods
        void LoadSavedDevices()
        {
            // saved BlinkSticks
            var blinkStickSettings = _settingsService.Settings.ConfiguredDevices;
            foreach (var blinkStickSetting in blinkStickSettings)
            {
                if(blinkStickSetting is IBlinkStickSettings)
                    AddBlinkStick((BlinkStickSettings)blinkStickSetting);
                // TODO: do other Devices after they have been implemented
            }
        }

        public void AddBlinkStick(IBlinkStickSettings settings = null)
        {
            var newDevice = IoC.Get<BlinkStickViewModel>();
            LedDevices.Add(newDevice);

            var str = settings == null ? _loc.GetByKey("MenuItem_NewDevice") : settings.Name;

            newDevice.Create(Menu.CreateChild(newDevice, icon: PackIconKind.UsbFlashDriveOutline, true, str));

            if (settings != null)
                newDevice.InitSettings(settings);
            newDevice.Init();
        }

        public void AddNewBlinkStick()
        {
            AddBlinkStick();
        }

        public void AddArduino()
        {
            //throw new NotImplementedException();
        }

        public void AddTestDevice()
        {
            //throw new NotImplementedException();
        }

        public void RowDoubleClick()
        {
            if (SelectedLedDevice == null)
                return;
            SelectedLedDevice.Menu.IsSelected = true;
            _eventAggregator.PublishOnUIThreadAsync(new TreeViewSelectionChangedMessage(SelectedLedDevice.Menu));
        }

        #endregion Methods

        public void Init()
        {
            LoadSavedDevices();
        }
    }
}
