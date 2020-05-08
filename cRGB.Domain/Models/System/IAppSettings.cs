#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

namespace cRGB.Domain.Models.System
{
    public interface IAppSettings
    {
        public int ShellViewWidth { get; set; }
        public int ShellViewHeight { get; set; }
    }
}