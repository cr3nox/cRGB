using System;
using System.Linq;
using Caliburn.Micro;
using cRGB.Domain.Models.Device;
using cRGB.Domain.Models.Enums;
using cRGB.Domain.Models.System;
using cRGB.Domain.Services;
using cRGB.Domain.Services.System;
using cRGB.Modules.Common.Base;
using cRGB.Modules.Common.Extensions;
using cRGB.Tools.Interfaces.ViewModel;
using cRGB.WPF.Helpers;
using cRGB.WPF.Messages;
using cRGB.WPF.ServiceLocation.Factories;
using cRGB.WPF.ViewModels.Navigation;
using MaterialDesignThemes.Wpf;

namespace cRGB.WPF.ViewModels.Device
{
    public class DeviceListViewModel : ViewModelBase, INotifyMeOnAppExit, IDisposable
    {
        #region Properties

        readonly ILocalizationHelper _loc;
        readonly IEventAggregator _eventAggregator;
        readonly ISettingsService _settingsService;
        readonly IBlinkStickViewModelFactory _blinkStickViewModelFactory;
        readonly IMessageFactory _messageFactory;

        public IMenuItemViewModel Menu { get; set; }

        public BindableCollection<LedDeviceViewModel> LedDevices { get; set; } = new BindableCollection<LedDeviceViewModel>();

        public LedDeviceViewModel SelectedLedDevice { get; set; }

        #endregion

        public DeviceListViewModel(IEventAggregator aggregator, ILocalizationHelper loc, ISettingsService settingsService, 
            IBlinkStickViewModelFactory blinkStickViewModelFactory, IMessageFactory messageFactory)
        {
            _eventAggregator = aggregator;
            _loc = loc;
            _settingsService = settingsService;
            _blinkStickViewModelFactory = blinkStickViewModelFactory;
            _messageFactory = messageFactory;

            _eventAggregator.SubscribeOnUIThread(this);
        }

        public async void DeleteDevice(LedDeviceViewModel deviceViewModel)
        {
            // Todo: implement Generic yes / no Dialog like DialogComboBoxSelectionViewModel
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
            _blinkStickViewModelFactory.Release(vm);
        }

        #region Methods
        void LoadSavedDevices()
        {
            // saved BlinkSticks
            var blinkStickSettings = _settingsService.Settings.ConfiguredDevices;
            foreach (var blinkStickSetting in blinkStickSettings)
            {
                if(blinkStickSetting is IBlinkStickSettings setting)
                    AddBlinkStick(setting);
                // TODO: do other Devices after they have been implemented
            }
        }

        public void AddBlinkStick(IBlinkStickSettings settings = null)
        {
            var newDevice = settings == null ? _blinkStickViewModelFactory.Create() : _blinkStickViewModelFactory.Create(settings);
            LedDevices.Add(newDevice);

            var str = settings == null ? _loc.GetByKey("MenuItem_NewDevice") : settings.Name;

            newDevice.Create(Menu.CreateChild(newDevice, icon: PackIconKind.UsbFlashDriveOutline, true, str));
        }

        public void AddNewBlinkStick()
        {
            AddBlinkStick();
            SelectedLedDevice = LedDevices.Last();
            RowDoubleClick();
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
            var message = _messageFactory.Create(typeof(TreeViewSelectionChangedMessage));
            _eventAggregator.PublishOnUIThreadAsync(message);
        }

        #endregion Methods

        public void Init()
        {
            LoadSavedDevices();
        }

        public void OnAppExit()
        {

        }
        public void Dispose()
        {
            _eventAggregator.Unsubscribe(this);
        }
    }
}
