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

        public BindableCollection<LedViewModel> RChannelLedColors { get; set; } = new BindableCollection<LedViewModel>();
        public BindableCollection<LedViewModel> GChannelLedColors { get; set; } = new BindableCollection<LedViewModel>();
        public BindableCollection<LedViewModel> BChannelLedColors { get; set; } = new BindableCollection<LedViewModel>();
        public MenuItemViewModel Menu { get; set; }

    }
}
