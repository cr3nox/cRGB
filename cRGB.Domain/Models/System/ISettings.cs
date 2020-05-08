#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System.Collections.Generic;
using cRGB.Domain.Models.Device;

namespace cRGB.Domain.Models.System
{
    public interface ISettings
    {
        public IAppSettings AppSettings { get; set; }
        public IList<IDeviceSettings> ConfiguredDevices { get; set; }
    }
}