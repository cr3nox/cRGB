using System.Collections.Generic;
using Caliburn.Micro;
using Castle.Core;
using cRGB.Domain.Models.Device;
using cRGB.Domain.Models.Enums;
using cRGB.WPF.ViewModels.Menu;

namespace cRGB.WPF.ViewModels.Device
{
    public abstract class LedDeviceViewModel : ViewModelBase, ILedDeviceViewModel
    {
        public virtual string DeviceName { get; set; }
        public virtual string Description { get; set; }
        public virtual ELedDeviceType DeviceType { get; set; }

        public List<LedViewModel> LedStates { get; set; } = new List<LedViewModel>();

        [DoNotWire]
        public MenuItemViewModel Menu { get; set; }

    }
}
