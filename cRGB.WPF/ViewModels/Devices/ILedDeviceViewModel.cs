using System;
using System.Collections.Generic;
using System.Text;
using cRGB.Domain.Enums;

namespace cRGB.WPF.ViewModels.Devices
{
    public interface ILedDeviceViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ELedDeviceType DeviceType { get; set; }
    }
}
