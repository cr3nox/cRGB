using Caliburn.Micro;
using cRGB.Domain.Enums;
using cRGB.Domain.Models.Device;
using cRGB.WPF.ViewModels.Menu;

namespace cRGB.WPF.ViewModels.Device
{
    public abstract class LedDeviceViewModel : ViewModelBase, ILedDeviceViewModel
    {
        public virtual string DeviceName { get; set; }
        public virtual string Description { get; set; }
        public virtual ELedDeviceType DeviceType { get; set; }

        public BindableCollection<Led> RChannelLedColors { get; set; } = new BindableCollection<Led>();
        public BindableCollection<Led> GChannelLedColors { get; set; } = new BindableCollection<Led>();
        public BindableCollection<Led> BChannelLedColors { get; set; } = new BindableCollection<Led>();
        public MenuItemViewModel Menu { get; set; }

    }
}
