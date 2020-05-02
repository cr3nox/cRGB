using Caliburn.Micro;
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

        public MenuItemViewModel Menu { get; set; }

        public BindableCollection<LedDeviceViewModel> LedDevices { get; set; } = new BindableCollection<LedDeviceViewModel>();

        public LedDeviceViewModel SelectedLedDevice { get; set; }

        #endregion

        public DeviceListViewModel(IEventAggregator aggregator, ILocalizationHelper loc)
        {
            _eventAggregator = aggregator;
            _eventAggregator.SubscribeOnUIThread(this);
            _loc = loc;
        }

        #region Methods

        public void AddBlinkStick()
        {
            var newDevice = IoC.Get<BlinkStickViewModel>();
            LedDevices.Add(newDevice);
            var name = _loc.GetByKey("MenuItem_NewDevice");
            newDevice.Create(name, Menu.CreateChild(newDevice, false, name, PackIconKind.UsbFlashDriveOutline));
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
            SelectedLedDevice.Menu.IsSelected = true;
            _eventAggregator.PublishOnUIThreadAsync(new TreeViewSelectionChangedMessage(SelectedLedDevice.Menu));
        }

        #endregion Methods

    }
}
