using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using cRGB.WPF.Messages;
using cRGB.WPF.ViewModels.Menu;
using MaterialDesignThemes.Wpf;

namespace cRGB.WPF.ViewModels.Devices
{
    public class DeviceListViewModel : ViewModelBase, IHandle<DeviceSelectedMessage>
    {
        #region Properties
        public MenuItemViewModel Menu { get; set; }

        public BindableCollection<LedDeviceViewModel> LedDevices { get; set; } = new BindableCollection<LedDeviceViewModel>();


        #endregion

        public DeviceListViewModel(IEventAggregator aggregator)
        {
            aggregator.SubscribeOnUIThread(this);
        }

        #region Methods

        public void AddBlinkStick()
        {
            var newDevice = IoC.Get<BlinkStickViewModel>();
            LedDevices.Add(newDevice);
            newDevice.Menu = Menu.CreateChild(newDevice, false, "New BlinkStick", PackIconKind.Pokeball);
        }

        public void AddArduino()
        {
            //throw new NotImplementedException();
        }

        public void AddTestDevice()
        {
            //throw new NotImplementedException();
        }

        public Task HandleAsync(DeviceSelectedMessage message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #endregion Methods

    }
}
