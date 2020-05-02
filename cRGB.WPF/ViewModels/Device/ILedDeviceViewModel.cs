﻿using Caliburn.Micro;
using cRGB.Domain.Enums;
using cRGB.Domain.Models.Device;

namespace cRGB.WPF.ViewModels.Device
{
    public interface ILedDeviceViewModel
    {
        public abstract string Name { get; set; }
        public abstract string Description { get; set; }
        public abstract ELedDeviceType DeviceType { get; set; }

        public BindableCollection<Led> RChannelLedColors { get; set; }
        public BindableCollection<Led> GChannelLedColors { get; set; }
        public BindableCollection<Led> BChannelLedColors { get; set; }
    }
}
