#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

namespace cRGB.Domain.Models.App
{
    public class AppSettings: IAppSettings
    {
        public int ShellViewWidth { get; set; } = 1020;
        public int ShellViewHeight { get; set; } = 700;
    }
}