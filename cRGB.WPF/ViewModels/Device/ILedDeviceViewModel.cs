using System.Collections.Generic;
using Caliburn.Micro;
using cRGB.Domain.Models.Device;
using cRGB.Domain.Models.Enums;

namespace cRGB.WPF.ViewModels.Device
{
    public interface ILedDeviceViewModel
    {
        public abstract string DeviceName { get; set; }
        public abstract string Description { get; set; }
        public abstract ELedDeviceType DeviceType { get; set; }

        public List<LedViewModel> LedStates { get; set; }
    }
}
