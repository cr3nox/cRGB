using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlinkStickDotNet;
using Caliburn.Micro;
using cRGB.Domain;
using cRGB.WPF.Messages;
using PropertyChanged;

namespace cRGB.WPF.ViewModels
{
    public class DeviceSelectionViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private List<object> DeviceObjects { get; set; }

        public BindableCollection<string> Devices { get; set; } = new BindableCollection<string>();
        
        [AlsoNotifyFor("CanApply")] public int SelectedDeviceIndex { get; set; } = -1;

        public bool CanApply => SelectedDeviceIndex > -1;
        
        public void AddDevices(List<string> devicesList) => Devices.AddRange(devicesList);
        public void AddDevices(string[] devicesList) => AddDevices(devicesList.ToList());

        public void AddBlinkSticks(List<BlinkStick> blinkSticks)
        {
            DeviceObjects = new List<object>(blinkSticks);
            AddDevices(blinkSticks.Select(o => $"{o.BlinkStickDevice} Serial: {o.Serial} Info1: {o.InfoBlock1} Info2: {o.InfoBlock2}").ToArray());
        }

        public DeviceSelectionViewModel(IEventAggregator aggregator)
        {
            _eventAggregator = aggregator;
        }

        public virtual void Cancel()
        {
            _eventAggregator.PublishOnUIThreadAsync(new DeviceSelectedMessage());
            TryCloseAsync(false);
        }

        public virtual void Apply()
        {
            _eventAggregator.PublishOnCurrentThreadAsync(new DeviceSelectedMessage(DeviceObjects == null ? Devices[SelectedDeviceIndex] : DeviceObjects[SelectedDeviceIndex]));
            TryCloseAsync(true);
        }
    }
}
