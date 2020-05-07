﻿#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System.Collections.Generic;
using cRGB.Domain.Models.App;
using cRGB.Domain.Models.Device;

namespace cRGB.Domain.Services
{
    public interface ISettingsService
    {
        public IList<IBlinkStickSettings> BlinkStickSettings { get; set; }
        public IAppSettings AppSettings { get; set; }

        public IBlinkStickSettings RegisterBlinkStickSettings(string serial);

        public void SaveAll();

        public void LoadAll();
    }
}