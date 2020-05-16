using System.Collections.Generic;
using System.Linq;
using BlinkStickDotNet;
using Caliburn.Micro;
using cRGB.Modules.Common.Base;
using cRGB.WPF.Messages;
using cRGB.WPF.ServiceLocation.Factories;
using PropertyChanged;

namespace cRGB.WPF.ViewModels.Device
{
    public class DeviceSelectionViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageFactory _messageFactory;

        private List<object> DeviceObjects { get; set; }

        public BindableCollection<string> Devices { get; set; } = new BindableCollection<string>();
        
        [AlsoNotifyFor("CanApply")] public int SelectedDeviceIndex { get; set; } = -1;

        public bool CanApply => SelectedDeviceIndex > -1;
        
        public void AddDevices(List<string> devicesList) => Devices.AddRange(devicesList);
        public void AddDevices(string[] devicesList) => AddDevices(devicesList.ToList());

        public void AddBlinkSticks(List<BlinkStick> blinkSticks)
        {
            DeviceObjects = new List<object>(blinkSticks);
            //AddDevices(blinkSticks.Select(o => $"{o.BlinkStickDevice} Serial: {o.Serial} Info1: {o.InfoBlock1} Info2: {o.InfoBlock2}").ToArray());
            AddDevices(blinkSticks.Select(o => $"{o.BlinkStickDevice} Serial: {o.Serial}").ToArray());
        }

        public DeviceSelectionViewModel(IEventAggregator aggregator, IMessageFactory messageFactory)
        {
            _eventAggregator = aggregator;
            _messageFactory = messageFactory;
        }

        public virtual void Cancel()
        {
            _eventAggregator.PublishOnUIThreadAsync(_messageFactory.Create(typeof(DeviceSelectedMessage)));
        }

        public virtual void Apply()
        {
            var message = (DeviceSelectedMessage) _messageFactory.Create(typeof(DeviceSelectedMessage));
            message.SelectedDevice = DeviceObjects == null ? Devices[SelectedDeviceIndex] : DeviceObjects[SelectedDeviceIndex];
            _eventAggregator.PublishOnCurrentThreadAsync(message);
        }
    }
}
