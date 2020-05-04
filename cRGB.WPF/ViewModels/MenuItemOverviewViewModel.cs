using Caliburn.Micro;
using System.Threading.Tasks;
using System.Threading;
using cRGB.WPF.Messages;
using cRGB.WPF.ViewModels.Device;

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
