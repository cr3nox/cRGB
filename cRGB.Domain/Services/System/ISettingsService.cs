#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System.Collections.Generic;
using cRGB.Domain.Models.Device;
using cRGB.Domain.Models.System;

namespace cRGB.Domain.Services.System
{
    public interface ISettingsService
    {
        public ISettings Settings { get; set; }

        public IBlinkStickSettings RegisterBlinkStickSettings(string serial);

        public void SaveAll();

        public void LoadAll();
    }
}