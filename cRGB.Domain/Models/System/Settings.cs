﻿#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System.Collections.Generic;
using System.Linq;
using cRGB.Domain.Models.Device;

namespace cRGB.Domain.Models.System
{
    public class Settings : ISettings
    {
        public IAppSettings AppSettings { get; set; }
        public IList<IDeviceSettings> ConfiguredDevices { get; set; }

        public Settings()
        {
            AppSettings = new AppSettings();
            ConfiguredDevices = new List<IDeviceSettings>();
        }
    }
}