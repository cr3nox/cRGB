using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using cRGB.Domain;
using cRGB.WPF.Messages;
using cRGB.WPF.ViewModels.Device;
using cRGB.WPF.ViewModels.Menu;

namespace cRGB.WPF.ViewModels
{
    public sealed class MenuItemOverviewViewModel : ViewModelBase, IHandle<DeviceSelectedMessage>
    {
        public BindableCollection<ViewModelBase> LedDevices { get; set; } = new BindableCollection<ViewModelBase>();
        public ViewModelBase SelectedLedDevice { get; set; }

        public MenuItemOverviewViewModel(IEventAggregator aggregator)
        {
            DisplayName = "Overview";
            aggregator.SubscribeOnUIThread(this);
        }

        public void AddBlinkStick()
        {
            var newDevice = IoC.Get<BlinkStickViewModel>();
            LedDevices.Add(newDevice);
            SelectedLedDevice = newDevice;
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
            if (message.SelectedDevice != null)
                return null;
            LedDevices.Remove(SelectedLedDevice);
            SelectedLedDevice = null;
            return Task.CompletedTask;

        }
    }
}
