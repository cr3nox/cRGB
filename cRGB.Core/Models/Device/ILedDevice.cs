using System;
using System.Collections.Generic;
using System.Text;
using cRGB.Domain.Enums;

namespace cRGB.Domain.Models.Device
{
    public interface ILedDevice
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ELedDeviceType DeviceType { get; set; }
    }
}
